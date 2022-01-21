using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class VisitorsTable
    {
        [JsonPropertyName("visitorId")]
        public int VisitorId { get; set; }
        [JsonPropertyName("tableId")]
        public int TableId { get; set; }
        [JsonPropertyName("dateBooking")]
        public DateTime? DateBooking { get; set; }
        [JsonPropertyName("table")]
        public virtual Table Table { get; set; }
        [JsonPropertyName("visitor")]
        public virtual Visitor Visitor { get; set; }
    }
}
