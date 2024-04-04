using System.Windows.Controls;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class LoginPageView : Page
    {
        public LoginPageView(Frame frame)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(frame);
        }
    }
}
