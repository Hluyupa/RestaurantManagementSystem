using RestarauntClient.Models;
using RestarauntClient.View.Pages;
using RestarauntClient.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestarauntClient.ViewModels
{
    public class WaiterWindowVM : INotifyPropertyChanged
    {
        private WaiterWindow WaiterWindow;
        public string login;
        public WaiterWindowVM(string _login, WaiterWindow waiterWindow)
        {
            WaiterWindow = waiterWindow;
            login = _login;
            //waiterCreateNewOrderPage = new WaiterCreateNewOrderPage(this); //Страница для создания Заказа
            //waiterCheckOrderPage = new WaiterCheckOrderPage(this); // Страница для просмотра списка заказов

            /*ListViewMenuItems = new ObservableCollection<ListViewMenuItemModel> //Инициализация списка элементов меню
            {
                new ListViewMenuItemModel { NameMenuItem = "Создать заказ"},
                new ListViewMenuItemModel { NameMenuItem = "Список заказов"},
                new ListViewMenuItemModel { NameMenuItem = "Выход"}
            };*/
        }

        //public ObservableCollection<ListViewMenuItemModel> ListViewMenuItems { get; set; } //Список элементов меню



        private Page currentPage;
        public Page CurrentPage 
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private ListViewItem selectedMenuItem;
        public ListViewItem SelectedMenuItem
        {
            get 
            {
                return selectedMenuItem;
            }
            set
            {
                switch (value.Content.ToString())
                {
                    case "Создать заказ":
                        CurrentPage = new WaiterCreateOrderPage(this);
                        break;
                    case "Список заказов":
                        CurrentPage = new WaiterCheckOrderPage(this);
                        break;
                    case "Выход":
                        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                        authorizationWindow.Show();
                        WaiterWindow.Close();
                        CurrentPage = null;
                        break;
                }
                selectedMenuItem = value;
                OnPropertyChanged("SelectedMenuItem");
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
