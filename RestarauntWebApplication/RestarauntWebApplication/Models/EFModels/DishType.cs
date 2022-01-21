using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class DishType
    {
        public DishType()
        {
            Dishes = new HashSet<Dish>();
        }

        public int DishTypeId { get; set; }
        public string DishTypeName { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
