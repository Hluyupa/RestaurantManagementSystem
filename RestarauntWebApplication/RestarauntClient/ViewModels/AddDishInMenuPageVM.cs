using Newtonsoft.Json;
using RestarauntClient.Models;
using RestarauntClient.Models.POCOModels;
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
using System.Windows.Controls;

namespace RestarauntClient.ViewModels
{
    public class AddDishInMenuPageVM : INotifyPropertyChanged
    {
        private string content;
        public Dish NewDish { get; set; }
        public RelayCommand CreateDishCommand { get; set; }
        public RelayCommand AddIngridientForDishes { get; set; }

        private string newDishName;
        public string NewDishName
        {
            get
            {
                return newDishName;
            }
            set
            {
                newDishName = value;
                OnPropertyChanged("NewDishName");
            }
        }

        private DishType newDishType;
        public DishType NewDishType
        {
            get
            {
                return newDishType;
            }
            set
            {
                newDishType = value;
                OnPropertyChanged("NewDishType");
            }
        }

        private string newDishCost;
        public string NewDishCost
        {
            get
            {
                return newDishCost;
            }
            set
            {
                newDishCost = value;
                OnPropertyChanged("NewDishCost");
            }
        }

        private string newDishSeason;
        public string NewDishSeason
        {
            get
            {
                return newDishSeason;
            }
            set
            {
                newDishSeason = value;
                OnPropertyChanged("NewDishSeason");
            }
        }

        private ObservableCollection<DishType> typesOfDishes;
        public ObservableCollection<DishType> TypesOfDishes
        {
            get
            {
                return typesOfDishes;
            }
            set
            {
                typesOfDishes = value;
                OnPropertyChanged("TypesOfDishes");
            }
        }


        private ObservableCollection<ChoiceIngridientModel> choiceIngridientsDishes;
        public ObservableCollection<ChoiceIngridientModel> ChoiceIngridientsDishes
        {
            get
            {
                return choiceIngridientsDishes;
            }
            set
            {
                choiceIngridientsDishes = value;
                OnPropertyChanged();
            }
        }

        public AddDishInMenuPageVM()
        {
            CreateDishCommand = new RelayCommand(createDishCommand);
            AddIngridientForDishes = new RelayCommand(addIngridientForDishes);

            ChoiceIngridientsDishes = new ObservableCollection<ChoiceIngridientModel>();
            GetTypesOfDishes();

            var requestChoiceIngridient = new RestRequest("api/Ingridients", Method.GET);
            var responceChoiceIngridient = Client.Instance().httpClient.Execute(requestChoiceIngridient);
            content = responceChoiceIngridient.Content;
        }

        private  void createDishCommand(object obj)
        {
            NewDish = new Dish
            {
                DishName = NewDishName,
                DishCost = Convert.ToInt32(NewDishCost),
                DishSeason = NewDishSeason,
                DishTypeId = NewDishType.DishTypeId,
            };
            foreach (var item in ChoiceIngridientsDishes)
            {
                NewDish.DishesIngridients.Add(new DishesIngridient { IngridientId = item.IngridientSelected.IngridientId, IngridientCount = Convert.ToInt32(item.CountIngridient) });

            }
            var requestCreateDish = new RestRequest("api/Dishes", Method.POST).AddJsonBody(JsonConvert.SerializeObject(NewDish, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            var responseCreateDish = Client.Instance().httpClient.Execute(requestCreateDish);
            if (responseCreateDish.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Новое блюдо успешно добавлено в меню", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void addIngridientForDishes(object obj)
        {
            ChoiceIngridientsDishes.Add(new ChoiceIngridientModel { ChoiceIngridient = JsonConvert.DeserializeObject<ObservableCollection<Ingridient>>(content), CountIngridient = string.Empty });
        }

        private void GetTypesOfDishes()
        {
            var requestTypesOfDishes = new RestRequest("api/DishTypes", Method.GET);
            var responceTypesOfDishes = Client.Instance().httpClient.Execute(requestTypesOfDishes);
            TypesOfDishes = JsonConvert.DeserializeObject<ObservableCollection<DishType>>(responceTypesOfDishes.Content);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
