using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DocumentosNegocioDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(DocumentosNegocio entidad)
        {
            try
            {
                bool exito = false;
                db.DocumentosNegocio.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(DocumentosNegocio entidad)
        {
            try
            {
                bool exito = false;

                DocumentosNegocio modificado = CopiarEntidad(entidad);
                db.DocumentosNegocio.Attach(modificado);
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

        public bool Eliminar(DocumentosNegocio entidad)
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

        public DocumentosNegocio CopiarEntidad(DocumentosNegocio entidad)
        {
            DocumentosNegocio nuevo = new DocumentosNegocio()
            {
                IdDatosNegocio = entidad.IdDatosNegocio,
                IdDocumentoNegocio = entidad.IdDocumentoNegocio,
                Institucion = entidad.Institucion,
                TipoRegistros = entidad.TipoRegistros,
                FechaEmision = entidad.FechaEmision,
                FechaVencimiento = entidad.FechaVencimiento,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<DocumentosNegocio> Listar()
        {
            return (from r in db.DocumentosNegocio
                    where r.EsActivo
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public DocumentosNegocio ObtenerPorIdDocumentosNegocio(Guid idDocumentosNegocio)
        {
            return (from r in db.DocumentosNegocio
                    where (r.IdDocumentoNegocio == idDocumentosNegocio) && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<DocumentosNegocio> ObtenerPorNombre(string nombre)
        {
            return (from r in db.DocumentosNegocio
                    where r.TipoRegistros.Equals(nombre)
                    select r).ToList();
        }

        public List<DocumentosNegocio> ObtenerDocumentosNegocioPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return (from r in db.DocumentosNegocio
                    where r.IdDatosNegocio == idDatosNegocio && r.EsActivo == true
                    select r).ToList();
        }
    }
}