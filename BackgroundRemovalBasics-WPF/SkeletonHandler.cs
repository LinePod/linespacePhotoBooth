﻿using Microsoft.Kinect;
using Svg;
using Svg.Pathing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Bitmap = System.Drawing.Bitmap;
using PointF = System.Drawing.PointF;
using Pen = System.Windows.Media.Pen;
using HPI.HCI.Bachelorproject1617.PhotoBooth;

namespace Hpi.Hci.Bachelorproject1617.PhotoBooth
{
    class SkeletonHandler
    {
        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly System.Windows.Media.Pen inferredBonePen = new System.Windows.Media.Pen(Brushes.Gray, 1);
        /// <summary>
        /// Width of output drawing
        /// </summary>
        public const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        public const float RenderHeight = 480.0f;

     
        
        
        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        public const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// Brush used to draw skeleton center point
        /// </summary>
        public readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly System.Windows.Media.Pen trackedBonePen = new Pen(Brushes.Green, 6);


        
        
        /// <summary>
        private const int scale = 100;
        private const int svgWidth = 200;
        private const int svgHeight = 200;

        
        
        KinectSensor sensor;
        SpeechInteraction speechInteraction;
        public SkeletonHandler(KinectSensor sensor, SpeechInteraction speechInt)
        {
            this.speechInteraction = speechInt;
            this.sensor = sensor;
        }

        public static String GenerateSkeletonSVG(Skeleton skel)
        {
            SvgDocument doc = new SvgDocument()
            {
                Width = svgWidth,
                Height = svgHeight
            };

            SvgPath path = new SvgPath()
            {
                FillOpacity = 0,
                Stroke = new SvgColourServer(System.Drawing.Color.Black)
            };

            Joint leftHand = skel.Joints[JointType.HandLeft];
            Joint rightHand = skel.Joints[JointType.HandRight];
            Joint leftWrist = skel.Joints[JointType.WristLeft];
            Joint rightWrist = skel.Joints[JointType.WristRight];
            Joint leftElbow = skel.Joints[JointType.ElbowLeft];
            Joint rightElbow = skel.Joints[JointType.ElbowRight];
            Joint leftShoulder = skel.Joints[JointType.ShoulderLeft];
            Joint rightShoulder = skel.Joints[JointType.ShoulderRight];
            Joint leftFoot = skel.Joints[JointType.FootLeft];
            Joint rightFoot = skel.Joints[JointType.FootRight];
            Joint leftAnkle = skel.Joints[JointType.AnkleLeft];
            Joint rightAnkle = skel.Joints[JointType.AnkleRight];
            Joint leftKnee = skel.Joints[JointType.KneeLeft];
            Joint rightKnee = skel.Joints[JointType.KneeRight];
            Joint leftHip = skel.Joints[JointType.HipLeft];
            Joint rightHip = skel.Joints[JointType.HipRight];
            Joint head = skel.Joints[JointType.Head];
            Joint shoulderCenter = skel.Joints[JointType.ShoulderCenter];
            Joint spine = skel.Joints[JointType.Spine];
            Joint hipCenter = skel.Joints[JointType.HipCenter];

            List<Joint> arms = new List<Joint>();
            arms.Add(leftHand);
            arms.Add(leftWrist);
            arms.Add(leftElbow);
            arms.Add(leftShoulder);
            arms.Add(shoulderCenter);
            arms.Add(rightShoulder);
            arms.Add(rightElbow);
            arms.Add(rightWrist);
            arms.Add(rightHand);

            List<Joint> back = new List<Joint>();
            //back.Add(head);
            back.Add(shoulderCenter);
            back.Add(spine);
            back.Add(hipCenter);

            List<Joint> legs = new List<Joint>();
            legs.Add(leftFoot);
            legs.Add(leftAnkle);
            legs.Add(leftKnee);
            legs.Add(leftHip);
            legs.Add(hipCenter);
            legs.Add(rightHip);
            legs.Add(rightKnee);
            legs.Add(rightAnkle);
            legs.Add(rightFoot);

            AddJointsToPath(path, arms, 100);
            AddJointsToPath(path, back, 100);
            AddJointsToPath(path, legs, 100);

            Console.WriteLine("svg output");

            //calculate intersecion point of head and neck
            //double shoulderToHead = Math.Sqrt(Math.Pow(head.Position.X - shoulderCenter.Position.X,2) + Math.Pow(head.Position.Y - shoulderCenter.Position.Y,2));
            /*float deltaX = leftHand.Position.X - leftElbow.Position.X;
            float deltaY = leftHand.Position.Y - leftElbow.Position.Y;
            float deltaZ = leftHand.Position.Z - leftElbow.Position.Z;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            float headRadius = (float)(distance * scale / 2.5);
            Vector headVec = new Vector(head.Position.X, head.Position.Y);
            Vector distVector = headVec - new Vector(shoulderCenter.Position.X, shoulderCenter.Position.Y);
            distVector.Normalize();
            Vector intersectingPoint = headVec - distVector * headRadius;
            */
            PointF headPointOnScreen = new PointF(TranslatePosition(head.Position.X), TranslatePosition(head.Position.Y));
            PointF shoulderPointOnScreen = new PointF(TranslatePosition(shoulderCenter.Position.X), TranslatePosition(shoulderCenter.Position.Y));
            double deltaX = headPointOnScreen.X - shoulderPointOnScreen.X;
            double deltaY = headPointOnScreen.Y - shoulderPointOnScreen.Y;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float headRadius = (float)(distance / 2);
            Vector headVec = new Vector(headPointOnScreen.X, headPointOnScreen.Y);
            Vector shoulderVec = new Vector(shoulderPointOnScreen.X, shoulderPointOnScreen.Y);
            Vector distVector = shoulderVec - headVec;
            distVector.Normalize();


            Vector intersectingPoint = shoulderVec - distVector * headRadius;
            PointF intersectingPointF = new PointF((float)intersectingPoint.X, (float)intersectingPoint.Y);
            SvgCircle headCircle = new SvgCircle()
            {
                Radius = headRadius,

                FillOpacity = 0,
                Stroke = new SvgColourServer(System.Drawing.Color.Black),
                CenterX = new Svg.SvgUnit(TranslatePosition(head.Position.X)),
                CenterY = new Svg.SvgUnit(TranslatePosition(head.Position.Y)),
                StrokeWidth = 1
            };
            doc.Children.Add(path);


            SvgPath path2 = new SvgPath()
            {
                FillOpacity = 0,
                Stroke = new SvgColourServer(System.Drawing.Color.Black)
            };
            //add the neck
            path2.PathData.Add(new SvgMoveToSegment(shoulderPointOnScreen));
            path2.PathData.Add(new SvgLineSegment(shoulderPointOnScreen, intersectingPointF));



            doc.Children.Add(headCircle);
            doc.Children.Add(path2);
            var stream = new MemoryStream();
            doc.Write(stream);
            Console.WriteLine("SVG from skeleton " + Encoding.UTF8.GetString(stream.GetBuffer()));
            return Encoding.UTF8.GetString(stream.GetBuffer());

        }

        private static void AddJointsToPath(SvgPath path, List<Joint> joints, int scale)
        {
            path.PathData.Add(new SvgMoveToSegment(new PointF(TranslatePosition(joints[0].Position.X), TranslatePosition(joints[0].Position.Y))));
            for (var i = 0; i < joints.Count - 1; i++)
            {
                var start = joints[i];
                var end = joints[i + 1];

                path.PathData.Add(new SvgLineSegment(new PointF(TranslatePosition(start.Position.X), TranslatePosition(start.Position.Y)), new PointF(TranslatePosition(end.Position.X), TranslatePosition(end.Position.Y))));
            }
        }

        private static float TranslatePosition(float pos)
        {

            return svgHeight - ((pos + 1) * scale);

        }



        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        public void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            //this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);
           
            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }

            //render head
            Joint leftHand = skeleton.Joints[JointType.HandLeft];
            Joint leftElbow = skeleton.Joints[JointType.ElbowLeft];

            Joint head = skeleton.Joints[JointType.Head];
            Joint shoulderCenter = skeleton.Joints[JointType.ShoulderCenter];

            //assumes that head radius fits 4 times in the distance between hand and elbow
            /*float deltaX = leftHand.Position.X - leftElbow.Position.X;
            float deltaY = leftHand.Position.Y - leftElbow.Position.Y;
            float deltaZ = leftHand.Position.Z - leftElbow.Position.Z;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            float headRadius = (float)(distance * scale * 1.5);*/



            System.Windows.Media.Pen drawPen = this.inferredBonePen;
            if (head.TrackingState == JointTrackingState.Tracked && shoulderCenter.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
                Brush drawBrush = this.trackedJointBrush;

                if (drawBrush != null)
                {
                    System.Windows.Point headPointOnScreen = SkeletonPointToScreen(head.Position);
                    System.Windows.Point shoulderPointOnScreen = SkeletonPointToScreen(shoulderCenter.Position);
                    double deltaX = headPointOnScreen.X - shoulderPointOnScreen.X;
                    double deltaY = headPointOnScreen.Y - shoulderPointOnScreen.Y;
                    float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    float headRadius = (float)(distance / 2);
                    Vector headVec = new Vector(headPointOnScreen.X, headPointOnScreen.Y);
                    Vector shoulderVec = new Vector(shoulderPointOnScreen.X, shoulderPointOnScreen.Y);
                    Vector distVector = shoulderVec - headVec;
                    distVector.Normalize();


                    Vector intersectingPoint = headVec + distVector * headRadius;
                    
                    drawingContext.DrawEllipse(drawBrush, drawPen, this.SkeletonPointToScreen(head.Position), headRadius, headRadius);
                    drawingContext.DrawLine(drawPen, new Point(intersectingPoint.X, intersectingPoint.Y), shoulderPointOnScreen);

                }
            }

        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        public System.Windows.Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            if (sensor != null)
            {
                DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
                return new System.Windows.Point(depthPoint.X, depthPoint.Y);
            }
            return new System.Windows.Point(0, 0);
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            System.Windows.Media.Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }
            //Console.WriteLine(drawPen.Brush.ToString());
            drawingContext.DrawLine(drawPen, SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }


        /// <summary>
        /// Draws indicators to show which edges are clipping skeleton data
        /// </summary>
        /// <param name="skeleton">skeleton to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        public static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

    }
}
