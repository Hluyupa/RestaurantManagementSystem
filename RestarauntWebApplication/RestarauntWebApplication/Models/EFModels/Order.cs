using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Order
    {
        public Order()
        {
            DishCookOrders = new HashSet<DishCookOrder>();
        }

        public int OrderId { get; set; }
        public int? TableId { get; set; }
        public int? WaiterId { get; set; }
        public int? OrderNumber { get; set; }
        public string OrderStatus { get; set; }

        public virtual Table Table { get; set; }
        public virtual Waiter Waiter { get; set; }
        public virtual ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
