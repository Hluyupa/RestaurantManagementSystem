using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Cook
    {
        [JsonPropertyName("cookId")]
        public int CookId { get; set; }
        [JsonPropertyName("workerId")]
        public int? WorkerId { get; set; }
        [JsonPropertyName("cookPost")]
        public string CookPost { get; set; }
        [JsonPropertyName("worker")]
        public Worker Worker { get; set; }
        [JsonPropertyName("dishCookOrders")]
        public ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
