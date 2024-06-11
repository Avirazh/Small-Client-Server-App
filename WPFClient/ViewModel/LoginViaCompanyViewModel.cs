using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class LoginViaCompanyViewModel : LoginBase
    {
        private Frame _frame;
        private AppHttpClient _client;
        private User _selectedUser;
        
        public ObservableCollection<Company> Companies { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ICommand LoginCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedUser)));
            }
        }
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                LoadUsers();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"{nameof(SelectedCompany)}"));
            }
        }
        public LoginViaCompanyViewModel(Frame frame, AppHttpClient client, ObservableCollection<Company> companies)
        {
            _frame = frame;
            _client = client;
            Companies = companies;
            Users = new ObservableCollection<User>();

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);

            GetAllCompaniesUsers();
        }

        private async void GetAllCompaniesUsers()
        {
            foreach (var company in Companies) 
            {
                var response = await _client.GetAsync($"/company/getUsersForCompany?companyId={company.Id}");
                var stringMessage = await response.Content.ReadAsStringAsync();

                company.Users = JsonConvert.DeserializeObject<List<User>>(stringMessage);
            }
        }
        private void LoadUsers()
        {
            Users.Clear();
            if(SelectedCompany != null)
            {
                foreach(var user in SelectedCompany.Users) 
                {
                    Users.Add(user);
                }
            }
        }
        private async void ExecuteLogin(object obj)
        {
            User user = new User()
            {
                Login = SelectedUser.Login,
                PasswordHash = Password,
            };
            string json = JsonConvert.SerializeObject(user);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/user/login", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                JwtService.CreateFile(responseContent);

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(SelectedUser.Login), null);

                _frame.Content = new AuthenticatedUserPageView(_frame);
            }
            else
            {
                MessageBox.Show("An error occured with log in!");
            }
        }

        private bool CanExecuteLogin(object obj)
        {
            return SelectedUser != null && !string.IsNullOrEmpty(Password);
        }
    }
}