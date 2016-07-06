using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ClienteDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Cliente entidad)
        {
            bool exito = false;
            try
            {
                db.Cliente.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exito;
        }

        public bool Actualizar(Cliente entidad)
        {
            bool exito = false;
            try
            {
                Cliente modificado = CopiarEntidad(entidad);
                db.Cliente.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
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
            return exito;
        }

        public bool Eliminar(Cliente entidad)
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

        public Cliente CopiarEntidad(Cliente entidad)
        {
            Cliente nuevo = new Cliente()
            {
                IdCliente = entidad.IdCliente,
                NombreCompleto = entidad.NombreCompleto,
                Nombres = entidad.Nombres,
                Apellidos = entidad.Apellidos,
                NoIdentificacion = entidad.NoIdentificacion,
                EstadoPerfil = entidad.EstadoPerfil,
                FechaPerfil = entidad.FechaPerfil,
                IdSucursal = entidad.IdSucursal,
                IdUsuario = entidad.IdUsuario,
                FechaEmision = entidad.FechaEmision,
                FechaVencimiento = entidad.FechaVencimiento,
                IdPais = entidad.IdPais,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<Cliente> Listar()
        {
            return (from r in db.Cliente
                    where r.EsActivo
                    orderby r.NombreCompleto
                    select r).ToList();
        }

        public Cliente ObtenerPorIdCliente(Guid idCliente)
        {
            return (from r in db.Cliente
                    where (r.IdCliente == idCliente) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Cliente> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Cliente
                    where r.NombreCompleto.Contains(nombre)
                    select r).ToList();
        }

        public List<Cliente> ObtenerporCuenta(string nocuenta)
        {

            return (from r in db.Cliente
                    join cc in db.tClienteCuenta on r.IdCliente equals cc.IdCliente
                    where (cc.NoCuenta == nocuenta)                     
                    select r).ToList();
        
        }

        public List<Cliente> ObtenerPorIdentificacion(string identificacion)
        {
            return (from r in db.Cliente
                    where r.NoIdentificacion.Contains(identificacion)
                    select r).ToList();
        }

        public List<vwDatosGeneralesPerfil> ObtenerDatosGeneralesPorIdentificacion(string identificacion)
        {
            return (from r in db.vwDatosGeneralesPerfil
                    where r.NoIdentificacion.Contains(identificacion)
                    select r).ToList();
        }

        public List<vwPerfilIngresado> ObtenervwPicIngresados(DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            IQueryable<vwPICIngresados> resultado = null;
            List<vwPerfilIngresado> perfiles = null;
            resultado = db.vwPICIngresados.Where(x => x.FechaRegistro >= fechaInicio && x.FechaRegistro <= fechaFin);

            if (estado != -1)
            {
                resultado = resultado.Where(x => x.EstadoPerfil == estado);
            }

            perfiles = resultado.GroupBy(x => new { x.Promotor, x.Sucursal }).Select(x => new vwPerfilIngresado
            {
                Sucursal = x.Key.Sucursal,
                Promotor = x.Key.Promotor,
                Total = x.Count()
            }).ToList();
            return perfiles;
        }

        public List<vwPerfilIngresado> ObtenervwPicIngresadosDetallado(DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            IQueryable<vwPICIngresadosDetallados> resultado = null;
            List<vwPerfilIngresado> perfiles = null;
            resultado = db.vwPICIngresadosDetallados.Where(x => x.FechaRegistro >= fechaInicio && x.FechaRegistro <= fechaFin);

            if (estado != -1)
            {
                resultado = resultado.Where(x => x.EstadoPerfil == estado);
            }

            perfiles = resultado.Select(x => new vwPerfilIngresado
            {
                NombreCompleto = x.NombreCompleto,
                Cedula = x.Cedula,
                FechaRegistro = x.FechaRegistro,
                Sucursal = x.Sucursal,
                Promotor = x.Promotor,
                Metodologia = x.Metodologia
            }).ToList();
            return perfiles;
        }
    }
}