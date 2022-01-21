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
    /// Логика взаимодействия для CheckIngridientsStoragePage.xaml
    /// </summary>
    public partial class CheckIngridientsStoragePage : Page
    {
        public CheckIngridientsStoragePage()
        {
            InitializeComponent();
            DataContext = new CheckIngridientsStoragePageVM();
        }
    }
}
