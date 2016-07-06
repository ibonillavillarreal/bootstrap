using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class FrecuenciaBLL
    {
        public bool Insertar(tFrecuencia entidad)
        {
            return new FrecuenciaDAO().Insertar(entidad);
        }

        public bool Actualizar(tFrecuencia entidad)
        {
            return new FrecuenciaDAO().Actualizar(entidad);
        }

        public bool Eliminar(tFrecuencia entidad)
        {
            return new FrecuenciaDAO().Eliminar(entidad);
        }

        public tFrecuencia CopiarEntidad(tFrecuencia entidad)
        {
            return new FrecuenciaDAO().CopiarEntidad(entidad);
        }

        public List<tFrecuencia> Listar()
        {
            return new FrecuenciaDAO().Listar();
        }
        public List<tFrecuencia> ListarActivos()
        {
            return new FrecuenciaDAO().ListarActivos();
        }

        public tFrecuencia ObtenerPorIdFrecuencia(Guid idFrecuencia)
        {
            return new FrecuenciaDAO().ObtenerPorIdFrecuencia(idFrecuencia);
        }

        public List<tFrecuencia> ObtenerPorNombre(string nombre)
        {
            return new FrecuenciaDAO().ObtenerPorNombre(nombre);
        }
    }
}
