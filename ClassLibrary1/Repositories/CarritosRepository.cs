using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.DTO;
using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories
{
    public class CarritosRepository : ICarritosRepository
    {
        private readonly MarketSystemsContext _marketSystemsContext;

        public CarritosRepository(MarketSystemsContext context)
        {
            _marketSystemsContext = context;
        }

        public IEnumerable<ArticuloCarrito> ObtenerArticulosCarrito(int userId)
        {
            var query = from cart in _marketSystemsContext.Carritos
                        join det in _marketSystemsContext.Detalles on cart.IdCart equals det.IdCart
                        join art in _marketSystemsContext.Articulos on det.IdArt equals art.IdArt
                        where cart.IdUser == userId && cart.StatusCart == false
                        select new ArticuloCarrito
                        {
                            Nombre = art.NameArt,
                            Cantidad = det.QuantityDetail,
                            Precio = art.PriceArt
                        };
            return query.ToList();
        }

        public Carrito InsertarCarrito(int? userId, List<DetalleRequest> detalles)
        {
            using var transaction = _marketSystemsContext.Database.BeginTransaction();
            try
            {
                var nuevoCarrito = new Carrito
                {
                    IdUser = userId,
                    StatusCart = false
                };
                _marketSystemsContext.Carritos.Add(nuevoCarrito);
                _marketSystemsContext.SaveChanges();

                int idCart = nuevoCarrito.IdCart;

                foreach(var detalle in detalles)
                {
                    var nuevoDetalle = new Detalle
                    {
                        IdCart = idCart,
                        IdArt = detalle.IdArt,
                        QuantityDetail = detalle.QuantityDetail
                    };
                    _marketSystemsContext.Detalles.Add(nuevoDetalle);
                }

                _marketSystemsContext.SaveChanges();
                transaction.Commit();

                return nuevoCarrito;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
        }

        public bool EliminarCarrito(int userId)
        {
            var carrito = _marketSystemsContext.Carritos
                .Include(c => c.Detalles)
                .FirstOrDefault(c => c.IdUser == userId && c.StatusCart == false);

            if(carrito == null)
            {
                return false;
            }

            _marketSystemsContext.Detalles.RemoveRange(carrito.Detalles);
            _marketSystemsContext.Carritos.Remove(carrito);
            _marketSystemsContext.SaveChanges();
            return true;
        }

        public bool EditarCarrito(CarritoRequest carritoRequest)
        {
             var carrito = _marketSystemsContext.Carritos
                .Include(c => c.Detalles)
                .FirstOrDefault(c => c.IdUser == carritoRequest.IdUser && c.StatusCart == false);
            if(carrito == null)
            {
                return false;
            }

            var detallesExistentes = carrito.Detalles.ToDictionary(d => d.IdArt);
            var nuevosIdsArt = new HashSet<int?>(carritoRequest.Detalles.Select(d => d.IdArt));
            var detallesEliminar = detallesExistentes.Keys.Except(nuevosIdsArt).ToList();
            foreach(var idArt in detallesEliminar)
            {
                var detalle = detallesExistentes[idArt];
                _marketSystemsContext.Detalles.Remove(detalle);
            }

            foreach(var nuevoDetalle in carritoRequest.Detalles)
            {
                if(detallesExistentes.TryGetValue(nuevoDetalle.IdArt, out var detalleExistentes))
                {
                    detalleExistentes.QuantityDetail = nuevoDetalle.QuantityDetail;
                    _marketSystemsContext.Detalles.Update(detalleExistentes);
                }
                else
                {
                    var detalle = new Detalle
                    {
                        IdCart = carrito.IdCart,
                        IdArt = nuevoDetalle.IdArt,
                        QuantityDetail = nuevoDetalle.QuantityDetail
                    };
                    _marketSystemsContext.Detalles.Add(detalle);
                }
            }
            _marketSystemsContext.SaveChanges();

            return true;
        }
    }
}
