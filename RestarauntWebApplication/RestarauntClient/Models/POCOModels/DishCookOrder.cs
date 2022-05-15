using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class DishCookOrder
    {
        
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("dishId")]
        public int DishId { get; set; }
        [JsonPropertyName("cookId")]
        public int? CookId { get; set; }
        [JsonPropertyName("dishCount")]
        public int? DishCount { get; set; }
        [JsonPropertyName("dishStatus")]
        private string dishStatus;
        public string DishStatus 
        { 
            get
            {
                return dishStatus;
            }
            set 
            {
                dishStatus = value;
                OnPropertyChanged("DishStatus");
            } 
        }
        [JsonPropertyName("cook")]
        public Cook Cook { get; set; }
        [JsonPropertyName("dish")]
        public Dish Dish { get; set; }
        [JsonPropertyName("order")]
        public Order Order { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
