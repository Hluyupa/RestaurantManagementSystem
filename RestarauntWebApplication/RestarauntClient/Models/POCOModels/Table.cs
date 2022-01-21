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
        [JsonPropertyName("tableNumber")]
        public int? TableNumber { get; set; }
        [JsonPropertyName("tableCountSeat")]
        public int? TableCountSeat { get; set; }
        [JsonPropertyName("serviceZones")]
        public virtual ICollection<ServiceZone> ServiceZones { get; set; }
        [JsonPropertyName("visitorsTables")]
        public virtual ICollection<VisitorsTable> VisitorsTables { get; set; }
    }
}
