using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VDS.RDF.Storage;

namespace SecProject.Controllers
{
    public class HomeController : Controller
    {
     //   public StardogConnector context;
        
     //   SecProject.BL.SecService service = new BL.SecService(context);
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            new BL.Base().Initialise();
          



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information";

            return View();
        }

        public ActionResult ChangeActiveMenu(string activeMenuName)
        {
            Session["activeMenuItem"] = activeMenuName;
            return View("About");
        }
    }
}
