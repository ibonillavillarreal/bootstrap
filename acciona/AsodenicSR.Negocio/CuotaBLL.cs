using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;
using System.Configuration;
using System.Data.SqlClient;



namespace AccionaSR.Negocio
{

    public class DetallePago
    {
        public string IdPago { get; set; }
        public string IdCuota { get; set; }
        public double Abono { get; set; }
        public double Saldo { get; set; }
    }
    public class CuotaBLL
    {


        System.Collections.Generic.List<DetallePago> listaDetallePago = new System.Collections.Generic.List<DetallePago>();

        public bool Insertar(tCuotas entidad)
        {
            return new CuotasDAO().Insertar(entidad);
        }

        public bool Actualizar(tCuotas entidad)
        {
            return new CuotasDAO().Actualizar(entidad);
        }

        public bool Eliminar(tCuotas entidad)
        {
            return new CuotasDAO().Eliminar(entidad);
        }

        public tCuotas CopiarEntidad(tCuotas entidad)
        {
            return new CuotasDAO().CopiarEntidad(entidad);
        }

        public List<tCuotas> Listar()
        {
            return new CuotasDAO().Listar();
        }

        private void llenarLista(string idpago, string idcuota, double abono, double saldo)
        {
            DetallePago dato = new DetallePago();
            dato.IdPago = idpago;
            dato.IdCuota = idcuota;
            dato.Abono = abono;
            dato.Saldo = saldo;

            listaDetallePago.Add(dato);
        }

        public bool AbonarCuota(string NoCuenta, double? MontoRecibido, string noRecibo, string idcuenta, string noserie,
            string Cliente, string fechapago, string user, string DireccionIP, string NombrePC)
        {
            try
            {
                var ListaCuotas = ListarCuotasPendientes(NoCuenta);
                double? cuotaprogramada;
                double? abonoCuota;
                double? saldo;
                string id;
                double? MRecibido = MontoRecibido;

                Guid idPago = Guid.NewGuid();
                if (ListaCuotas.Count > 0)
                {

                    foreach (var item in ListaCuotas)
                    {

                        id = item.IdCuota.ToString();
                        abonoCuota = item.AbonoCuota;
                        cuotaprogramada = item.MontoCouta;
                        saldo = item.SaldoCouta;
                        double? AbonoPagadoR = 0;
                        double? SaldoPagadaR = 0;

                        if (MontoRecibido > 0)
                        {
                            if (MontoRecibido <= saldo) //CASO 1 CUANDO LA BILLETERA SEA MENOR AL SALDO 
                            {
                                AbonoPagadoR = MontoRecibido;
                                saldo = item.SaldoCouta - MontoRecibido;
                                abonoCuota = AbonoPagadoR;
                                MontoRecibido = MontoRecibido - MontoRecibido;

                                //AbonoPagadoR = item.SaldoCouta;
                                SaldoPagadaR = saldo;

                                item.MontoCouta = cuotaprogramada;
                                item.AbonoCuota = abonoCuota;
                                item.SaldoCouta = saldo;



                                bool exito = Actualizar(item);

                            }
                            else if (saldo <= MontoRecibido) //CASO 2 CUANDO EL SALDO SEA MENOR QUE LA BILLETERA
                            {
                                MontoRecibido = MontoRecibido - saldo;
                                AbonoPagadoR = saldo;
                                SaldoPagadaR = 0;

                                item.MontoCouta = cuotaprogramada;
                                item.AbonoCuota = cuotaprogramada;
                                item.SaldoCouta = 0;

                                bool exito = Actualizar(item);
                            }

                            llenarLista(idPago.ToString(), id, Convert.ToDouble(AbonoPagadoR), Convert.ToDouble(SaldoPagadaR));

                            if (MontoRecibido == 0)
                            {
                                tCredito credito = new tCredito()
                                {
                                    IdPago = idPago,
                                    IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                                    IdCuenta = Guid.Parse(idcuenta),
                                    Serie = noserie,
                                    NoReferencia = noRecibo,
                                    Recibidode = Cliente,
                                    Concepto = "Abono",
                                    MontoRecibido = MRecibido,
                                    MontoPagado = MRecibido,
                                    MontoAcreditado = MRecibido - MontoRecibido,
                                    FechaEfectiva = Convert.ToDateTime(fechapago),
                                    Estado = "Procesado",
                                    FechaRegistro = DateTime.Now,
                                    Usuario = user,
                                    DireccionIP = DireccionIP,
                                    NombrePC = NombrePC
                                };

                                bool exito = InsertarCredito(credito);

                                foreach (var detalle in listaDetallePago)
                                {
                                    tDetallePago detallePago = new tDetallePago()
                                    {
                                        IdDetallePago = Guid.NewGuid(),
                                        IdPago = Guid.Parse(detalle.IdPago),
                                        IdCuota = Guid.Parse(detalle.IdCuota),
                                        AbonoCouta = detalle.Abono,
                                        SaldoCuota = detalle.Saldo,
                                        FechaRegistro = DateTime.Now,
                                        Usuario = user,
                                        DireccionIP = DireccionIP,
                                        NombrePC = NombrePC
                                    };

                                    bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                                }
                            }
                        }// de lo contrario salir del ciclo
                        else
                        {
                            break;
                        }
                    } //fin del for each

                    ////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////
                    if (MontoRecibido > 0)//si sobra saldo y ya se acabaron los saldos
                    {
                        tCredito credito = new tCredito()
                        {
                            IdPago = idPago,
                            IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                            IdCuenta = Guid.Parse(idcuenta),
                            Serie = noserie,
                            NoReferencia = noRecibo,
                            Recibidode = Cliente,
                            Concepto = "Abono",
                            MontoRecibido = MRecibido,
                            MontoPagado = MontoRecibido,
                            MontoAcreditado = MRecibido - MontoRecibido,
                            FechaEfectiva = Convert.ToDateTime(fechapago),
                            Estado = "Procesado",
                            FechaRegistro = DateTime.Now,
                            Usuario = user,
                            DireccionIP = DireccionIP,
                            NombrePC = NombrePC
                        };

                        bool exito = InsertarCredito(credito);

                        foreach (var detalle in listaDetallePago)
                        {
                            tDetallePago detallePago = new tDetallePago()
                            {
                                IdDetallePago = Guid.NewGuid(),
                                IdPago = Guid.Parse(detalle.IdPago),
                                IdCuota = Guid.Parse(detalle.IdCuota),
                                AbonoCouta = detalle.Abono,
                                SaldoCuota = detalle.Saldo,
                                FechaRegistro = DateTime.Now,
                                Usuario = user,
                                DireccionIP = DireccionIP,
                                NombrePC = NombrePC
                            };

                            bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                        }
                    }
                }
                else
                {
                    tCredito credito = new tCredito()
                    {
                        IdPago = idPago,
                        IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                        IdCuenta = Guid.Parse(idcuenta),
                        Serie = noserie,
                        NoReferencia = noRecibo,
                        Recibidode = Cliente,
                        Concepto = "Abono",
                        MontoRecibido = MRecibido,
                        MontoPagado = MRecibido,
                        MontoAcreditado = MRecibido,
                        FechaEfectiva = Convert.ToDateTime(fechapago),
                        Estado = "Procesado",
                        FechaRegistro = DateTime.Now,
                        Usuario = user,
                        DireccionIP = DireccionIP,
                        NombrePC = NombrePC
                    };

                    bool exito = InsertarCredito(credito);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool AbonarCuotaSoloCredito(string NoCuenta, double? MontoRecibido, string noRecibo, string idcuenta, string idmovimiento, string noserie,
            string Cliente, string fechapago, string user, string DireccionIP, string NombrePC)
        {
            try
            {                 
                Guid idPago = Guid.NewGuid();
                ////////////////////////////////////////////////////
                ////////////////////////////////////////////////////
                if (MontoRecibido > 0)
                {
                    tCredito credito = new tCredito()
                    {
                        IdPago = idPago,
                        IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                        IdCuenta = Guid.Parse(idcuenta),
                        IdMovimiento = Guid.Parse(idmovimiento),
                        Serie = noserie,
                        NoReferencia = noRecibo,
                        Recibidode = Cliente,
                        Concepto = "Abono",
                        MontoRecibido = MontoRecibido,
                        MontoPagado = MontoRecibido,
                        MontoAcreditado = MontoRecibido,
                        FechaEfectiva = Convert.ToDateTime(fechapago),
                        Estado = "Procesado",
                        FechaRegistro = DateTime.Now,
                        Usuario = user,
                        DireccionIP = DireccionIP,
                        NombrePC = NombrePC
                    };

                    bool exito = InsertarCredito(credito);  

                }



                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool IngresarDetallePagoyCuotas(string NoCuenta, double? MontoRecibido, Guid IdPago,
             string user, string DireccionIP, string NombrePC)
        {
            try
            {
                var ListaCuotas = ListarCuotasPendientes(NoCuenta);
                double? cuotaprogramada;
                double? abonoCuota;
                double? saldo;
                string id;
                double? MRecibido = MontoRecibido;

                Guid idPago = IdPago;
                if (ListaCuotas.Count > 0)
                {

                    foreach (var item in ListaCuotas)
                    {

                        id = item.IdCuota.ToString();
                        abonoCuota = item.AbonoCuota;
                        cuotaprogramada = item.MontoCouta;
                        saldo = item.SaldoCouta;
                        double? AbonoPagadoR = 0;
                        double? SaldoPagadaR = 0;

                        if (MontoRecibido > 0)
                        {
                            if (MontoRecibido <= saldo) //CASO 1 CUANDO LA BILLETERA SEA MENOR AL SALDO 
                            {
                                AbonoPagadoR = MontoRecibido;
                                saldo = item.SaldoCouta - MontoRecibido;
                                abonoCuota = AbonoPagadoR;
                                MontoRecibido = MontoRecibido - MontoRecibido;

                                //AbonoPagadoR = item.SaldoCouta;
                                SaldoPagadaR = saldo;

                                item.MontoCouta = cuotaprogramada;
                                item.AbonoCuota = abonoCuota;
                                item.SaldoCouta = saldo;



                                bool exito = Actualizar(item);

                            }
                            else if (saldo <= MontoRecibido) //CASO 2 CUANDO EL SALDO SEA MENOR QUE LA BILLETERA
                            {
                                MontoRecibido = MontoRecibido - saldo;
                                AbonoPagadoR = saldo;
                                SaldoPagadaR = 0;

                                item.MontoCouta = cuotaprogramada;
                                item.AbonoCuota = cuotaprogramada;
                                item.SaldoCouta = 0;

                                bool exito = Actualizar(item);
                            }

                            llenarLista(idPago.ToString(), id, Convert.ToDouble(AbonoPagadoR), Convert.ToDouble(SaldoPagadaR));

                            if (MontoRecibido == 0)
                            {
                                //tCredito credito = new tCredito()
                                //{
                                //    IdPago = idPago,
                                //    IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                                //    IdCuenta = Guid.Parse(idcuenta),
                                //    Serie = noserie,
                                //    NoReferencia = noRecibo,
                                //    Recibidode = Cliente,
                                //    Concepto = "Abono",
                                //    MontoRecibido = MRecibido,
                                //    MontoPagado = MRecibido,
                                //    MontoAcreditado = MRecibido - MontoRecibido,
                                //    FechaEfectiva = Convert.ToDateTime(fechapago),
                                //    Estado = "Procesado",
                                //    FechaRegistro = DateTime.Now,
                                //    Usuario = user,
                                //    DireccionIP = DireccionIP,
                                //    NombrePC = NombrePC
                                //};

                                //bool exito = InsertarCredito(credito);

                                foreach (var detalle in listaDetallePago)
                                {
                                    tDetallePago detallePago = new tDetallePago()
                                    {
                                        IdDetallePago = Guid.NewGuid(),
                                        IdPago = Guid.Parse(detalle.IdPago),
                                        IdCuota = Guid.Parse(detalle.IdCuota),
                                        AbonoCouta = detalle.Abono,
                                        SaldoCuota = detalle.Saldo,
                                        FechaRegistro = DateTime.Now,
                                        Usuario = user,
                                        DireccionIP = DireccionIP,
                                        NombrePC = NombrePC
                                    };

                                    bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                                }
                            }
                        }// de lo contrario salir del ciclo
                        else
                        {
                            break;
                        }
                    } //fin del for each

                    ////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////
                    if (MontoRecibido > 0)//si sobra saldo y ya se acabaron los saldos
                    {
                        //tCredito credito = new tCredito()
                        //{
                        //    IdPago = idPago,
                        //    IdTipoTransaccion = Guid.Parse("7C3E964A-E460-495A-8BA6-FA30FE7B91B3"),
                        //    IdCuenta = Guid.Parse(idcuenta),
                        //    Serie = noserie,
                        //    NoReferencia = noRecibo,
                        //    Recibidode = Cliente,
                        //    Concepto = "Abono",
                        //    MontoRecibido = MRecibido,
                        //    MontoPagado = MontoRecibido,
                        //    MontoAcreditado = MRecibido - MontoRecibido,
                        //    FechaEfectiva = Convert.ToDateTime(fechapago),
                        //    Estado = "Procesado",
                        //    FechaRegistro = DateTime.Now,
                        //    Usuario = user,
                        //    DireccionIP = DireccionIP,
                        //    NombrePC = NombrePC
                        //};

                        //bool exito = InsertarCredito(credito);

                        foreach (var detalle in listaDetallePago)
                        {
                            tDetallePago detallePago = new tDetallePago()
                            {
                                IdDetallePago = Guid.NewGuid(),
                                IdPago = Guid.Parse(detalle.IdPago),
                                IdCuota = Guid.Parse(detalle.IdCuota),
                                AbonoCouta = detalle.Abono,
                                SaldoCuota = detalle.Saldo,
                                FechaRegistro = DateTime.Now,
                                Usuario = user,
                                DireccionIP = DireccionIP,
                                NombrePC = NombrePC
                            };

                            bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                        }
                    }
                }
                else
                {
                    //esto es para cuando no hay cuotas pendientes pero se recibe abono
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool IngresarDetallePagoyCuotas2(string NoCuenta, double? MontoRecibido, Guid IdPago,
             string user, string DireccionIP, string NombrePC)
        {
            try
            {

                var lstMovimientos = new MovimientoBLL().ListarMovimientosXCuenta(NoCuenta);
                if (lstMovimientos.Count > 0)
                {
                    foreach (var item in lstMovimientos)
                    {
                        var ListaCuotas = ListarCuotasPendientes2(NoCuenta, item.NoMovimiento);

                        double? cuotaprogramada;
                        double? abonoCuota;
                        double? saldo;
                        string id;
                        double? MRecibido = MontoRecibido;

                        Guid idPago = IdPago;
                        if (ListaCuotas.Count > 0)
                        {
                            foreach (var cuotas in ListaCuotas)
                            {

                                id = cuotas.IdCuota.ToString();
                                abonoCuota = cuotas.AbonoCuota;
                                cuotaprogramada = cuotas.MontoCouta;
                                saldo = cuotas.SaldoCouta;
                                double? AbonoPagadoR = 0;
                                double? SaldoPagadaR = 0;

                                if (MontoRecibido > 0)
                                {
                                    if (MontoRecibido <= saldo) //CASO 1 CUANDO LA BILLETERA SEA MENOR AL SALDO 
                                    {
                                        AbonoPagadoR = MontoRecibido;
                                        saldo = cuotas.SaldoCouta - MontoRecibido;
                                        abonoCuota = AbonoPagadoR;
                                        MontoRecibido = MontoRecibido - MontoRecibido;

                                        //AbonoPagadoR = item.SaldoCouta;
                                        SaldoPagadaR = saldo;

                                        cuotas.MontoCouta = cuotaprogramada;
                                        cuotas.AbonoCuota = abonoCuota;
                                        cuotas.SaldoCouta = saldo;



                                        bool exito = Actualizar(cuotas);

                                    }
                                    else if (saldo <= MontoRecibido) //CASO 2 CUANDO EL SALDO SEA MENOR QUE LA BILLETERA
                                    {
                                        MontoRecibido = MontoRecibido - saldo;
                                        AbonoPagadoR = saldo;
                                        SaldoPagadaR = 0;

                                        cuotas.MontoCouta = cuotaprogramada;
                                        cuotas.AbonoCuota = cuotaprogramada;
                                        cuotas.SaldoCouta = 0;

                                        bool exito = Actualizar(cuotas);
                                    }

                                    llenarLista(idPago.ToString(), id, Convert.ToDouble(AbonoPagadoR), Convert.ToDouble(SaldoPagadaR));

                                    if (MontoRecibido == 0)
                                    {

                                        foreach (var detalle in listaDetallePago)
                                        {
                                            tDetallePago detallePago = new tDetallePago()
                                            {
                                                IdDetallePago = Guid.NewGuid(),
                                                IdPago = Guid.Parse(detalle.IdPago),
                                                IdCuota = Guid.Parse(detalle.IdCuota),
                                                AbonoCouta = detalle.Abono,
                                                SaldoCuota = detalle.Saldo,
                                                FechaRegistro = DateTime.Now,
                                                Usuario = user,
                                                DireccionIP = DireccionIP,
                                                NombrePC = NombrePC
                                            };

                                            bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                                        }
                                    }
                                }// de lo contrario salir del ciclo
                                else
                                {
                                    break;
                                }
                            } //fin del for each

                            ////////////////////////////////////////////////////
                            ////////////////////////////////////////////////////
                            if (MontoRecibido > 0)//si sobra saldo y ya se acabaron los saldos
                            {

                                foreach (var detalle in listaDetallePago)
                                {
                                    tDetallePago detallePago = new tDetallePago()
                                    {
                                        IdDetallePago = Guid.NewGuid(),
                                        IdPago = Guid.Parse(detalle.IdPago),
                                        IdCuota = Guid.Parse(detalle.IdCuota),
                                        AbonoCouta = detalle.Abono,
                                        SaldoCuota = detalle.Saldo,
                                        FechaRegistro = DateTime.Now,
                                        Usuario = user,
                                        DireccionIP = DireccionIP,
                                        NombrePC = NombrePC
                                    };

                                    bool exitoDetalle = new DetallePagoBLL().Insertar(detallePago);
                                }
                            }
                        }

                    }

                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public tCuotas ObtenerPorIdCuota(Guid idCuota)
        {
            return new CuotasDAO().ObtenerPorIdCuota(idCuota);
        }

        public List<tCuotas> ObtenerPorIdMovimiento(Guid idCuota)
        {
            return new CuotasDAO().ObtenerPorIdMovimiento(idCuota);
        }

        public List<tCuotas> ListarCuotasPendientes(string NoCuenta)
        {
            return new CuotasDAO().ListarCuotasPendientes(NoCuenta);
        }

        public List<tCuotas> ListarCuotasPendientes2(string NoCuenta, string NoMovimiento)
        {
            return new CuotasDAO().ListarCuotasPendientes2(NoCuenta, NoMovimiento);
        }

        public List<SP_CuotasPendientes_Result> ObtenerCuotasPendientes(Guid idCliente)
        {
            return new CuotasDAO().ObtenerCuotasPendientes(idCliente);
        }

        public List<SP_ListadeCobroXColector_Result> ObtenerListadeCobroxColector(Guid idUsuario, string fecha)
        {
            return new CuotasDAO().ObtenerListadeCobroxColector(idUsuario, fecha);
        }

        public bool InsertarCredito(tCredito entidad)
        {
            return new CuotasDAO().InsertarCredito(entidad);
        }

        public bool ActualizarCredito(tCredito entidad)
        {
            return new CuotasDAO().ActualizarCredito(entidad);
        }

        public bool EliminarListaCuotas(List<tMovimientos> lstMovimientos)
        {
            return new CuotasDAO().EliminarListaCuotas(lstMovimientos);
        }

    }
}
