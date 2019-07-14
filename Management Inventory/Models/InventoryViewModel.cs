using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_Inventory.Models
{
    public class InventoryViewModel
    {
        public string InventoryId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Describe { get; set; }
        public bool isActive { get; set; }
    }

}