using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DetalleClienteDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(DetalleCliente entidad)
        {
            try
            {
                bool exito = false;
                db.DetalleCliente.Add(entidad);
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public DetalleCliente ObtenerDetalleClientePorIdCliente(Guid idCliente)
        {
            return (from r in db.DetalleCliente
                    where (r.IdCliente == idCliente) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public bool Actualizar(DetalleCliente detalleCliente)
        {
            try
            {
                bool exito = false;

                db.DetalleCliente.Attach(detalleCliente);
                db.Entry(detalleCliente).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }
    }
}