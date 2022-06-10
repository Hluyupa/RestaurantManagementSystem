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
    public class AdministratorWindowVM : INotifyPropertyChanged
    {
        private AdministratorWindow AdministratorWindow;
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
                    case "Заказ продуктов":
                        CurrentPage = new CheckIngridientsStoragePage();
                        break;
                    
                    case "Склад":
                        CurrentPage = new AddIngridientsInStoragePage();
                        break;
                    case "Меню":
                        CurrentPage = new DishesInMenuPage(this);
                        break;
                    case "Ингредиенты":
                        CurrentPage = new RestarauntIngridientsPage(this);
                        break;
                    case "Сотрудники":
                        CurrentPage = new RestarauntWorkersPage(this); 
                        break;

                    case "Выход":
                        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                        authorizationWindow.Show();
                        AdministratorWindow.Close();
                        break;
                }

                selectedMenuItem = value;
                OnPropertyChanged("SelectedMenuItem");
            }
        }
        public ObservableCollection<ListViewItem> MenuItems { get; set; }
        public AdministratorWindowVM(AdministratorWindow _administratorWindow)
        {
            AdministratorWindow = _administratorWindow;
            Style styleMenuItemListView = Application.Current.FindResource("MenuItemListView") as Style;
            MenuItems = new ObservableCollection<ListViewItem>
            {
                new ListViewItem{ Content = "Заказ продуктов", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Склад", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Меню", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Ингредиенты", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Сотрудники", Style = styleMenuItemListView},
                new ListViewItem{ Content = "Выход", Style = styleMenuItemListView}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
