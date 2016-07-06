using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;
using AccionaSR.Negocio;

namespace AccionaSR.Negocio
{
    public class MatrizCalificacionBLL
    {
        public bool Insertar(MatrizCalificacion entidad)
        {
            return new MatrizCalificacionDAO().Insertar(entidad);
        }

        public bool Actualizar(MatrizCalificacion entidad)
        {
            return new MatrizCalificacionDAO().Actualizar(entidad);
        }

        public bool Eliminar(MatrizCalificacion entidad)
        {
            return new MatrizCalificacionDAO().Eliminar(entidad);
        }

        public MatrizCalificacion CopiarEntidad(MatrizCalificacion entidad)
        {
            return new MatrizCalificacionDAO().CopiarEntidad(entidad);
        }

        public List<MatrizCalificacion> Listar()
        {
            return new MatrizCalificacionDAO().Listar();
        }

        public MatrizCalificacion ObtenerPorIdMatrizCalificacion(Guid idMatrizCalificacion)
        {
            return new MatrizCalificacionDAO().ObtenerPorIdMatrizCalificacion(idMatrizCalificacion);
        }

        public List<MatrizCalificacion> ObtenerPorNombre(string nombre)
        {
            return new MatrizCalificacionDAO().ObtenerPorNombre(nombre);
        }

        public List<MatrizCalificacion> ObtenerPorValor(int valor)
        {
            return new MatrizCalificacionDAO().ObtenerPorValor(valor);
        }
    }
}
