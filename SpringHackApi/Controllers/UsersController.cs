using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using SpringHackApi.Models;
using AeroORMFramework;
using Newtonsoft.Json;

namespace SpringHackApi.Controllers
{
    public class UsersController : ApiController
    {
        private string ConnectionString { get; } = "Server=tcp:springhack.database.windows.net,1433;Initial Catalog=springHackDB;Persist Security Info=False;User ID=AeroOne;Password=iYH-FXn-Vw5-dz8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private Connector Connector { get; set; }

        public async Task<HttpResponseMessage> GetAllUsers()
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector = new Connector(ConnectionString);    
                    List<UserInfo> users = Connector.GetAllRecords<UserInfo>();

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(users),
                            System.Text.Encoding.UTF8, "application/json")
                    };
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                }
            });
        }

        public async Task<HttpResponseMessage> GetUserByID(int userID)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Connector = new Connector(ConnectionString);
                    UserInfo user = Connector.GetRecord<UserInfo>("ID", userID);

                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8,
                            "application/json")
                    };
                }
                catch
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                }
            });
        }
    }
}