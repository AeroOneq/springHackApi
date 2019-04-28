using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;

using SpringHackApi.Models;
using Newtonsoft.Json;
using AeroORMFramework;

namespace SpringHackApi.Controllers
{
    public class UserSessionsController : ApiController
    {
        private string ConnectionString { get; } = "Server=tcp:springhack.database.windows.net,1433;Initial Catalog=springHackDB;Persist Security Info=False;User ID=AeroOne;Password=iYH-FXn-Vw5-dz8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpGet]
        public async Task<HttpResponseMessage> ExecuteQueryAndGetResult()
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

                    SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = body,
                        Connection = sqlConnection
                    };

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<UserSession> userSessions = new List<UserSession>();
                    while (sqlDataReader.Read())
                    {
                        UserSession userSession = new UserSession()
                        {
                            ID = (int)sqlDataReader.GetValue(0),
                            ChatID = (int)sqlDataReader.GetValue(1),
                            Raiting = (int)sqlDataReader.GetValue(2),
                            Reason = (string)sqlDataReader.GetValue(3),
                            IsTicket = (int)sqlDataReader.GetValue(4),
                            Result = (string)sqlDataReader.GetValue(5)
                        };

                        userSessions.Add(userSession);
                    }

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(userSessions),
                            System.Text.Encoding.UTF8, "application/json")
                    };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(ex.Message,
                            System.Text.Encoding.UTF8, "text/plain")
                    };
                }
            });
        }

        [HttpPost]
        public async Task<HttpResponseMessage> ExecuteQuery()
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

                    SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = body,
                        Connection = sqlConnection
                    };

                    sqlCommand.ExecuteNonQuery();

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(ex.Message,
                            System.Text.Encoding.UTF8, "text/plain")
                    };
                }
            });
        }
    }
}