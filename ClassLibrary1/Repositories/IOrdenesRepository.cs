using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;
using ClassLibrary1.DTO;

namespace ClassLibrary1.Repositories
{
    public interface IOrdenesRepository
    {
        public bool RealizarCompra(int userId);
        public IEnumerable<OrdenUsuario> ListarOrdenesPorUser( int userId);
    }
}
