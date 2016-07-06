using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ProveedoresDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(NegocioProveedores entidad)
        {
            try
            {
                bool exito = false;
                db.NegocioProveedores.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
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
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar(NegocioProveedores entidad)
        {
            try
            {
                bool exito = false;

                NegocioProveedores modificado = CopiarEntidad(entidad);
                db.NegocioProveedores.Attach(modificado);
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
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(NegocioProveedores entidad)
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

        public NegocioProveedores CopiarEntidad(NegocioProveedores entidad)
        {
            NegocioProveedores nuevo = new NegocioProveedores()
            {
                IdDatosNegocio = entidad.IdDatosNegocio,
                IdNegocioProveedor = entidad.IdNegocioProveedor,
                Nombre = entidad.Nombre,
                EsCliente = entidad.EsCliente,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<NegocioProveedores> Listar()
        {
            return (from r in db.NegocioProveedores
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public NegocioProveedores ObtenerPorIdNegocioProveedores(Guid idNegocioProveedores)
        {
            return (from r in db.NegocioProveedores
                    where r.IdNegocioProveedor == idNegocioProveedores
                    select r).FirstOrDefault();
        }

        public List<NegocioProveedores> ObtenerPorNombre(string nombre)
        {
            return (from r in db.NegocioProveedores
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }

        public List<NegocioProveedores> ObtenerNegocioProveedoresPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return (from r in db.NegocioProveedores
                    where r.IdDatosNegocio.Equals(idDatosNegocio) && r.EsActivo == true
                    select r).ToList();
        }
    }
}