using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class RolBLL
    {
        public bool Insertar(Rol entidad)
        {
            return new RolDAO().Insertar(entidad);
        }

        public bool Actualizar(Rol entidad)
        {
            return new RolDAO().Actualizar(entidad);
        }

        public bool Eliminar(Rol entidad)
        {
            return new RolDAO().Eliminar(entidad);
        }

        public Rol CopiarEntidad(Rol entidad)
        {
            return new RolDAO().CopiarEntidad(entidad);
        }

        public List<Rol> Listar()
        {
            return new RolDAO().Listar();
        }

        public List<Rol> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                return Listar();
            return new RolDAO().ObtenerPorNombre(nombre);
        }

        public Rol ObtenerPorIdRol(Guid idRol)
        {
            if (idRol == Guid.Empty)
                return null;
            return new RolDAO().ObtenerPorIdRol(idRol);
        }
    }
}
