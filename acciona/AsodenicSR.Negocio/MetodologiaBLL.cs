using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class MetodologiaBLL
    {
        public bool Insertar(Metodologia entidad)
        {
            return new MetodologiaDAO().Insertar(entidad);
        }

        public bool Actualizar(Metodologia entidad)
        {
            return new MetodologiaDAO().Actualizar(entidad);
        }

        public bool Eliminar(Metodologia entidad)
        {
            return new MetodologiaDAO().Eliminar(entidad);
        }

        public Metodologia CopiarEntidad(Metodologia entidad)
        {
            return new MetodologiaDAO().CopiarEntidad(entidad);
        }

        public List<Metodologia> Listar()
        {
            return new MetodologiaDAO().Listar();
        }

        public Metodologia ObtenerPorIdMetodologia(Guid idMetodologia)
        {
            return new MetodologiaDAO().ObtenerPorIdMetodologia(idMetodologia);
        }

        public List<Metodologia> ObtenerPorNombre(string nombre)
        {
            return new MetodologiaDAO().ObtenerPorNombre(nombre);
        }
    }
}
