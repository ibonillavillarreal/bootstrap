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

namespace Acciona.Pagos
{
    public partial class Creditos : Pagina
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

        public tDetallePago DetallePagoActual
        {
            get
            {
                if (Session["DetallePagoActuales"] == null)
                    Session["DetallePagoActuales"] = new tDetallePago();
                return (tDetallePago)Session["DetallePagoActuales"];
            }
            set
            {
                Session["DetallePagoActuales"] = value;
            }
        }

        public tClienteCuenta ClienteCuentaActual
        {
            get
            {
                if (Session["ClienteCuentaActuales"] == null)
                    Session["ClienteCuentaActuales"] = new tClienteCuenta();
                return (tClienteCuenta)Session["ClienteCuentaActuales"];
            }
            set
            {
                Session["ClienteCuentaActuales"] = value;
            }
        }

        public tMovimientos MovimientoActual
        {
            get
            {
                if (Session["MovimientoActuales"] == null)
                    Session["MovimientoActuales"] = new tMovimientos();
                return (tMovimientos)Session["MovimientoActuales"];
            }
            set
            {
                Session["MovimientoActuales"] = value;
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
        //protected void imbImprimir_Click(object sender, ImageClickEventArgs e)
        //{
        //    Button b = sender as Button;
        //    if (b != null)
        //    {
        //        LitNoCuenta.Text = "clicked!!";
        //    }
        //}



        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Reportes/Crystal/Reporte.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                if (ddlTipo.SelectedValue == "0")     //placa
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorNoPlaca(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;
                        //txtNoTarjeta.Text = datosCredito.LastOrDefault().NoTarjeta;
                        //txtNocuenta.Text = datosCredito.LastOrDefault().NoCuenta;
                        //txtMonto.Text = string.Format("{0:0,0.00}", datosCredito.LastOrDefault().monto.ToString());
                        //txtNoCuotas.Text = datosCredito.LastOrDefault().NoCuotas.ToString();
                        //txtPlazo.Text = datosCredito.LastOrDefault().PlazoMeses.ToString();
                        //txtColector.Text = datosCredito.LastOrDefault().colector;

                        //txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso.ToString();
                        //txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso != null ? ((DateTime)datosCredito.LastOrDefault().fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                        //txtFechaVencimiento.Text = datosCredito.LastOrDefault().fechav;

                        //var idCliente = new ClienteBLL().ObtenerPorIdentificacion(datosCredito.LastOrDefault().NoIdentificacion);

                        //var dsCuentas = new CuotaBLL().ObtenerCuotasPendientes(idCliente.FirstOrDefault().IdCliente);
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
                        //txtNoTarjeta.Text = datosCredito.LastOrDefault().NoTarjeta;
                        //txtNocuenta.Text = datosCredito.LastOrDefault().NoCuenta;
                        //txtMonto.Text = string.Format("{0:0,0.00}", datosCredito.LastOrDefault().monto.ToString());
                        //txtNoCuotas.Text = datosCredito.LastOrDefault().NoCuotas.ToString();
                        //txtPlazo.Text = datosCredito.LastOrDefault().PlazoMeses.ToString();
                        //txtColector.Text = datosCredito.LastOrDefault().colector;

                        ////txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso;
                        //txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso != null ? ((DateTime)datosCredito.LastOrDefault().fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                        //txtFechaVencimiento.Text = datosCredito.LastOrDefault().fechav;

                        //var idCliente = new ClienteBLL().ObtenerPorIdentificacion(datosCredito.LastOrDefault().NoIdentificacion);

                        //var dsCuentas = new CuotaBLL().ObtenerCuotasPendientes(idCliente.FirstOrDefault().IdCliente);
                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else  //codigo
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
                string id = gvDetalle.Rows[index].ToString();

                var listacredito = new MovimientoBLL().ObtenerDatosGeneralesPorIdMovimiento2(Guid.Parse(id)).FirstOrDefault();

                txtNombre.Text = listacredito.NombreCompleto;
                txtIdentificacion.Text = listacredito.NoIdentificacion;
                txtNoTarjeta.Text = listacredito.NoTarjeta;
                txtNocuenta.Text = listacredito.NoCuenta;
                txtMonto.Text = string.Format("{0:0,0.00}", listacredito.monto.ToString());
                txtNoCuotas.Text = listacredito.NoCuotas.ToString();
                txtPlazo.Text = listacredito.PlazoMeses.ToString();
                txtColector.Text = listacredito.colector;

                //txtFechaDesembolso.Text = listacredito.fechadesembolso;
                txtFechaDesembolso.Text = listacredito.fechadesembolso != null ? ((DateTime)listacredito.fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                //txtFechaVencimiento.Text = listacredito.fechav;

                var idCliente = new ClienteBLL().ObtenerPorIdentificacion(listacredito.NoIdentificacion);
                hfIdCliente.Value = idCliente.ToString();
                //var dsCuentas = new CuotaBLL().ObtenerCuotasPendientes(idCliente.FirstOrDefault().IdCliente);
                //gvDatos.DataSource = dsCuentas;
                //gvDatos.DataBind();

            }

        }




        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        public void CargarDatosGenerales()
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

                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                string DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                string NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                string fmt = "000000";
                string col1 = txtNombre.Text;  //cliente                           
                string col3 = txtNocuenta.Text;  //nocuenta               
                string col4 = txtSerie.Text.ToUpper();  //serie
                double colum5 = Convert.ToDouble(txtMontoRecibido.Text.Trim());  // monto recibido
                int colum6 = Convert.ToInt16(txtNoRecibo.Text.Trim()); //no recibo     
                string fecha = txtFechaEfectiva.Text;
                string recibo = colum6.ToString(fmt);

                if (VerificarRecibo(recibo, txtSerie.Text.Trim()))
                {

                    MostrarMensaje("Este Recibo ya ha sido procesado!", TipoMensaje.Danger);
                    return;

                }
                else
                {
                    var idCuenta = new ClienteCuentaBLL().ObtenerPorNoCuenta(col3).FirstOrDefault().IdCuenta;

                    string idCuentastr = idCuenta.ToString();

                    bool exito = new CuotaBLL().AbonarCuota(col3, colum5, recibo, idCuentastr, col4, col1, fecha, user.Login, DireccionIP, NombrePC);
                    //actualizar el procesado por cada linea de rutacobro
                    if (!exito)
                    {
                        Response.Write("Error al guardar el recibo no" + colum6);
                    }

                    MostrarMensaje("Los pagos se han guardado satisfactoriamente!", TipoMensaje.Success);
                }


            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private tDetallePago LlenarObjetoPrincipal()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tDetallePago DetallePagoNuevo = new tDetallePago()
            {

                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            DetallePagoActual = DetallePagoNuevo;

            return DetallePagoActual;
        }

        private tDetallePago LlenarObjetoDetalle()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tDetallePago DetallePagoNuevo = new tDetallePago()
            {

                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            return DetallePagoNuevo;
        }

        //public string NoMovimientoMaximo()
        //{

        //    string fmt = "000000";
        //    int consecutivo = new MovimientoBLL().ObtenerMaxMovimiento() + 1;
        //    string numero = consecutivo.ToString(fmt);

        //    return numero;
        //}


        private void LimpiarControles()
        {
            txtBuscar.Text = string.Empty;
            txtColector.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtNocuenta.Text = string.Empty;
            txtNoTarjeta.Text = string.Empty;
            txtFechaDesembolso.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtNoCuotas.Text = string.Empty;
            //txtFechaVencimiento.Text = string.Empty;
            txtPlazo.Text = string.Empty;

            txtSerie.Text = string.Empty;
            txtNoRecibo.Text = string.Empty;
            txtMontoRecibido.Text = string.Empty;
            txtFechaEfectiva.Text = string.Empty;


            //gvDatos.DataSource = null;
            //gvDatos.DataBind();

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
            ddlTipoTransaccion.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoTransaccion.DataSource = new RecibosBLL().ListarTipoTransacciones();
            ddlTipoTransaccion.DataTextField = "Descripcion";
            ddlTipoTransaccion.DataValueField = "IdTIpoTransaccion";
            ddlTipoTransaccion.DataBind();
        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdMovimiento.Value = string.Empty;
            DetallePagoActual = null;
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