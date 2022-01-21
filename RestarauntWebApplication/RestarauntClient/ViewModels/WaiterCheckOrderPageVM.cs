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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestarauntClient.ViewModels
{
    public class WaiterCheckOrderPageVM : INotifyPropertyChanged
    {
        public RelayCommand OrderDetailsCommand { get; set; }

        private WaiterWindowVM waiterWindow;

        /*private ComboBoxItem orderStatusSelected;
        public ComboBoxItem OrderStatusSelected
        {
            get 
            { 
                return orderStatusSelected; 
            }
            set 
            { 
                orderStatusSelected = value;
                if(orderStatusSelected.Content.ToString() != "Все заказы")
                {
                    var orders = Orders;
                    Orders = new ObservableCollection<Order>(orders.Where(p => p.Equals(orderStatusSelected.Content.ToString())));
                }
                OnPropertyChanged("OrderStatusSelected");
            }
        }


        private ObservableCollection<ComboBoxItem> ordersStatusComboBox;
        public ObservableCollection<ComboBoxItem> OrdersStatusComboBox
        {
            get 
            {
                return ordersStatusComboBox; 
            }
            set 
            {
                ordersStatusComboBox = value;
                OnPropertyChanged();
            }
        }*/


        private Order selectedOrder;
        public Order SelectedOrder
        {
            get 
            { 
                return selectedOrder; 
            }
            set 
            { 
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get 
            {
                if(orders is null)
                {
                    orders = new ObservableCollection<Order>();
                }
                return orders;
            }
            set 
            {
                orders = value;
                OnPropertyChanged();
            } 
        }
        public WaiterCheckOrderPageVM(WaiterWindowVM _waiterWindow)
        {
            OrderDetailsCommand = new RelayCommand(orderDetailsCommand);
            waiterWindow = _waiterWindow;
            /*OrdersStatusComboBox = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Content = "Все заказы" },
                new ComboBoxItem { Content = "Активен" },
                new ComboBoxItem { Content = "Неактивен"}
            };*/
            var requestOrders = new RestRequest("api/Waiters/WaiterLogin/" + waiterWindow.login);
            var responceOrders = Client.Instance().httpClient.Execute(requestOrders);
            
            if (responceOrders.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var waiter = JsonSerializer.Deserialize<List<Waiter>>(responceOrders.Content);
                Orders = new ObservableCollection<Order>(waiter.First().Orders);
                
            }
        }

        private void orderDetailsCommand(object obj)
        {
            waiterWindow.CurrentPage = new OrderDetailsPage(SelectedOrder);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
