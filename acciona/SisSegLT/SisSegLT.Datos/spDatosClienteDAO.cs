using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class spDatosClienteDAO
    {
        private SisSegDB db = new SisSegDB();

        public List<SP_DatosCliente_Result> Listar(Guid idCliente)
        {
            return db.SP_DatosCliente(idCliente).ToList();
        }

        public List<SP_DatosDocumentosCliente_Result> ListarDocumentosCliente(Guid idCliente)
        {
            return db.SP_DatosDocumentosCliente(idCliente).ToList();
        }

    }
}
