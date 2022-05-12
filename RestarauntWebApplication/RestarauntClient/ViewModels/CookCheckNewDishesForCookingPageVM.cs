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
    public class CookCheckNewDishesForCookingPageVM : INotifyPropertyChanged
    {
        public RelayCommand AddDishToCooking { get; set; }

        private CookWindowVM cookWindowVM;
        private CookCheckNewDishesForCookingPage cookingPage;

        private ObservableCollection<DishCookOrder> dishCookOrders;
        public ObservableCollection<DishCookOrder> DishCookOrders 
        { 
            get 
            {
                return dishCookOrders;
            } 
            set 
            {
                dishCookOrders = value;
                OnPropertyChanged();
            }
        }

        private DishCookOrder dishToCookingSelected;
        public DishCookOrder DishToCookingSelected
        { 
            get 
            {
                return dishToCookingSelected;
            } 
            set 
            {
                dishToCookingSelected = value;
                OnPropertyChanged("DishToCookingSelected");
            } 
        }

        
        //public ObservableCollection<Kitchen> DishesForCooking { get; set; }
        public CookCheckNewDishesForCookingPageVM(CookWindowVM _cookWindowVM, CookCheckNewDishesForCookingPage _cookingPage)
        {
            cookWindowVM = _cookWindowVM;
            cookingPage = _cookingPage;
            DishCookOrders = new ObservableCollection<DishCookOrder>();
            AddDishToCooking = new RelayCommand(addDishToCooking);
            DishesForCookingCheck();
            Client.Instance().hubConnection.On<string>("NewOrders", (dishCookOrders) => 
            {
                DishCookOrders = new ObservableCollection<DishCookOrder>(JsonConvert.DeserializeObject<ObservableCollection<DishCookOrder>>(dishCookOrders).Where(p => p.CookId == null));
            });
            Client.Instance().hubConnection.On<string>("UpdateStatusOrders", (dishCookOrders) =>
            {
                DishCookOrders = new ObservableCollection<DishCookOrder>(JsonConvert.DeserializeObject<ObservableCollection<DishCookOrder>>(dishCookOrders).Where(p => p.CookId == null));
            });
        }

        private void addDishToCooking(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите приготовить выбранное блюдо?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var requestIngridintCount = new RestRequest("api/Ingridients/UpdateIngridientsQuanity", Method.PUT).AddJsonBody(DishToCookingSelected);
                var responceIngridintCount = Client.Instance().httpClient.Execute(requestIngridintCount);
                
                DishCookOrderUpdateModel dishCookOrderUpdateModel = new DishCookOrderUpdateModel { DishCookOrder = DishToCookingSelected, Login = cookWindowVM.login };
                var requestDishCookOrder = new RestRequest("api/DishCookOrders/UpdateCookIdDCO", Method.PUT).AddJsonBody(dishCookOrderUpdateModel);
                var responceDishCookOrder = Client.Instance().httpClient.Execute(requestDishCookOrder);
                if(responceDishCookOrder.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Вы взяли на себя выбранное блюдо");
                }
            }
        }

        private void DishesForCookingCheck()
        {
            var requestDishCookOrders = new RestRequest("api/DishCookOrders", Method.GET);
            var responceDishCookOrders = Client.Instance().httpClient.Execute(requestDishCookOrders);
            if (responceDishCookOrders.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DishCookOrders = JsonConvert.DeserializeObject<ObservableCollection<DishCookOrder>>(responceDishCookOrders.Content);
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
