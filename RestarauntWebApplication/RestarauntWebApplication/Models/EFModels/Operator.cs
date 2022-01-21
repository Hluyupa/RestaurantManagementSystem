using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Operator
    {
        public int OperatorId { get; set; }
        public int WorkerId { get; set; }
        public string OperatorPost { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
