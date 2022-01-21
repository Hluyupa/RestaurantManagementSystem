﻿using RestarauntClient.Models;
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
    /// Логика взаимодействия для WaiterCreateOrderPage.xaml
    /// </summary>
    public partial class WaiterCreateOrderPage : Page
    {
        public WaiterCreateOrderPage(WaiterWindowVM _waiterWindowVM)
        {
            InitializeComponent();
            DataContext = new WaiterCreateOrderPageVM(_waiterWindowVM);
        }
    }
}
