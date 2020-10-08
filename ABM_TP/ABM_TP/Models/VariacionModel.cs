using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABM_TP.Models
{
    public class VariacionModel
    {
        // [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nombre"),
         Required(ErrorMessage = "El nombre es requerido")]
        public string Tamanio{ get; set; }

        [Display(Name = "Stock"),
         Required(ErrorMessage = "El stock es requerido")]
        public int Stock { get; set; }

        [Display(Name = "Precio"),
         Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }

        //para mostrar los colores que existan

        public List<SelectListItem> Color { get; set; }
        [Display(Name = "Color"),
         Required(ErrorMessage = "El color es requerido")]


        public int Id_color { get; set; }

        public int Quantity { get; set; }

        // para mostrar los productos que existan

        public List<SelectListItem> Producto { get; set; }
        [Display(Name = "Producto"),
         Required(ErrorMessage = "El producto es requerido")]


        public int Id_producto { get; set; }

        public int Quantity2 { get; set; }

    }
}