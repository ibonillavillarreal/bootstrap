using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ReferenciasBLL
    {
        public bool Insertar(Referencias entidad)
        {
            return new ReferenciasDAO().Insertar(entidad);
        }

        public bool Actualizar(Referencias entidad)
        {
            return new ReferenciasDAO().Actualizar(entidad);
        }

        public bool Eliminar(Referencias entidad)
        {
            return new ReferenciasDAO().Eliminar(entidad);
        }

        public Referencias CopiarEntidad(Referencias entidad)
        {
            return new ReferenciasDAO().CopiarEntidad(entidad);
        }

        public List<Referencias> Listar()
        {
            return new ReferenciasDAO().Listar();
        }

        public Referencias ObtenerPorIdReferencias(Guid idReferencias)
        {
            return new ReferenciasDAO().ObtenerPorIdReferencias(idReferencias);
        }

        public List<Referencias> ObtenerPorNombre(string nombre)
        {
            return new ReferenciasDAO().ObtenerPorNombre(nombre);
        }

        public List<Referencias> ObtenerReferenciasPorIdCliente(Guid idCliente)
        {
            return new ReferenciasDAO().ObtenerReferenciasPorIdCliente(idCliente);
        }

        public List<vwReferencias> ObtenervwReferenciasPorIdCliente(Guid idCliente)
        {
            return new ReferenciasDAO().ObtenervwReferenciasPorIdCliente(idCliente);
        }
    }
}