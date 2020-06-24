using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonSatis.Models
{
    public class Phone
    {
        [Key]
        public int PhoneId { get; set; }
        
        public string Name { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public string MinDesc { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public int TotalScore { get; set; }
        public int TotalPeople { get; set; }
        public int Score { get; set; }
        
        public int BrandId { get; set; }
        public virtual Brand brand { get; set; }
        public virtual List<Comment> comments { get; set; }
    }
}
