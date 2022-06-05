using RestarauntClient.Models;
using RestarauntClient.Models.POCOModels;
using RestarauntClient.View.Pages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class WaiterCreateNewOrderPageVM:INotifyPropertyChanged
    {
        WaiterCreateOrderPage waiterCreateOrderPage;
        private WaiterWindowVM waiterWindowVM;
        public RelayCommand CreateOrder { get; set; }

        private string number;
        public string Number
        {
            get 
            { 
                return number; 
            }
            set 
            { 
                number = value;
                OnPropertyChanged("Number");
            }
        }


        


        public WaiterCreateNewOrderPageVM(WaiterWindowVM _waiterWindowVM)
        {
           

            CreateOrder = new RelayCommand(createOrder);
            waiterWindowVM = _waiterWindowVM;
        }

        private void createOrder(object obj)
        {
            /*CompositeOrderModel compositeOrderModel = new CompositeOrderModel();
            compositeOrderModel.VisitorId = 0;
            compositeOrderModel.WaiterLogin = waiterWindowVM.login;
            compositeOrderModel.OrderNumber = 0;
            compositeOrderModel.OrderStatus = "Активен";
            waiterCreateOrderPage = new WaiterCreateOrderPage(compositeOrderModel);
            waiterWindowVM.CurrentPage = waiterCreateOrderPage;*/
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
