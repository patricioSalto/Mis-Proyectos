using ABM_TP.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABM_TP.Controllers
{
    public class VariacionController : Controller
    {
        private static DBParam DBParam = new DBParam();
        private DBConnection DBConnection = new DBConnection(DBParam.genStrConn());

        // GET: Variacion
        public ActionResult Index()
        {
            List<Models.VariacionModel> variacionList = new List<Models.VariacionModel>();

            variacionList = DBConnection.Obtener_Variacion();
            return View(variacionList);
        }

        // GET: Variacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Variacion/Create
        public ActionResult Create()
        {
            Models.VariacionModel cl = new Models.VariacionModel();
            cl.Color = ListarColores();
            cl.Producto = ListarProducto();
            return View(cl);

        }

        // POST: Variacion/Create
        [HttpPost]
        public ActionResult Create(Models.VariacionModel datosVariacion)
        {
            DBConnection.mtd_Insertar_Variacion(datosVariacion);

            //mostrar colores

            datosVariacion.Color = ListarColores();
            var selectedItem = datosVariacion.Color.Find(p => p.Value == datosVariacion.Id_color.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                ViewBag.Message = "Color: " + selectedItem.Text;
                ViewBag.Message += "\\nQuantity: " + datosVariacion.Quantity;
            }

            //mostrar producto

            datosVariacion.Producto = ListarProducto();
            var selectedItem2 = datosVariacion.Producto.Find(p => p.Value == datosVariacion.Id_producto.ToString());
            if (selectedItem2 != null)
            {
                selectedItem2.Selected = true;
                ViewBag.Message = "Producto: " + selectedItem2.Text;
                ViewBag.Message += "\\nQuantity2: " + datosVariacion.Quantity2;
            }
            return View(datosVariacion);
        }



        private static List<SelectListItem> ListarColores()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            MySqlConnection connection = new MySqlConnection(DBParam.genStrConn());

            connection.Open();

            string sql = "SELECT * FROM color";
            using (MySqlCommand myCommand = new MySqlCommand(sql, connection))
            {
                connection.Close();

                connection.Open();

                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();

                connection.Close();
                connection.Open();
                using (MySqlDataReader sdr = myCommand.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        items.Add(new SelectListItem
                        {
                            Text = sdr["nombre"].ToString(),
                            Value = sdr["idColor"].ToString()
                        });
                    }
                }
                connection.Close();

            }


            return items;
        }

        // listar productos

        private static List<SelectListItem> ListarProducto()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            MySqlConnection connection = new MySqlConnection(DBParam.genStrConn());

            connection.Open();

            string sql = "SELECT * FROM producto";
            using (MySqlCommand myCommand = new MySqlCommand(sql, connection))
            {
                connection.Close();

                connection.Open();

                MySqlDataReader reader;
                reader = myCommand.ExecuteReader();

                connection.Close();
                connection.Open();
                using (MySqlDataReader sdr = myCommand.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        items.Add(new SelectListItem
                        {
                            Text = sdr["nombre"].ToString(),
                            Value = sdr["idProducto"].ToString()
                        });
                    }
                }
                connection.Close();

            }


            return items;
        }

        // GET: Variacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DBConnection.Obtener_Variacion().Find(lsmodel => lsmodel.Id == id));
        }

        // POST: Variacion/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.VariacionModel datosVariacion)
        {
            DBConnection.mtd_Update_Variacion(datosVariacion);

            return RedirectToAction("Index");

        }

        // GET: Variacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DBConnection.Obtener_Variacion().Find(lsmodel => lsmodel.Id == id));

        }

        // POST: Variacion/Delete/5
        [HttpPost]
        public ActionResult Delete(Models.VariacionModel datosVariacion)
        {

            DBConnection.mtd_Eliminar_Variacion(datosVariacion.Id);
            return RedirectToAction("Index");

        }
    }
}
