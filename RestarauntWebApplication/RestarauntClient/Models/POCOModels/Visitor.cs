using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Visitor
    {
        [JsonPropertyName("visitorId")]
        public int VisitorId { get; set; }
        [JsonPropertyName("visitorFullName")]
        public string VisitorFullName { get; set; }
        [JsonPropertyName("visitorTelephone")]
        public string VisitorTelephone { get; set; }
        [JsonPropertyName("visitorEmail")]
        public string VisitorEmail { get; set; }
        [JsonPropertyName("orders")]
        public virtual ICollection<Order> Orders { get; set; }
        [JsonPropertyName("visitorsTables")]
        public virtual ICollection<VisitorsTable> VisitorsTables { get; set; }
    }
}
