using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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

            RegistrationCommand = new RelayCommand(ExecuteShowRegistrationView, CanExecuteShowRegistrationView);
            LoginCommand = new RelayCommand(ExecuteShowLoginView, CanExecuteShowLoginView);

            _frame.Navigated += OnStartViewNavigatedLoadAllCompanies;
        }

        private async void OnStartViewNavigatedLoadAllCompanies(object sender, NavigationEventArgs e)
        {
            var response = await _client.GetAsync("/company/getall");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
                _gotCompanies = true;
            }
            _frame.Navigated -= OnStartViewNavigatedLoadAllCompanies;
        }

        private void ExecuteShowRegistrationView(object obj)
        {
            _frame.Content = new RegistrationPageView(_frame, _client, Companies);
        }
        public void ExecuteShowLoginView(object obj)
        {
            _frame.Content = new LoginPageView(_frame, _client, Companies);
        }

        private bool CanExecuteShowRegistrationView(object obj)
        {
            return _gotCompanies;
        }
        private bool CanExecuteShowLoginView(object obj)
        {
            return _gotCompanies;
        }
    }
}