using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmailId { get; set; }
        public string Empaddress { get; set; }
    }
}
