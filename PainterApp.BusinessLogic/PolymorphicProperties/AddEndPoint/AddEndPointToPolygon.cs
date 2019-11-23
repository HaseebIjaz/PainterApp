using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint
{
    public class AddEndPointToPolygon : IAddEndPoint
    {
        public void AddEndPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            (shape as Polygon).Points.Add(point);

        }
    }
}