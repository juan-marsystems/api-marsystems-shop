using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DTO
{
    public class CarritoRequest
    {
        public int? IdUser { get; set; }
        public List<DetalleRequest> Detalles { get; set; }
    }
}
