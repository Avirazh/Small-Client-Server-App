using System;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.View;

namespace WPFClient.ViewModel
{
    public class StartViewModel
    {
        private Frame _frame;
        public ICommand RegistrationCommand { get;set; }
        public ICommand LoginCommand { get;set; }

        public StartViewModel(Frame frame)
        {
            _frame = frame;
            RegistrationCommand = new RelayCommand(ShowRegistrationView, CanShowRegistrationView);
            LoginCommand = new RelayCommand(ShowLoginView, CanShowLoginView);
        }

        private void ShowRegistrationView(object obj)
        {
            _frame.Content = new RegistrationPageView(_frame);
        }
        public void ShowLoginView(object obj)
        {
            _frame.Content = new LoginPageView(_frame);
        }

        private bool CanShowRegistrationView(object obj)
        {
            return true;
        }
        private bool CanShowLoginView(object obj)
        {
            return true;
        }
    }
}