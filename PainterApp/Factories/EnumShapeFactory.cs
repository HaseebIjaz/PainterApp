using System.Windows.Shapes;
using PainterApp.BusinessLogic;
using PainterApp.BusinessLogic.PolymorphicProperties.AddEndPoint;
using PainterApp.BusinessLogic.PolymorphicProperties.AddInitialPoint;
using PainterApp.Enums;
using PainterApp.Factories.FactoryInterfaces;
using PainterApp.ParameterStructs;

namespace PainterApp.Factories
{

    /// <summary>
    /// 
    /// </summary>
    public class EnumShapeFactory : IShapeFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public EnumShapeFactory()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapeTypeEnum"></param>
        /// <param name="stroke"></param>
        /// <returns></returns>
        public TrackableShape CreateShape(ShapeType shapeTypeEnum, object drawingParameters)
        {
            DrawingParameters shapeDrawingParameters = drawingParameters as DrawingParameters;
            TrackableShape createdShape = new TrackableShape();
            switch (shapeTypeEnum)
            {
                case (ShapeType.Line):
                    createdShape.TwoDShape = new Line()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToLine());
                    createdShape.SetAddEndPointMethod(new AddEndPointToLine());
                    break;

                case (ShapeType.Ellipse):
                    createdShape.TwoDShape = new Ellipse()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        Fill = shapeDrawingParameters.Fill,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToEllipse());
                    createdShape.SetAddEndPointMethod(new AddEndPointToEllipse());
                    break;

                case (ShapeType.Rectangle):
                    createdShape.TwoDShape = new Rectangle()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        Fill = shapeDrawingParameters.Fill,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToRectangle());
                    createdShape.SetAddEndPointMethod(new AddEndPointToRectangle());
                    break;

                case (ShapeType.Pencil):
                    createdShape.TwoDShape = new Polyline()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToPolyline());
                    createdShape.SetAddEndPointMethod(new AddEndPointToPolyline());
                    break;

                case (ShapeType.Polygon):
                    createdShape.TwoDShape = new Polygon()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness,
                        Fill = shapeDrawingParameters.Fill
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToPolygon());
                    createdShape.SetAddEndPointMethod(new AddEndPointToPolygon());
                    break;

                case (ShapeType.Polyline):
                    createdShape.TwoDShape = new Polyline()
                    {
                        Stroke = shapeDrawingParameters.Stroke,
                        StrokeThickness = shapeDrawingParameters.StrokeThickness
                    };
                    createdShape.SetAddInitialPointMethod(new AddInitialPointToPolyline());
                    createdShape.SetAddEndPointMethod(new AddEndPointToPolyline());
                    break;
            }
            return createdShape;
        }
    }

}
