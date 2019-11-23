using System.Windows;
using System.Windows.Shapes;

namespace PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint
{
    public interface IAddEndPoint
    {
        void AddEndPoint(Shape shape,Point point);
    }
}
