using System.Windows.Controls;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class RegistrationPageView : Page
    {
        public RegistrationPageView(Frame frame)
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(frame);
        }
    }
}