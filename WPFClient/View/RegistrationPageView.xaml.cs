using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class RegistrationPageView : Page
    {
        public RegistrationPageView(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(frame, client, companies);
        }
    }
}