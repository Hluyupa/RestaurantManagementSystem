using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RestarauntClient.Models;
using RestarauntClient.Models.POCOModels;
using RestarauntClient.View.Pages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace RestarauntClient.ViewModels
{
    public class OrderDetailsPageVM : INotifyPropertyChanged
    {
        public RelayCommand DishGiven { get; set; }

        private DishCookOrder givenDishSelected;
        public DishCookOrder GivenDishSelected
        {
            get
            {
                return givenDishSelected;
            }
            set
            {
                givenDishSelected = value;
                OnPropertyChanged("GivenDishSelected");
            }
        }

        private ObservableCollection<DishCookOrder> orderDetailsList;
        public ObservableCollection<DishCookOrder> OrderDetailsList 
        { 
            get 
            {
                return orderDetailsList;
            } 
            set 
            {
                orderDetailsList = value;
                OnPropertyChanged();
            } 
        }
        private Order selectedOrder;
        public OrderDetailsPageVM(Order _selectedOrder)
        {
            DishGiven = new RelayCommand(dishGiven);
            selectedOrder = _selectedOrder;
            OrderDetailsList = new ObservableCollection<DishCookOrder>();
            GetOrderDetails();
            Client.Instance().hubConnection.On<string>("UpdateOrders", (dishCookOrders) =>
            {
                OrderDetailsList = new ObservableCollection<DishCookOrder>(JsonConvert.DeserializeObject<ObservableCollection<DishCookOrder>>(dishCookOrders).Where(p=>p.OrderId.Equals(selectedOrder.OrderId)));
            });
        }

        private void dishGiven(object obj)
        {
            int orderId = GivenDishSelected.OrderId;
            var requestDishGiven = new RestRequest("api/DishCookOrders/UpdateDishGiven", Method.PUT).AddJsonBody(GivenDishSelected);
            var responceDishGiven = Client.Instance().httpClient.Execute(requestDishGiven);
            if(responceDishGiven.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Блюдо успешно доставлено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            int i = 1;
            foreach(var item in OrderDetailsList)
            {
                if(item.DishStatus != "Доставлено")
                {
                    i = 0;
                    break;
                }
            }
            if(i == 1)
            {
                var requestOrderStatus = new RestRequest($"api/Orders/UpdateOrderStatus/{orderId}", Method.PUT);
                var responceOrderStatus = Client.Instance().httpClient.Execute(requestOrderStatus);
                if(responceOrderStatus.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Заказ успешно закрыт!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void GetOrderDetails()
        {
            var requestOrderDetails = new RestRequest("api/DishCookOrders/DishDetails/"+selectedOrder.OrderId);
            var responceOrderDetails = Client.Instance().httpClient.Execute(requestOrderDetails);
            if(responceOrderDetails.StatusCode == System.Net.HttpStatusCode.OK)
            {
                OrderDetailsList = JsonConvert.DeserializeObject<ObservableCollection<DishCookOrder>>(responceOrderDetails.Content);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
