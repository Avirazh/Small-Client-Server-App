using System.Windows.Controls;
using WPFClient.Net;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class StartPageView : Page
    {
        public StartPageView(Frame frame, AppHttpClient client)
        {
            InitializeComponent();
            DataContext = new StartViewModel(frame, client);
        }
    }
}