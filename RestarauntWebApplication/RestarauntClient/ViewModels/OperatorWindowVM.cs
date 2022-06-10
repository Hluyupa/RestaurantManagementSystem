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
    public class OperatorWindowVM : INotifyPropertyChanged
    {
        private OperatorWindow OperatorWindow;
        
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
                OnPropertyChanged();
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
                    case "Список гостей":
                        CurrentPage = new OperatorVisitorsPage();
                        break;
                    case "Выход":
                        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                        authorizationWindow.Show();
                        OperatorWindow.Close();
                        break;
                    default:
                        break;
                }
                selectedMenuItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ListViewItem> menuItems;
        public ObservableCollection<ListViewItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value;}
        }

        
        public OperatorWindowVM(OperatorWindow _operatorWindow)
        {
            OperatorWindow = _operatorWindow;
            Style styleMenuItemListView = Application.Current.FindResource("MenuItemListView") as Style;
            MenuItems = new ObservableCollection<ListViewItem>
            {
                new ListViewItem{ Content="Список гостей", Style = styleMenuItemListView },
                new ListViewItem{ Content="Выход", Style = styleMenuItemListView }
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
