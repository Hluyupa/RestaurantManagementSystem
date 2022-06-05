using RestarauntClient.Models.POCOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntClient.Models
{
    public class DishListModel : INotifyPropertyChanged
    {
        public Dish Dish { get; set; }


        private int? dishCount;
        public int? DishCount
        {
            get 
            { 
                return dishCount;
            }
            set
            {
                dishCount = value;
                OnPropertyChanged();
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
