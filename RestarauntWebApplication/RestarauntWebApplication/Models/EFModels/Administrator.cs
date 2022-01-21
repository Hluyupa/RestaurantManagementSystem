using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Administrator
    {
        public int AdministratorId { get; set; }
        public int WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
