using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;
namespace AccionaSR.Negocio
{
    public class MovimientoBLL
    {
        public bool Insertar(tMovimientos entidad)
        {
            return new MovimientosDAO().Insertar(entidad);
        }

        public bool Actualizar(tMovimientos entidad)
        {
            return new MovimientosDAO().Actualizar(entidad);
        }

        public bool Eliminar(tMovimientos entidad)
        {
            return new MovimientosDAO().Eliminar(entidad);
        }

        public tMovimientos CopiarEntidad(tMovimientos entidad)
        {
            return new MovimientosDAO().CopiarEntidad(entidad);
        }

        public List<tMovimientos> Listar()
        {
            return new MovimientosDAO().Listar();
        }
        public List<tMovimientos> ListarActivos()
        {
            return new MovimientosDAO().ListarActivos();
        }

        public List<tMovimientos> ListarMovimientosHijos(Guid idpadre)
        {
            return new MovimientosDAO().ListarMovimientosHijos(idpadre);
        }

        public tMovimientos ObtenerPorIdMovimiento(Guid idMovimiento)
        {
            return new MovimientosDAO().ObtenerPorIdMovimiento(idMovimiento);
        }

        public List<tMovimientos> ObtenerPorNoMovimiento(string noMovimiento)
        {
            return new MovimientosDAO().ObtenerPorNoMovimiento(noMovimiento);
        }

        public List<tMovimientos> ObtenerPorNoMovimientoContain(string noMovimiento)
        {
            return new MovimientosDAO().ObtenerPorNoMovimientoContain(noMovimiento);
        }

        public int ObtenerMaxMovimiento()
        {
            return new MovimientosDAO().ObtenerMaximoMovimiento();
        }

        public tCuotas ObtenerFechaVencimientoxMovimiento(Guid IdMovimiento)
        {
            return new MovimientosDAO().ObtenerMaximaFecha(IdMovimiento);
        }

        public DateTime FechaCalendario(DateTime fecha)
        {
            DateTime fechaValida;
            string idFeriado = string.Empty;             

            List<Feriados> lstDiasFeriados = new List<Feriados>();
            lstDiasFeriados = new FeriadoBLL().Listar();

            var query = (from item in lstDiasFeriados
                        where item.Dia.Equals(fecha.Day) && item.Mes.Equals(fecha.Month)
                             select item).ToList();

            //if (string.IsNullOrEmpty(lstDiasFeriados.Where(x => x.Dia == fecha.Day && x.Mes == fecha.Month).FirstOrDefault().IdFeriado.ToString()))              
            //    idFeriado = "";            
            //else
            //    idFeriado = lstDiasFeriados.Where(x => x.Dia == fecha.Day && x.Mes == fecha.Month).FirstOrDefault().IdFeriado.ToString();


            if (query.Count() == 0)
            {
                lstDiasFeriados.Clear();
                return fechaValida = fecha; 
                
            }
            else
            {
                lstDiasFeriados.Clear();
                return FechaCalendario(fecha.AddDays(1));
            }
        }

        /// <summary>
        /// Genera el debito de la prima para ser cancelado en una sola cuota
        /// </summary>
        /// <param name="movimientoActual"></param>
        /// <param name="idMovimiento"></param>
        /// <returns></returns>
        public bool GenerarCuotaPrima(tMovimientos movimientoActual, Guid idMovimiento)
        {
            tCuotas nuevaCuota = new tCuotas();
            tMovimientos Movimiento = new tMovimientos();   
            
            Movimiento = ObtenerPorIdMovimiento(idMovimiento);

            //string diaFeriado;
            //List<Feriados> lstDiasFeriados = new List<Feriados>();
            //lstDiasFeriados = new FeriadoBLL().Listar();
            //Guid idFeriado = new Guid();
            //double? saldoTotal = Movimiento.Saldo + Movimiento.ComisionDesembolso;
            //double? cuota = Movimiento.MontoTransaccion * (Convert.ToDouble(Movimiento.Abono) / 100);

            nuevaCuota.IdCuota = Guid.NewGuid();
            nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;              
            nuevaCuota.FechaCouta = Movimiento.FechaEfectiva;
            nuevaCuota.MontoCouta = Movimiento.MontoTransaccion;
            nuevaCuota.AbonoCuota = 0;
            nuevaCuota.SaldoCouta = Movimiento.MontoTransaccion;
            nuevaCuota.EstadoCuota = "Vigente";
            nuevaCuota.FechaRegistro = DateTime.Now;
            nuevaCuota.Usuario = Movimiento.Usuario;
            nuevaCuota.DireccionIP = Movimiento.DireccionIP;
            nuevaCuota.NombrePC = Movimiento.NombrePC;

            bool exito = new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
            if (exito)
                return true;
            else
                return false;  
            
        }
        public bool GenerarCuotasNuevas(tMovimientos movimientoActual, Guid idMovimiento)
        {               
            tCuotas nuevaCuota = new tCuotas();
            tMovimientos Movimiento = new tMovimientos();

            DateTime fechacuota;
            Movimiento = ObtenerPorIdMovimiento(idMovimiento);

            //string diaFeriado;
            //List<Feriados> lstDiasFeriados = new List<Feriados>();
            //lstDiasFeriados = new FeriadoBLL().Listar();
            //Guid idFeriado = new Guid();
            //double? saldoTotal = Movimiento.Saldo + Movimiento.ComisionDesembolso;
            double? cuota = Movimiento.Saldo / Convert.ToDouble(Movimiento.NoCuotas);
            switch (Movimiento.tFrecuencia.Descripcion)
            {   
                case "Diario":
                    for (int i = 1; i <= Convert.ToInt32(movimientoActual.NoCuotas) ; i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;

                        //idFeriado = lstDiasFeriados.Where(x => x.Dia == Movimiento.FechaEfectiva.Value.AddDays(1).Day && x.Mes == Movimiento.FechaEfectiva.Value.AddDays(1).Month).FirstOrDefault().IdFeriado;
                        fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                        fechacuota = FechaCalendario(fechacuota);
                        fechacuota = fechacuota.DayOfWeek.ToString() == "Saturday" ? fechacuota.AddDays(2) : fechacuota;
                        //diaFeriado = lstDiasFeriados.Where((x => x.Dia == fechacuota.Day && x.Mes == fechacuota.Month)).FirstOrDefault().Descripcion != "" ? fechacuota.AddDays(1).ToString() : fechacuota.ToString();
                        //fechacuota = fechacuota.ToString() != diaFeriado ? fechacuota : fechacuota.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;
                        
                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;    
                        
                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    //generar cuotas de intereses
                    nuevaCuota = new tCuotas();
                    double? cuotaDetalle;
                    if (Movimiento.tMovimientos1.FirstOrDefault() == null)
                        cuotaDetalle = Movimiento.Saldo / Convert.ToDouble(Movimiento.NoCuotas);
                    else
                        cuotaDetalle = Movimiento.tMovimientos1.FirstOrDefault().Saldo / Convert.ToDouble(Movimiento.NoCuotas);
                   
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.tMovimientos1.FirstOrDefault() == null ? Movimiento.IdMovimiento : Movimiento.tMovimientos1.FirstOrDefault().IdMovimiento;

                        fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(1);
                        fechacuota = FechaCalendario(fechacuota);
                        fechacuota = fechacuota.DayOfWeek.ToString() == "Saturday" ? fechacuota.AddDays(2) : fechacuota;
                        movimientoActual.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(1);
                            movimientoActual.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuotaDetalle;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuotaDetalle;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    var fechaVencimiento = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    Movimiento.FechaVencimiento = fechaVencimiento;
                    bool exito = Actualizar(Movimiento);

                    if (Movimiento.tMovimientos1.Count > 0)
                    {
                        Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento = fechaVencimiento;
                        bool exito1 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());
                    }                        
                    else
                    {
                        Movimiento.tMovimientos2.FechaVencimiento = fechaVencimiento;
                        bool exito1 = Actualizar(Movimiento.tMovimientos2);
                    }
                    //Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    //var fechaVencimiento = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    //Movimiento.FechaVencimiento = fechaVencimiento;
                    //bool exito = Actualizar(Movimiento);

                    //Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento =  fechaVencimiento;                    
                    //bool exito1 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());
                   

                    break;
                case "Semanal":

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    for (int i = 1; i <= Convert.ToInt32(movimientoActual.NoCuotas) ; i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;

                        fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7);
                        fechacuota = FechaCalendario(fechacuota);
                        if (fechacuota.DayOfWeek.ToString() == "Saturday")
                            fechacuota = fechacuota.AddDays(9);
                        else if (fechacuota.AddDays(7).DayOfWeek.ToString() == "Sunday")
                            fechacuota = fechacuota.AddDays(8);
                        

                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            fechacuota = FechaCalendario(fechacuota);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;
                        
                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;    
                        
                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    //generar cuotas de intereses
                    nuevaCuota = new tCuotas();

                    double? cuotaSemanal;
                    if (Movimiento.tMovimientos1.FirstOrDefault() == null)
                        cuotaSemanal = Movimiento.Saldo / Convert.ToDouble(Movimiento.NoCuotas);
                    else
                        cuotaSemanal = Movimiento.tMovimientos1.FirstOrDefault().Saldo / Convert.ToDouble(Movimiento.NoCuotas);

                    //double? cuotaSemanal = Movimiento.tMovimientos1.FirstOrDefault().Saldo / Convert.ToDouble(Movimiento.NoCuotas);

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        //nuevaCuota.IdMovimiento = Movimiento.tMovimientos1.FirstOrDefault().IdMovimiento;
                        nuevaCuota.IdMovimiento = Movimiento.tMovimientos1.FirstOrDefault() == null ? Movimiento.IdMovimiento : Movimiento.tMovimientos1.FirstOrDefault().IdMovimiento;

                        //if (Movimiento.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Saturday")
                        //    fechacuota = Movimiento.FechaEfectiva.Value.AddDays(9);
                        //else if (Movimiento.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Sunday")
                        //    fechacuota = Movimiento.FechaEfectiva.Value.AddDays(8);
                        //else
                        //    fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7);

                        fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7);
                        fechacuota = FechaCalendario(fechacuota);
                        if (fechacuota.DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(9);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(8);

                        //fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Saturday" ? movimientoActual.FechaEfectiva.Value.AddDays(2) : movimientoActual.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        //if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        //{
                        //    fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(1);
                        //    movimientoActual.FechaEfectiva = fechacuota;
                        //}
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            fechacuota = FechaCalendario(fechacuota);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuotaSemanal;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuotaSemanal;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    var fechaVencimiento1 = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    Movimiento.FechaVencimiento = fechaVencimiento1;
                    bool exito2 = Actualizar(Movimiento);

                    if (Movimiento.tMovimientos1.Count > 0)
                    {
                        Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento = fechaVencimiento1;
                        bool exito3 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());
                    }                        
                    else
                    {
                        Movimiento.tMovimientos2.FechaVencimiento = fechaVencimiento1;
                        bool exito3 = Actualizar(Movimiento.tMovimientos2);
                    }
                        

                    break;
                case "Quincenal":

                    for (int i = 1; i <= Convert.ToInt32(movimientoActual.NoCuotas) ; i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(17);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(16);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(15);
                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;
                        
                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;    
                        
                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    //generar cuotas de intereses
                    nuevaCuota = new tCuotas();
                    double? cuotaQuincenal = Movimiento.tMovimientos1.FirstOrDefault().Saldo / Convert.ToDouble(Movimiento.NoCuotas);
                   
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.tMovimientos1.FirstOrDefault().IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(17);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(16);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(15);
                        //fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday" ? movimientoActual.FechaEfectiva.Value.AddDays(2) : movimientoActual.FechaEfectiva.Value.AddDays(1);
                        movimientoActual.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(1);
                            movimientoActual.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuotaQuincenal;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuotaQuincenal;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    var fechaVencimiento2 = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    Movimiento.FechaVencimiento = fechaVencimiento2;
                    bool exito4 = Actualizar(Movimiento);

                    if (Movimiento.tMovimientos1.Count > 0)
                    {
                        Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento = fechaVencimiento2;
                        bool exito5 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());
                    }                        
                    else
                    {
                        Movimiento.tMovimientos2.FechaVencimiento = fechaVencimiento2;
                        bool exito5 = Actualizar(Movimiento.tMovimientos2);
                    }
                    //Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    //var fechaVencimiento2 = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    //Movimiento.FechaVencimiento = fechaVencimiento2;
                    //Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento =  fechaVencimiento2;

                    //bool exito4 = Actualizar(Movimiento);
                    //bool exito5 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());

                    break;
                case "Mensual":
                    for (int i = 1; i <= Convert.ToInt32(movimientoActual.NoCuotas) ; i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(32);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(31);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(30);
                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;
                        
                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;    
                        
                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    //generar cuotas de intereses
                    nuevaCuota = new tCuotas();
                    double? cuotaMensual = Movimiento.tMovimientos1.FirstOrDefault().Saldo / Convert.ToDouble(Movimiento.NoCuotas);
                   
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.tMovimientos1.FirstOrDefault().IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(32);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(31);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(30);
                        //fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday" ? movimientoActual.FechaEfectiva.Value.AddDays(2) : movimientoActual.FechaEfectiva.Value.AddDays(1);
                        movimientoActual.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = movimientoActual.FechaEfectiva.Value.AddDays(1);
                            movimientoActual.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuotaMensual;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuotaMensual;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    var fechaVencimiento3 = ObtenerFechaVencimientoxMovimiento(idMovimiento).FechaCouta;

                    Movimiento.FechaVencimiento = fechaVencimiento3;
                    bool exito6 = Actualizar(Movimiento);

                    if (Movimiento.tMovimientos1.Count > 0)
                    {
                        Movimiento.tMovimientos1.FirstOrDefault().FechaVencimiento = fechaVencimiento3;
                        bool exito7 = Actualizar(Movimiento.tMovimientos1.FirstOrDefault());
                    }                        
                    else
                    {
                        Movimiento.tMovimientos2.FechaVencimiento = fechaVencimiento3;
                        bool exito7 = Actualizar(Movimiento.tMovimientos2);
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorIdMovimiento(Guid idMovimiento)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorIdMovimiento(idMovimiento);
        }

        public List<vw_DatosCredito2> ObtenerDatosGeneralesPorIdMovimiento2(Guid idMovimiento)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorIdMovimiento2(idMovimiento);
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorCedula(string cedula)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorCedula(cedula);
        }

        public List<vw_DatosCredito2> ObtenerDatosGeneralesPorCedula2(string cedula)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorCedula2(cedula);
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorNombre(string nombre)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorNombre(nombre);
        }

        public List<vw_DatosCredito> ObtenerDatosGeneralesPorNoCuenta(string nocuenta)
        {
            return new MovimientosDAO().ObtenerDatosGeneralesPorNoCuenta(nocuenta);
        }

        public List<SP_CalculoCuotas_Result> ObtenerListaCuotasPorIdMovimiento(Guid idMovimiento)
        {
            return new MovimientosDAO().ObtenerListaCuotasPorIdMovimiento(idMovimiento);
        }

        public List<tMovimientos> ListarMovimientosXCartera()
        {
            return new MovimientosDAO().ListarMovimientosXCartera();
        }

        public List<tMovimientos> ListarMovimientosXCuenta(string NoCuenta)
        {
            return new MovimientosDAO().ListarMovimientosXCuenta(NoCuenta);
        }

        public List<OrigenFondos> ListarOrigenFondos()
        {
            return new MovimientosDAO().ListarOrigendeFondos();
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorNoPlaca(string noPlaca)
        {
            return new MovimientosDAO().ObtenerDatosVehiculoPorNoPlaca(noPlaca);
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorIdentificacion(string Identificacion)
        {
            return new MovimientosDAO().ObtenerDatosVehiculoPorIdentificacion(Identificacion);
        }

        public List<vwDatosVehiculos> ObtenerDatosVehiculoPorCodigo(string Codigo)
        {
            return new MovimientosDAO().ObtenerDatosVehiculoPorCodigo(Codigo);
        }

        public List<tMovimientos> ListarMovimientosXNoCuentaConHijos (string NoCuenta)
        {
            return new MovimientosDAO().ListarMovimientosXNoCuentaConHijos(NoCuenta);
        }

        public List<tMovimientos> ListarMovimientosXIdCuenta(Guid IdCuenta)
        {
            return new MovimientosDAO().ListarMovimientosXIdCuenta(IdCuenta);
        }

        public bool GenerarCuotasSarandear(Guid idMovimiento)
        {
            tCuotas nuevaCuota = new tCuotas();
            tMovimientos Movimiento = new tMovimientos();

            DateTime fechacuota;
            Movimiento = ObtenerPorIdMovimiento(idMovimiento);

            //string diaFeriado;
            //List<Feriados> lstDiasFeriados = new List<Feriados>();
            //lstDiasFeriados = new FeriadoBLL().Listar();
            //Guid idFeriado = new Guid();
            //double? saldoTotal = Movimiento.Saldo + Movimiento.ComisionDesembolso;
            double? cuota = Movimiento.Saldo / Convert.ToDouble(Movimiento.NoCuotas);
            switch (Movimiento.tFrecuencia.Descripcion)
            {
                case "Diario":
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;

                        //idFeriado = lstDiasFeriados.Where(x => x.Dia == Movimiento.FechaEfectiva.Value.AddDays(1).Day && x.Mes == Movimiento.FechaEfectiva.Value.AddDays(1).Month).FirstOrDefault().IdFeriado;
                        fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                        fechacuota = FechaCalendario(fechacuota);
                        fechacuota = fechacuota.DayOfWeek.ToString() == "Saturday" ? fechacuota.AddDays(2) : fechacuota;
                        //diaFeriado = lstDiasFeriados.Where((x => x.Dia == fechacuota.Day && x.Mes == fechacuota.Month)).FirstOrDefault().Descripcion != "" ? fechacuota.AddDays(1).ToString() : fechacuota.ToString();
                        //fechacuota = fechacuota.ToString() != diaFeriado ? fechacuota : fechacuota.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }                      


                    break;
                case "Semanal":

                    Movimiento = ObtenerPorIdMovimiento(idMovimiento);
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;

                        fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7);
                        fechacuota = FechaCalendario(fechacuota);
                        if (fechacuota.DayOfWeek.ToString() == "Saturday")
                            fechacuota = fechacuota.AddDays(9);
                        else if (fechacuota.AddDays(7).DayOfWeek.ToString() == "Sunday")
                            fechacuota = fechacuota.AddDays(8);


                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(7).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            fechacuota = FechaCalendario(fechacuota);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    
                    break;
                case "Quincenal":

                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(17);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(16);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(15);
                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(15).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                    
                    break;
                case "Mensual":
                    for (int i = 1; i <= Convert.ToInt32(Movimiento.NoCuotas); i++)
                    {
                        nuevaCuota.IdCuota = Guid.NewGuid();
                        nuevaCuota.IdMovimiento = Movimiento.IdMovimiento;
                        if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(32);
                        else if (Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Sunday")
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(31);
                        else
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(30);
                        //fechacuota = Movimiento.FechaEfectiva.Value.AddDays(30).DayOfWeek.ToString() == "Saturday" ? Movimiento.FechaEfectiva.Value.AddDays(2) : Movimiento.FechaEfectiva.Value.AddDays(1);
                        Movimiento.FechaEfectiva = fechacuota;
                        if (fechacuota.DayOfWeek.ToString() == "Sunday")
                        {
                            fechacuota = Movimiento.FechaEfectiva.Value.AddDays(1);
                            Movimiento.FechaEfectiva = fechacuota;
                        }
                        nuevaCuota.FechaCouta = fechacuota;

                        nuevaCuota.MontoCouta = cuota;
                        nuevaCuota.AbonoCuota = 0;
                        nuevaCuota.SaldoCouta = cuota;
                        nuevaCuota.FechaRegistro = DateTime.Now;
                        nuevaCuota.Usuario = Movimiento.Usuario;
                        nuevaCuota.DireccionIP = Movimiento.DireccionIP;
                        nuevaCuota.NombrePC = Movimiento.NombrePC;

                        new CuotaBLL().Insertar(nuevaCuota); //Inserta las cuotas que aún no existan
                    }

                   
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}
