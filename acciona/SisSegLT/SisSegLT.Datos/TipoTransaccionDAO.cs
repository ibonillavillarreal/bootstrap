using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class TipoTransaccionDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(TipoTransaccion entidad)
        {
            try
            {
                bool exito = false;
                db.TipoTransaccion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(TipoTransaccion entidad)
        {
            try
            {
                bool exito = false;

                TipoTransaccion modificado = CopiarEntidad(entidad);
                db.TipoTransaccion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(TipoTransaccion entidad)
        {
            try
            {
                bool exito = false;
                entidad.EsActivo = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public TipoTransaccion CopiarEntidad(TipoTransaccion entidad)
        {
            TipoTransaccion nuevo = new TipoTransaccion()
            {
                IdTipoTransaccion = entidad.IdTipoTransaccion,
                CuentaContable = entidad.CuentaContable,
                ContraCuenta = entidad.ContraCuenta,
                Descripcion = entidad.Descripcion,
                Concepto = entidad.Concepto,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<TipoTransaccion> Listar()
        {
            return (from r in db.TipoTransaccion
                    orderby r.Descripcion
                    select r).ToList();
        }
        public List<TipoTransaccion> ListarActivos()
        {
            return (from r in db.TipoTransaccion
                    where r.EsActivo == true
                    orderby r.Descripcion
                    select r).ToList();
        }

        public List<TipoTransaccion> ListarHijos()
        {
            return (from r in db.TipoTransaccion
                    where r.EsActivo == true && r.IdTipoTransaccionHijo != null
                    orderby r.Descripcion
                    select r).ToList();
        }

        public TipoTransaccion ObtenerPorIdTipoTransaccion(Guid idTipoTransaccion)
        {
            return (from r in db.TipoTransaccion
                    where (r.IdTipoTransaccion == idTipoTransaccion)
                    select r).FirstOrDefault();
        }

        public List<TipoTransaccion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.TipoTransaccion
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }
    }
}
