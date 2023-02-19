using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3._1Database.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Namn { get; set; }
        public string Efternamn { get; set; }
        public string Personnummer { get; set; }
        public int KlassID { get; set; }
        public Klass Klass { get; set; }
    }
}
