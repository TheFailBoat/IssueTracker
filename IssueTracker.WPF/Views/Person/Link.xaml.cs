using System.Windows;
using IssueTracker.Data.Requests.People;

namespace IssueTracker.WPF.Views.Person
{
    /// <summary>
    /// Using both Person and PersonId is unsupported and will probably break
    /// </summary>
    public partial class Link
    {
        public static readonly DependencyProperty PersonProperty =
            DependencyProperty.Register("Person", typeof(Data.Person), typeof(Link),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, ChangePerson, CoercePerson));

        public static readonly DependencyProperty PersonIdProperty = DependencyProperty.Register("PersonId", typeof(long), typeof(Link), new PropertyMetadata(default(long), ChangePerson, CoercePerson));

        public Data.Person Person
        {
            get
            {
                return (Data.Person)GetValue(PersonProperty);
            }
            set
            {
                SetValue(PersonProperty, value);
            }
        }

        public long PersonId
        {
            get { return (long) GetValue(PersonIdProperty); }
            set { SetValue(PersonIdProperty, value); }
        }

        private static void ChangePerson(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static object CoercePerson(DependencyObject dependencyObject, object baseValue)
        {
            if (baseValue is long)
            {
                var person = Load((long)baseValue);
                dependencyObject.SetValue(PersonProperty, person);

                return baseValue;
            }
            return baseValue as Data.Person;
        }

        public Link()
        {
            InitializeComponent();
        }

        private static Data.Person Load(long id)
        {
            if (id == 0) return null;

            return App.Client.Get(new PersonDetails { Id = id });
        }
    }
}
