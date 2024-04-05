using System.Windows.Controls;
using WPFClient.Net;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class LoginViaCompanyPageView : Page
    {
        public LoginViaCompanyPageView(Frame frame, AppHttpClient client)
        {
            InitializeComponent();
            DataContext = new LoginViaCompanyViewModel(frame, client);
        }
    }
}