using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class spPlasticoListaBLL
    {
        public List<SP_PlasticoLista_Result> ObtenerspListaPlasticoPorIdCliente(Guid idCliente)
        {
            return new spPlasticoListadDAO().Listar(idCliente);
        }
    }
}
