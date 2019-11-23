using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint
{
    public class AddInitialPointToPolyline : IAddInitialPoint
    {
        public void AddInitialPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            Polyline polygon = shape as Polyline;
            polygon.Points.Add(point);
        }
    }
}
