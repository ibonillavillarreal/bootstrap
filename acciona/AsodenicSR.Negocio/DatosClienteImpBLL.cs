using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DatosClienteImpBLL
    {
        public DatosClienteImp CopiarEntidad(DatosClienteImp entidad)
        {
            return new DatosClienteImpDAO().CopiarEntidad(entidad);
        }

        public List<DatosClienteImp> Listar()
        {
            return new DatosClienteImpDAO().Listar();
        }

        public DatosClienteImp ObtenerPorIdDatosClienteImp(Guid idDatosClienteImp)
        {
            return new DatosClienteImpDAO().ObtenerPorIdDatosClienteImp(idDatosClienteImp);
        }

        public List<DatosClienteImp> ObtenerPorCedula(string noCedula)
        {
            return new DatosClienteImpDAO().ObtenerPorCedula(noCedula);
        }
    }
}
