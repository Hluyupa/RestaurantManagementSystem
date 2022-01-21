
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
    public class RestarauntWorkersPageVM : INotifyPropertyChanged
    {
        private AdministratorWindowVM administratorWindowVM;
        public RelayCommand AddWorkerCommand { get; set; }

        private ObservableCollection<Worker> workersList;
        public ObservableCollection<Worker> WorkersList
        {
            get
            { 
                return workersList; 
            }
            set 
            { 
                workersList = value;
                OnPropertyChanged();
            }
        }

        public RestarauntWorkersPageVM(AdministratorWindowVM _administratorWindowVM)
        {
            administratorWindowVM = _administratorWindowVM;
            AddWorkerCommand = new RelayCommand(addWorkerCommand);
            GetWorkersList();
        }

        private void addWorkerCommand(object obj)
        {
            administratorWindowVM.CurrentPage = new AddRestarauntWorkersPage();
        }

        private void GetWorkersList()
        {
            var requestWorkersList = new RestRequest("api/Workers", Method.GET);
            var responceWorkersList = Client.Instance().httpClient.Execute(requestWorkersList);
            WorkersList = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(responceWorkersList.Content);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
