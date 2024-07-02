using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly MarketSystemsContext _marketSystemsContext;
        
        public UsuariosRepository(MarketSystemsContext context)
        {
            _marketSystemsContext = context;
        }
        public Usuario ObtenerUsuario(string email, string password)
        {
            return _marketSystemsContext.Usuarios.FirstOrDefault(u => u.EmailUser == email && u.PassUser == password);
        }

        public Usuario InsertarUsuario(string nombre, string apellido, int? edad, string email, string password)
        {
            var nuevoUsuario = new Usuario
            {
                NameUser = nombre,
                SurnameUser = apellido,
                AgeUser = edad,
                EmailUser = email,
                PassUser = password
            };

            _marketSystemsContext.Usuarios.Add(nuevoUsuario);
            _marketSystemsContext.SaveChanges();

            return nuevoUsuario;
        }
        
    }
}
