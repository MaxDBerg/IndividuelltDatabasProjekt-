using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3._1Database.Models
{
    public class Betyg
    {
        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int PersonalID { get; set; }
        public Personal Personal { get; set; }
        public int KursID { get; set; }
        public Kurs Kurs { get; set; }
        public DateTime Date { get; set; }
        public int Betygbetyg { get; set; }
    }
}
