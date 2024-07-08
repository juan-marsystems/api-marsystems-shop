using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;

namespace ClassLibrary1.Repositories
{
    public class ArticulosRepository : IArticulosRepository
    {
        private readonly MarketSystemsContext _marketSystemsContext;

        public ArticulosRepository(MarketSystemsContext context)
        {
            _marketSystemsContext = context;
        }

        public IEnumerable<Articulo> ListarArticulos()
        {
            return _marketSystemsContext.Articulos
                                .OrderBy(a => a.IdArt)
                                .ToList();
        }

        public Articulo ObtenerArticuloPorId(int id)
        {
            return _marketSystemsContext.Articulos.FirstOrDefault(u => u.IdArt == id);
        }
    }
}
