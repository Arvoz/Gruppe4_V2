using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Core.Domain
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string Type { get; set; }
        public bool Status { get; set; }

        public Device()
        {
        }

        public Device(string name, string type, bool status)
        {
            DeviceName = name;
            Type = type;
            Status = status;
        }

        public void UpdateStatus(bool status) 
        {
            Status = status;
        }

    }
}