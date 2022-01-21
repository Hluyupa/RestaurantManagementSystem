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
using System.Windows;
using System.Windows.Controls;


namespace RestarauntClient.ViewModels
{
    public class CookWindowVM : INotifyPropertyChanged
    {
        public string login;
        private CookWindow CookWindow;
        public ObservableCollection<ListViewItem> MenuItems { get; set; }
        public CookWindowVM(string _login, CookWindow _cookWindow)
        {
            CookWindow = _cookWindow;
            login = _login;
            Style styleMenuItemListView = Application.Current.FindResource("MenuItemListView") as Style;
            MenuItems = new ObservableCollection<ListViewItem>
            {
                new ListViewItem{ Content = "Новые заказы", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Моя кухня", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Выход", Style = styleMenuItemListView}
            };

        }

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
                    case "Новые заказы":
                        CurrentPage = new CookCheckNewDishesForCookingPage(this);
                        break;
                    case "Моя кухня":
                        CurrentPage = new DishCookingPage(this);
                        break;
                    case "Выход":
                        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                        authorizationWindow.Show();
                        CookWindow.Close();
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
