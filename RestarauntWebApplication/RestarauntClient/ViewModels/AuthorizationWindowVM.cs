using Microsoft.AspNetCore.SignalR.Client;
using RestarauntClient.Models;
using RestarauntClient.Models.POCOModels;
using RestarauntClient.View.Windows;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntClient.ViewModels
{
    class AuthorizationWindowVM : INotifyPropertyChanged
    {
        private AuthorizationWindow AuthorizationWindow;
        public LoginModel LoginModel { get; set; }
        public RelayCommand AuthCommand { get; set; }
        public AuthorizationWindowVM(AuthorizationWindow _authorizationWindow)
        {
            AuthorizationWindow = _authorizationWindow;
            LoginModel = new LoginModel();
            Client.Instance().httpClient = new RestClient("http://172.20.1.95:8240");
            AuthCommand = new RelayCommand(authCommand);
        }

        private void authCommand(object obj)
        {
            var request = new RestRequest("api/Authentification", Method.POST).AddJsonBody(new { login = LoginModel.Login, password = LoginModel.Password });
            var responce = Client.Instance().httpClient.Execute(request);
            
            if (responce.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var user = JsonSerializer.Deserialize<string>(responce.Content);

                switch (user)
                {
                    case "Повар":
                        CookWindow cookWindow = new CookWindow(LoginModel.Login);
                        cookWindow.Show();
                        AuthorizationWindow.Close();
                        break;
                    case "Официант":
                        WaiterWindow waiterWindow = new WaiterWindow(LoginModel.Login);
                        waiterWindow.Show();
                        AuthorizationWindow.Close();
                        break;
                    case "Оператор":
                        break;
                    case "Администратор":
                        AdministratorWindow administratorWindow = new AdministratorWindow();
                        administratorWindow.Show();
                        AuthorizationWindow.Close();
                        break;
                }
                Client.Instance().hubConnection = new HubConnectionBuilder()
                   .WithUrl("http://172.20.1.95:8240/notification")
                   .Build();
                Client.Instance().hubConnection.StartAsync();
            }
            else if(responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Неверный логин или пароль", "Ощибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
           
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
