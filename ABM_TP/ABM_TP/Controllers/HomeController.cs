using ABM_TP.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABM_TP.Controllers
{
    public class HomeController : Controller
    {
        private static DBParam DBParam = new DBParam();

        private DBConnection DBConnection = new DBConnection(DBParam.genStrConn());

        public ActionResult Index()
        {
           // DBConnection.connect();

          //  DBConnection.ExecQuery("INSERT INTO productos (nombre,descripcion, tipo) VALUES (\"pantalon\", \"ejemplo\",\"tipoelemplo\")");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}