using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net;

using SpringHackApi.Models;

using AeroORMFramework;
using Newtonsoft.Json;
using System.IO;

namespace SpringHackApi.Controllers
{
    public class AssesmentsController : ApiController
    {
        private string ConnectionString { get; } = "Server=tcp:springhack.database.windows.net,1433;Initial Catalog=springHackDB;Persist Security Info=False;User ID=AeroOne;Password=iYH-FXn-Vw5-dz8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpPost]
        public async Task<HttpResponseMessage> InsertAssesment()
        {
            return await Task.Run(async() =>
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

                    Connector connector = new Connector(ConnectionString);

                    Assesment assesment = JsonConvert.DeserializeObject<Assesment>(body);
                    Coupon coupon = connector.GetRecord<Coupon>("ID", assesment.CouponID);
                    CouponCode couponCode = connector.GetRecord<CouponCode>("CouponID", coupon.ID);
                    couponCode.CouponCodeStatus = CouponCodeStatus.Used;
                    connector.UpdateRecord(couponCode);

                    connector.Insert(assesment);

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetOfficeAssesments(int officeID)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    List<Assesment> assesments = connector.GetRecords<Assesment>("OfficeID", officeID);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(assesments), System.Text.Encoding.UTF8,
                        "application/json")
                    };
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllAssesments()
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    List<Assesment> assesments = connector.GetAllRecords<Assesment>();

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(assesments),
                         System.Text.Encoding.UTF8, "application/json")
                    };
                }
                catch
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            });
        }
    }
}