using Newtonsoft.Json;
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
    public class AddRestarauntIngridientsPageVM : INotifyPropertyChanged
    {
        public RelayCommand AddNewIngridientCommand { get; set; }
        private string selectedIngridientMeasure;
        public string SelectedIngridientMeasure 
        {
            get 
            {
                return selectedIngridientMeasure;
            } 
            set 
            {
                selectedIngridientMeasure = value;
                OnPropertyChanged("SelectedIngridientMeasure");
            } 
        }

        public ObservableCollection<string> IngridientMeasure { get; set;}

        private string newIngridientName;
        public string NewIngridientName
        {
            get
            {
                return newIngridientName;
            }
            set
            {
                newIngridientName = value;
                OnPropertyChanged("NewIngridientName");
            }
        }

        private string newIngridientQuantity;
        public string NewIngridientQuantity
        {
            get
            {
                return newIngridientQuantity;
            }
            set
            {
                newIngridientQuantity = value;
                OnPropertyChanged("NewIngridientQuantity");
            }
        }

        public AddRestarauntIngridientsPageVM()
        {
            IngridientMeasure = new ObservableCollection<string>
            {
                "гр.",
                "шт.",
                "мл."
            };
            AddNewIngridientCommand = new RelayCommand(addNewIngridientCommand);
        }

        private void addNewIngridientCommand(object obj)
        {
            Ingridient ingridient = new Ingridient
            {
                IngridientName = NewIngridientName,
                IngridientQuantity = Convert.ToInt32(NewIngridientQuantity),
                IngridientMeasure = SelectedIngridientMeasure,
                IngridientUnits = 0
            };
            var requestAddNewIngridient = new RestRequest("api/Ingridients", Method.POST).AddJsonBody(ingridient);
            var responceAddNewIngridient = Client.Instance().httpClient.Execute(requestAddNewIngridient);
            if (responceAddNewIngridient.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Ингридент добавлен", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
