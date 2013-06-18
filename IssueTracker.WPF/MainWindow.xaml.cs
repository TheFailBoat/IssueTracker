using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using IssueTracker.WPF.Core;
using IssueTracker.WPF.ViewModels;

namespace IssueTracker.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool clearPrev;
        internal MainWindowViewModel Data { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Data = new MainWindowViewModel();
            DataContext = Data;

            Loaded += OnLoaded;
            MainFrame.Navigating += MainFrame_Navigating;
            MainFrame.Navigated += MainFrame_Navigated;

            App.PropertyChanged += AppOnPropertyChanged;
        }

        private void AppOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser")
            {
                Data.CurrentUser = App.CurrentUser;
            }
        }

        void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (clearPrev)
            {
                MainFrame.NavigationService.RemoveBackEntry();
                clearPrev = false;
            }
        }

        void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.ExtraData == ClearHistory.Instance)
            {
                clearPrev = true;
            }
        }

        async void OnLoaded(object sender, RoutedEventArgs e)
        {
            Data.CurrentUser = await App.GetCurrentUser();

            if (Data.CurrentUser == null)
            {
                MainFrame.Navigate(new Views.Login());
            }
            else
            {
                MainFrame.Navigate(new Views.Issues.List());
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.GoBack();
            }
        }
    }
}
