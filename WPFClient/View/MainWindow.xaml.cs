using System.Windows;
using System.Windows.Navigation;
using WPFClient.Net;

namespace WPFClient.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppHttpClient client = new AppHttpClient();
            NavigationService.GetNavigationService(frame);
            frame.Content = new StartPageView(frame, client);
        }
    }
}