using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ToDoItemService
    {
        private static string _dbid;
        private static string _uri;
        private static string _authkey;

        public ToDoItemService()
        {
            _dbid = ConfigurationManager.AppSettings["databaseid"];
            _uri = ConfigurationManager.AppSettings["uri"];
            _authkey = ConfigurationManager.AppSettings["authkey"];
        }

        public List<ToDoItem> QueryAll()
        {
            List<ToDoItem> result = new List<ToDoItem>();

            try
            {
                using (DocumentClient client = new DocumentClient(new Uri(_uri), _authkey))
                {
                    var db = client.CreateDatabaseQuery()
                                 .Where(d => d.Id == _dbid)
                                 .AsEnumerable()
                                 .FirstOrDefault();

                    var collection = client.CreateDocumentCollectionQuery(db.SelfLink)
                        .Where(c => c.Id == "item")
                        .AsEnumerable()
                        .FirstOrDefault();
                    
                    foreach (var item in client.CreateDocumentQuery<ToDoItem>(collection.SelfLink
                       , "select * from item "))
                    {
                        result.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void Add(ToDoItem item)
        {
            try
            {
                AddItemAsync(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task AddItemAsync(ToDoItem item)
        {
            using (DocumentClient client = new DocumentClient(new Uri(_uri), _authkey))
            {
                var db = client.CreateDatabaseQuery()
                             .Where(d => d.Id == _dbid)
                             .AsEnumerable()
                             .FirstOrDefault();

                var collection = client.CreateDocumentCollectionQuery(db.SelfLink)
                    .Where(c => c.Id == "item")
                    .AsEnumerable()
                    .FirstOrDefault();

                await client.CreateDocumentAsync(collection.SelfLink, item);
            }
        }
    }
}
