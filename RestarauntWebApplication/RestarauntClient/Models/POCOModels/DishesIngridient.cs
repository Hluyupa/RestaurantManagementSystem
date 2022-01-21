using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class DishesIngridient
    {
        [JsonPropertyName("dishId")]
        public int DishId { get; set; }
        [JsonPropertyName("ingridientId")]
        public int IngridientId { get; set; }
        [JsonPropertyName("ingridientCount")]
        public int? IngridientCount { get; set; }
        [JsonPropertyName("dish")]
        public virtual Dish Dish { get; set; }
        [JsonPropertyName("ingridient")]
        public virtual Ingridient Ingridient { get; set; }
    }
}
