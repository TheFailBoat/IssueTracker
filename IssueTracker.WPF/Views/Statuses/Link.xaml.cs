using System.Windows;
using IssueTracker.Data.Requests.Statuses;

namespace IssueTracker.WPF.Views.Statuses
{
    /// <summary>
    /// Using both Status and StatusId is unsupported and will probably break
    /// </summary>
    public partial class Link
    {
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(Data.Status), typeof(Link),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, ChangeStatus, CoerceStatus));

        public static readonly DependencyProperty StatusIdProperty = DependencyProperty.Register("StatusId", typeof(long), typeof(Link), new PropertyMetadata(default(long), ChangeStatus, CoerceStatus));

        public Data.Status Status
        {
            get
            {
                return (Data.Status)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        public long StatusId
        {
            get { return (long) GetValue(StatusIdProperty); }
            set { SetValue(StatusIdProperty, value); }
        }

        private static void ChangeStatus(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static object CoerceStatus(DependencyObject dependencyObject, object baseValue)
        {
            if (baseValue is long)
            {
                var Status = Load((long)baseValue);
                dependencyObject.SetValue(StatusProperty, Status);

                return baseValue;
            }
            return baseValue as Data.Status;
        }

        public Link()
        {
            InitializeComponent();
        }

        private static Data.Status Load(long id)
        {
            if (id == 0) return null;

            return App.Client.Get(new StatusDetails { Id = id });
        }
    }
}
