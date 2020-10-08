using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABM_TP.Models
{
    public class ProductoModel
    {

        // [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nombre"),
         Required(ErrorMessage = "El Nombre es requerido"),
         StringLength(255, ErrorMessage = "Máximo Permitido 50")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion"),
         Required(ErrorMessage = "La descripcion es requerida"),
         StringLength(255, ErrorMessage = "Máximo Permitido 50")]
        public string Descripcion { get; set; }

    }
}