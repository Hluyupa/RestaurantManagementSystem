using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class DishesIngridient
    {
        public int DishId { get; set; }
        public int IngridientId { get; set; }
        public int? IngridientCount { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Ingridient Ingridient { get; set; }
    }
}
