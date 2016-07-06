using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace Acciona.Negocio
{
    public class UsuarioRolBLL
    {
        public bool Insertar(UsuarioRol entidad)
        {
            return new UsuarioRolDAO().Insertar(entidad);
        }

        public bool Actualizar(UsuarioRol entidad)
        {
            return new UsuarioRolDAO().Actualizar(entidad);
        }

        public bool Eliminar(UsuarioRol entidad)
        {
            return new UsuarioRolDAO().Eliminar(entidad);
        }

        public UsuarioRol CopiarEntidad(UsuarioRol entidad)
        {
            return new UsuarioRolDAO().CopiarEntidad(entidad);
        }

        public List<UsuarioRol> Listar()
        {
            return new UsuarioRolDAO().ListarActivos();
        }
        public List<UsuarioRol> ListarActivos()
        {
            return new UsuarioRolDAO().ListarActivos();
        }

        public UsuarioRol ObtenerPorIdUsuarioRol(Guid IdUsuarioRol)
        {
            return new UsuarioRolDAO().ObtenerPorIdUsuarioRol(IdUsuarioRol);
        }

        public List<UsuarioRol> ObtenerPorIdRol(Guid idRol)
        {
            return new UsuarioRolDAO().ObtenerPorIdRol(idRol);
        }
        public List<UsuarioRol> ObtenerPorIdUsuario(Guid idUsuario)
        {
            return new UsuarioRolDAO().ObtenerPorIdUsuario(idUsuario);
        }

    }
}
