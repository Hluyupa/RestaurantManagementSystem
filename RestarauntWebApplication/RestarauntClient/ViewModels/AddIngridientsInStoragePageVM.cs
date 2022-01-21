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
    public class AddIngridientsInStoragePageVM : INotifyPropertyChanged
    {
        public RelayCommand SaveIngridientsStorage { get; set; }

        private ObservableCollection<AddIngridientsStorageModel> storageIngridientsList;
        public ObservableCollection<AddIngridientsStorageModel> StorageIngridientsList
        {
            get { return storageIngridientsList; }
            set { storageIngridientsList = value; OnPropertyChanged(); }
        }

        public AddIngridientsInStoragePageVM()
        {
            StorageIngridientsList = new ObservableCollection<AddIngridientsStorageModel>();
            SaveIngridientsStorage = new RelayCommand(saveIngridientsStorage);
            GetStorageIngridients();
        }

        private void saveIngridientsStorage(object obj)
        {
            
            ObservableCollection<Ingridient> ingridients = new ObservableCollection<Ingridient>();
            foreach (var item in StorageIngridientsList)
            {
                if (item.IngridientQuantity != 0)
                {
                    item.Ingridient.IngridientUnits = item.IngridientQuantity / item.Ingridient.IngridientQuantity;
                    ingridients.Add(item.Ingridient);
                }
            }
            var requestSaveIngridientsStorage = new RestRequest("api/Ingridients/SaveIngridientsStorage", Method.PUT).AddJsonBody(JsonConvert.SerializeObject(ingridients, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            var responceSaveIngridientsStorage = Client.Instance().httpClient.Execute(requestSaveIngridientsStorage);
            if (responceSaveIngridientsStorage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Продукты успешно внесены в склад");
            }
            
        }

        private void GetStorageIngridients()
        {
            var requestStorageIngridients = new RestRequest("api/Ingridients", Method.GET);
            var responceStorageIngridients = Client.Instance().httpClient.Execute(requestStorageIngridients);
            var list = JsonConvert.DeserializeObject<ObservableCollection<Ingridient>>(responceStorageIngridients.Content);
            foreach (var item in list)
            {
                StorageIngridientsList.Add(new AddIngridientsStorageModel { Ingridient = item, IngridientQuantity = 0 });
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
