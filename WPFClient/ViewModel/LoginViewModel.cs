using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Core;
using WPFClient.Jwt;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.View;

namespace WPFClient.ViewModel
{
    public class LoginViewModel : LoginBase
    {
        private Frame _frame;
        private AppHttpClient _client;
        public ObservableCollection<Company> Companies { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand ShowLoginViaCompanyPageCommand { get; set; }
        public LoginViewModel(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            _frame = frame;
            _client = client;
            Companies = companies;

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            ShowLoginViaCompanyPageCommand = new RelayCommand(ExecuteShowLoginViaCompany, CanExecuteShowLoginViaCompany);
        }

        private async void ExecuteLogin(object obj)
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
                var responseContent = await response.Content.ReadAsStringAsync();
                JwtService.CreateFile(responseContent);

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserLogin), null);

                _frame.Content = new AuthenticatedUserPageView(_frame);
            }
            else
            {
                MessageBox.Show("An error occured with log in!, response code: " + response.StatusCode);
            }
        }
        private void ExecuteShowLoginViaCompany(object obj)
        {
            _frame.Content = new LoginViaCompanyPageView(_frame, _client, Companies);
        }
           
        private bool CanExecuteLogin(object obj)
        {
            return !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(Password);
        }
        private bool CanExecuteShowLoginViaCompany(object obj)
        {
            return true;
        }

    }
}