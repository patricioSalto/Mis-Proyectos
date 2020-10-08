using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABM_TP.Models
{
    public class ColorModel
    {
        // [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nombre"),
         Required(ErrorMessage = "El Nombre es requerido"),
         StringLength(255, ErrorMessage = "Máximo Permitido 50")]
        public string Nombre { get; set; }

        [Display(Name = "Hexadecimal"),
         Required(ErrorMessage = "El hexadecimal es requerido"),
         StringLength(255, ErrorMessage = "Máximo Permitido 50")]
        public string Hexa { get; set; }

    }
}