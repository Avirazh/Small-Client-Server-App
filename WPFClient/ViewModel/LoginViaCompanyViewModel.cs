using System.Windows.Controls;
using WPFClient.Net;

namespace WPFClient.ViewModel
{
    public class LoginViaCompanyViewModel
    {
        private Frame _frame;
        private AppHttpClient _httpClient;
        public LoginViaCompanyViewModel(Frame frame, AppHttpClient client)
        {
            _frame = frame;
            _httpClient = client;
        }
    }
}
