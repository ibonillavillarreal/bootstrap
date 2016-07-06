using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class CiudadBLL
    {
        public bool Insertar(Ciudad entidad)
        {
            return new CiudadDAO().Insertar(entidad);
        }

        public bool Actualizar(Ciudad entidad)
        {
            return new CiudadDAO().Actualizar(entidad);
        }

        public bool Eliminar(Ciudad entidad)
        {
            return new CiudadDAO().Eliminar(entidad);
        }

        public Ciudad CopiarEntidad(Ciudad entidad)
        {
            return new CiudadDAO().CopiarEntidad(entidad);
        }

        public List<Ciudad> Listar()
        {
            return new CiudadDAO().Listar();
        }

        public List<Ciudad> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                return Listar();
            return new CiudadDAO().ObtenerPorNombre(nombre);
        }

        public Ciudad ObtenerPorIdCiudad(Guid idCiudad)
        {
            if (idCiudad == Guid.Empty)
                return null;
            return new CiudadDAO().ObtenerPorIdCiudad(idCiudad);
        }

        public List<Ciudad> ObtenerPorIdPais(Guid idPais)
        {
            if (idPais == Guid.Empty)
                return null;
            return new CiudadDAO().ObtenerPorIdPais(idPais);
        }
    }
}
