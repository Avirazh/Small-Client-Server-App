using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class LoginPageView : Page
    {
        public LoginPageView(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(frame, client, companies);
        }
    }
}
