using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_Inventory.Models
{
    public class InventoryDetailViewModel
    {
        public string InventoryDetailId { get; set; }
        public string InventoryId { get; set; }
        public string ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public bool isActive { get; set; }
        public IEnumerable<InventoryViewModel> listInventory { get; set; }
        public IEnumerable<ProductViewModel> listProduct { get; set; }
    }
}