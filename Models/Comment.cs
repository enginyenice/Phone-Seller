﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonSatis.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PhoneId { get; set; }
        public int UserId { get; set; }
        public string UserComment { get; set; }
        public virtual User User { get; set; }
    }
}
