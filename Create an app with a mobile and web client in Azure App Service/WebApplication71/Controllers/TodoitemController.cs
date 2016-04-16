using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication71.Models;

namespace WebApplication71.Controllers
{
    public class TodoitemController : ApiController
    {
        List<Todoitem> result = new List<Todoitem>() {
                new Todoitem { Id=1, Subject = "AzureBootCamp1"
                , Content="Develop and Deploy ASP.NET Core 1 application to Azure Docker VM" },

                new Todoitem { Id=2,Subject = "AzureBootCamp2"
                , Content="Create an app with a mobile and web client in Azure App Service" },

                new Todoitem { Id=3,Subject = "AzureBootCamp3"
                , Content="Infrastructure as a Service in Microsoft Azure" },
            };

        public IEnumerable<Todoitem> GetAllTodoitem()
        {
            return result;
        }

        public IHttpActionResult GetTodoitem(int id)
        {
            var item = result.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
