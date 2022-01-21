using RestarauntClient.View.Windows;
using RestarauntClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestarauntClient.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для DishCookingPage.xaml
    /// </summary>
    public partial class DishCookingPage : Page
    {
        public DishCookingPage(CookWindowVM _cookWindow)
        {
            InitializeComponent();
            DataContext = new DishCookingPageVM(_cookWindow);
        }
    }
}
