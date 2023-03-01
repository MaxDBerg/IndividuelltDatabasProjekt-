using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3._1Database.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string RoleName { get; set; }
    }
}
