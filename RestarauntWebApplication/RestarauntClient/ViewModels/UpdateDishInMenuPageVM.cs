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
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class UpdateDishInMenuPageVM : INotifyPropertyChanged
    {
        private Dish selectedDish;
        private string content;
        public Dish UpdateDish { get; set; }
        public RelayCommand UpdateDishCommand { get; set; }
        public RelayCommand UpdateIngridientForDishes { get; set; }

        private string updateDishName;
        public string UpdateDishName
        {
            get
            {
                return updateDishName;
            }
            set
            {
                updateDishName = value;
                OnPropertyChanged("UpdateDishName");
            }
        }

        private DishType updateDishType;
        public DishType UpdateDishType
        {
            get
            {
                return updateDishType;
            }
            set
            {
                updateDishType = value;
                OnPropertyChanged("UpdateDishType");
            }
        }

        private string updateDishCost;
        public string UpdateDishCost
        {
            get
            {
                return updateDishCost;
            }
            set
            {
                updateDishCost = value;
                OnPropertyChanged("UpdateDishCost");
            }
        }

        private string updateDishSeason;
        public string UpdateDishSeason
        {
            get
            {
                return updateDishSeason;
            }
            set
            {
                updateDishSeason = value;
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
        public UpdateDishInMenuPageVM(Dish selectedDish) 
        {
            this.selectedDish = selectedDish;

            UpdateIngridientForDishes = new RelayCommand(updateIngridientForDishes);
            UpdateDishCommand = new RelayCommand(updateDishCommand);

            TypesOfDishes = new ObservableCollection<DishType>();
            GetTypesOfDishes();

            var requestChoiceIngridient = new RestRequest("api/Ingridients", Method.GET);
            var responceChoiceIngridient = Client.Instance().httpClient.Execute(requestChoiceIngridient);
            content = responceChoiceIngridient.Content;

            UpdateDishName = selectedDish.DishName;
            UpdateDishType = TypesOfDishes.FirstOrDefault(p=>p.DishTypeId.Equals(selectedDish.DishType.DishTypeId));
            UpdateDishCost = selectedDish.DishCost.ToString();
            UpdateDishSeason = selectedDish.DishSeason;

            ChoiceIngridientsDishes = new ObservableCollection<ChoiceIngridientModel>();

            ObservableCollection<Ingridient> collection = new ObservableCollection<Ingridient>();
            collection = JsonConvert.DeserializeObject<ObservableCollection<Ingridient>>(content);

            foreach (var item in selectedDish.DishesIngridients)
            {
                ChoiceIngridientsDishes.Add(new ChoiceIngridientModel
                {
                    ChoiceIngridient = collection,
                    CountIngridient = item.IngridientCount.ToString(),
                    IngridientSelected = collection.Where(p => p.IngridientId.Equals(item.IngridientId)).FirstOrDefault()
                });
            }

        }

        private void updateDishCommand(object obj)
        {
            Dish updateDish = new Dish
            {
                DishId = selectedDish.DishId,
                DishName = UpdateDishName,
                DishType = UpdateDishType,
                DishCost = Convert.ToDecimal(UpdateDishCost),
                DishSeason = UpdateDishSeason,
            };
            foreach (var item in ChoiceIngridientsDishes)
            {
                updateDish.DishesIngridients.Add(new DishesIngridient { IngridientId = item.IngridientSelected.IngridientId, IngridientCount = Convert.ToInt32(item.CountIngridient) });
            }
            var requestUpdateDish = new RestRequest("api/Dishes/UpdateDishInMenu", Method.PUT).AddJsonBody(JsonConvert.SerializeObject(updateDish, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            var responseUpdateDish = Client.Instance().httpClient.Execute(requestUpdateDish);
            if(responseUpdateDish.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Блюдо успешно обновлено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void updateIngridientForDishes(object obj)
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
