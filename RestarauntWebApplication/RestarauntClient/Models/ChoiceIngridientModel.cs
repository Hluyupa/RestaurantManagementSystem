using RestarauntClient.Models.POCOModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntClient.Models
{
    public class ChoiceIngridientModel:INotifyPropertyChanged
    {
        private Ingridient ingridientSelected;
        public Ingridient IngridientSelected
        {
            get 
            { 
                return ingridientSelected; 
            }
            set 
            { 
                ingridientSelected = value;
                OnPropertyChanged("IngridientSelected");
            }
        }


        private ObservableCollection<Ingridient> choiceIngridient;
        public ObservableCollection<Ingridient> ChoiceIngridient 
        {
            get 
            {
                return choiceIngridient;
            } 
            set 
            {
                choiceIngridient = value;
                OnPropertyChanged("ChoiceIngridient");
            } 
        }
        private string countIngridient;
        public string CountIngridient 
        {
            get 
            {
                return countIngridient;
            }
            set 
            {
                countIngridient = value;
                OnPropertyChanged("CountIngridient");
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
