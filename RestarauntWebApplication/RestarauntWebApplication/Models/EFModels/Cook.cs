using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Cook
    {
        public Cook()
        {
            DishCookOrders = new HashSet<DishCookOrder>();
        }

        public int CookId { get; set; }
        public int? WorkerId { get; set; }
        public string CookPost { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
