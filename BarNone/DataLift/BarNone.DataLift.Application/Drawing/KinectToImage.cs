using BarNone.DataLift.DataModel.KinectData;
using BarNone.Shared.DataTransfer;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace BarNone.DataLift.UI.Drawing
{
    public static class KinectToImage
    {
        #region Brushes
        private static readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
        private static readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));
        private static readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        private static readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private static readonly Brush inferredJointBrush = Brushes.Yellow;
        private static readonly Pen inferredBonePen = new Pen(Brushes.Blue, 6);

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

        #region API Draws
        internal static void DrawFrameFrontView(IList<JointDTO> frames, DrawingGroup canvas, int height, int width)
        {
            using (DrawingContext dc = canvas.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, width, height));


                var originJoint = frames.FirstOrDefault(j => j.JointTypeID == (int)JointType.SpineBase);
                if (originJoint == null)
                    return;

                var origin = new Point3D() { X = originJoint.X, Y = originJoint.Y, Z = originJoint.Z };

                // convert the joint points to depth (display) space
                Dictionary<JointDTO, Point> jointPoints = new Dictionary<JointDTO, Point>();

                // convert the joint points to depth (display) space
                foreach (var f in frames)
                {
                    Point3D position = new Point3D() { X = f.X, Y = f.Y, Z = f.Z };

                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    //CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    jointPoints[f] = new Point((position.X - origin.X) * -153.34 + width / 2, position.Y * -153.34 + height / 2);
                }
                DrawBodyFromDTO(jointPoints, dc, bodyColor);
            }
        }

        internal static void DrawFrameSideView(IList<JointDTO> frames, DrawingGroup canvas, int height, int width)
        {
            using (DrawingContext dc = canvas.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, width, height));


                var originJoint = frames.FirstOrDefault(j => j.JointTypeID == (int)JointType.SpineBase);
                if (originJoint == null)
                    return;

                var origin = new Point3D() { X = originJoint.X, Y = originJoint.Y, Z = originJoint.Z };

                // convert the joint points to depth (display) space
                Dictionary<JointDTO, Point> jointPoints = new Dictionary<JointDTO, Point>();

                // convert the joint points to depth (display) space
                foreach (var f in frames)
                {
                    Point3D position = new Point3D() { X = f.X, Y = f.Y, Z = f.Z };

                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    //CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    jointPoints[f] = new Point((position.Z - origin.Z) * 153.34 + width / 2, position.Y * (-153.34) + height / 2);
                }
                DrawBodyFromDTO(jointPoints, dc, bodyColor);
            }
        }


        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        internal static void DrawBodyFromDTO(IDictionary<JointDTO, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in Skeleton.bones)
            {
                DrawBoneFromDTO(jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (var j in jointPoints.Keys)
            {
                if (j.JointTrackingStateTypeID == (int)TrackingState.Tracked)
                    drawingContext.DrawEllipse(inferredJointBrush, null, jointPoints[j], JointThickness, JointThickness);
            }
        }

        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        internal static void DrawBoneFromDTO(IDictionary<JointDTO, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            var joint0 = jointPoints.First(p => p.Key.JointTypeID == (int)jointType0);
            var joint1 = jointPoints.First(p => p.Key.JointTypeID == (int)jointType1);

            // If we can't find either of these joints, exit
            if (joint0.Key.JointTrackingStateTypeID == (int)TrackingState.NotTracked ||
                joint1.Key.JointTrackingStateTypeID == (int)TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if ((joint0.Key.JointTrackingStateTypeID == (int)TrackingState.Tracked) && (joint1.Key.JointTrackingStateTypeID == (int)TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, joint0.Value, joint1.Value);
        }

        #endregion

    }
}
