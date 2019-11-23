using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint
{
    public interface IAddInitialPoint
    {
        void AddInitialPoint(Shape shape,Point point);
    }
}
