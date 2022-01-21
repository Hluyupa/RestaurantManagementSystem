using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    
    public class Worker
    {
        public Worker()
        {
            Cooks = new List<Cook>();
            Operators = new List<Operator>();
            Waiters = new List<Waiter>();
            Administrators = new List<Administrator>();
        }

        [JsonPropertyName("workerId")]
        public int WorkerId { get; set; }
        [JsonPropertyName("workerFullName")]
        public string WorkerFullName { get; set; }
        [JsonPropertyName("workerLogin")]
        public string WorkerLogin { get; set; }
        [JsonPropertyName("workerPassword")]
        public string WorkerPassword { get; set; }
        [JsonPropertyName("workerPosition")]
        public string WorkerPosition { get; set; }
        [JsonPropertyName("cooks")]
        public virtual ICollection<Cook> Cooks { get; set; }
        [JsonPropertyName("operators")]
        public virtual ICollection<Operator> Operators { get; set; }
        [JsonPropertyName("waiters")]
        public virtual ICollection<Waiter> Waiters { get; set; }
        [JsonPropertyName("administrators")]
        public virtual ICollection<Administrator> Administrators { get; set; }
    }
}
