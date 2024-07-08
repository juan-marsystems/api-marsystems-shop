using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DTO
{
    public class ArticuloCarrito
    {
        public int? idArt {  get; set; }
        public string Nombre { get; set; }
        public int? Cantidad { get; set; }
        public double? Precio { get; set; }
    }
}
