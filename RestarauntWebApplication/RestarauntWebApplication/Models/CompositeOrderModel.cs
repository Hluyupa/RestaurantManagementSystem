using RestarauntWebApplication.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntWebApplication.Models
{
    public class CompositeOrderModel
    {
        public int OrderId { get; set; }
        public int? VisitorId { get; set; }
        public string WaiterLogin { get; set; }
        public int OrderNumber { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<DishCookOrder> DishCookOrders { get; set; }
    }
}
