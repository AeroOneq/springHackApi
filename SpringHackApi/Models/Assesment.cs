using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AeroORMFramework;

namespace SpringHackApi.Models
{
    public class Assesment
    {
        [AutoincrementID, PrimaryKey, CanBeNull(false)]
        public int ID { get; set; }
        [CanBeNull(false)]
        public int UserID { get; set; }
        [CanBeNull(false)]
        public int CouponID { get; set; }

        [CanBeNull(false)]
        public int Mark { get; set; }
        [CanBeNull(false)]
        public string Comment { get; set; }
    }
}