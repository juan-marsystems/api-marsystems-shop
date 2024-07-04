using ClassLibrary1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;

namespace ClassLibrary1.Repositories
{
    public interface ICarritosRepository
    {
        public IEnumerable<ArticuloCarrito> ObtenerArticulosCarrito(int userID);
        public Carrito InsertarCarrito(int? userId, List<DetalleRequest> detalles);
        public bool EliminarCarrito(int userId);
        public bool EditarCarrito(CarritoRequest carritoRequest);
    }
}
