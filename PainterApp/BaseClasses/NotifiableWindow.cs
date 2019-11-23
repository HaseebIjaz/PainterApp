using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PainterApp.BaseClasses
{
    /// <summary>
    ///Abstract Base Window Class For Property Changed Notification Purposes
    /// </summary>
    public abstract class NotifiableWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// The event provided by INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// This method is called whenever a property undergoes change
        /// </summary>
        /// <param name="caller"></param>
        public void OnPropertyChanged(
            [CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
