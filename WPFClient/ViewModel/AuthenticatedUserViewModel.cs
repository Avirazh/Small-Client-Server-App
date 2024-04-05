using System.Windows.Controls;

namespace WPFClient.ViewModel
{
    public class AuthenticatedUserViewModel
    {
        private Frame _frame;
        public AuthenticatedUserViewModel(Frame frame)
        {
            _frame = frame;
        }
    }
}