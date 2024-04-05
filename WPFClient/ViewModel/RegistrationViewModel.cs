using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Core;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.View;

namespace WPFClient.ViewModel
{
    public class RegistrationViewModel : LoginBase 
    {
        private Frame _frame;
        private AppHttpClient _client;

        public ICommand RegistrationRequestCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        public RegistrationViewModel(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            _frame = frame;
            _client = client;
            Companies = companies;

            RegistrationRequestCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }

        public async void ExecuteRegister(object obj)
        {
            var user = new User
            {
                Login = UserLogin,
                PasswordHash = Password,
                Company = SelectedCompany
            };

            string json = JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/user/register", stringContent);

            if(response.IsSuccessStatusCode)
            {
                MessageBox.Show("You have successfully registered!");
                _frame.Content = new LoginPageView(_frame, _client, Companies);
            }
            else
            {
                MessageBox.Show("Something went wrong :(");
            }
        }
        private bool CanExecuteRegister(object obj)
        {
            return !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(Password) && SelectedCompany != null;
        }
    }
}