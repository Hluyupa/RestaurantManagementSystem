using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class DishCookOrder
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int? CookId { get; set; }
        public int DishCount { get; set; }
        public string DishStatus { get; set; }

        public virtual Cook Cook { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Order Order { get; set; }
    }
}
