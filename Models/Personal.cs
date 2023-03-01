using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3._1Database.Models
{
    public class Personal
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateEmployed { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
