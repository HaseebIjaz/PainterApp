using System;
using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint
{
    public class AddEndPointToLine : IAddEndPoint
    {
        public void AddEndPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            (shape as Line).X2 = point.X;
            (shape as Line).Y2 = point.Y;
        }
    }
}
