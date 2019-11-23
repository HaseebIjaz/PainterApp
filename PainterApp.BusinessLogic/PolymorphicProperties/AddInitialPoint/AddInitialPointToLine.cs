using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint
{
    public class AddInitialPointToLine : IAddInitialPoint
    {
        public void AddInitialPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            (shape as Line).X1 = point.X;
            (shape as Line).Y1 = point.Y;
        }
    }
}
