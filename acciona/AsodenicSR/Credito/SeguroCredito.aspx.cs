using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Seguridad;
using Acciona.App_Code;
using AccionaSR.Negocio;
using Newtonsoft.Json;
using SisSegLT.Datos;
using System.Globalization;

namespace Acciona.Credito
{
    public partial class SeguroCredito : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioNegocio"] == null)
                    ViewState["EstadoFormularioNegocio"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioNegocio"];
            }
            set
            {
                ViewState["EstadoFormularioNegocio"] = value;
            }
        }

        public tMovimientos MovimientoPadre
        {
            get
            {
                if (Session["MovimientoActualesP"] == null)
                    Session["MovimientoActualesP"] = new tMovimientos();
                return (tMovimientos)Session["MovimientoActualesP"];
            }
            set
            {
                Session["MovimientoActualesP"] = value;
            }
        }

        public CargosMensuales CargoActual
        {
            get
            {
                if (Session["CargosMensualesActuales"] == null)
                    Session["CargosMensualesActuales"] = new CargosMensuales();
                return (CargosMensuales)Session["CargosMensualesActuales"];
            }
            set
            {
                Session["CargosMensualesActuales"] = value;
            }
        }




        #endregion Propiedades

        #region Eventos

        #region Otros eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LimpiarSesion();
                CargarCombo();
                ModoInicial();

            }
        }

        #endregion Otros eventos

        #region Botones de acción


        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();

        }


        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Reportes/Crystal/Reporte.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                if (ddlTipo.SelectedValue == "0")  //placa
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorNoPlaca(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;

                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else if (ddlTipo.SelectedValue == "1") //identificacion
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorIdentificacion(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;

                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else //codigo
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorCodigo(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;

                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }


            }
        }

        protected void gvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //
                // Se obtiene indice de la row seleccionada
                //
                int index = Convert.ToInt32(e.CommandArgument);

                //
                // Obtengo el id de la entidad que se esta editando
                // en este caso de la entidad tmovimientos
                //
                Guid id = (Guid)gvDetalle.DataKeys[index].Values[0];    //idmovimiento
                hfIdMovimiento.Value = id.ToString();
                var listacredito = new MovimientoBLL().ObtenerDatosGeneralesPorIdMovimiento2(id).FirstOrDefault();
                txtFechaCorte.Text = listacredito.fechadesembolso.ToString();
                txtIdentificacion.Text = listacredito.NoIdentificacion;


                var idCliente = new ClienteBLL().ObtenerPorIdentificacion(listacredito.NoIdentificacion);
                hfIdCliente.Value = idCliente.ToString();

                var listaseguros = new CargosMensualesBLL().ObtenerCargosMensualessPorIdMovimiento(id);

                if (listaseguros.Count > 0)
                {
                   gvDatosSeguro.DataSource = listaseguros;
                   gvDatosSeguro.DataBind(); 
                }


            }

        }

        protected void gvDatosSeguro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //
                // Se obtiene indice de la row seleccionada
                //
                int index = Convert.ToInt32(e.CommandArgument);

                //
                // Obtengo el id de la entidad que se esta editando
                // en este caso de la entidad tmovimientos
                //
                Guid id = (Guid)gvDatosSeguro.DataKeys[index].Values[0];    //idprestamo

                CargoActual = new CargosMensualesBLL().ObtenerPorIdCargosMensuales(id);

                ddlFrecuencia.SelectedValue = CargoActual.IdFrecuencia != null ? CargoActual.IdFrecuencia.ToString() : ddlFrecuencia.SelectedValue;
                ddlTipoTransaccion.SelectedValue = CargoActual.IdTipoTransaccion != null ? CargoActual.IdTipoTransaccion.ToString() : ddlTipoTransaccion.SelectedValue;
                txtFechaCorte.Text = ((DateTime)CargoActual.FechaCorte).ToString("dd/MM/yyyy");
                txtFechaInicio.Text = ((DateTime)CargoActual.FechaInicio).ToString("dd/MM/yyyy");
                txtFechaFin.Text = ((DateTime)CargoActual.FechaFin).ToString("dd/MM/yyyy"); 
                txtObservaciones.Text = CargoActual.Observaciones;
                txtMonto.Text = CargoActual.Monto.ToString();
                EstadoFormulario = Generales.EstadoFormulario.Editar;

            }
        }


        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        public void CargarDatos()
        {
            
        }

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;

        }

        #endregion Modos

        #region Otros métodos

        private bool VerificarRecibo(string noRecibo, string serie)
        {
            var existe = new RecibosDAO().ObtenerPorNoReciboySerie(noRecibo, serie);

            if (existe != null)
                return true;
            else
                return false;
        }

        private void Guardar()
        {
            try
            {
                switch (EstadoFormulario)
                {
                    case Generales.EstadoFormulario.Editar:
                        CargosMensuales cargoNuevo = new CargosMensuales();
                        cargoNuevo.IdPrestamo = CargoActual.IdPrestamo;
                        cargoNuevo.IdTipoTransaccion = Guid.Parse(ddlTipoTransaccion.SelectedValue);
                        cargoNuevo.IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue);
                        cargoNuevo.IdMovimiento = CargoActual.IdMovimiento;
                        cargoNuevo.Monto = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                        cargoNuevo.FechaCorte = DateTime.ParseExact(txtFechaCorte.Text,"dd/MM/yyyy", CultureInfo.InvariantCulture);
                        cargoNuevo.FechaInicio = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        cargoNuevo.FechaFin = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        cargoNuevo.Observaciones = txtObservaciones.Text;
                        cargoNuevo.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                        cargoNuevo.EsActivo = CargoActual.EsActivo;
                        cargoNuevo.FechaRegistro = CargoActual.FechaRegistro;
                        cargoNuevo.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                        cargoNuevo.Usuario = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario.Login;

                        if (new CargosMensualesBLL().Actualizar(cargoNuevo))
                        {
                             MostrarMensaje("El registro se ha actualizado con exito!", TipoMensaje.Success);
                        }
                        else
                            MostrarMensaje("No se pudo actualizar el registro!", TipoMensaje.Danger);


                        ModoInicial();
                        break;
                    default:
                        if (!string.IsNullOrEmpty(hfIdMovimiento.Value) || !string.IsNullOrEmpty(txtFechaInicio.Text))
                        {
                            //tMovimientos nuevoMovimiento = LlenarObjetoMovimiento();
                            //nuevoMovimiento.tMovimientos2 = MovimientoPadre;
                            CargosMensuales nuevosCargos = LlenarObjeto();
                            //nuevosCargos.IdMovimiento = nuevoMovimiento.IdMovimiento;

                            if (new CargosMensualesBLL().Insertar(nuevosCargos))
                            {
                                MostrarMensaje("El registro se ha guardado con exito!", TipoMensaje.Success);
                            }
                            else
                            {
                                MostrarMensaje("No se pudo registrar el cargo!", TipoMensaje.Danger);
                            }

                            LimpiarSesion();
                        }
                        else
                        {
                            MostrarMensaje("Por favor seleccione un elemento o vehiculo!", TipoMensaje.Danger);
                        }
                        break;
                }
                

            }
            catch (Exception ex)
            {          
                throw;
            }

        }

        private CargosMensuales LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");  
            DateTime fecha = Convert.ToDateTime(txtFechaCorte.Text);        
            CargosMensuales nuevoCargoMensual = new CargosMensuales()
            {
                IdPrestamo = Guid.NewGuid(),
                IdTipoTransaccion = Guid.Parse(ddlTipoTransaccion.SelectedValue),
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                IdMovimiento = Guid.Parse(hfIdMovimiento.Value),
                Monto = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                FechaCorte = DateTime.ParseExact(fecha.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaInicio = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaFin = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Observaciones = txtObservaciones.Text,
                EsActivo = true,
                FechaRegistro = DateTime.Now,
                Usuario = user.Login,
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)

            };

            CargoActual = nuevoCargoMensual;

            return CargoActual;
        }

        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }

        private tMovimientos LlenarObjetoMovimiento()
        {

            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            int nomovimiento = 0;
            string no = "";
            var MovimientoPadre = new MovimientoBLL().ObtenerPorIdMovimiento(Guid.Parse(hfIdMovimiento.Value));
            var lstMovimientos = new MovimientoBLL().ListarMovimientosHijos(MovimientoPadre.IdMovimiento);
            if (lstMovimientos.Count == 0)
            {
                no = "01";                
            }
            else
            {
                nomovimiento = Convert.ToInt16(lstMovimientos.LastOrDefault().NoMovimiento) + 1;
                no = nomovimiento.ToString("00");
            }

            var plazo = CalcularMesesDeDiferencia(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text));
            //var fechac = Convert.double(plazo.ToString()) / 30;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tMovimientos movimientoNuevo = new tMovimientos()
            {
                IdMovimiento = Guid.NewGuid(),
                IdMovimientoPadre = MovimientoPadre.IdMovimiento,
                IdPlastico = MovimientoPadre.IdPlastico,
                IdPromotor = MovimientoPadre.IdPromotor,
                IdDesembolsa = MovimientoPadre.IdDesembolsa,
                NoMovimiento = MovimientoPadre.NoMovimiento + "-" + no,
                FechaEfectiva = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                IdColector = MovimientoPadre.IdColector,
                IdTipoTransaccion = Guid.Parse(ddlTipoTransaccion.SelectedValue),
                Flujo = true,
                FechaCorte = DateTime.ParseExact(((DateTime)MovimientoPadre.FechaCorte).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture),                
                NoCuotas = 1,
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                IdOrigen = MovimientoPadre.IdOrigen,
                MontoTransaccion = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Canon = 0,
                Salvamento = 0,
                CuotaProgramada = 0,
                PlazoMeses = double.Parse(plazo.ToString(), System.Globalization.NumberStyles.Currency, Generales.cultura),
                Saldo = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoAnios = 0,
                //PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                FechaVencimiento = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Interes = MovimientoPadre.Interes,
                TipoInteres = MovimientoPadre.TipoInteres,
                Abono = 0,
                EstadoTransaccion = "Activa",
                Moneda = "Dolares",
                Observaciones = "",            
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                FechaRegistro = DateTime.Now,
                Usuario = user.Login
            };

            MovimientoPadre = movimientoNuevo;

            return MovimientoPadre; 
        }



        private void LimpiarControles()
        {
            txtBuscar.Text = string.Empty;
            //txtColector.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtFechaCorte.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            ddlFrecuencia.SelectedIndex = -1;
            ddlTipoTransaccion.SelectedIndex = -1;

            gvDetalle.DataSource = null;
            gvDetalle.DataBind();

        }

        private void LlenarFormulario()
        {
            //hfIdCliente.Value = Cuenta.IdCliente.ToString();
            //hfIdCuenta.Value = Cuenta.IdCuenta.ToString();
            //ddlAprobado.SelectedValue = Cuenta.IdAprobado != null ? Cuenta.IdAprobado.ToString() : ddlAprobado.SelectedValue;
            //ddlTipoCuenta.SelectedValue = Cuenta.IdTipoCuenta != null ? Cuenta.IdTipoCuenta.ToString() : ddlTipoCuenta.SelectedValue;
            //txtNoCuenta.Text = Cuenta.NoCuenta;
            //txtFechaAprobacion.Text = Cuenta.FechaAprobacion != null ? ((DateTime)Cuenta.FechaAprobacion).ToString("dd/MM/yyyy") : string.Empty;
            //txtLimite.Text = string.Format("{0:0,0.00}", Convert.ToDouble(Cuenta.Limite));
            //ddlEstadoCuenta.Text = Cuenta.IdEstadoCuenta != null ? Cuenta.IdEstadoCuenta.ToString() : ddlEstadoCuenta.SelectedValue;

        }

        private void CargarCombo()
        {
            //Combo TipoTransaccion
            ddlTipoTransaccion.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoTransaccion.DataSource = new TipoTransaccionBLL().ObtenerListaTransacciones();
            ddlTipoTransaccion.DataTextField = "Descripcion";
            ddlTipoTransaccion.DataValueField = "IdTipoTransaccion";
            ddlTipoTransaccion.DataBind();

            //tipo de FRECUENCIA
            ddlFrecuencia.SelectedIndex = -1; //Limpia cualquier selección
            ddlFrecuencia.DataSource = new FrecuenciaBLL().ListarActivos();
            ddlFrecuencia.DataTextField = "Descripcion";
            ddlFrecuencia.DataValueField = "IdFrecuencia";
            ddlFrecuencia.DataBind();

        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdMovimiento.Value = string.Empty;
            //DetallePagoActual = null;
            LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            string tipoMensaje = "alert-" + tipo.ToString().ToLower();
            litmensaje.Text = string.Format(@"<div class='alert {0} alert-dismissible' role='alert'>
              <button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>"
             + mensaje +
            @"</div>", tipoMensaje);

            ScriptManager.RegisterStartupScript(this, GetType(), ClientID, @"$(document).ready(function() {
                                                                                window.setTimeout(function() {
                                                                                    $('.alert').fadeTo(500, 0).slideUp(500, function() {
                                                                                        $(this).remove();
                                                                                    });
                                                                                }, 5000);
                                                                            });", true);
        }

        internal enum TipoMensaje
        {
            Info,
            Success,
            Warning,
            Danger
        }

        #endregion Otros métodos



        #endregion Métodos
    }
}