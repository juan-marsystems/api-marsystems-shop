using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;
using ClassLibrary1.DTO;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly MarketSystemsContext _marketSystemsContext;

        public OrdenesRepository(MarketSystemsContext context)
        {
            _marketSystemsContext = context;
        }

        public bool RealizarCompra(int userId)
        {
            using var transaction = _marketSystemsContext.Database.BeginTransaction();
            try
            {
                //Obtiene carrito
                var carrito = _marketSystemsContext.Carritos.FirstOrDefault(c => c.IdUser == userId && c.StatusCart == false);
                if (carrito == null)
                {
                    return false; //No encontro carrito
                }

                //Validar que haya stock
                var detalles = _marketSystemsContext.Detalles.Where(d => d.IdCart == carrito.IdCart).ToList();
                foreach(var detalle in detalles)
                {
                    var articulo = _marketSystemsContext.Articulos.FirstOrDefault(a => a.IdArt == detalle.IdArt);
                    if(articulo == null || articulo.QuantityArt < detalle.QuantityDetail)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                var totalOrder = (from det in _marketSystemsContext.Detalles
                                  join art in _marketSystemsContext.Articulos on det.IdArt equals art.IdArt
                                  where det.IdCart == carrito.IdCart
                                  select det.QuantityDetail * art.PriceArt).Sum();
                var nuevaOrden = new Ordene
                {
                    IdUser = userId,
                    IdCart = carrito.IdCart,
                    DateOrder = DateTime.Now,
                    TotalOrder = totalOrder
                };
                _marketSystemsContext.Ordenes.Add(nuevaOrden);

                //Actualiza stock
                foreach (var detalle in detalles)
                {
                    var articulo = _marketSystemsContext.Articulos.FirstOrDefault(a => a.IdArt == detalle.IdArt);
                    articulo.QuantityArt -= detalle.QuantityDetail;
                    _marketSystemsContext.Articulos.Update(articulo);
                }

                //Actualiza estado de carrito
                carrito.StatusCart = true;
                _marketSystemsContext.Carritos.Update(carrito);

                _marketSystemsContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch 
            {
                transaction.Rollback();
                return false;
            }
        }

        public IEnumerable<OrdenUsuario> ListarOrdenesPorUser(int userId)
        {
            var ordenes = _marketSystemsContext.Ordenes
            .Where(o => o.IdUser == userId)
            .OrderBy(o => o.DateOrder)  // Ordena por fecha de orden si es necesario
            .ToList()
            .Select((o, index) => new OrdenUsuario
            {
                NumeroOrden = index + 1, // Contador de ordenes por usuario
                FechaOrden = o.DateOrder,
                TotalOrden = o.TotalOrder,
                NumeroCarrito = o.IdCart,
                Articulos = _marketSystemsContext.Detalles
                    .Where(d => d.IdCart == o.IdCart)
                    .Select(d => new ArticuloCarrito
                    {
                        Nombre = d.IdArtNavigation.NameArt,
                        Cantidad = d.QuantityDetail,
                        Precio = d.IdArtNavigation.PriceArt
                    }).ToList()
            })
            .ToList();

            return ordenes;
        }
    }
}
