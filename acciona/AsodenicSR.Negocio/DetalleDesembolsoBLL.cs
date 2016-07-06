using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DetalleDesembolsoBLL
    {
        public bool Insertar(InformacionCheque entidad)
        {
            return new DetalleDesembolsoDAO().Insertar(entidad);
        }

        public bool Actualizar(InformacionCheque entidad)
        {
            return new DetalleDesembolsoDAO().Actualizar(entidad);
        }

        public bool Eliminar(InformacionCheque entidad)
        {
            return new DetalleDesembolsoDAO().Eliminar(entidad);
        }

        public InformacionCheque CopiarEntidad(InformacionCheque entidad)
        {
            return new DetalleDesembolsoDAO().CopiarEntidad(entidad);
        }

        public List<InformacionCheque> Listar()
        {
            return new DetalleDesembolsoDAO().Listar();
        }

        public InformacionCheque ObtenerPorIdDetalleDesembolso(Guid idDetalle)
        {
            return new DetalleDesembolsoDAO().ObtenerPorIdDetalleDesembolso(idDetalle);
        }

        public List<InformacionCheque> ObtenerDetallePorBanco(string banco)
        {
            return new DetalleDesembolsoDAO().ObtenerContactosPorBanco(banco);
        }

        public List<InformacionCheque> ObtenerPorNoCheque(string noCheque)
        {
            return new DetalleDesembolsoDAO().ObtenerPorNoCheque(noCheque);
        }
    }
}
