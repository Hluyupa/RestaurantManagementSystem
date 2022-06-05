using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Table
    {
        [JsonPropertyName("tableId")]
        public int TableId { get; set; }
        [JsonPropertyName("tableDescription")]
        public string TableDescription { get; set; }
        [JsonPropertyName("tableCountSeat")]
        public int? TableCountSeat { get; set; }
        [JsonPropertyName("tableMapPosition")]
        public string TableMapPosition { get; set; }
        [JsonPropertyName("tableFloor")]
        public int? TableFloor { get; set; }
        [JsonPropertyName("orders")]
        public virtual ICollection<Order> Orders { get; set; }
        [JsonPropertyName("visitorsTables")]
        public virtual ICollection<VisitorsTable> VisitorsTables { get; set; }
    }
}
