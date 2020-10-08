using ABM_TP.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABM_TP.Controllers
{
    public class ColorController : Controller
    {
        private static DBParam DBParam = new DBParam();
        private DBConnection DBConnection = new DBConnection(DBParam.genStrConn());


        // GET: Color
        public ActionResult Index()
        {
            List<Models.ColorModel> colorList = new List<Models.ColorModel>();


            colorList = DBConnection.Obtener_Color();

            return View(colorList);
        }

        // GET: Color/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Color/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        [HttpPost]
        public ActionResult Create(Models.ColorModel datosColor)
        {



            DBConnection.mtd_Insertar_Color(datosColor);
            return RedirectToAction("Index");

        }

        // GET: Color/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DBConnection.Obtener_Color().Find(lsmodel => lsmodel.Id == id)); ;
        }

        // POST: Color/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ColorModel datosColor)
        {
            DBConnection.mtd_Update_Color(datosColor);

            return RedirectToAction("Index");
        }

        // GET: Color/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DBConnection.Obtener_Color().Find(lsmodel => lsmodel.Id == id));
        }

        // POST: Color/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.ColorModel datosColor)
        {
            DBConnection.mtd_Eliminar_Color(datosColor.Id);

            return RedirectToAction("Index");
        }
    }
}
