using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

using AeroORMFramework;

namespace SpringHackApi.Models
{
    public class Office
    {
        [PrimaryKey]
        [AutoincrementID]
        [CanBeNull(false)]
        public int ID { get; set; }
        [CanBeNull(false)]
        public int CompanyID { get; set; }
        [CanBeNull(false)]
        public string Name { get; set; }
        [CanBeNull(false)]
        public string Address { get; set; }
        [CanBeNull(false)]
        public string Country { get; set; }
        [Json]
        [CanBeNull(false)]
        public Coordinates Coordinates { get; set; }
        [CanBeNull(false)]
        public string Website { get; set; }
        [CanBeNull(false)]
        public string WorkHours { get; set; }
        [Json]
        [CanBeNull(false)]
        public List<Phone> Phone { get; set; }
    }
}