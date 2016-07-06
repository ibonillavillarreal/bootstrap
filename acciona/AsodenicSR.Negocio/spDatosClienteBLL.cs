using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class spDatosClienteBLL
    {
        public List<SP_DatosCliente_Result> ObtenerspDatosClientePorIdCliente(Guid idCliente)
        {
            return new spDatosClienteDAO().Listar(idCliente);
        }

        public List<SP_DatosDocumentosCliente_Result> ObtenerspDocClientePorIdCliente(Guid idCliente)
        {
            return new spDatosClienteDAO().ListarDocumentosCliente(idCliente);
        }
    }
}
