using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.View;

namespace WPFClient.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private Frame _frame;
        private AppHttpClient _client;

        private string _userLogin;
        private string _password;

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
        public bool isAuthenticated = false;
        public ObservableCollection<Company> Companies { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand ShowLoginViaCompanyPageCommand { get; set; }
        public LoginViewModel(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            _frame = frame;
            _client = client;
            Companies = companies;

            LoginCommand = new RelayCommand(Login, CanLogin);
            ShowLoginViaCompanyPageCommand = new RelayCommand(ShowLoginViaCompany, CanShowLoginViaCompany);
        }

        private async void Login(object obj)
        {
            User user = new User()
            {
                Login = UserLogin,
                PasswordHash = Password,
            };
            string json = JsonConvert.SerializeObject(user);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/user/login", requestContent);

            if(response.IsSuccessStatusCode)
            {
                isAuthenticated = true;

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserLogin), null);

                _frame.Content = new AuthenticatedUserPageView(_frame);
            }
            else
            {
                MessageBox.Show("An error occured with log in!");
            }
        }
        private void ShowLoginViaCompany(object obj)
        {
            _frame.Content = new LoginViaCompanyPageView(_frame, _client);
        }
           
        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(Password);
        }
        private bool CanShowLoginViaCompany(object obj)
        {
            return true;
        }

    }
}