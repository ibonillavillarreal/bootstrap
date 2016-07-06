using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DatosNegocioDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(DatosNegocio entidad)
        {
            try
            {
                bool exito = false;
                db.DatosNegocio.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(DatosNegocio entidad)
        {
            try
            {
                bool exito = false;

                DatosNegocio modificado = CopiarEntidad(entidad);
                db.DatosNegocio.Attach(modificado);
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

        public bool Eliminar(DatosNegocio entidad)
        {
            ProveedoresDAO pDAO = new ProveedoresDAO();
            DocumentosNegocioDAO dDAO = new DocumentosNegocioDAO();
            var proveedores = pDAO.ObtenerNegocioProveedoresPorIdDatosNegocio(entidad.IdDatosNegocio);
            var documentos = dDAO.ObtenerDocumentosNegocioPorIdDatosNegocio(entidad.IdDatosNegocio);
            try
            {
                bool exito = false;
                foreach (var item in proveedores)
                {
                    item.EsActivo = false;
                    pDAO.Actualizar(item);
                }
                foreach (var item in documentos)
                {
                    item.EsActivo = false;
                    dDAO.Actualizar(item);
                }
                entidad.EsActivo = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public DatosNegocio CopiarEntidad(DatosNegocio entidad)
        {
            DatosNegocio nuevo = new DatosNegocio()
            {
                IdDatosNegocio = entidad.IdDatosNegocio,
                IdCliente = entidad.IdCliente,
                Alquila = entidad.Alquila,
                EsPropio = entidad.EsPropio,
                Familiar = entidad.Familiar,
                Tiempo = entidad.Tiempo,
                IngresoVolumen = entidad.IngresoVolumen,
                TipoNegocio = entidad.TipoNegocio,
                UbicacionNegocio = entidad.UbicacionNegocio,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                IdDestinoCredito = entidad.IdDestinoCredito,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<DatosNegocio> Listar()
        {
            return (from r in db.DatosNegocio                      
                    orderby r.IngresoVolumen
                    select r).ToList();
        }

        public DatosNegocio ObtenerPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return (from r in db.DatosNegocio
                    where r.IdDatosNegocio == idDatosNegocio && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<DatosNegocio> ObtenerPorNombre(string nombre)
        {
            return (from r in db.DatosNegocio
                    where r.TipoNegocio.Equals(nombre) && r.EsActivo == true
                    select r).ToList();
        }

        public List<DatosNegocio> ObtenerDatosNegocioPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return (from r in db.DatosNegocio
                    where r.IdCliente.Equals(idDatosNegocio) && r.EsActivo == true
                    select r).ToList();
        }

        public List<vwDatosNegocio> ObtenerDatosNegocioPorIdCliente(Guid idCliente)
        {
            return (from r in db.vwDatosNegocio
                    where r.IdCliente.Equals(idCliente)
                    select r).ToList();
        }
    }
}