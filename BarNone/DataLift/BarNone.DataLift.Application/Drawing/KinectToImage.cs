using BarNone.Shared.DomainModel;
using BarNone.Shared.DomainModel.Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace BarNone.DataLift.UI.Drawing
{
    /// <summary>
    /// Class handling the drawing of kinect retrieved data
    /// </summary>
    public static class KinectToImage
    {
        #region Brushes
        /// <summary>
        /// Color for a closed hand, debugging purposes
        /// </summary>
        private static readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// Color for an open hand, debugging purposes
        /// </summary>
        private static readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// Color for a lasso hand (index touching thumb), debugging purposes
        /// </summary>
        private static readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// Color of a tracked joint
        /// </summary>
        private static readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Color of an infered joint location
        /// </summary>
        private static readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Color of an inffered bone
        /// </summary>
        private static readonly Pen inferredBonePen = new Pen(Brushes.Blue, 6);

        /// <summary>
        /// Color of a non inferred body
        /// </summary>
        private static Pen bodyColor = new Pen(Brushes.Violet, 6);

        #endregion

        #region Joint Dimension Details
        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;
        #endregion

        #region DM API Draws
        /// <summary>
        /// Draws all the front profile of collected body data <paramref name="joints"/>
        /// </summary>
        /// <param name="joints">Body data to be drawn</param>
        /// <param name="canvas">Drawing group which will be drawn on</param>
        /// <param name="height">Height of the <paramref name="canvas"/></param>
        /// <param name="width">Width of the <paramref name="canvas"/></param>
        internal static void DrawFrameFrontView(BodyDataFrame frame, DrawingGroup canvas, int height, int width)
        {
            DrawFrame(frame, canvas, height, width, (origin, compare) => new Point((compare.X - origin.X) * -153.34 + width / 2, compare.Y * -153.34 + height / 2));
        }

        /// <summary>
        /// Draws all the side profile of collected body data <paramref name="joints"/>
        /// </summary>
        /// <param name="joints">Body data to be drawn</param>
        /// <param name="canvas">Drawing group which will be drawn on</param>
        /// <param name="height">Height of the <paramref name="canvas"/></param>
        /// <param name="width">Width of the <paramref name="canvas"/></param>
        internal static void DrawFrameSideView(BodyDataFrame frame, DrawingGroup canvas, int height, int width)
        {
            DrawFrame(frame, canvas, height, width, (origin, compare) => new Point((compare.Z - origin.Z) * 153.34 + width / 2, compare.Y * (-153.34) + height / 2));
        }

        /// <summary>
        /// Generalized Draw function for comonalities between 
        /// <see cref="DrawFrameFrontView(BodyDataFrame, DrawingGroup, int, int)"/> 
        /// and <see cref="DrawFrameSideView(BodyDataFrame, DrawingGroup, int, int)"/>
        /// </summary>
        /// <param name="joints">Body data to be drawn</param>
        /// <param name="canvas">Drawing group which will be drawn on</param>
        /// <param name="height">Height of the <paramref name="canvas"/></param>
        /// <param name="width">Width of the <paramref name="canvas"/></param>
        /// <param name="Convert3DToPoint">Arg1: Origin, Arg2: Compare Point, Return 2D representation of compare normalized to Origin</param>
        private static void DrawFrame(BodyDataFrame frame, DrawingGroup canvas, int height, int width, Func<Point3D, Point3D, Point> Convert3DToPoint)
        {
            using (DrawingContext dc = canvas.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, width, height));
                var joints = frame.Joints;

                var originJoint = joints.FirstOrDefault(j => j.JointType == EJointType.SpineBase);
                if (originJoint == null)
                    return;

                var origin = new Point3D() { X = originJoint.X, Y = originJoint.Y, Z = originJoint.Z };

                // convert the joint points to depth (display) space
                Dictionary<Joint, Point> jointPoints = new Dictionary<Joint, Point>();

                // convert the joint points to depth (display) space
                foreach (var j in joints)
                {
                    Point3D position = new Point3D() { X = j.X, Y = j.Y, Z = j.Z };

                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    //CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                        position.Z = InferredZPositionClamp;

                    jointPoints[j] = Convert3DToPoint.Invoke(origin, position);
                }
                DrawBodyFromDTO(jointPoints, dc, bodyColor);
            }
        }
        
        /// <summary>
        /// Draws a body to <paramref name="drawingContext"/>
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        internal static void DrawBodyFromDTO(IDictionary<Joint, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in Skeleton.bones)
            {
                DrawBoneFromDTO(jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (var j in jointPoints.Keys)
            {
                if (j.JointTrackingStateType == EJointTrackingStateType.Tracked)
                    drawingContext.DrawEllipse(inferredJointBrush, null, jointPoints[j], JointThickness, JointThickness);
            }
        }

        /// <summary>
        /// Draws one bone of a body (joint to joint) to <paramref name="drawingContext"/>
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific bone</param>
        internal static void DrawBoneFromDTO(IDictionary<Joint, Point> jointPoints, EJointType jointType0, EJointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            var joint0 = jointPoints.First(p => p.Key.JointType == jointType0);
            var joint1 = jointPoints.First(p => p.Key.JointType == jointType1);

            // If we can't find either of these joints, exit
            if (joint0.Key.JointTrackingStateType == EJointTrackingStateType.NotTracked ||
                joint1.Key.JointTrackingStateType == EJointTrackingStateType.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if ((joint0.Key.JointTrackingStateType == EJointTrackingStateType.Tracked) 
                && (joint1.Key.JointTrackingStateType == EJointTrackingStateType.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, joint0.Value, joint1.Value);
        }

        #endregion
    }
}
