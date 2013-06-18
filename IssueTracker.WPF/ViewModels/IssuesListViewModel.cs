using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IssueTracker.Data;
using IssueTracker.WPF.Annotations;

namespace IssueTracker.WPF.ViewModels
{
    internal class IssuesListViewModel : INotifyPropertyChanged
    {
        private bool isLoading;
        private List<Issue> issues;

        public List<Issue> Issues
        {
            get { return issues; }
            set
            {
                if (Equals(value, issues)) return;
                issues = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (value.Equals(isLoading)) return;
                isLoading = value;
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
