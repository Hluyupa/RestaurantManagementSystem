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
    public class AddRestarauntWorkersPageVM : INotifyPropertyChanged
    {

        public RelayCommand AddWorkerCommand { get; set; }

        private string selectedWorkerType;

        public string SelectedWorkerType
        {
            get 
            {
                return selectedWorkerType; 
            }
            set 
            {
                selectedWorkerType = value;
                OnPropertyChanged("SelectedWorkerType");
            }
        }


        private ObservableCollection<string> workerTypes;
        public ObservableCollection<string> WorkerTypes
        {
            get 
            {
                return workerTypes;
            }
            set
            {
                workerTypes = value;
                OnPropertyChanged();
            }
        }


        private string fioWorker;
        public string FIOWorker
        {
            get 
            {
                return fioWorker; 
            }
            set 
            { 
                fioWorker = value;
                OnPropertyChanged("FIOWorker");
            }
        }

        private string loginWorker;
        public string LoginWorker
        {
            get
            {
                return loginWorker;
            }
            set
            {
                loginWorker = value;
                OnPropertyChanged("LoginWorker");
            }
        }

        private string passwordWorker;
        public string PasswordWorker
        {
            get
            {
                return passwordWorker;
            }
            set
            {
                passwordWorker = value;
                OnPropertyChanged("PasswordWorker");
            }
        }

        public AddRestarauntWorkersPageVM()
        {
            AddWorkerCommand = new RelayCommand(addWorkerCommand);
            WorkerTypes = new ObservableCollection<string> { "Официант", "Оператор", "Повар", "Администратор" };
        }

        private void addWorkerCommand(object obj)
        {
            Worker worker = new Worker 
            {
                WorkerFullName = FIOWorker,
                WorkerLogin = LoginWorker,
                WorkerPassword = PasswordWorker,
                WorkerPosition = SelectedWorkerType 
            };
            switch (SelectedWorkerType)
            {
                case "Официант":
                    worker.Waiters.Add(new Waiter());
                    break;
                case "Оператор":
                    worker.Operators.Add(new Operator());
                    break;
                case "Повар":
                    worker.Cooks.Add(new Cook());
                    break;
                case "Администратор":
                    worker.Administrators.Add(new Administrator());
                    break;
            }
            var requestAddWorker = new RestRequest("api/Workers", Method.POST).AddJsonBody(JsonConvert.SerializeObject(worker, new JsonSerializerSettings 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            var responceAddWorker = Client.Instance().httpClient.Execute(requestAddWorker);
            if (responceAddWorker.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Успешно добавлен новый аккаунт сотрудника", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
