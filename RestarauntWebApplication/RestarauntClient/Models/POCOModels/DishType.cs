using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestarauntClient.Models.POCOModels
{
    public class DishType
    {
        [JsonPropertyName("dishTypeId")]
        public int DishTypeId { get; set; }
        [JsonPropertyName("dishTypeName")]
        public string DishTypeName { get; set; }
        [JsonPropertyName("dishes")]
        public virtual ICollection<Dish> Dishes { get; set; }

    }
}
