using System;
using System.Collections.Generic;
using System.Linq;
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
        public string DishStatus { get; set; }
        [JsonPropertyName("cook")]
        public Cook Cook { get; set; }
        [JsonPropertyName("dish")]
        public Dish Dish { get; set; }
        [JsonPropertyName("order")]
        public Order Order { get; set; }
    }
}
