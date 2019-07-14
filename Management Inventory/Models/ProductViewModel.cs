using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_Inventory.Models
{
    public class ProductViewModel
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Describe { get; set; }
        public bool isActive { get; set; }
        public string UnitId { get; set; }
        public IEnumerable<CategoryViewModel> listCate { get;set;}
        public IEnumerable<UnitViewModel> listUnit { get;set;}
    }
}