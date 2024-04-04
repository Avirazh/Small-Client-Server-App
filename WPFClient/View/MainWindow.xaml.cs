using System.Windows;
using System.Windows.Navigation;

namespace WPFClient.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NavigationService.GetNavigationService(frame);
            frame.Content = new StartPageView(frame);
        }
    }
}