using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Ingridient
    {
        public Ingridient()
        {
            DishesIngridients = new HashSet<DishesIngridient>();
        }

        public int IngridientId { get; set; }
        public string IngridientName { get; set; }
        public double? IngridientUnits { get; set; }
        public int? IngridientQuantity { get; set; }
        public string IngridientMeasure { get; set; }

        public virtual ICollection<DishesIngridient> DishesIngridients { get; set; }
    }
}
