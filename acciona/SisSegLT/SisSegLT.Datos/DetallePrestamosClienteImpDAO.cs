using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DetallePrestamosClienteImpDAO
    {

        private SisSegDB db = new SisSegDB();

        public DetallePrestamosClienteImp CopiarEntidad(DetallePrestamosClienteImp entidad)
        {
            DetallePrestamosClienteImp nuevo = new DetallePrestamosClienteImp()
            {
                IdDetalleClienteImp = entidad.IdDetalleClienteImp,
                NoCedula = entidad.NoCedula,
                CodigoExpediente = entidad.CodigoExpediente,
                NombreExpediente = entidad.NombreExpediente,
                NoPrestamo = entidad.NoPrestamo,
                FechaAprobacion = entidad.FechaAprobacion,
                FechaCancelacion = entidad.FechaCancelacion,
                MontoAprobado = entidad.MontoAprobado,
                FechaRegistro = entidad.FechaRegistro,
            };
            return nuevo;
        }

        public List<DetallePrestamosClienteImp> Listar()
        {
            return (from r in db.DetallePrestamosClienteImp
                    orderby r.NoCedula
                    select r).ToList();
        }

        public DetallePrestamosClienteImp ObtenerPorIdDetallePrestamosClienteImp(Guid idDetallePrestamosClienteImp)
        {
            return (from r in db.DetallePrestamosClienteImp
                    where (r.IdDetalleClienteImp == idDetallePrestamosClienteImp)
                    select r).FirstOrDefault();
        }

        public List<DetallePrestamosClienteImp> ObtenerPorCedula(string noCedula)
        {
            return (from r in db.DetallePrestamosClienteImp
                    where r.NoCedula.Equals(noCedula)
                    orderby r.FechaRegistro descending
                    select r).ToList();
        }

        public List<DetallePrestamosClienteImp> ObtenerPorCedulaCodExpediente(string noCedula, string codigoExpediente)
        {
            return (from r in db.DetallePrestamosClienteImp
                    where (r.NoCedula.Equals(noCedula)) && (r.CodigoExpediente.Equals(codigoExpediente))
                    orderby r.FechaRegistro descending
                    select r).ToList();
        }

    }
}
