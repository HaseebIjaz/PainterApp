using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PainterApp.BaseClasses;
using PainterApp.Enums;
using PainterApp.Factories;
using PainterApp.ParameterStructs;
using PainterApp.Singletones;

namespace PainterApp.ViewModels
{
    public class MainWindowViewModel : Notifiable
    {
        protected ObservableCollection<Line> lines = new ObservableCollection<Line>();
        protected ObservableCollection<Rectangle> rectangles = new ObservableCollection<Rectangle>();
        protected ObservableCollection<Ellipse> circles = new ObservableCollection<Ellipse>();
        protected ObservableCollection<Polygon> polygons = new ObservableCollection<Polygon>();
        protected ObservableCollection<Polyline> polylines = new ObservableCollection<Polyline>();
        protected CompositeCollection collection = new CompositeCollection();

        public MainWindowViewModel()
        {
            Collection.Add(new CollectionContainer() { Collection = this.Lines });
            Collection.Add(new CollectionContainer() { Collection = this.Rectangles });
            Collection.Add(new CollectionContainer() { Collection = this.Ellipses });
            Collection.Add(new CollectionContainer() { Collection = this.Polygons });
            Collection.Add(new CollectionContainer() { Collection = this.Polylines });

            MouseDownCommand = new RelayCommand(new System.Action<object>(ConstuctShape),new System.Func<object, bool>(CanConstructShape));
            MouseMoveCommand = new RelayCommand(new System.Action<object>(ResizeShapeDuringCreation), new System.Func<object, bool>(CanResizeShapeDuringCreation));
            MouseUpCommand = new RelayCommand(new System.Action<object>(SetOperationBasedOnShapeProperties), new System.Func<object, bool>(CanSetOperationBasedOnShapeProperties));
        }

        public CompositeCollection Collection
        {
            get
            {
                return collection;
            }
            set
            {
                collection = value;
                OnPropertyChanged("Collection");
            }
        }

        public ObservableCollection<Line> Lines
        {
            get
            {
                return lines;
            }

            set
            {
                lines = value;
                OnPropertyChanged("Lines");
            }
        }

        public ObservableCollection<Rectangle> Rectangles
        {
            get
            {
                return rectangles;
            }

            set
            {
                rectangles = value;
                OnPropertyChanged("Rectangles");
            }
        }

        public ObservableCollection<Ellipse> Ellipses
        {
            get
            {
                return circles;
            }

            set
            {
                circles = value;
                OnPropertyChanged("Circles");
            }
        }

        public ObservableCollection<Polygon> Polygons
        {
            get => polygons;
            set
            {
                polygons = value;
                OnPropertyChanged("Polygons");
            }
        }

        public ObservableCollection<Polyline> Polylines
        {
            get => polylines;
            set
            {
                polylines = value;
                OnPropertyChanged("Polylines");
            }
        }


        #region Helper Classes
        private EnumShapeFactory shapeFactory = new EnumShapeFactory();
        private ShapePositioningOnUI positioningTool = ShapePositioningOnUI.Instance;
        #endregion

        #region Properties Only used in Interaction Logic & Not Linked to UI
        public OperationType ShapeOperation { get; set; }
        #endregion (Moved)

        #region Directly Bound Properties

        protected ShapeType shapeType = ShapeType.None;
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

        protected Brush strokeBrush = Brushes.Black;
        public Brush StrokeBrush
        {
            get { return strokeBrush; }
            set
            {
                strokeBrush = value;
                OnPropertyChanged();
            }
        }

        protected Brush fillBrush = Brushes.White;
        public Brush FillBrush
        {
            get { return fillBrush; }
            set
            {
                fillBrush = value;
                OnPropertyChanged();
            }
        }

        protected double strokeThickness = 1;
        public double StrokeThickness
        {
            get { return strokeThickness; }
            set
            {
                strokeThickness = value;
                OnPropertyChanged();

            }
        }

        #endregion

        #region Dependent Properties
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

        #endregion

        #region ICommands
        protected ICommand mouseDownCommand;
        public ICommand MouseDownCommand
        {
            get { return mouseDownCommand; }
            set
            {
                mouseDownCommand = value;
                OnPropertyChanged("MouseDownCommand");
            }
        }

        protected ICommand mouseMoveCommand;
        public ICommand MouseMoveCommand
        {
            get { return mouseMoveCommand; }
            set
            {
                mouseMoveCommand = value;
                OnPropertyChanged("MouseMoveCommand");
            }
        }

        protected ICommand mouseUpCommand;

        public ICommand MouseUpCommand
        {
            get
            {
                return mouseUpCommand;
            }
            set
            {
                mouseUpCommand = value;
                OnPropertyChanged("MouseUpCommand");
            }
        }

        #endregion

        #region Functions Linked With Command
        private bool CanConstructShape(object parameters)
        {
            if (!IsShapeSelected)
                return false;
            return true;
        }
        private void ConstuctShape(object parameters)
        {
            var e = parameters as MouseButtonEventArgs;
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
            var mousePoint = e.GetPosition((IInputElement)e.Source);
            positioningTool.InitialPoint = new Point(mousePoint.X, mousePoint.Y);
            positioningTool.AddInitialPointToShape(ShapeType);
        }
        private bool CanResizeShapeDuringCreation(object parameters)
        {
            if (!IsShapeSelected)
                return false;
            if (ShapeOperation != OperationType.CreationResize)
                return false;
            return true;
        }
        private void ResizeShapeDuringCreation(object parameters)
        {
            var e = parameters as MouseEventArgs;
            var mousePoint = e.GetPosition((IInputElement)e.Source);
            positioningTool.EndPoint = new Point(mousePoint.X, mousePoint.Y);
            positioningTool.AddEndPointToShape(ShapeType);
        }
        private bool CanSetOperationBasedOnShapeProperties(object parameters)
        {
            if (!IsShapeSelected)
                return false;
            return true;
        }
        private void SetOperationBasedOnShapeProperties(object prameter)
        {
            MouseButtonEventArgs e = prameter as MouseButtonEventArgs;
            ShapeOperation = ClickMultiplicity == ClickMultiplicity.Single ? OperationType.CreationCompleted : ShapeOperation;
            ShapeOperation = ShapeOperation == OperationType.CreationCompleted ? OperationType.ReadyForCreation : ShapeOperation;
            var Element = (UIElement)e.Source;
            Element.ReleaseMouseCapture();
        }

        #endregion

        #region Helping Functions
        private void CreateNewShape()
        {
            positioningTool.selectedShape = shapeFactory.CreateShape(ShapeType, DrawingParameters);
            AddShapeToRelevantCollection();
            ShapeOperation = OperationType.CreationResize;
        }
        private void AddShapeToRelevantCollection()
        {
            var shapeUnderConstruction = positioningTool.selectedShape.TwoDShape;
            switch (ShapeType)
            {
                case (ShapeType.Ellipse):
                    Ellipses.Add(shapeUnderConstruction as Ellipse);
                    break;
                case (ShapeType.Rectangle):
                    Rectangles.Add(shapeUnderConstruction as Rectangle);
                    break;
                case (ShapeType.Line):
                    Lines.Add(shapeUnderConstruction as Line);
                    break;
                case (ShapeType.Polygon):
                    Polygons.Add(shapeUnderConstruction as Polygon);
                    break;
                case (ShapeType.Polyline):
                    Polylines.Add(shapeUnderConstruction as Polyline);
                    break;
                case (ShapeType.Pencil):
                    Polylines.Add(shapeUnderConstruction as Polyline);
                    break;
            }
        }
        #endregion

    }
}
