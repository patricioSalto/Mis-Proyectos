using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABM_TP.Database;

namespace ABM_TP.Controllers
{
    public class ProductoController : Controller
    {
        private static DBParam DBParam = new DBParam();
        private DBConnection DBConnection = new DBConnection(DBParam.genStrConn());
   


        // GET: Producto
        public ActionResult Index()
        {
            List<Models.ProductoModel> productoList = new List<Models.ProductoModel>();
        

            productoList = DBConnection.Obtener_Productos();

            return View(productoList);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Models.ProductoModel datosProducto)
        {
           DBConnection.mtd_Insertar_Producto(datosProducto);
            return RedirectToAction("Index");
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DBConnection.Obtener_Productos().Find(lsmodel => lsmodel.Id == id));
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ProductoModel datosProducto)
        {
            DBConnection.mtd_Update_Producto(datosProducto);

            return RedirectToAction("Index");
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DBConnection.Obtener_Productos().Find(lsmodel => lsmodel.Id == id));
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.ProductoModel datosProducto)
        {
            DBConnection.mtd_Eliminar_Producto(datosProducto.Id);

            return RedirectToAction("Index");

        }
    }
}
