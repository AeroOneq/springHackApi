using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

using AeroORMFramework;
using Newtonsoft.Json;
using SpringHackApi.Models;

namespace SpringHackApi.Controllers
{
    public class CouponCodesController : ApiController
    {
        private string ConnectionString { get; }
        [HttpGet]
        public async Task<HttpResponseMessage> CheckUserCouponCode(string code)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    List<CouponCode> couponCodes = connector.GetRecords<CouponCode>("Code", code);

                    if (couponCodes.Count > 0)
                    {
                        if (couponCodes[0].CouponCodeStatus == CouponCodeStatus.Unused)
                        {
                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new StringContent(JsonConvert.SerializeObject(couponCodes[0]), System.Text.Encoding.UTF8,
                                    "application/json")
                            };
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.NoContent);
                        }
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);
                    }
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }

        public async Task<HttpResponseMessage> GetCuponCodeCouponCode(int couponID)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    CouponCode couponCode = connector.GetRecord<CouponCode>("CouponID", couponID);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(couponCode), 
                            System.Text.Encoding.UTF8, "application/json")
                    };

                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }
    }
}