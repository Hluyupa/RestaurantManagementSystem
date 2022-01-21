using Newtonsoft.Json;
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

namespace RestarauntClient.ViewModels
{
    public class RestarauntIngridientsPageVM : INotifyPropertyChanged
    {
        private AdministratorWindowVM administratorWindowVM;
        public RelayCommand AddRestarauntIngridient { get; set; }

        private ObservableCollection<Ingridient> ingridientsList;
        public ObservableCollection<Ingridient> IngridientsList
        {
            get
            {
                return ingridientsList;
            }
            set
            {
                ingridientsList = value;
                OnPropertyChanged();
            }
        }

        public RestarauntIngridientsPageVM(AdministratorWindowVM _administratorWindowVM)
        {
            administratorWindowVM = _administratorWindowVM;
            AddRestarauntIngridient = new RelayCommand(addRestarauntIngridient);
            GetIngridientsList();
        }

        private void addRestarauntIngridient(object obj)
        {
            administratorWindowVM.CurrentPage = new AddRestarauntIngridientsPage();
        }

        private void GetIngridientsList()
        {
            var requestIngridientsList = new RestRequest("api/Ingridients", Method.GET);
            var responceIngridientsList = Client.Instance().httpClient.Execute(requestIngridientsList);
            IngridientsList = JsonConvert.DeserializeObject<ObservableCollection<Ingridient>>(responceIngridientsList.Content);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
