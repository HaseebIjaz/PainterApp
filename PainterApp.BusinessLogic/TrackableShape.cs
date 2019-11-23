using System.Windows.Shapes;
using PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint;
using PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint;
using System.Windows;

namespace PainterApp.BusinessLogic
{
    public class TrackableShape
    {
        public Rectangle BoundingRect { get; set; }
        public Shape TwoDShape { get; set; }

        private IAddInitialPoint addInitialPoint;
        private IAddEndPoint addEndPoint;

        public void SetAddInitialPointMethod(IAddInitialPoint shapeSpecificAddInitialPoint)
        {
            if (shapeSpecificAddInitialPoint == null)
                return;
            addInitialPoint = shapeSpecificAddInitialPoint;
        }
        public void SetAddEndPointMethod(IAddEndPoint shapeSpecificEndAddPoint)
        {
            if (shapeSpecificEndAddPoint == null)
                return;
            addEndPoint = shapeSpecificEndAddPoint;
        }

        public void AddInitialPointToShape(Point intialPoint)
        {
            addInitialPoint.AddInitialPoint(TwoDShape, intialPoint);
        }
        public void AddEndPointToShape(Point endPoint)
        {
            addEndPoint.AddEndPoint(TwoDShape, endPoint);
        }
    }
}
