using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PainterApp.BusinessLogic.Algorithms;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint
{
    public class AddEndPointToRectangle : IAddEndPoint
    {
        public void AddEndPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            Point oldTopLeft=  new Point (Canvas.GetLeft(shape), Canvas.GetTop(shape));
            Point newtopLeftPoint = PlaneAlgorithms.CalculateTopLeftPoint(oldTopLeft, point);
            Canvas.SetLeft(shape, newtopLeftPoint.X);
            Canvas.SetTop(shape, newtopLeftPoint.Y);
            shape.Height = PlaneAlgorithms.YDistance(oldTopLeft, point);
            shape.Width = PlaneAlgorithms.XDistance(oldTopLeft, point);
        }
    }
}
