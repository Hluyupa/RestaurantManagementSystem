using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Worker
    {
        public Worker()
        {
            Administrators = new HashSet<Administrator>();
            Cooks = new HashSet<Cook>();
            Operators = new HashSet<Operator>();
            Waiters = new HashSet<Waiter>();
        }

        public int WorkerId { get; set; }
        public string WorkerFullName { get; set; }
        public string WorkerLogin { get; set; }
        public string WorkerPassword { get; set; }
        public string WorkerPosition { get; set; }

        public virtual ICollection<Administrator> Administrators { get; set; }
        public virtual ICollection<Cook> Cooks { get; set; }
        public virtual ICollection<Operator> Operators { get; set; }
        public virtual ICollection<Waiter> Waiters { get; set; }
    }
}
