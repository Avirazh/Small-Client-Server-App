using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.Model;

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client;

        private ObservableCollection<Company> _companies;
        public MainWindow()
        {
            InitializeComponent();
            _client = new HttpClient();
            var response = _client.GetAsync("https://localhost:7275/company/getall").Result;
            if(response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                _companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
                foreach(var item in _companies)
                {
                    Trace.WriteLine(item.Name);
                }
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }
           
            string url = "https://localhost:7275/company/getall"; 
            var content = new StringContent
                ($"username={username}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded");
            

            try
            {
                HttpResponseMessage response = await _client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Авторизация успешна");
                    // ....
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации: " + response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Ошибка при отправке запроса: " + ex.Message);
            }
        }
    }
}