using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint
{
    public class AddInitialPointToPolygon : IAddInitialPoint
    {
        public void AddInitialPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            Polygon polygon = shape as Polygon;
            polygon.Points.Add(point);
        }
    }
}
