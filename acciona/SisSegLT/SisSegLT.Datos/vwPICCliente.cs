using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class vwPICCliente
    {
        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public Guid IdSucursal { get; set; }

        public string Sucursal { get; set; }

        private SisSegDB db = new SisSegDB();

        public List<vwPICCliente> ObtenerClientes(Guid sucursal, Int32 iDisplayStart, int displayLength, string filtro)
        {
            List<vwPICCliente> clientesFinales = new List<vwPICCliente>();
            if (!string.IsNullOrEmpty(filtro))
            {
                var Customers = (from d in db.Cliente
                                 where d.IdSucursal == sucursal && d.NombreCompleto.Contains(filtro) || d.NoIdentificacion.Contains(filtro)
                                 select new vwPICCliente
                                 {
                                     Nombre = d.NombreCompleto,
                                     Cedula = d.NoIdentificacion,
                                     IdSucursal = d.IdSucursal
                                 }).OrderBy(s => s.Nombre).Skip(iDisplayStart).Take(displayLength);

                clientesFinales = Customers.ToList();
            }
            else
            {
                var Customers = (from d in db.Cliente
                                 where d.IdSucursal == sucursal
                                 select new vwPICCliente
                                 {
                                     Nombre = d.NombreCompleto,
                                     Cedula = d.NoIdentificacion,
                                     IdSucursal = d.IdSucursal
                                 }).OrderBy(s => s.Nombre).Skip(iDisplayStart).Take(displayLength);

                clientesFinales = Customers.ToList();
            }

            return clientesFinales;
        }

        public int ObtenerContador(Guid sucursal)
        {
            return db.Cliente.Where(d => d.IdSucursal == sucursal).Count();
        }
    }
}