using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ResumenTransaccionDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(ResumenTransaccion entidad)
        {
            try
            {
                bool exito = false;
                db.ResumenTransaccion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(ResumenTransaccion entidad)
        {
            try
            {
                bool exito = false;

                ResumenTransaccion modificado = CopiarEntidad(entidad);
                db.ResumenTransaccion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(ResumenTransaccion entidad)
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

        public ResumenTransaccion CopiarEntidad(ResumenTransaccion entidad)
        {
            ResumenTransaccion nuevo = new ResumenTransaccion()
            {
                IdTransaccionesInstitucion = entidad.IdTransaccionesInstitucion,
                IdCliente = entidad.IdCliente,
                IdMetodologia = entidad.IdMetodologia,
                NoPrestamo = entidad.NoPrestamo,
                FechaInicioCredito = entidad.FechaInicioCredito,
                FechaFinCredito = entidad.FechaFinCredito,
                MontoPromedio = entidad.MontoPromedio,
                MaximoDiasMora = entidad.MaximoDiasMora,
                Observaciones = entidad.Observaciones,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<ResumenTransaccion> Listar()
        {
            return (from r in db.ResumenTransaccion                   
                    orderby r.NoPrestamo
                    select r).ToList();
        }

        public ResumenTransaccion ObtenerPorIdResumenTransaccion(Guid idResumenTransaccion)
        {
            return (from r in db.ResumenTransaccion
                    where r.IdTransaccionesInstitucion == idResumenTransaccion && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<ResumenTransaccion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.ResumenTransaccion
                    where r.NoPrestamo.Equals(nombre)
                    select r).ToList();
        }

        public List<ResumenTransaccion> ObtenerResumenTransaccionPorIdCliente(Guid idCliente)
        {
            return (from r in db.ResumenTransaccion
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }

        public List<vwResumenTransacciones> ObtenervwResumenTransaccionPorIdCliente(Guid idCliente)
        {
            return (from r in db.vwResumenTransacciones
                    where r.IdCliente.Equals(idCliente)
                    select r).ToList();
        }
    }
}