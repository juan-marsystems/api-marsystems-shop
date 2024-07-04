using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Models;

namespace ClassLibrary1.Repositories
{
    public interface IUsuariosRepository
    {
        public Usuario ObtenerUsuario(string email, string password);
        public Usuario InsertarUsuario(string nombre, string apellido, int? edad, string email, string password);

    }
}
