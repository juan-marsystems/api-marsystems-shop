using ClassLibrary1.models;
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
    }
}
