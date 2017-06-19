using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LujetonA.Models.aux_object
{
    public class Factura_completa
    {
        public factura header { set; get; }
        public List<detalle_factura> detalles { set; get; }      
    }

   
}