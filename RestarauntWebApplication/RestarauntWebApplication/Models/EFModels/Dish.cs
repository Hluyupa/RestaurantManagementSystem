using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Dish
    {
        public Dish()
        {
            DishCookOrders = new HashSet<DishCookOrder>();
            DishesIngridients = new HashSet<DishesIngridient>();
        }

        public int DishId { get; set; }
        public string DishName { get; set; }
        public decimal? DishCost { get; set; }
        public string DishSeason { get; set; }
        public int? DishTypeId { get; set; }

        public virtual DishType DishType { get; set; }
        public virtual ICollection<DishCookOrder> DishCookOrders { get; set; }
        public virtual ICollection<DishesIngridient> DishesIngridients { get; set; }
    }
}
