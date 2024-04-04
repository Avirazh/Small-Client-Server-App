using System.Windows.Controls;
using WPFClient.ViewModel;

namespace WPFClient.View
{
    public partial class StartPageView : Page
    {
        public StartPageView(Frame frame)
        {
            InitializeComponent();
            DataContext = new StartViewModel(frame);
        }
    }
}