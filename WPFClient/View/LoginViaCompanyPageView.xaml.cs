using System.Windows.Controls;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class LoginViaCompanyPageView : Page
    {
        public LoginViaCompanyPageView(Frame frame)
        {
            InitializeComponent();
            DataContext = new LoginViaCompanyViewModel(frame);
        }
    }
}