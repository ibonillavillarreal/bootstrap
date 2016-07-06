using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class RecibosBLL
    {

        public List<TipoTransaccion> ListarTipoTransacciones()
        {
            return new RecibosDAO().ListarTipoTransacciones();
        }

        public List<TipoTransaccion> ListarTipoDebitos()
        {
            return new RecibosDAO().ListarTipoDebitos();
        }

        public List<tCredito> ObtenerReciboPorIdMovimiento(Guid idmovimiento)
        {
            return new RecibosDAO().ObtenerReciboPorIdMovimiento(idmovimiento);
        }

        //private SisSegDB db = new SisSegDB();
        public bool RevertirRecibos(tCredito credito)
        {
            try
            {
                var ListaDetallePagos = new RecibosDAO().ListarporIdPago(credito.IdPago);
                foreach (var item in ListaDetallePagos)
                {
                    var cuota = new CuotaBLL().ObtenerPorIdCuota(Guid.Parse(item.IdCuota.ToString()));
                    if (cuota.MontoCouta == item.AbonoCouta)
                    {
                        cuota.AbonoCuota = 0;
                        cuota.SaldoCouta = item.AbonoCouta;

                    }
                    else if (cuota.MontoCouta != item.AbonoCouta && item.SaldoCuota == 0)
                    {
                        cuota.SaldoCouta = item.AbonoCouta;
                        cuota.AbonoCuota = cuota.MontoCouta - item.AbonoCouta;
                    }
                    else
                    {
                        cuota.SaldoCouta = cuota.SaldoCouta + item.AbonoCouta;
                        cuota.AbonoCuota = cuota.AbonoCuota - item.AbonoCouta;
                    }

                    bool exito = new CuotaBLL().Actualizar(cuota);
                }

                bool eliminarDetalles = EliminarDetallePago(ListaDetallePagos);

                var recibo = new RecibosDAO().ObtenerPorIdPago(credito.IdPago);
                bool eliminarCredito = new RecibosDAO().EliminarRecibo(recibo);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool EliminarDetallePago(List<tDetallePago> ListaDetallePagos)
        {
            return new RecibosDAO().EliminarDetallePago(ListaDetallePagos);
        }

        public bool EliminarDetalleRecibos(Guid IdCuenta)
        {
            return new RecibosDAO().EliminarDetalleRecibos(IdCuenta);
        }

        /// <summary>
        /// Funcion bool que recalcula los movimientos de una determinada cuenta
        /// dado el idCuenta
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <returns>Verdadero - Falso</returns>
        public bool RecalcularMovimientosxCuenta(Guid IdCuenta, string NoCuenta)
        {
            try
            {
                bool exito = false;
                //lista los recibos de la cuenta
                var lstRecibos = new RecibosDAO().ObtenerReciboPorIdCuenta(IdCuenta);
                if (lstRecibos.Count > 0)
                {
                    //eliminar los detalles de los recibos
                    exito = EliminarDetalleRecibos(IdCuenta);

                    if (exito)
                    {
                        //listar los movimientos de la cuenta
                        var lstMovimientos = new MovimientoBLL().ListarMovimientosXIdCuenta(IdCuenta);
                        if (lstMovimientos.Count > 0)
                        {
                            //eliminar las cuotas de los movimientos
                            exito = new CuotaBLL().EliminarListaCuotas(lstMovimientos);
                            if (exito)
                            {
                                //calcular las cuotas de los movimientos 
                                foreach (var item in lstMovimientos)
                                {
                                    exito = new MovimientoBLL().GenerarCuotasSarandear(item.IdMovimiento);
                                }


                                //aplicar los recibos
                                foreach (var item in lstRecibos.OrderBy(x => x.FechaEfectiva))
                                {

                                    exito = new CuotaBLL().IngresarDetallePagoyCuotas2(NoCuenta, item.MontoRecibido, item.IdPago,
                                    item.Usuario, item.DireccionIP, item.NombrePC);

                                }

                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else //si no encuentra recibos verifica si tiene movimientos y genera las cuotas
                {
                    //listar los movimientos de la cuenta
                    var lstMovimientos = new MovimientoBLL().ListarMovimientosXIdCuenta(IdCuenta);
                    if (lstMovimientos.Count > 0)
                    {
                        //eliminar las cuotas de los movimientos
                        exito = new CuotaBLL().EliminarListaCuotas(lstMovimientos);
                        if (exito)
                        {
                            //calcular las cuotas de los movimientos 
                            foreach (var item in lstMovimientos)
                            {
                                exito = new MovimientoBLL().GenerarCuotasNuevas(item, item.IdMovimiento);
                            }
                        }
                    }
                    else
                        return false;

                }
                return exito;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
