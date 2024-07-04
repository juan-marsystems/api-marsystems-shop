using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DTO
{
    public class OrdenUsuario
    {
        public int? NumeroOrden { get; set; }
        public DateTime? FechaOrden { get; set; }
        public double? TotalOrden { get; set; }
        public int? NumeroCarrito { get; set; }
        public List<ArticuloCarrito> Articulos { get; set; }
    }
}
