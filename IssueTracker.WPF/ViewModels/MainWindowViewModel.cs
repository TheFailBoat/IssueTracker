using System.ComponentModel;
using System.Runtime.CompilerServices;
using IssueTracker.Data;
using IssueTracker.WPF.Annotations;

namespace IssueTracker.WPF.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private Person currentUser;
        public Person CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (Equals(value, currentUser)) return;
                currentUser = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
