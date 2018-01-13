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
        private static readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

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
        public static void DrawFrameFrontProfile(BodyDataFrame frame, DrawingGroup canvas, int height, int width)
        {
            using (DrawingContext dc = canvas.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, width, height));

                //TODO I guess
                //DrawClippedEdges(body, dc);

                // convert the joint points to depth (display) space
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                foreach (JointType jointType in frame.Joints.Keys)
                {
                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    jointPoints[jointType] = new Point((position.X - frame.Joints[JointType.SpineBase].Position.X) * -153.34 + width / 2, position.Y * -153.34 + height / 2);
                }

                DrawBody(frame.Joints, jointPoints, dc, bodyColor);

                //DrawHand(lifter.HandLeftState, jointPoints[JointType.HandLeft], dc);
                //DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);

                //TODO: Maybe have clipping if group wants it prevent drawing outside of our render area
                //canvas.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, displayWidth, displayHeight));
            }
        }

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
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

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

                    jointPoints[(JointType)f.JointTypeID] = new Point((position.X - origin.X) * -153.34 + width / 2, position.Y * -153.34 + height / 2);
                }
                DrawBodyFromDTO(jointPoints, dc, bodyColor);
            }
        }



        internal static void DrawFrameSideView(BodyDataFrame frame, DrawingGroup canvas, int height, int width)
        {
            using (DrawingContext dc = canvas.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, width, height));

                // convert the joint points to depth (display) space
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                foreach (JointType jointType in frame.Joints.Keys)
                {
                    // sometimes the depth(Z) of an inferred joint may show as negative
                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                    CameraSpacePoint position = frame.Joints[jointType].Position;
                    if (position.Z < 0)
                    {
                        position.Z = InferredZPositionClamp;
                    }

                    jointPoints[jointType] = new Point((position.Z - frame.Joints[JointType.SpineBase].Position.Z) * 153.34 + width / 2, position.Y * (-153.34) + height / 2);
                }

                DrawBody(frame.Joints, jointPoints, dc, bodyColor);


                // prevent drawing outside of our render area
                //canvas.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, width, height));
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
                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

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

                    jointPoints[(JointType)f.JointTypeID] = new Point((position.Z - origin.Z) * 153.34 + width / 2, position.Y * (-153.34) + height / 2);
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
        internal static void DrawBodyFromDTO(IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in Skeleton.bones)
            {
                DrawBoneFromDTO(jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in jointPoints.Keys)
            {
                drawingContext.DrawEllipse(inferredJointBrush, null, jointPoints[jointType], JointThickness, JointThickness);
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
        internal static void DrawBoneFromDTO(IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Point joint0 = jointPoints[jointType0];
            Point joint1 = jointPoints[jointType1];

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;

            drawPen = drawingPen;

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        #endregion

        #region Internal Draws
        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        internal static void DrawBody(IDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in Skeleton.bones)
            {
                DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
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
        internal static void DrawBone(IDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        internal static void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        internal static void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            /* FrameEdges clippedEdges = body.ClippedEdges;

             if (clippedEdges.HasFlag(FrameEdges.Bottom))
             {
                 drawingContext.DrawRectangle(
                     Brushes.Red,
                     null,
                     new Rect(0, displayHeight - ClipBoundsThickness, displayWidth, ClipBoundsThickness));
             }

             if (clippedEdges.HasFlag(FrameEdges.Top))
             {
                 drawingContext.DrawRectangle(
                     Brushes.Red,
                     null,
                     new Rect(0, 0, displayWidth, ClipBoundsThickness));
             }

             if (clippedEdges.HasFlag(FrameEdges.Left))
             {
                 drawingContext.DrawRectangle(
                     Brushes.Red,
                     null,
                     new Rect(0, 0, ClipBoundsThickness, displayHeight));
             }

             if (clippedEdges.HasFlag(FrameEdges.Right))
             {
                 drawingContext.DrawRectangle(
                     Brushes.Red,
                     null,
                     new Rect(displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, displayHeight));
             } */
        }

        #endregion

    }
}
