using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AeroORMFramework; 

namespace SpringHackApi.Models
{
    public class UserSession
    {
        [PrimaryKey, AutoincrementID, CanBeNull(false)]
        public int ID { get; set; } 
        [CanBeNull(false)]
        public int ChatID { get; set; }
        [CanBeNull(false)]
        public int Rating { get; set; }
        [CanBeNull(false)]
        public string Reason { get; set; }
        [CanBeNull(false)]
        public int IsTicket { get; set; }
        [CanBeNull(false)]
        public string Result { get; set; }
    }
}