using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PainterApp.BusinessLogic.Algorithms;
using PainterApp.BaseClasses;
using PainterApp.BusinessLogic;
using PainterApp.Enums;

namespace PainterApp.Singletones
{

    public sealed class ShapePositioningOnUI : Notifiable
    {
        // The Constructor is declared private since it should be made only within the class and only once
        private ShapePositioningOnUI()
        {
            selectedShape = null;
        }

        private static readonly object _synclock = new object();

        private static ShapePositioningOnUI _instance;
        public static ShapePositioningOnUI Instance
        {
            get
            {
                if (_instance != null) return _instance;
                //create the instance if it does not exist
                lock (_synclock)
                {
                    if (_instance == null)
                    {
                        _instance = new ShapePositioningOnUI();
                    }
                }
                return _instance;
            }
        }


        public Point InitialPoint;
        public Point EndPoint;
        public TrackableShape selectedShape;

        internal void SetHeightWidthOfShape()
        {
            selectedShape.TwoDShape.Height = PlaneAlgorithms.YDistance(InitialPoint, EndPoint);
            selectedShape.TwoDShape.Width = PlaneAlgorithms.XDistance(InitialPoint, EndPoint);
        }

        internal void ResizeShape()
        {
            Point topLeftPoint = PlaneAlgorithms.CalculateTopLeftPoint(InitialPoint, EndPoint);
            SetTopLeftPoint(selectedShape.TwoDShape, topLeftPoint);
            SetHeightWidthOfShape();
        }

        internal void SetTopLeftPoint(UIElement element, Point topLeftPoint)
        {
            Canvas.SetLeft(element, topLeftPoint.X);
            Canvas.SetTop(element, topLeftPoint.Y);
        }

        internal void ChangeLineCoordinates()
        {
            (selectedShape.TwoDShape as Line).X2 = EndPoint.X;
            (selectedShape.TwoDShape as Line).Y2 = EndPoint.Y;
        }

        internal void AddInitialPointToShape(ShapeType shapeType)
        {
            selectedShape.AddInitialPointToShape(InitialPoint);
        }
        internal void AddEndPointToShape(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case (ShapeType.Pencil):
                    AddPointsToPencil();
                    break;

                case (ShapeType.Ellipse):
                case (ShapeType.Rectangle):
                    ResizeShape();
                    break;

                case (ShapeType.Line):
                    ChangeLineCoordinates();
                    break;
            }
        }
        internal void AddPointsToPencil()
        {
            (selectedShape.TwoDShape as Polyline).Points.Add(EndPoint);
        }
    }

}
