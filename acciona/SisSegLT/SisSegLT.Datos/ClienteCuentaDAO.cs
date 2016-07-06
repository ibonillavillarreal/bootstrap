using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ClienteCuentaDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(tClienteCuenta entidad)
        {
            try
            {
                bool exito = false;
                db.tClienteCuenta.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tClienteCuenta entidad)
        {
            try
            {
                bool exito = false;

                tClienteCuenta modificado = CopiarEntidad(entidad);
                db.tClienteCuenta.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " Los errores de validacion fueron los siguientes: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(tClienteCuenta entidad)
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

        public tClienteCuenta CopiarEntidad(tClienteCuenta entidad)
        {
            tClienteCuenta nuevo = new tClienteCuenta()
            {
                IdCuenta = entidad.IdCuenta,
                IdCliente = entidad.IdCliente,
                IdAprobado = entidad.IdAprobado,
                IdTipoCuenta = entidad.IdTipoCuenta,
                NoCuenta = entidad.NoCuenta,
                FechaAprobacion = entidad.FechaAprobacion,
                Limite = entidad.Limite,
                IdEstadoCuenta = entidad.IdEstadoCuenta,              
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,                
                Usuario = entidad.Usuario,
                NombrePC = entidad.NombrePC,
                DireccionIP = entidad.DireccionIP
            };
            return nuevo;
        }

        public List<tClienteCuenta> Listar()
        {
            return (from r in db.tClienteCuenta
                    orderby r.NoCuenta where r.EsActivo  == true
                    select r).ToList();
        }

        public tClienteCuenta ObtenerPorIdCuenta(Guid idCuenta)
        {
            return (from r in db.tClienteCuenta
                    where r.IdCuenta == idCuenta && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<tClienteCuenta> ObtenerPorNoCuenta(string NumeroCuenta)
        {
            return (from r in db.tClienteCuenta
                    where r.NoCuenta.Equals(NumeroCuenta) && r.EsActivo == true
                    select r).ToList();
        }

        public List<tClienteCuenta> ObtenerCuentaPorIdCliente(Guid idCliente)
        {
            return (from r in db.tClienteCuenta
                    where r.IdCliente == idCliente && r.EsActivo == true
                    select r).ToList();
        }

       
    }
}
