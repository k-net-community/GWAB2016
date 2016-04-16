using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ToDoItemController : Controller
    {
        // GET: ToDoItem
        public ActionResult Index()
        {
            ToDoItemService service = new ToDoItemService();
            return View(service.QueryAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateItem([Bind(Include = "EventId,Title,Content")] ToDoItem item)
        {
            ToDoItemService service = new ToDoItemService();
            service.Add(item);
            return RedirectToAction("Index");
        }
    }
}