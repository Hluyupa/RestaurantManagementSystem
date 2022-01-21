using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Ingridient
    {
        [JsonPropertyName("ingridientId")]
        public int IngridientId { get; set; }
        [JsonPropertyName("ingridientName")]
        public string IngridientName { get; set; }
        [JsonPropertyName("ingridientUnits")]
        public double? IngridientUnits { get; set; }
        [JsonPropertyName("ingridientQuantity")]
        public int? IngridientQuantity { get; set; }
        [JsonPropertyName("ingridientMeasure")]
        public string IngridientMeasure { get; set; }
        [JsonPropertyName("dishesIngridients")]
        public ICollection<DishesIngridient> DishesIngridients { get; set; }
    }
}
