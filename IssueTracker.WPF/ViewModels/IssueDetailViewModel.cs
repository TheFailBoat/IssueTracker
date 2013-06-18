using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Comments;
using IssueTracker.WPF.Annotations;

namespace IssueTracker.WPF.ViewModels
{
    internal class IssueDetailViewModel : INotifyPropertyChanged
    {
        private List<CommentDetailsResponse> comments;
        private Priority priority;
        private Status status;
        private Person reporter;
        private Category category;
        private Issue issue;

        public Issue Issue
        {
            get { return issue; }
            set
            {
                if (Equals(value, issue)) return;
                issue = value;
                OnPropertyChanged();
            }
        }

        public Category Category
        {
            get { return category; }
            set
            {
                if (Equals(value, category)) return;
                category = value;
                OnPropertyChanged();
            }
        }

        public Person Reporter
        {
            get { return reporter; }
            set
            {
                if (Equals(value, reporter)) return;
                reporter = value;
                OnPropertyChanged();
            }
        }

        public Status Status
        {
            get { return status; }
            set
            {
                if (Equals(value, status)) return;
                status = value;
                OnPropertyChanged();
            }
        }

        public Priority Priority
        {
            get { return priority; }
            set
            {
                if (Equals(value, priority)) return;
                priority = value;
                OnPropertyChanged();
            }
        }

        public List<CommentDetailsResponse> Comments
        {
            get { return comments; }
            set
            {
                if (Equals(value, comments)) return;
                comments = value;
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
