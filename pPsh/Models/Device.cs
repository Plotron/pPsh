using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace pPsh.Models
{
    public class Device
    {
        public int ID { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]  
        [StringLength(32)]
        public string Key { get; set; }
        public bool Enabled { get; set; }
        [Display(Name = "Image")]
        public string ImageName { get; set; }
        public int ProfileID { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<DeviceEvent> AssociatedEvents { get; set; }
    }
}