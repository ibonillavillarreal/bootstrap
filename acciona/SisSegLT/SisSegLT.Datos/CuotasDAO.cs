using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class CuotasDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tCuotas entidad)
        {
            try
            {
                bool exito = false;
                db.tCuotas.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tCuotas entidad)
        {
            try
            {
                bool exito = false;

                tCuotas modificado = CopiarEntidad(entidad);
                db.tCuotas.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(tCuotas entidad)
        {
            try
            {
                bool exito = false;
                tCuotas cuota = db.tCuotas.First(x => x.IdCuota == entidad.IdCuota);

                db.tCuotas.Remove(cuota);
                //db.Entry(entidad).State = System.Data.EntityState.Deleted;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarListaCuotas(List<tMovimientos> entidad)
        {

            try
            {
                bool exito = false;
                foreach (var item in entidad)
                {
                    var queryCliente_TCuotas =
                    from t in db.tCuotas
                    where
                    t.IdMovimiento == item.IdMovimiento
                    select t;
                    //foreach (var item1 in queryCliente_TCuotas)
                    //{
                    //db.tCuotas.Remove(item1);

                    db.Database.ExecuteSqlCommand("Delete from cliente.tcuotas where idmovimiento = {0}", item.IdMovimiento);

                    //exito = db.SaveChanges() > 0;                                         
                    //}
                }

                exito = true;

                return exito;
            }
            catch (Exception ex)
            {

                return false;
            }


        }

        public tCuotas CopiarEntidad(tCuotas entidad)
        {
            tCuotas nuevo = new tCuotas()
            {
                IdCuota = entidad.IdCuota,
                IdMovimiento = entidad.IdMovimiento,
                MontoCouta = entidad.MontoCouta,
                AbonoCuota = entidad.AbonoCuota,
                SaldoCouta = entidad.SaldoCouta,
                FechaCouta = entidad.FechaCouta,
                FechaNegociacion = entidad.FechaNegociacion,
                FechaRegistro = entidad.FechaRegistro,
                FechaProceso = entidad.FechaRegistro,
                EstadoCuota = entidad.EstadoCuota,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tCuotas> Listar()
        {
            return (from r in db.tCuotas
                    orderby r.FechaRegistro
                    select r).ToList();
        }


        public tCuotas ObtenerPorIdCuota(Guid idCuota)
        {
            return (from r in db.tCuotas
                    where (r.IdCuota == idCuota)
                    orderby r.FechaRegistro
                    select r).FirstOrDefault();
        }

        public List<tCuotas> ObtenerPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.tCuotas
                    where r.IdMovimiento == idMovimiento
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public List<SP_CuotasPendientes_Result> ObtenerCuotasPendientes(Guid IdCliente)
        {
            return db.SP_CuotasPendientes(IdCliente).ToList();
        }

        public List<SP_ListadeCobroXColector_Result> ObtenerListadeCobroxColector(Guid idUsuario, string fecha)
        {
            return db.SP_ListadeCobroXColector(Convert.ToDateTime(fecha), idUsuario).ToList();
        }

        public List<tCuotas> ListarCuotasPendientes(string NoCuenta)
        {
            return (from c in db.tCuotas
                    join cn in db.tMovimientos on c.IdMovimiento equals cn.IdMovimiento
                    join ct in db.tPlastico on cn.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.NoCuenta == NoCuenta) && (c.SaldoCouta > 0)                    
                    orderby c.FechaCouta
                    select c).ToList();
        }
        public List<tCuotas> ListarCuotasPendientes2(string NoCuenta, string NoMovimiento)
        {
            return (from c in db.tCuotas
                    join cn in db.tMovimientos on c.IdMovimiento equals cn.IdMovimiento
                    join ct in db.tPlastico on cn.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.NoCuenta == NoCuenta) && (c.SaldoCouta > 0)
                    && (cn.NoMovimiento.StartsWith(NoMovimiento))
                    orderby c.FechaCouta ascending
                    select c).ToList();
        }

        public bool InsertarCredito(tCredito entidad)
        {
            try
            {
                bool exito = false;
                db.tCredito.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarCredito(tCredito entidad)
        {
            try
            {
                bool exito = false;

                tCredito modificado = CopiarEntidadCredito(entidad);
                db.tCredito.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public tCredito CopiarEntidadCredito(tCredito entidad)
        {
            tCredito nuevo = new tCredito()
            {
                IdPago = entidad.IdPago,
                IdTipoTransaccion = entidad.IdTipoTransaccion,
                IdCuenta = entidad.IdCuenta,
                Serie = entidad.Serie,
                NoReferencia = entidad.NoReferencia,
                Recibidode = entidad.Recibidode,
                Concepto = entidad.Concepto,
                MontoRecibido = entidad.MontoRecibido,
                MontoPagado = entidad.MontoPagado,
                MontoAcreditado = entidad.MontoAcreditado,
                PendienteAcreditar = entidad.PendienteAcreditar,
                TipoCambio = entidad.TipoCambio,
                FechaEfectiva = entidad.FechaEfectiva,
                Estado = entidad.Estado,
                FechaProceso = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }


    }
}
