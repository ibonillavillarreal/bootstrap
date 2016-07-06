using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class RecibosDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tCredito entidad)
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

        public bool InsertarReversion(Reversion entidad)
        {
            try
            {
                bool exito = false;
                db.Reversion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarRecibo(tCredito entidad)
        {
            try
            {
                bool exito = false;

                tCredito credito = db.tCredito.First(x => x.IdPago == entidad.IdPago);

                db.tCredito.Remove(credito);
                //db.Entry(entidad).State = System.Data.EntityState.Deleted;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarDetalleRecibos(Guid IdCuenta)
        {
            try
            {
                bool exito = false;
                //foreach (var item1 in ListaRecibos)
                //{
                //    var queryTDetallePago =
                //    from t in db.tDetallePago
                //    where
                //    t.IdPago == item1.IdPago
                //    select t;
                //    foreach (var item in queryTDetallePago)
                //    {
                db.Database.ExecuteSqlCommand("delete dp FROM Cliente.tdetallepago dp inner join cliente.tcredito c  on c.idpago=dp.idpago where c.idcuenta = {0}", IdCuenta);
                db.Database.ExecuteSqlCommand("select dp.* FROM Cliente.tdetallepago dp inner join cliente.tcredito c  on c.idpago=dp.idpago where c.idcuenta = {0}", IdCuenta);
                        //tDetallePago detalle = db.tDetallePago.First(x => x.IdPago == item.IdPago);
                        //db.tDetallePago.Remove(item);
                        //db.Entry(item).State = System.Data.EntityState.Deleted;
                        //exito = db.SaveChanges() > 0;
                //    }

                //}

                exito = true;
                return exito;

            }
            catch (Exception ex)
            {                  
                return false;
            }

        }

        public bool EliminarDetallePago(List<tDetallePago> entidades)
        {
            try
            {
                bool exito = false;

                foreach (var item in entidades)
                {
                    tDetallePago detalle = db.tDetallePago.First(x => x.IdDetallePago == item.IdDetallePago);
                    db.tDetallePago.Remove(detalle);
                    //db.Entry(item).State = System.Data.EntityState.Deleted;
                    exito = db.SaveChanges() > 0;
                }

                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tCredito entidad)
        {
            try
            {
                bool exito = false;

                tCredito modificado = CopiarEntidad(entidad);
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



        public tCredito CopiarEntidad(tCredito entidad)
        {
            tCredito nuevo = new tCredito()
            {
                IdPago = entidad.IdPago,
                IdCuenta = entidad.IdCuenta,
                IdTipoTransaccion = entidad.IdTipoTransaccion,
                IdMovimiento = entidad.IdMovimiento,
                Concepto = entidad.Concepto,
                Estado = entidad.Estado,
                FechaEfectiva = entidad.FechaEfectiva,
                FechaProceso = entidad.FechaProceso,
                MontoAcreditado = entidad.MontoAcreditado,
                MontoRecibido = entidad.MontoRecibido,
                NoReferencia = entidad.NoReferencia,
                PendienteAcreditar = entidad.PendienteAcreditar,
                Recibidode = entidad.Recibidode,
                Serie = entidad.Serie,
                TipoCambio = entidad.TipoCambio,
                FechaRegistro = entidad.FechaRegistro,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tCredito> Listar()
        {
            return (from r in db.tCredito
                    orderby r.NoReferencia
                    select r).ToList();
        }

        public List<tDetallePago> ListarporIdPago(Guid idPago)
        {
            return (from r in db.tDetallePago
                    where r.IdPago == idPago
                    orderby r.FechaRegistro
                    select r).ToList();
        }


        public tCredito ObtenerPorIdPago(Guid idPago)
        {
            return (from r in db.tCredito
                    where (r.IdPago == idPago)
                    select r).FirstOrDefault();
        }

        public tCredito ObtenerPorNoReciboySerie(string noRecibo, string serie)
        {
            return (from r in db.tCredito
                    where r.NoReferencia.Contains(noRecibo) && r.Serie.Contains(serie)
                    select r).FirstOrDefault();
        }

        public List<tCredito> ObtenerListaNoReciboySerie(string noRecibo, string serie)
        {
            return (from r in db.tCredito
                    where r.NoReferencia.Contains(noRecibo) && r.Serie.Contains(serie)
                    select r).ToList();
        }

        public List<tCredito> ObtenerReciboPorIdCuenta(Guid idCuenta)
        {
            return (from r in db.tCredito
                    where r.IdCuenta == idCuenta
                    orderby r.FechaEfectiva
                    select r).ToList();
        }

        public List<tCredito> ObtenerReciboPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.tCredito
                    where r.IdMovimiento == idMovimiento
                    orderby r.FechaEfectiva
                    select r).ToList();
        }

        public List<TipoTransaccion> ListarTipoTransacciones()
        {
            return (from r in db.TipoTransaccion
                    where r.EsActivo == true && r.Concepto == "Credito"
                    orderby r.Descripcion
                    select r).ToList();
        }

        public List<TipoTransaccion> ListarTipoDebitos()
        {
            return (from r in db.TipoTransaccion
                    where r.EsActivo == true && r.Concepto == "Debito"
                    orderby r.Descripcion
                    select r).ToList();
        }
    }
}
