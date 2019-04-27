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
    public class RaitingsController : ApiController
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

                    Assesment assesment = JsonConvert.DeserializeObject<Assesment>(body);

                    Connector connector = new Connector(ConnectionString);
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
        public async Task<HttpResponseMessage> GetOfficeAssesments()
        {
            return await Task.Run(() =>
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            });
        }
    }
}