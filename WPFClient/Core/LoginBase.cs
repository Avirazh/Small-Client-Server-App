using System;
using System.ComponentModel;
using WPFClient.Model;

namespace WPFClient.Core
{
    public class LoginBase : INotifyPropertyChanged
    {
        protected string _userLogin;
        protected string _password;
        protected Company _selectedCompany;

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserLogin)));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{nameof(SelectedCompany)}"));
            }
        }
    }
}