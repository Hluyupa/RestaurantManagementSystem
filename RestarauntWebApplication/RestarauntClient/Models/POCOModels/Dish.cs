using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Dish
    {
        public Dish()
        {
            DishesIngridients = new List<DishesIngridient>();
        }
        [JsonPropertyName("dishId")]
        public int DishId { get; set; }
        [JsonPropertyName("dishName")]
        public string DishName { get; set; }
        [JsonPropertyName("dishCost")]
        public decimal? DishCost { get; set; }
        [JsonPropertyName("dishSeason")]
        public string DishSeason { get; set; }
        [JsonPropertyName("dishTypeId")]
        public int? DishTypeId { get; set; }
        [JsonPropertyName("dishType")]
        public DishType DishType { get; set; }
        [JsonPropertyName("dishesIngridients")]
        public ICollection<DishesIngridient> DishesIngridients { get; set; }
        [JsonPropertyName("dishCookOrders")]
        public ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
