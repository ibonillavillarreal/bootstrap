using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DetallePrestamosClienteImpBLL
    {
        public DetallePrestamosClienteImp CopiarEntidad(DetallePrestamosClienteImp entidad)
        {
            return new DetallePrestamosClienteImpDAO().CopiarEntidad(entidad);
        }

        public List<DetallePrestamosClienteImp> Listar()
        {
            return new DetallePrestamosClienteImpDAO().Listar();
        }

        public DetallePrestamosClienteImp ObtenerPorIdDetallePrestamosClienteImp(Guid idDetallePrestamosClienteImp)
        {
            return new DetallePrestamosClienteImpDAO().ObtenerPorIdDetallePrestamosClienteImp(idDetallePrestamosClienteImp);
        }

        public List<DetallePrestamosClienteImp> ObtenerPorCedula(string noCedula)
        {
            return new DetallePrestamosClienteImpDAO().ObtenerPorCedula(noCedula);
        }

        public List<DetallePrestamosClienteImp> ObtenerPorCedulaCodExpediente(string noCedula, string codigoExpediente)
        {
            return new DetallePrestamosClienteImpDAO().ObtenerPorCedulaCodExpediente(noCedula, codigoExpediente);
        }
    }
}
