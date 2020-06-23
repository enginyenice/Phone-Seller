using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonSatis.Models
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public string SSS { get; set; }
        public string SliderImages { get; set; }

    }
}
