using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DetalleDesembolsoDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(InformacionCheque entidad)
        {
            try
            {
                bool exito = false;
                db.InformacionCheque.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar(InformacionCheque entidad)
        {
            try
            {
                bool exito = false;

                InformacionCheque modificado = CopiarEntidad(entidad);
                db.InformacionCheque.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(InformacionCheque entidad)
        {
            try
            {
                bool exito = false;                  
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public InformacionCheque CopiarEntidad(InformacionCheque entidad)
        {
            InformacionCheque nuevo = new InformacionCheque()
            {
                IdDetalleCheque = entidad.IdDetalleCheque,
                NoCheque = entidad.NoCheque,
                Banco = entidad.Banco,
                Moneda = entidad.Moneda,             
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                DireccionIP = entidad.DireccionIP,
                DireccionMAC = entidad.DireccionMAC
            };
            return nuevo;
        }

        public List<InformacionCheque> Listar()
        {
            return (from r in db.InformacionCheque                       
                    orderby r.NoCheque
                    select r).ToList();
        }

        public InformacionCheque ObtenerPorIdDetalleDesembolso(Guid idDetalle)
        {
            return (from r in db.InformacionCheque
                    where (r.IdDetalleCheque == idDetalle) 
                    select r).FirstOrDefault();
        }

        public List<InformacionCheque> ObtenerPorNoCheque(string noCheque)
        {
            return (from r in db.InformacionCheque
                    where r.NoCheque.Equals(noCheque)
                    select r).ToList();
        }

        public List<InformacionCheque> ObtenerContactosPorBanco(string banco)
        {
            return (from r in db.InformacionCheque
                    where r.Banco.Equals(banco) 
                    select r).ToList();
        }
    }
}
