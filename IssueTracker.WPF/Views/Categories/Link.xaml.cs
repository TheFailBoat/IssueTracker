using System.Windows;
using IssueTracker.Data.Requests.Categories;

namespace IssueTracker.WPF.Views.Categories
{
    /// <summary>
    /// Using both Category and CategoryId is unsupported and will probably break
    /// </summary>
    public partial class Link
    {
        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(Data.Category), typeof(Link),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, ChangeCategory, CoerceCategory));

        public static readonly DependencyProperty CategoryIdProperty = DependencyProperty.Register("CategoryId", typeof(long), typeof(Link), new PropertyMetadata(default(long), ChangeCategory, CoerceCategory));

        public Data.Category Category
        {
            get
            {
                return (Data.Category)GetValue(CategoryProperty);
            }
            set
            {
                SetValue(CategoryProperty, value);
            }
        }

        public long CategoryId
        {
            get { return (long) GetValue(CategoryIdProperty); }
            set { SetValue(CategoryIdProperty, value); }
        }

        private static void ChangeCategory(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static object CoerceCategory(DependencyObject dependencyObject, object baseValue)
        {
            if (baseValue is long)
            {
                var category = Load((long)baseValue);
                dependencyObject.SetValue(CategoryProperty, category);

                return baseValue;
            }
            return baseValue as Data.Category;
        }

        public Link()
        {
            InitializeComponent();
        }

        private static Data.Category Load(long id)
        {
            if (id == 0) return null;

            return App.Client.Get(new CategoryDetails { Id = id });
        }
    }
}
