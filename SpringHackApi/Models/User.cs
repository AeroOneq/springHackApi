using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AeroORMFramework;

namespace SpringHackApi.Models
{
    public class UserInfo
    {
        [PrimaryKey, AutoincrementID, CanBeNull(false)]
        public int ID { get; set; } 
        [CanBeNull(false)]
        public string Name { get; set; }
        [CanBeNull(false)]
        public string Surname { get; set; }
        [CanBeNull(false)]
        public string MiddleName { get; set; }
        [CanBeNull(false)]
        public DateTime BirthDate { get; set; }
        [CanBeNull(false)]
        public string Address { get; set; }
        [CanBeNull(false)]
        public string Type { get; set; }
        [CanBeNull(false)]
        public string Gender { get; set; }
    }
}