using System.Windows.Controls;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class AuthenticatedUserPageView : Page
    {
        public AuthenticatedUserPageView(Frame frame)
        {
            InitializeComponent();
            DataContext = new AuthenticatedUserViewModel(frame);
        }
    }
}