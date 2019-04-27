using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

using AeroORMFramework;

using SpringHackApi.Models;

namespace SpringHackApi.Controllers
{
    public class CouponCodesController : ApiController
    {
        private string ConnectionString { get; } = "Server=tcp:springhack.database.windows.net,1433;Initial Catalog=springHackDB;Persist Security Info=False;User ID=AeroOne;Password=iYH-FXn-Vw5-dz8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private async Task<HttpResponseMessage> CheckUserCouponCode(string code)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    CouponCode couponCode = connector.GetRecord<CouponCode>("Code", code);

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }
    }
}