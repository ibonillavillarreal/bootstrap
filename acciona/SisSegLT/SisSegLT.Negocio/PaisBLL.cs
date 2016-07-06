using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class PaisBLL
    {
        public bool Insertar(Pais entidad)
        {
            return new PaisDAO().Insertar(entidad);
        }

        public bool Actualizar(Pais entidad)
        {
            return new PaisDAO().Actualizar(entidad);
        }

        public bool Eliminar(Pais entidad)
        {
            return new PaisDAO().Eliminar(entidad);
        }

        public Pais CopiarEntidad(Pais entidad)
        {
            return new PaisDAO().CopiarEntidad(entidad);
        }

        public List<Pais> Listar()
        {
            return new PaisDAO().Listar();
        }

        public List<Pais> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                return Listar();
            return new PaisDAO().ObtenerPorNombre(nombre);
        }

        public Pais ObtenerPorIdPais(Guid idPais)
        {
            if (idPais == Guid.Empty)
                return null;
            return new PaisDAO().ObtenerPorIdPais(idPais);
        }
    }
}
