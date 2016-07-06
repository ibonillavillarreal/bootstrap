using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class vwListaPlastico
    {
        private Guid _IdPlastico { get; set; }
        private String _Descripcion { get; set; }
        

        private SisSegDB db = new SisSegDB();

        public List<vwListaPlastico> ListarPlasticoCuenta(Guid idCliente)
        {
            List<vwListaPlastico> lstPlastico = new List<vwListaPlastico>();

            var listap = (from p in db.tPlastico
                                 join r in db.tClienteCuenta on p.IdCuenta equals r.IdCuenta
                                 join t in db.tTipoCuenta on r.IdTipoCuenta equals t.IdTipoCuenta
                                 where r.IdCliente == idCliente && r.EsActivo == true
                                select new vwListaPlastico
                                 {
                                     _IdPlastico = p.IdPlastico,
                                     _Descripcion = t.Descripcion
                                 });

            lstPlastico = listap.ToList();


            return lstPlastico;
        }
        
    }
}
