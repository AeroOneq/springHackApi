using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using SpringHackApi.Models;

using AeroORMFramework;

using Newtonsoft.Json;
using System.IO;

namespace SpringHackApi.Controllers
{
    public class CouponsController : ApiController
    {
        private Connector Connector { get; set; }
        private static Random Random { get; } = new Random();
        private string ConnectionString { get; } = "Server=tcp:springhack.database.windows.net,1433;Initial Catalog=springHackDB;Persist Security Info=False;User ID=AeroOne;Password=iYH-FXn-Vw5-dz8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public async Task<HttpResponseMessage> GetUserCoupons(int userID)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    List<Coupon> coupons = connector.GetRecords<Coupon>("UserID", userID);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(coupons),
                            System.Text.Encoding.UTF8, "application/json")
                    };
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }

        public async Task<HttpResponseMessage> GetAllCoupons()
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector = new Connector(ConnectionString);
                    List<Coupon> coupons = Connector.GetAllRecords<Coupon>();

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(coupons),
                            System.Text.Encoding.UTF8, "application/json")
                    };
                }
                catch
                {
                    Coupon coupon = new Coupon()
                    {
                        CouponStatus = CouponStatus.Active,
                        CreationDate = DateTime.Now,
                        OfficeAddress = "asdasdasd",
                        OfficeID = 0,
                        ServiceType = "asd",
                        UserID = 0,
                        VisitTime = DateTime.Now,
                    };

                    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(coupon),
                            System.Text.Encoding.UTF8, "application/json")
                    };
                }
            });
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateNewCoupon()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    string body;
                    using (Stream stream = await Request.Content.ReadAsStreamAsync())
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            body = await sr.ReadToEndAsync();
                        }
                    }

                    Connector = new Connector(ConnectionString);

                    Coupon coupon = JsonConvert.DeserializeObject<Coupon>(body);
                    CouponCode couponCode = GetCouponCode(coupon);

                    Connector.Insert(coupon);
                    Connector.Insert(couponCode);

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }

        private CouponCode GetCouponCode(Coupon coupon)
        {
            return new CouponCode()
            {
                Code = GenerateCode(),
                CouponCodeStatus = CouponCodeStatus.Unused,
                CouponID = coupon.ID
            };
        }

        private string GenerateCode()
        {
            bool doWeNeedToRepeat = false;
            string code;
            do
            {
                code = string.Empty;
                for (int i = 0; i < 7; i++)
                {
                    code += (char)Random.Next('A', 'Z' + 1);
                }

                if (Connector.GetRecords<CouponCode>("Code", code).Count > 0)
                {
                    doWeNeedToRepeat = true;
                }
            }
            while (doWeNeedToRepeat);
            return code;
        }
    }
}