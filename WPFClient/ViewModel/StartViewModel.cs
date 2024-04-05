using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Diagnostics;
using System.Windows.Input;
using System.Windows.Navigation;
using WPFClient.Model;
using WPFClient.Net;
using WPFClient.View;

namespace WPFClient.ViewModel
{
    public class StartViewModel
    {
        private Frame _frame;
        private ObservableCollection<Company> Companies { get; set; }
        private bool _gotCompanies = false;
        private AppHttpClient _client;
        public ICommand RegistrationCommand { get;set; }
        public ICommand LoginCommand { get;set; }

        
        public StartViewModel(Frame frame, AppHttpClient client)
        {
            _frame = frame;
            _client = client;

            RegistrationCommand = new RelayCommand(ShowRegistrationView, CanShowRegistrationView);
            LoginCommand = new RelayCommand(ShowLoginView, CanShowLoginView);

            _frame.Navigated += OnStartViewNavigated;
        }

        private async void OnStartViewNavigated(object sender, NavigationEventArgs e)
        {
            var response = await _client.GetAsync("/company/getall");
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
                foreach (var item in Companies)
                {
                    Trace.Write(item.Name);
                    foreach(var user in item.Users)
                    {
                        Trace.Write($" : users: {user.Login}, ");
                    }
                    Trace.WriteLine("");
                }
                _gotCompanies = true;
            }
        }

        private void ShowRegistrationView(object obj)
        {
            _frame.Content = new RegistrationPageView(_frame, _client, Companies);
        }
        public void ShowLoginView(object obj)
        {
            _frame.Content = new LoginPageView(_frame);
        }

        private bool CanShowRegistrationView(object obj)
        {
            return _gotCompanies;
        }
        private bool CanShowLoginView(object obj)
        {
            return _gotCompanies;
        }
    }
}