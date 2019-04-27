using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AeroORMFramework;

namespace SpringHackApi.Models
{
    public class Coupon
    {
        [AutoincrementID, PrimaryKey, CanBeNull(false)]
        public int ID { get; set; }
        [CanBeNull(false)]
        public int UserID { get; set; }
        [CanBeNull(false)]
        public int OfficeID { get; set; }
        [CanBeNull(false)]
        public DateTime CreationDate { get; set; }
        [CanBeNull(false)]
        public DateTime VisitTime { get; set; }
        [CanBeNull(false)]
        public string OfficeAddress { get; set; }
        [CanBeNull(false)]
        public string ServiceType { get; set; }
        [CanBeNull(false), SetAzureSQLDataType("int")]
        public CouponStatus CouponStatus { get; set; }
    }
}