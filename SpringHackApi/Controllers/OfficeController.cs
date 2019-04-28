using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Xml;
using System.Runtime.Serialization.Json;

using SpringHackApi.Models;

using AeroORMFramework;

using Newtonsoft.Json;
using System.Web.Http;

namespace SpringHackApi.Controllers
{
    public class OfficeController : ApiController
    {
        private string ConnectionString { get; }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllOffices()
        {
            return await Task.Run(() =>
            {
                Connector connector = new Connector(ConnectionString);
                List<Office> offices = connector.GetAllRecords<Office>();


                return new HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(offices), System.Text.Encoding.UTF8,
                        "application/json")
                };
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetOffice(int officeID)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector connector = new Connector(ConnectionString);
                    List<Office> offices = connector.GetRecords<Office>("ID", officeID);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(offices),
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