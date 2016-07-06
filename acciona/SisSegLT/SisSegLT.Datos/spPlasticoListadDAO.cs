using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class spPlasticoListadDAO
    {
        private SisSegDB db = new SisSegDB();

        public List<SP_PlasticoLista_Result> Listar(Guid idCliente)
        {
            return db.SP_PlasticoLista(idCliente).ToList();
        }

        
    }
}
