using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class ServiceZone
    {
        [JsonPropertyName("waiterId")]
        public int WaiterId { get; set; }
        [JsonPropertyName("tableId")]
        public int TableId { get; set; }
        [JsonPropertyName("table")]
        public virtual Table Table { get; set; }
        [JsonPropertyName("waiter")]
        public virtual Waiter Waiter { get; set; }
    }
}
