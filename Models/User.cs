using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonSatis.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Permission { get; set; }
    }
}
