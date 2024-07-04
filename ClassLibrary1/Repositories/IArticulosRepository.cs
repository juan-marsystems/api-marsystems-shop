using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
    public interface IArticulosRepository
    {
        public IEnumerable<Articulo> ListarArticulos();
        public Articulo ObtenerArticuloPorId (int id);
    }
}
