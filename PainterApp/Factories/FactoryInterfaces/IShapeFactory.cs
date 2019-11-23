using System.Windows.Media;
using System.Windows.Shapes;
using PainterApp.BusinessLogic;
using PainterApp.Enums;
using PainterApp.ParameterStructs;

namespace PainterApp.Factories.FactoryInterfaces
{
    interface IShapeFactory
    {
        TrackableShape CreateShape(ShapeType shapeType, object drawingParameters);
    }
}
