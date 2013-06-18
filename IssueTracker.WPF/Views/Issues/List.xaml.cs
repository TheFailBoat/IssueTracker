using System.Windows.Input;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Issues;
using IssueTracker.WPF.ViewModels;

namespace IssueTracker.WPF.Views.Issues
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List
    {
        public List()
        {
            InitializeComponent();

            Loaded += (s, e) => Reload();
        }

        public void Reload()
        {
            var ctx = new IssuesListViewModel { IsLoading = true };

            DataContext = ctx;

            App.Client.GetAsync(new IssuesList(), list =>
                {
                    ctx.Issues = list;
                    ctx.IsLoading = false;
                },
                (list, exception) =>
                {
                    throw exception;
                });
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var issue = IssuesList.SelectedItem as Issue;
            if (issue == null) return;

            NavigationService.Navigate(new Views.Issues.Detail(issue.Id));
        }
    }
}
