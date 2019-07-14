using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_Inventory.Models
{
    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }

        public string Descrive { get; set; }

        public bool isActive { get; set; }
    }
}