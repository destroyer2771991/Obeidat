using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ObeidatLog.Logger.Instance.Debug("Web", "Alaa", @"Id: 10, Page: 15 ,Password:123, Image: data:image/png;base64,iVBO++Rw0KGgoAAAANSUhEUgAAB0IAAAQ4CAIAAeXiM/H/wAAAABJRU5ErkJggg==, Data:Welcome to home , Other Image is  data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAB0IAAAQ4CAIAAeXiM/H/wAAAABJRU5ErkJggg==");
            ObeidatLog.Logger.Instance.Info("Love", "I love you");
            ObeidatLog.Logger.Instance.LogException(new Exception("Fuck"), "Love");
            return View();
        }
    }
}