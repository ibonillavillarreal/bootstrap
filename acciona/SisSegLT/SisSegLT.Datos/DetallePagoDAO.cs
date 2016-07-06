using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DetallePagoDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tDetallePago entidad)
        {
            try
            {
                bool exito = false;
                db.tDetallePago.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tDetallePago entidad)
        {
            try
            {
                bool exito = false;

                tDetallePago modificado = CopiarEntidad(entidad);
                db.tDetallePago.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tDetallePago entidad)
        {
            try
            {
                bool exito = false;
                //entidad.EsActivo = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public tDetallePago CopiarEntidad(tDetallePago entidad)
        {
            tDetallePago nuevo = new tDetallePago()
            {
                IdDetallePago = entidad.IdDetallePago,
                IdCuota = entidad.IdCuota,                
                SaldoCuota = entidad.SaldoCuota,
                AbonoCouta = entidad.AbonoCouta,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tDetallePago> Listar()
        {
            return (from r in db.tDetallePago
                    orderby r.FechaRegistro
                    select r).ToList();
        }


        public tDetallePago ObtenerPorIdDetallePago(Guid idDetallePago)
        {
            return (from r in db.tDetallePago
                    where (r.IdDetallePago == idDetallePago)
                    orderby r.FechaRegistro
                    select r).FirstOrDefault();
        }

        public List<tDetallePago> ObtenerListaporIdDetallePago(Guid idDetallePago)
        {
            return (from r in db.tDetallePago
                    where r.IdDetallePago == idDetallePago
                    orderby r.FechaRegistro
                    select r).ToList();
        }
    }
}
