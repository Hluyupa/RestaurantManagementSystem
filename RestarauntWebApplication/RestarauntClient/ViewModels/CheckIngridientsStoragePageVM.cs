using RestarauntClient.Models.POCOModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class CheckIngridientsStoragePageVM : INotifyPropertyChanged
    {
        string Path;
        public RelayCommand FormationINgridientFile { get; set; }
        public RelayCommand AddIngridientsInDeliveryOrder { get; set; }

        private string countIngridients;

        public string CountIngridients
        {
            get 
            { 
                return countIngridients; 
            }
            set 
            {
                countIngridients = value;
                OnPropertyChanged("CountIngridients");
            }
        }


        private ObservableCollection<Ingridient> ingridientsInDeliveryOrderList;
        public ObservableCollection<Ingridient> IngridientsInDeliveryOrderList
        {
            get 
            { 
                return ingridientsInDeliveryOrderList; 
            }
            set 
            { 
                ingridientsInDeliveryOrderList = value;
                OnPropertyChanged();
            }
        }

        private IngridientStatusCount selectedIngridientStorage;
        public IngridientStatusCount SelectedIngridientStorage
        {
            get 
            {
                return selectedIngridientStorage; 
            }
            set 
            {
                selectedIngridientStorage = value;
                OnPropertyChanged("SelectedIngridientStorage");
            }
        }

        private ObservableCollection<IngridientStatusCount> ingridientsStorage;
        public ObservableCollection<IngridientStatusCount> IngridientsStorage
        {
            get 
            {
                return ingridientsStorage; 
            }
            set 
            { 
                ingridientsStorage = value;
                OnPropertyChanged();
            }
        }

        public CheckIngridientsStoragePageVM()
        {
            IngridientsStorage = new ObservableCollection<IngridientStatusCount>();
            IngridientsInDeliveryOrderList = new ObservableCollection<Ingridient>();
            FormationINgridientFile = new RelayCommand(formationINgridientFile);
            AddIngridientsInDeliveryOrder = new RelayCommand(addIngridientsInDeliveryOrder);
            GetIngridientsStorage();
        }

        private void formationINgridientFile(object obj)
        {
            string text = string.Empty;
            Path = @"C:\Restaraunt";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            using (FileStream fileStream = new FileStream($"{Path}\\Ingridients.txt", FileMode.OpenOrCreate))
            {
                foreach (var item in IngridientsInDeliveryOrderList)
                {
                    text += $"{item.IngridientName} {item.IngridientUnits*item.IngridientQuantity}{item.IngridientMeasure}\n";
                }
                byte[] array = Encoding.Default.GetBytes(text);
                fileStream.Write(array, 0, array.Length);
            }
        }

        private void addIngridientsInDeliveryOrder(object obj)
        {
            var itemSelected = SelectedIngridientStorage;
            if(Convert.ToInt32(CountIngridients) <= 0 || CountIngridients.Length == 0)
            {
                MessageBox.Show("Введите количество, которое небходимо закзать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                //itemSelected.Ingridient.IngridientUnits = 1;
            }
            else
            {
                itemSelected.Ingridient.IngridientUnits = 1 * Convert.ToInt32(CountIngridients);
            }

            /*foreach (var item in IngridientsInDeliveryOrderList)
            {
                if(item.IngridientId == itemSelected.Ingridient.IngridientId)
                {
                    return;
                }
            }*/
            var item = IngridientsInDeliveryOrderList.FirstOrDefault(p => p.IngridientId.Equals(itemSelected.Ingridient.IngridientId));
            if(item != null)
            {
                IngridientsInDeliveryOrderList.Remove(item);
                item.IngridientUnits += itemSelected.Ingridient.IngridientUnits;
                IngridientsInDeliveryOrderList.Add(new Ingridient { IngridientId = item.IngridientId, IngridientName = item.IngridientName, IngridientUnits = item.IngridientUnits, IngridientQuantity = item.IngridientQuantity, IngridientMeasure = item.IngridientMeasure, DishesIngridients = item.DishesIngridients});
            }
            else
            {
                IngridientsInDeliveryOrderList.Add(itemSelected.Ingridient);
            }
           
        }

        private void GetIngridientsStorage()
        {
            string status = string.Empty;
            var requestIngridientsStorage = new RestRequest("api/Ingridients", Method.GET);
            var responceIngridientsStorage = Client.Instance().httpClient.Execute(requestIngridientsStorage);
            var ingridients = JsonSerializer.Deserialize<ObservableCollection<Ingridient>>(responceIngridientsStorage.Content);
            foreach (var item in ingridients)
            {
                if(item.IngridientUnits == 0 || item.IngridientUnits == null)
                {
                    status = "Отсутствует";
                }
                else if (item.IngridientUnits >= 0 && item.IngridientUnits <= 5)
                {
                    status = "Кончается";
                }
                else if (item.IngridientUnits > 5  )
                {
                    status = "В избытке";
                }
                IngridientsStorage.Add(new IngridientStatusCount { Ingridient = item, StatusOfCount = status });
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
