using PainterApp.BaseClasses;
using PainterApp.ViewModels;
//using PainterApp.ViewModels;

namespace PainterApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NotifiableWindow
    {
        MainWindowViewModel mainWindowVM = new MainWindowViewModel();
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowVM;
        }


    }
}
