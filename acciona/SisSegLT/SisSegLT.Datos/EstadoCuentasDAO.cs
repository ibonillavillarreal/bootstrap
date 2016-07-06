using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class EstadoCuentasDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tEstadoCuentas entidad)
        {
            try
            {
                bool exito = false;
                db.tEstadoCuentas.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tEstadoCuentas entidad)
        {
            try
            {
                bool exito = false;

                tEstadoCuentas modificado = CopiarEntidad(entidad);
                db.tEstadoCuentas.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tEstadoCuentas entidad)
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

        public tEstadoCuentas CopiarEntidad(tEstadoCuentas entidad)
        {
            tEstadoCuentas nuevo = new tEstadoCuentas()
            {
                IdEstadoCuenta = entidad.IdEstadoCuenta,
                Descripcion = entidad.Descripcion,                 
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tEstadoCuentas> Listar()
        {
            return (from r in db.tEstadoCuentas
                    orderby r.Descripcion
                    select r).ToList();
        }
        public List<tEstadoCuentas> ListarActivos()
        {
            return (from r in db.tEstadoCuentas
                    where r.EsActivo == true
                    orderby r.Descripcion
                    select r).ToList();
        }

        public tEstadoCuentas ObtenerPorIdEstadoCuenta(Guid idEstadoCuenta)
        {
            return (from r in db.tEstadoCuentas
                    where (r.IdEstadoCuenta == idEstadoCuenta)
                    select r).FirstOrDefault();
        }

        public List<tEstadoCuentas> ObtenerPorNombre(string nombre)
        {
            return (from r in db.tEstadoCuentas
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }
                                                           
        public List<SP_EncabezadoEstadoCuenta_Result> ConsultaEncabezadoEC(string noCuenta)
        {
            return db.SP_EncabezadoEstadoCuenta(noCuenta).ToList();
        }

        public List<Cliente> ClienteXCodigoVehiculo(string noCodigo)
        {
            return (from r in db.tVehiculos
                    join m in db.tMovimientos on r.IdVehiculo equals m.IdVehiculo
                    join p in db.tPlastico on m.IdPlastico equals p.IdPlastico
                    join cc in db.tClienteCuenta on p.IdCuenta equals cc.IdCuenta
                    join c in db.Cliente on cc.IdCliente equals c.IdCliente
                    where r.Codigo.Equals(noCodigo)
                    select c).ToList();
        }
        /// <summary>
        /// Consulta de datos de pago por codigo de vehiculo 
        /// </summary>
        /// <param name="noCodigo"></param>
        /// <returns></returns>
        public List<SP_ConsultaCodigoVehiculo_Result> ConsultaxCodigoVehiculo(string noCodigo)
        {
            return db.SP_ConsultaCodigoVehiculo(noCodigo).ToList();
        }

        public List<SP_EstadoCuenta_Result> ConsultaEC(string noCuenta, string nocedula, DateTime fechai, DateTime fechaf)
        {
            return db.SP_EstadoCuenta(fechai,fechaf,noCuenta,nocedula).ToList();
        }
    }
}
