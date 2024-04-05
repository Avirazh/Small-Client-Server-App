using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Model;
using WPFClient.Net;

namespace WPFClient.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private Frame _frame;
        private AppHttpClient _client;

        private string _userLogin;
        private string _password;
        private Company _selectedCompany;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RegistrationRequestCommand { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

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

        public RegistrationViewModel(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            _frame = frame;
            _client = client;
            Companies = companies;

            RegistrationRequestCommand = new RelayCommand(Register, CanRegister);
        }

        public async void Register(object obj)
        {
            var user = new User
            {
                Login = UserLogin,
                PasswordHash = Password,
                Company = SelectedCompany
            };

            Trace.WriteLine($"{UserLogin}, {Password}, {SelectedCompany}");
            string json = JsonConvert.SerializeObject(user);
            Trace.WriteLine($"{json}");

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/user/register", stringContent);
            Trace.WriteLine("response is success? : " + response.IsSuccessStatusCode);
        }
        private bool CanRegister(object obj)
        {
            return !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(Password) && SelectedCompany != null;
        }
    }
}
