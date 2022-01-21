using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class ServiceZone
    {
        public int WaiterId { get; set; }
        public int TableId { get; set; }

        public virtual Table Table { get; set; }
        public virtual Waiter Waiter { get; set; }
    }
}
