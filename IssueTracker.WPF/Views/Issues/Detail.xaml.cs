using System.Windows;
using System.Windows.Controls;
using IssueTracker.Data.Requests.Comments;
using IssueTracker.Data.Requests.Issues;
using IssueTracker.WPF.ViewModels;

namespace IssueTracker.WPF.Views.Issues
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Page
    {
        public long IssueId { get; private set; }

        public Detail(long issueId)
        {
            InitializeComponent();

            IssueId = issueId;
            this.Loaded += Detail_Loaded;
        }

        void Detail_Loaded(object sender, RoutedEventArgs e)
        {
            var data = new IssueDetailViewModel();
            DataContext = data;

            App.Client.GetAsync(new IssueDetails { Id = IssueId },
                x =>
                {
                    data.Issue = x.Issue;
                    data.Category = x.Category;
                    data.Status = x.Status;
                    data.Priority = x.Priority;
                    data.Reporter = x.Reporter;
                },
                (response, exception) => { throw exception; });
            App.Client.GetAsync(new CommentsList { Id = IssueId },
                x =>
                {
                    data.Comments = x;
                },
                (response, exception) => { throw exception; });
        }
    }
}
