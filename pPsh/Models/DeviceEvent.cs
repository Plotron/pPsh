using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace pPsh.Models
{
    public class DeviceEvent
    {
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public virtual Device Device { get; set; }
        public int ScenarioID { get; set; }
        public virtual Scenario Scenario { get; set; }
        public readonly DateTime Timestamp;
        internal string _Parameters { get; set; }
        [NotMapped]
        public Dictionary<string, string> Parameters
        {
            get
            { return _Parameters == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(_Parameters); }

            set { _Parameters = JsonConvert.SerializeObject(value); }
        }
    }
}