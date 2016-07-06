using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;
using SisSegLT.Negocio;

namespace SisSegLT.Seguridad
{
    public class Consulta
    {
        public static Usuario ValidarCredenciales(string user, string pass)
        {
            try
            {
                UsuarioBLL usuarioBll = new UsuarioBLL();
                Usuario usuario = usuarioBll.ObtenerPorUsuarioContrasena(user, pass);
                if (usuario != null)
                {
                    if ((usuario.Login == user) && (usuario.Pass == pass))
                    {
                        return usuario;
                    }
                }
                return null;
            }
            catch 
            {
                throw;   
            }
            
        }
    }
}
