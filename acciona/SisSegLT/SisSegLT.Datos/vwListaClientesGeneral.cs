using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class vwListaClientesGeneral
    {

        private String _NoIdentificacion { get; set; }
        private String _NombreCompleto { get; set; }
        private String _NoCuenta { get; set; }
        private String _NoTarjeta { get; set; }

        private SisSegDB db = new SisSegDB();

        public List<vwListaClientesGeneral> ListarClientes()
        {
            List<vwListaClientesGeneral> lstClientes = new List<vwListaClientesGeneral>();

            var clientes = (from c in db.Cliente
                             join cc in db.tClienteCuenta on c.IdCliente equals cc.IdCliente
                             join p in db.tPlastico on cc.IdCuenta equals p.IdCuenta
                             where
                               c.EsActivo == true &&
                               cc.EsActivo == true &&
                               p.EsActivo == true
                             orderby
                               cc.NoCuenta
                             select new vwListaClientesGeneral
                             {
                                _NoIdentificacion = c.NoIdentificacion,
                                _NombreCompleto = c.NombreCompleto,
                                _NoCuenta = cc.NoCuenta,
                                _NoTarjeta = p.NoTarjeta
                             });

                lstClientes = clientes.ToList();
                     

            return lstClientes;
        }
    }
}
