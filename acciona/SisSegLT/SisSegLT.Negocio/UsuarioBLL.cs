using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class UsuarioBLL
    {
        public bool Insertar(Usuario entidad)
        {
            return new UsuarioDAO().Insertar(entidad);
        }

        public bool Actualizar(Usuario entidad)
        {
            return new UsuarioDAO().Actualizar(entidad);
        }

        public bool Eliminar(Usuario entidad)
        {
            return new UsuarioDAO().Eliminar(entidad);
        }

        public Usuario CopiarEntidad(Usuario entidad)
        {
            return new UsuarioDAO().CopiarEntidad(entidad);
        }

        public List<Usuario> Listar()
        {
            return new UsuarioDAO().Listar();
        }

        public List<Usuario> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                return Listar();
            return new UsuarioDAO().ObtenerPorNombre(nombre);
        }

        public Usuario ObtenerPorUsuarioContrasena(string user, string pass)
        {
            if (string.IsNullOrEmpty(user.Trim()))
                return null;
            if (string.IsNullOrEmpty(user.Trim()))
                return null;
            return new UsuarioDAO().ObtenerPorUsuarioContrasena(user, pass);
        }

        public Usuario ObtenerPorIdUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty)
                return null;
            return new UsuarioDAO().ObtenerPorIdUsuario(idUsuario);
        }

        public List<Usuario> ObtenerPorIdSucursal(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty)
                return null;
            return new UsuarioDAO().ObtenerPorIdSucursal(idSucursal);
        }

        public List<Usuario> ObtenerPorIdRol(Guid idRol)
        {
            if (idRol == Guid.Empty)
                return null;
            return new UsuarioDAO().ObtenerPorIdRol(idRol);
        }
    }
}
