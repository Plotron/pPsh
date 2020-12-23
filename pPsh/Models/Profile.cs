using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pPsh.Models
{
    public class Profile
    {
        public Profile(string userName)
        {
            UserName = userName;
        }

        public Profile()
        {
        }

        public int ID { get; set; }
        public string UserName { get; set; }
        public ICollection<Scenario> Scenarios { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}