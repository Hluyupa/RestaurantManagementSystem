using Microsoft.AspNetCore.SignalR.Client;
using RestarauntClient.Models;
using RestarauntClient.Models.POCOModels;
using RestarauntClient.View.Windows;
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
    public class WaiterCreateOrderPageVM : INotifyPropertyChanged
    {


        private CompositeOrderModel compositeOrderModel;

        private WaiterWindowVM WaiterWindowVM;
        public RelayCommand AddDishCommand { get; set; }
        public RelayCommand AddDishInOrder { get; set; }

        private ObservableCollection<DishListModel> addDishes;
        public ObservableCollection<DishListModel> AddDishes 
        { 
            get 
            {
                if (addDishes == null)
                {
                    addDishes = new ObservableCollection<DishListModel>();
                }
                return addDishes;
            } 
            set 
            {
                addDishes = value;
                OnPropertyChanged("AddDishes");
            } 
        }

        private string dishCount;
        public string DishCount 
        {
            get 
            {
                return dishCount;
            } 
            set 
            {
                dishCount = value;
                OnPropertyChanged("DishCount");
            } 
        }


        private Dish dishSelected;
        public Dish DishSelected
        { 
            get 
            {
                return dishSelected;
            }
            set
            {
                dishSelected = value;
                OnPropertyChanged("DishSelected");
            } 
        }
        public ObservableCollection<DishType> DishTypes { get; set; } //Для выбора типов

        private ObservableCollection<Dish> _dishesOfType; // Для обновления списка блюд
        public ObservableCollection<Dish> DishesOfType //Список для выбора блюда, который выводится по выбранному типу
        {
            get
            {
                if (_dishesOfType is null)
                    _dishesOfType = new ObservableCollection<Dish>();
                return _dishesOfType;
            }
            set
            {
                _dishesOfType = value;
                OnPropertyChanged();
            }
        } 

        private DishType dishTypeSelected;
        public DishType DishTypeSelected { 
            get 
            {
                return dishTypeSelected;
            } 
            set 
            {
                dishTypeSelected = value;
                OnPropertyChanged("DishTypeSelected");
                
                var request = new RestRequest("api/Dishes/DishesOfType/{idDishType}", Method.GET);
                request.AddUrlSegment("idDishType", dishTypeSelected.DishTypeId);
                var responce = Client.Instance().httpClient.Execute(request);
                if(responce.StatusCode == HttpStatusCode.OK)
                {
                    DishesOfType = new ObservableCollection<Dish>(JsonSerializer.Deserialize<IEnumerable<Dish>>(responce.Content));
                }
            } 
        }
        
        public WaiterCreateOrderPageVM(WaiterWindowVM waiterWindowVM)
        {

            WaiterWindowVM = waiterWindowVM;

            AddDishes = new ObservableCollection<DishListModel>();

            var requestDishTypes = new RestRequest("api/DishTypes", Method.GET);
            var responceDishTypes = Client.Instance().httpClient.Execute(requestDishTypes);

            if(responceDishTypes.StatusCode == HttpStatusCode.OK)
            {
                DishTypes = JsonSerializer.Deserialize<ObservableCollection<DishType>>(responceDishTypes.Content);
               

                /*foreach (var item in Dishes)
                {
                    if (DishTypes.FirstOrDefault(p => p.Equals(item.DishType)) == null)
                    {
                        DishTypes.Add(item.DishType);
                    }
                }*/
            }

            var requestDishes = new RestRequest("api/Dishes", Method.GET);
            var responceDishes = Client.Instance().httpClient.Execute(requestDishes);

            if (responceDishes.StatusCode == HttpStatusCode.OK)
            {
                DishesOfType = JsonSerializer.Deserialize<ObservableCollection<Dish>>(responceDishes.Content);
            }

            AddDishCommand = new RelayCommand(addDishCommand);
            AddDishInOrder = new RelayCommand(addDishInOrder);

        }

        private void addDishInOrder(object obj)
        {
            compositeOrderModel = new CompositeOrderModel 
            {
                VisitorId = 0,
                WaiterLogin = WaiterWindowVM.login,
                OrderNumber = 0,
                OrderStatus = "Активен",
                DishCookOrders = new List<DishCookOrder>()
            };

            foreach (var item in AddDishes)
            {
                compositeOrderModel.DishCookOrders.Add(new DishCookOrder { CookId = null, DishId = item.Dish.DishId, DishStatus = "Не готово", DishCount = item.DishCount });
            }
            var requestOrder = new RestRequest("api/Orders/CompositeOrder", Method.POST).AddJsonBody(compositeOrderModel);
            var responceOrder = Client.Instance().httpClient.Execute(requestOrder);
            if(responceOrder.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Заказ успешно создан");
            }
            else
            {
                MessageBox.Show(responceOrder.StatusCode.ToString());
            }
           
        }

        private void addDishCommand(object obj)
        {
            if((DishSelected == null)||(DishCount == null))
            {
                MessageBox.Show("Веберите блюдо");
                return;
            }
            
            AddDishes.Add(new DishListModel { Dish = DishSelected, DishCount = Convert.ToInt32(DishCount)});
            
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
