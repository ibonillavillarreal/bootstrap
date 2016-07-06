using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DetallePagoBLL
    {
        public bool Insertar(tDetallePago entidad)
        {
            return new DetallePagoDAO().Insertar(entidad);
        }

        public bool Actualizar(tDetallePago entidad)
        {
            return new DetallePagoDAO().Actualizar(entidad);
        }

        public bool Eliminar(tDetallePago entidad)
        {
            return new DetallePagoDAO().Eliminar(entidad);
        }

        public tDetallePago CopiarEntidad(tDetallePago entidad)
        {
            return new DetallePagoDAO().CopiarEntidad(entidad);
        }

        public List<tDetallePago> Listar()
        {
            return new DetallePagoDAO().Listar();
        }

        public tDetallePago ObtenerPorIdDetallePago(Guid idDetallePago)
        {
            return new DetallePagoDAO().ObtenerPorIdDetallePago(idDetallePago);
        }

        public List<tDetallePago> ObtenerListaporIdDetallePago(Guid IdDetallePago)
        {
            return new DetallePagoDAO().ObtenerListaporIdDetallePago(IdDetallePago);
        }

    }
}
