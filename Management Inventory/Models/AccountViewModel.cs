using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Management_Inventory.Models
{
    public class AccountViewModel
    {
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public string Address { get; set; }
        public string isActive { get; set; }
    }
}