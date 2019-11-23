using System;
using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint
{
    public class AddEndPointToPolyline : IAddEndPoint
    {
        public void AddEndPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            (shape as Polyline).Points.Add(point);
        }
    }
}
