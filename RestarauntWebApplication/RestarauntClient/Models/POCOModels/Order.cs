using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Order
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("tableId")]
        public int? TableId { get; set; }
        [JsonPropertyName("waiterId")]
        public int? WaiterId { get; set; }
        [JsonPropertyName("orderNumber")]
        public int? OrderNumber { get; set; }
        [JsonPropertyName("orderStatus")]
        public string OrderStatus { get; set; }
        [JsonPropertyName("table")]
        public Table Table { get; set; }
        [JsonPropertyName("waiter")]
        public Waiter Waiter { get; set; }
        [JsonPropertyName("dishCookOrders")]
        public ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
