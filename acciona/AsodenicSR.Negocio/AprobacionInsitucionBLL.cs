using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class AprobacionInstitucionBLL
    {
        public bool Insertar(AprobacionInstitucion entidad)
        {
            return new AprobacionInstitucionDAO().Insertar(entidad);
        }

        public bool Actualizar(AprobacionInstitucion entidad)
        {
            return new AprobacionInstitucionDAO().Actualizar(entidad);
        }

        public bool Eliminar(AprobacionInstitucion entidad)
        {
            return new AprobacionInstitucionDAO().Eliminar(entidad);
        }

        public AprobacionInstitucion CopiarEntidad(AprobacionInstitucion entidad)
        {
            return new AprobacionInstitucionDAO().CopiarEntidad(entidad);
        }

        public List<AprobacionInstitucion> Listar()
        {
            return new AprobacionInstitucionDAO().Listar();
        }

        public AprobacionInstitucion ObtenerPorIdAprobacionInstitucion(Guid idAprobacionInstitucion)
        {
            return new AprobacionInstitucionDAO().ObtenerPorIdAprobacionInstitucion(idAprobacionInstitucion);
        }

        public List<AprobacionInstitucion> ObtenerPorNombre(string nombre)
        {
            return new AprobacionInstitucionDAO().ObtenerPorNombre(nombre);
        }

        public List<AprobacionInstitucion> ObtenerAprobacionInstitucionPorIdCliente(Guid idCliente)
        {
            return new AprobacionInstitucionDAO().ObtenerAprobacionInstitucionPorIdCliente(idCliente);
        }

        public List<vwExclusivoInstitucion> ObtenervwInstitucionPorIdCliente(Guid idCliente)
        {
            return new AprobacionInstitucionDAO().ObtenervwInstitucionPorIdCliente(idCliente);
        }
    }
}