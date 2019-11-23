using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PainterApp.BaseClasses
{
    /// <summary>
    ///Abstract Base Class For Property Changed Notification Purposes
    /// </summary>
    public abstract class Notifiable : INotifyPropertyChanged
    {
        /// <summary>
        ///  The event provided by INotifyPropertyChanged
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
