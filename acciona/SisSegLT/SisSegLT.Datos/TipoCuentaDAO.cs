using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class TipoCuentaDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tTipoCuenta entidad)
        {
            try
            {
                bool exito = false;
                db.tTipoCuenta.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tTipoCuenta entidad)
        {
            try
            {
                bool exito = false;

                tTipoCuenta modificado = CopiarEntidad(entidad);
                db.tTipoCuenta.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tTipoCuenta entidad)
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

        public tTipoCuenta CopiarEntidad(tTipoCuenta entidad)
        {
            tTipoCuenta nuevo = new tTipoCuenta()
            {
                IdTipoCuenta = entidad.IdTipoCuenta,
                Prefijo = entidad.Prefijo,
                Numero = entidad.Numero,
                Descripcion = entidad.Descripcion,
                PrefijoPlastico = entidad.PrefijoPlastico,
                ConsecutivoPlastico = entidad.ConsecutivoPlastico,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tTipoCuenta> Listar()
        {
            return (from r in db.tTipoCuenta
                    orderby r.Descripcion
                    select r).ToList();
        }
        public List<tTipoCuenta> ListarActivos()
        {
            return (from r in db.tTipoCuenta
                    where r.EsActivo == true
                    orderby r.Descripcion
                    select r).ToList();
        }

        public tTipoCuenta ObtenerPorIdTipoCuenta(Guid idTipoCuenta)
        {
            return (from r in db.tTipoCuenta
                    where (r.IdTipoCuenta == idTipoCuenta)
                    select r).FirstOrDefault();
        }

        public List<tTipoCuenta> ObtenerPorNombre(string nombre)
        {
            return (from r in db.tTipoCuenta
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<tTipoCuenta> ObtenerPorPrefijoCuenta(string prefijoCuenta)
        {
            return (from r in db.tTipoCuenta
                    where r.Prefijo.Equals(prefijoCuenta)
                    select r).ToList();
        }
    }
}
