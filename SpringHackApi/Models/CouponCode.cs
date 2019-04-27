using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AeroORMFramework;

namespace SpringHackApi.Models
{
    public class CouponCode
    {
        [AutoincrementID, PrimaryKey, CanBeNull(false)]
        public int ID { get; set; }
        [CanBeNull(false)]
        public int CouponID { get; set; }
        [CanBeNull(false)]
        public string Code { get; set; } 
        [SetAzureSQLDataType("int"), CanBeNull(false)]
        public CouponCodeStatus CouponCodeStatus { get; set; }
    }
}