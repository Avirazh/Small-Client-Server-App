using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFClient.Model;
using WPFClient.Net;

namespace WPFClient.ViewModel
{
    public class RegistrationViewModel
    {
        private Frame _frame;
        public ICommand RegistrationRequestCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public RegistrationViewModel(Frame frame)
        {
            _frame = frame;
            var client = new AppHttpClient();
            var response = client.GetAsync("/company/getall").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                Companies = JsonConvert.DeserializeObject<ObservableCollection<Company>>(json);
                foreach (var item in Companies)
                {
                    Trace.WriteLine(item.Name);
                }
            }
        }

        public async void Register(object parameter)
        {
            AppHttpClient client = new AppHttpClient();
            Companies = await client.GetAsync<ObservableCollection<Company>>("Company/GetAll");
            foreach(var company in Companies)
            {
                Trace.WriteLine(company.Name);
            }
            
        }
     
    }
}
