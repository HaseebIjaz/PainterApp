using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PainterApp.BaseClasses;
using PainterApp.BusinessLogic;
using PainterApp.Enums;
using PainterApp.Factories;
using PainterApp.ParameterStructs;
using PainterApp.Singletones;

namespace PainterApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NotifiableWindow
    {
        private EnumShapeFactory shapeFactory;
        private ShapePositioningOnUI positioningTool;
        public DrawingParameters DrawingParameters
        {
            get
            {
                DrawingParameters parameters = new DrawingParameters();
                parameters.Fill = FillBrush;
                parameters.Stroke = StrokeBrush;
                parameters.StrokeThickness = StrokeThickness;
                return parameters;
            }
        }

        //        private ObservableCollection<Shape> canvasShapes = new ObservableCollection<Shape>();
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ShapeType = ShapeType.None;
            StrokeThickness = 1;
            StrokeBrush = Brushes.Black;
            FillBrush = Brushes.White;
            shapeFactory = new EnumShapeFactory();
            positioningTool = ShapePositioningOnUI.Instance;
        }

        public ClickMultiplicity ClickMultiplicity
        {
            get
            {
                ClickMultiplicity clickMultiplicity;

                switch (ShapeType)
                {
                    case (ShapeType.Pencil):
                    case (ShapeType.Rectangle):
                    case (ShapeType.Ellipse):
                    case (ShapeType.Line):
                        clickMultiplicity = ClickMultiplicity.Single;
                        break;

                    case (ShapeType.Polygon):
                    case (ShapeType.Polyline):
                        clickMultiplicity = ClickMultiplicity.Multiple;
                        break;

                    case (ShapeType.None):
                        clickMultiplicity = ClickMultiplicity.None;
                        break;

                    default:
                        clickMultiplicity = ClickMultiplicity.None;
                        break;
                }
                return clickMultiplicity;
            }
        }
        public bool IsShapeSelected
        {
            get
            {
                return ShapeType != ShapeType.None;
            }
        }

        private ShapeType shapeType;
        public ShapeType ShapeType
        {
            get { return shapeType; }
            set
            {
                if (value != ShapeType.None)
                    ShapeOperation = OperationType.ReadyForCreation;

                shapeType = value;
                OnPropertyChanged();
            }

        }

        private Brush strokeBrush;
        public Brush StrokeBrush
        {
            get { return strokeBrush; }
            set
            {
                strokeBrush = value;
                OnPropertyChanged();
            }
        }

        private Brush fillBrush;
        public Brush FillBrush
        {
            get { return fillBrush; }
            set
            {
                fillBrush = value;
                OnPropertyChanged();
            }
        }

        private double strokeThickness;
        public double StrokeThickness
        {
            get { return strokeThickness; }
            set
            {
                strokeThickness = value;
                OnPropertyChanged();

            }
        }

        public OperationType ShapeOperation { get; set; }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsShapeSelected)
                return;

            bool isDoubleClicked = e.ClickCount == 2;

            if (isDoubleClicked)
            {
                ShapeOperation = OperationType.CreationCompleted;
            }
            else if (ShapeOperation == OperationType.ReadyForCreation)
            {
                CreateNewShape();
            }

            // Resize Creation State
            var mousePoint = e.GetPosition(DrawingCanvas as IInputElement);
            positioningTool.InitialPoint = new Point(mousePoint.X, mousePoint.Y);
            positioningTool.AddInitialPointToShape(ShapeType);
        }

        private void CreateNewShape()
        {
            positioningTool.selectedShape = shapeFactory.CreateShape(ShapeType, DrawingParameters);
            DrawingCanvas.Children.Add(positioningTool.selectedShape.TwoDShape);
            ShapeOperation = OperationType.CreationResize;
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsShapeSelected)
                return;
            if (ShapeOperation != OperationType.CreationResize)
                return;

            var mousePoint = e.GetPosition(DrawingCanvas as IInputElement);
            positioningTool.EndPoint = new Point(mousePoint.X, mousePoint.Y);
            positioningTool.AddEndPointToShape(ShapeType);
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsShapeSelected)
                return;
            SetOperationBasedOnShapeProperties();
            ReleaseMouseCapture();
        }

        private void SetOperationBasedOnShapeProperties()
        {
            ShapeOperation = ClickMultiplicity == ClickMultiplicity.Single ? OperationType.CreationCompleted : ShapeOperation;
            ShapeOperation = ShapeOperation == OperationType.CreationCompleted ? OperationType.ReadyForCreation : ShapeOperation;
        }
    }
}
