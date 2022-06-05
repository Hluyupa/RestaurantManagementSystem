using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Waiter
    {
        public Waiter()
        {
            Orders = new HashSet<Order>();
        }

        public int WaiterId { get; set; }
        public int? WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
