using RestarauntClient.Models.POCOModels;
using RestarauntClient.View.Windows;
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
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class DishCookingPageVM : INotifyPropertyChanged
    {
        private bool checkButtonClick = false;
        public RelayCommand UpdateStatusDish { get; set; }

        private ObservableCollection<DishesIngridient> dishIngridientsList;
        public ObservableCollection<DishesIngridient> DishIngridientsList
        {
            get 
            {
                return dishIngridientsList;
            }
            set 
            {
                dishIngridientsList = value;
                OnPropertyChanged();
            } 
        }

        private DishCookOrder dishPreparedSelected;
        public DishCookOrder DishPreparedSelected
        {
            get 
            {
                return dishPreparedSelected; 
            }
            set 
            {
                dishPreparedSelected = value;
                if (dishPreparedSelected != null)
                {
                    var requestDishIngridients = new RestRequest("api/DishesIngridients/GetDishIngridients/" + dishPreparedSelected.Dish.DishId);
                    var responceDishIngridients = Client.Instance().httpClient.Execute(requestDishIngridients);
                    if (responceDishIngridients.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        DishIngridientsList = JsonSerializer.Deserialize<ObservableCollection<DishesIngridient>>(responceDishIngridients.Content);
                        
                    }
                    checkButtonClick = false;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DishCookOrder> dishesToCookingList;
        public ObservableCollection<DishCookOrder> DishesToCookingList 
        {
            get 
            {
                return dishesToCookingList;
            }
            set 
            {
                dishesToCookingList = value;
                OnPropertyChanged();
            }
        }

        CookWindowVM cookWindow;
        public DishCookingPageVM(CookWindowVM _cookWindow)
        {
            cookWindow = _cookWindow;
            UpdateStatusDish = new RelayCommand(updateStatusDish);
            FillDishesToCookingList();
        }

        private void updateStatusDish(object obj)
        {
            checkButtonClick = true;
            var requestDishPrepared = new RestRequest("api/DishCookOrders/UpdateDishStatusDCO", Method.PUT).AddJsonBody(DishPreparedSelected);
            var responceDishPrepared = Client.Instance().httpClient.Execute(requestDishPrepared);
            if(responceDishPrepared.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Блюдо приготовлено и ожидает выноса гостю", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DishesToCookingList.Remove(DishPreparedSelected);
            }

        }

        private void FillDishesToCookingList()
        {
            var requestDishesToCooking = new RestRequest("api/DishCookOrders/GetDishToCooking/" + cookWindow.login);
            var responceDishesToCooking = Client.Instance().httpClient.Execute(requestDishesToCooking);
            if(responceDishesToCooking.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DishesToCookingList = JsonSerializer.Deserialize<ObservableCollection<DishCookOrder>>(responceDishesToCooking.Content);
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
