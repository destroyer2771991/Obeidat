using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ObeidatLog.Logger.Instance.Debug("Web", "Alaa", "Id:5, Image: image/png;base64,iVBORw0KGgoAAAANSUhEUgAAB0IAAAQ4CAIAA...eXiM / H / wAAAABJRU5ErkJggg ==  //146 kb img file, Date: 12/025/2120");
            return View();
        }
    }
}
