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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class DishesInMenuPageVM : INotifyPropertyChanged
    {
        private AdministratorWindowVM AdministratorWindowVM;

        public RelayCommand UpdateDish { get; set; }
        public RelayCommand DeleteDish { get; set; }
        public RelayCommand AddNewDish { get; set; }

        private Dish selectedDishInMenu;
        public Dish SelectedDishInMenu
        {
            get 
            {
                return selectedDishInMenu; 
            }
            set 
            { 
                selectedDishInMenu = value;
                OnPropertyChanged("SelectedDishInMenu");
            }
        }


        private ObservableCollection<Dish> dishesInMenuList;
        public ObservableCollection<Dish> DishesInMenuList
        {
            get 
            {
                return dishesInMenuList; 
            }
            set 
            {
                dishesInMenuList = value;
                OnPropertyChanged();
            }
        }

        public DishesInMenuPageVM(AdministratorWindowVM _administratorWindowVM)
        {
            AdministratorWindowVM = _administratorWindowVM;
            SelectedDishInMenu = new Dish();
            UpdateDish = new RelayCommand(updateDish);
            DeleteDish = new RelayCommand(deleteDish);
            AddNewDish = new RelayCommand(addNewDish);
            GetDishesInMenu();
        }

        private void updateDish(object obj)
        {
            AdministratorWindowVM.CurrentPage = new UpdateDishInMenuPage(SelectedDishInMenu);
        }

        private void deleteDish(object obj)
        {
            var requestDeleteDish = new RestRequest($"api/Dishes/{SelectedDishInMenu.DishId}", Method.DELETE);
            var responceDeleteDish = Client.Instance().httpClient.Execute(requestDeleteDish);
            if (responceDeleteDish.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show("Блюдо успешно удалено из меню ресторана", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void addNewDish(object obj)
        {
            AdministratorWindowVM.CurrentPage = new AddDishInMenuPage();
        }

        private void GetDishesInMenu()
        {
            var requestDishesInMenu = new RestRequest("api/Dishes/DishesInMenu", Method.GET);
            var responceDishesInMenu = Client.Instance().httpClient.Execute(requestDishesInMenu);
            if(responceDishesInMenu.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DishesInMenuList = JsonSerializer.Deserialize<ObservableCollection<Dish>>(responceDishesInMenu.Content);
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
