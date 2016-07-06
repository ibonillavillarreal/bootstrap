using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class ClasificacionBLL
    {
        public bool Insertar(Clasificacion entidad)
        {
            return new ClasificacionDAO().Insertar(entidad);
        }

        public bool Actualizar(Clasificacion entidad)
        {
            return new ClasificacionDAO().Actualizar(entidad);
        }

        public bool Eliminar(Clasificacion entidad)
        {
            return new ClasificacionDAO().Eliminar(entidad);
        }

        public Clasificacion CopiarEntidad(Clasificacion entidad)
        {
            return new ClasificacionDAO().CopiarEntidad(entidad);
        }

        public List<Clasificacion> Listar()
        {
            return new ClasificacionDAO().Listar();
        }

        public Clasificacion ObtenerPorIdClasificacion(Guid idClasificacion)
        {
            return new ClasificacionDAO().ObtenerPorIdClasificacion(idClasificacion);
        }

        public List<Clasificacion> ObtenerPorIdCategoria(Guid idCategoria)
        {
            return new ClasificacionDAO().ObtenerPorIdCategoria(idCategoria);
        }

        public List<Clasificacion> ObtenerPorNombre(string nombre)
        {
            return new ClasificacionDAO().ObtenerPorNombre(nombre);
        }
    }
}
