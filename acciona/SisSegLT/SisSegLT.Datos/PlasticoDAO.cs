using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class PlasticoDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tPlastico entidad)
        {
            try
            {
                bool exito = false;
                db.tPlastico.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tPlastico entidad)
        {
            try
            {
                bool exito = false;

                tPlastico modificado = CopiarEntidad(entidad);
                db.tPlastico.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tPlastico entidad)
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

        public tPlastico CopiarEntidad(tPlastico entidad)
        {
            tPlastico nuevo = new tPlastico()
            {
                IdPlastico = entidad.IdPlastico,
                IdCuenta = entidad.IdCuenta,
                NoTarjeta = entidad.NoTarjeta,
                NombrePlastico = entidad.NombrePlastico,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tPlastico> Listar()
        {
            return (from r in db.tPlastico
                    orderby r.NoTarjeta
                    select r).ToList();
        }
        public List<tPlastico> ListarActivos()
        {
            return (from r in db.tPlastico
                    where r.EsActivo == true
                    orderby r.NoTarjeta
                    select r).ToList();
        }

        public tPlastico ObtenerPorIdPlastico(Guid idPlastico)
        {
            return (from r in db.tPlastico
                    where (r.IdPlastico == idPlastico)
                    select r).FirstOrDefault();
        }

        public tPlastico ObtenerPorNoTarjeta(string noTarjeta)
        {
            return (from r in db.tPlastico
                    where r.NoTarjeta.Equals(noTarjeta)
                    select r).FirstOrDefault();
        }

        public List<tPlastico> ObtenerPorIdCuenta(Guid idCuenta)
        {
            return (from r in db.tPlastico
                    where r.IdCuenta.Equals(idCuenta)
                    select r).ToList();
        }
        public List<tPlastico> ObtenerPorIdCliente(Guid idCliente)
        {
            return (from r in db.tPlastico
                    join c in db.tClienteCuenta on r.IdCuenta equals c.IdCuenta
                    join p in db.Cliente on c.IdCliente equals p.IdCliente
                    where p.IdCliente.Equals(idCliente)
                    select r).ToList();
        }
    }
}
