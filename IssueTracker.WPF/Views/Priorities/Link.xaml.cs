using System.Windows;
using IssueTracker.Data.Requests.Priorities;

namespace IssueTracker.WPF.Views.Priorities
{
    /// <summary>
    /// Using both Priority and PriorityId is unsupported and will probably break
    /// </summary>
    public partial class Link
    {
        public static readonly DependencyProperty PriorityProperty =
            DependencyProperty.Register("Priority", typeof(Data.Priority), typeof(Link),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, ChangePriority, CoercePriority));

        public static readonly DependencyProperty PriorityIdProperty = DependencyProperty.Register("PriorityId", typeof(long), typeof(Link), new PropertyMetadata(default(long), ChangePriority, CoercePriority));

        public Data.Priority Priority
        {
            get
            {
                return (Data.Priority)GetValue(PriorityProperty);
            }
            set
            {
                SetValue(PriorityProperty, value);
            }
        }

        public long PriorityId
        {
            get { return (long) GetValue(PriorityIdProperty); }
            set { SetValue(PriorityIdProperty, value); }
        }

        private static void ChangePriority(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static object CoercePriority(DependencyObject dependencyObject, object baseValue)
        {
            if (baseValue is long)
            {
                var Priority = Load((long)baseValue);
                dependencyObject.SetValue(PriorityProperty, Priority);

                return baseValue;
            }
            return baseValue as Data.Priority;
        }

        public Link()
        {
            InitializeComponent();
        }

        private static Data.Priority Load(long id)
        {
            if (id == 0) return null;

            return App.Client.Get(new PriorityDetails { Id = id });
        }
    }
}
