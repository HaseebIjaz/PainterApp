using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint
{
    public class AddInitialPointToRectangle : IAddInitialPoint
    {
        public void AddInitialPoint(Shape shape, Point point)
        {
            if (point == null) return;
            if (shape == null) return;

            Canvas.SetLeft(shape, point.X);
            Canvas.SetTop(shape, point.Y);
        }
    }
}
