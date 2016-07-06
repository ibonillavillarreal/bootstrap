using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class MovimientosDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(tMovimientos entidad)
        {
            try
            {
                bool exito = false;
                db.tMovimientos.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(tMovimientos entidad)
        {
            try
            {
                bool exito = false;

                tMovimientos modificado = CopiarEntidad(entidad);
                db.tMovimientos.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(tMovimientos entidad)
        {
            try
            {
                bool exito = false;
                //entidad.EsActivo = false;
                //exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public tMovimientos CopiarEntidad(tMovimientos entidad)
        {
            tMovimientos nuevo = new tMovimientos()
            {
                IdMovimiento = entidad.IdMovimiento,
                IdMovimientoPadre = entidad.IdMovimientoPadre,
                IdPlastico = entidad.IdPlastico,
                IdPromotor = entidad.IdPromotor,
                IdDesembolsa = entidad.IdDesembolsa,
                IdColector = entidad.IdColector,
                IdDetalleCheque = entidad.IdDetalleCheque,
                IdOrigen = entidad.IdOrigen,
                IdVehiculo = entidad.IdVehiculo,
                NoMovimiento = entidad.NoMovimiento,
                FechaRegistro = entidad.FechaRegistro,
                FechaEfectiva = entidad.FechaEfectiva,
                FechaProceso = entidad.FechaProceso,
                IdTipoTransaccion = entidad.IdTipoTransaccion,
                Flujo = entidad.Flujo,
                NoCuotas = entidad.NoCuotas,
                IdFrecuencia = entidad.IdFrecuencia,
                MontoTransaccion = entidad.MontoTransaccion,
                Abono = entidad.Abono,
                Salvamento = entidad.Salvamento,
                Canon = entidad.Canon,
                Saldo = entidad.Saldo,
                PlazoAnios = entidad.PlazoAnios,
                PlazoMeses = entidad.PlazoMeses,
                Interes = entidad.Interes,
                FechaVencimiento = entidad.FechaVencimiento,
                FechaCorte = entidad.FechaCorte,
                CuotaProgramada = entidad.CuotaProgramada,
                TipoInteres = entidad.TipoInteres,
                EstadoTransaccion = entidad.EstadoTransaccion,
                Observaciones = entidad.Observaciones,
                Moneda = entidad.Moneda,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC,
                Usuario = entidad.Usuario
            };
            return nuevo;
        }

        public List<tMovimientos> Listar()
        {
            return (from r in db.tMovimientos
                    orderby r.FechaRegistro
                    select r).ToList();
        }
        public List<tMovimientos> ListarActivos()
        {
            return (from r in db.tMovimientos
                    where r.EstadoTransaccion == "Activa"
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public List<tMovimientos> ListarMovimientosHijos(Guid idpadre)
        {
            return (from r in db.tMovimientos
                    where r.IdMovimientoPadre == idpadre
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public tMovimientos ObtenerPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.tMovimientos
                    where (r.IdMovimiento == idMovimiento)
                    select r).FirstOrDefault();
        }

        public List<tMovimientos> ObtenerListaPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.tMovimientos
                    where (r.IdMovimiento == idMovimiento)
                    select r).ToList();
        }

        public List<tMovimientos> ObtenerMovimientoPadresPorNoMovimiento(string noMovimiento)
        {
            return (from r in db.tMovimientos
                    where r.NoMovimiento.Equals(noMovimiento) && r.IdMovimientoPadre == null
                    select r).ToList();
        }

        public List<tMovimientos> ObtenerPorNoMovimiento(string noMovimiento)
        {
            return (from r in db.tMovimientos
                    where r.NoMovimiento.Equals(noMovimiento)
                    select r).ToList();
        }

        public List<tMovimientos> ObtenerPorNoMovimientoContain(string noMovimiento)
        {
            return (from r in db.tMovimientos
                    where r.NoMovimiento.Contains(noMovimiento)
                    select r).ToList();
        }

        public tCuotas ObtenerMaximaFecha(Guid IdMovimiento)
        {
            try
            {
                //int Max = db.tMovimientos.Select(X => int.Parse(X.NoMovimiento)).Max() == null ? 0 :  inicio;
                //DateTime? lista = (db.tCuotas.Where(x => x.IdMovimiento == IdMovimiento).OrderBy(x=>x.FechaCouta).Max()).FechaCouta;
                var Max = db.tCuotas
                                .Where(c => c.IdMovimiento == IdMovimiento)
                                .OrderByDescending(t => t.FechaCouta)
                                .FirstOrDefault();

                return Max;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public int ObtenerMaximoMovimiento()
        {
            try
            {
                //int Max = db.tMovimientos.Select(X => int.Parse(X.NoMovimiento)).Max() == null ? 0 :  inicio;
                int Max = db.tMovimientos.Select(X => X.NoMovimiento.Substring(0, 6)).Cast<int?>().Max() ?? 0;

                return Max;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorIdMovimiento(Guid idMovimiento)
        {
            return (from r in db.vw_DatosCredito
                    where r.IdMovimiento == idMovimiento
                    select r).ToList();
        }

        public List<vw_DatosCredito2> ObtenerDatosGeneralesPorIdMovimiento2(Guid idMovimiento)
        {
            return (from r in db.vw_DatosCredito2
                    where r.IdMovimiento == idMovimiento
                    select r).ToList();
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorCedula(string cedula)
        {
            return (from r in db.vw_DatosCredito
                    where r.NoIdentificacion == cedula
                    orderby r.fechadesembolso
                    select r).ToList();
        }

        public List<vw_DatosCredito2> ObtenerDatosGeneralesPorCedula2(string cedula)
        {
            return (from r in db.vw_DatosCredito2
                    where r.NoIdentificacion == cedula
                    orderby r.fechadesembolso
                    select r).ToList();
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorNombre(string nombre)
        {
            return (from r in db.vw_DatosCredito
                    where r.NombreCompleto.Contains(nombre)
                    orderby r.fechadesembolso
                    select r).Distinct().ToList();
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorNoCuenta(string nocuenta)
        {
            return (from r in db.vw_DatosCredito
                    where r.NoCuenta.Contains(nocuenta)
                    orderby r.fechadesembolso
                    select r).ToList();
        }

        public List<SP_CalculoCuotas_Result> ObtenerListaCuotasPorIdMovimiento(Guid idMovimiento)
        {
            return db.SP_CalculoCuotas(idMovimiento).ToList();
        }

        public List<tMovimientos> ListarMovimientoParaCambio(Guid idCliente)
        {
            return (from c in db.tMovimientos
                    join ct in db.tPlastico on c.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.IdCliente == idCliente)
                    orderby c.FechaEfectiva
                    select c).ToList();
        }

        public List<tMovimientos> ListarMovimientosXCartera()
        {
            return (from c in db.tMovimientos
                    join ct in db.tPlastico on c.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (c.IdMovimientoPadre == null)
                    orderby c.FechaEfectiva
                    select c).ToList();
        }

        public List<tMovimientos> ListarMovimientosXNoCuentaConHijos(string noCuenta)
        {
            return (from c in db.tMovimientos
                    join ct in db.tPlastico on c.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.NoCuenta == noCuenta)
                    orderby c.FechaEfectiva
                    select c).ToList();
        }

        public List<tMovimientos> ListarMovimientosXCuenta(string noCuenta)
        {
            return (from c in db.tMovimientos
                    join ct in db.tPlastico on c.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.NoCuenta == noCuenta) && (c.IdMovimientoPadre == null)
                    orderby c.FechaEfectiva
                    select c).ToList();
        }

        public List<tMovimientos> ListarMovimientosXIdCuenta(Guid IdCuenta)
        {
            return (from c in db.tMovimientos
                    join ct in db.tPlastico on c.IdPlastico equals ct.IdPlastico
                    join cc in db.tClienteCuenta on ct.IdCuenta equals cc.IdCuenta
                    where (cc.IdCuenta == IdCuenta)
                    orderby c.FechaEfectiva
                    select c).ToList();
        }

        public List<OrigenFondos> ListarOrigendeFondos()
        {
            return (from c in db.OrigenFondos
                    orderby c.Descripcion
                    select c).ToList();
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorNoPlaca(string noplaca)
        {
            return (from r in db.vwDatosVehiculos
                    where r.Placa.Contains(noplaca)
                    select r).ToList();
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorIdentificacion(string Identificacion)
        {
            return (from r in db.vwDatosVehiculos
                    where r.NoIdentificacion.Contains(Identificacion)
                    select r).ToList();
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorCodigo(string Codigo)
        {
            return (from r in db.vwDatosVehiculos
                    where r.Codigo.Contains(Codigo)
                    select r).ToList();
        }

        public IQueryable<vw_ListaCreditos> ObtenerCreditosPorTipoTransaccion(string TipoTrans, string fecha, string fechafi, bool todo)
        {
            IQueryable<vw_ListaCreditos> resultado = null;
            DateTime fechai = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fechaf = DateTime.ParseExact(fechafi, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (todo)
            {
                resultado = db.vw_ListaCreditos.Where(x => x.fechadesembolso >= fechai && x.fechadesembolso <= fechaf).OrderBy(x=>x.NoMovimiento);
            }
            else
            {
                resultado = db.vw_ListaCreditos.Where(x => x.fechadesembolso >= fechai && x.fechadesembolso <= fechaf && x.TipoTransaccion.Equals(TipoTrans)).OrderBy(x => x.NoMovimiento);
            }
            
            return resultado;
        }




    }
}
