using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client;

        public MainWindow()
        {
            InitializeComponent();
            _client = new HttpClient();
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
           
            string url = "http://localhost:8080/auth"; 
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
