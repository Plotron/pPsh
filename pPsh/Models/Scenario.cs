using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pPsh.Models
{
    public class Scenario
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int ProfileID { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<DeviceEvent> DeviceEvents { get; set; }
        public virtual ICollection<ScenarioEmail> ScenarioEmails { get; set; }
    }
}