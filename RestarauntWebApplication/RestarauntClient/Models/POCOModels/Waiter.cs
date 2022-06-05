using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class Waiter
    {
        [JsonPropertyName("waiterId")]
        public int WaiterId { get; set; }
        [JsonPropertyName("workerId")]
        public int? WorkerId { get; set; }
        [JsonPropertyName("worker")]
        public virtual Worker Worker { get; set; }
        [JsonPropertyName("orders")]
        public virtual ICollection<Order> Orders { get; set; }
       
    }
}
