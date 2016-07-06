using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acciona.App_Code;
using AccionaSR.Negocio;
using Microsoft.Reporting.WebForms;
using SisSegLT.Seguridad;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SisSegLT.Datos;
using System.Globalization;

namespace Acciona.Credito
{
    public partial class RecalcularCredito : Pagina
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

        public List<tMovimientos> MovimientosCalcular
        {
            get
            {
                if (Session["MovimientoCalcular"] == null)
                    Session["MovimientoCalcular"] = new List<tMovimientos>();
                return (List<tMovimientos>)Session["MovimientoCalcular"];
            }
            set
            {
                Session["MovimientoCalcular"] = value;
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
                Guid id = Guid.Parse(gvDatos.DataKeys[index].Value.ToString());
                MovimientoActual = new tMovimientos();
                MovimientoActual = new MovimientoBLL().ObtenerPorIdMovimiento(id);

                LimpiarControles();
                //CargarCombo();
                LlenarFormulario();

            }

        }




        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {

                var datosVehiculos = new MovimientoBLL().ObtenerDatosVehiculoPorCodigo(txtBuscar.Text.Trim());
                if (datosVehiculos.Count > 0)
                {
                    litCliente.Text = datosVehiculos.LastOrDefault().NombreCompleto;
                   hfIdMovimiento.Value = datosVehiculos.LastOrDefault().IdMovimiento.ToString();

                   var movimientos = new MovimientosDAO().ObtenerListaPorIdMovimiento(Guid.Parse(hfIdMovimiento.Value));
                   if (movimientos != null)
                   {
                       MovimientosCalcular = movimientos;
                       gvDatos.DataSource = movimientos;
                       gvDatos.DataBind();
                   }
                   
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }
                //var movimientos = new MovimientoBLL().ListarMovimientosXCuenta(txtBuscar.Text.Trim());
                //if (movimientos.Count > 0)
                //{
                //    MovimientosCalcular = movimientos;
                //    gvDatos.DataSource = movimientos;
                //    gvDatos.DataBind();

                //    litCliente.Text = movimientos.FirstOrDefault().tPlastico.tClienteCuenta.Cliente.NombreCompleto;
                //    hfIdCuenta.Value = movimientos.FirstOrDefault().tPlastico.tClienteCuenta.IdCuenta.ToString();
                //}
                //else
                //{
                //    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                //}
              
            }
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                bool exito = false;

                exito = new RecibosBLL().RecalcularMovimientosxCuenta(Guid.Parse(hfIdCuenta.Value), txtBuscar.Text.Trim());

                if (exito)
                {
                    MostrarMensaje("Se ha recalculado la cuenta correctamente!", TipoMensaje.Success);
                }
                else
                {
                    MostrarMensaje("Hubo un error al recalcular la cuenta!", TipoMensaje.Danger);
                }

            }
            catch (Exception)
            {

                MostrarMensaje("Hubo un error al recalcular la cuenta!", TipoMensaje.Danger);
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
            //EstadoFormulario = Generales.EstadoFormulario.Inicial;
            ////litAyuda.Text = "Agregue, edite o deshabilite sucursales"; 

            //if (hfIdCliente.Value != string.Empty || hfIdCliente.Value != "")
            //{
            //    //gvDatos.Visible = true;
            //    //gvDatos.SelectedIndex = -1;
            //    var dsCuentas = new ClienteCuentaBLL().ObtenerCuentaPorIdCliente(Guid.Parse(hfIdCliente.Value.ToString()));
            //    //gvDatos.DataSource = dsCuentas;
            //    //gvDatos.DataBind();
            //}
        }

       


        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                if (!string.IsNullOrEmpty(txtMonto.Text.Trim()))
                {                                                                   
                    //MovimientoActual.IdMovimientoPadre = null;
                    //MovimientoActual.IdPlastico = MovimientoActual.IdPlastico;
                    //if (ddlPromotor.SelectedValue == "")                    
                    //    MovimientoActual.IdPromotor = null;
                    //else
                    //    MovimientoActual.IdPromotor = Guid.Parse(ddlPromotor.SelectedValue);

                    if (ddlDesembolsa.SelectedValue == "")
                       MovimientoActual.IdDesembolsa = null;
                    else
                        MovimientoActual.IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue);
                    
                    //MovimientoActual.NoMovimiento = MovimientoActual.NoMovimiento;
                    MovimientoActual.FechaEfectiva = Convert.ToDateTime(txtFechaMovimiento.Text);

                    //if (ddlColector.SelectedValue == "")
                    //    MovimientoActual.IdColector = null;
                    //else
                    //    MovimientoActual.IdColector = Guid.Parse(ddlColector.SelectedValue); 

                    MovimientoActual.IdTipoTransaccion = MovimientoActual.IdTipoTransaccion;
                    MovimientoActual.NoCuotas = Convert.ToInt16(txtNoCuotas.Text);

                    if (ddlFrecuencia.SelectedValue == "")
                        MovimientoActual.IdFrecuencia = null;
                    else
                        MovimientoActual.IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue);
                    
                    MovimientoActual.MontoTransaccion = double.Parse(txtMonto.Text);
                    //MovimientoActual.Abono = 0;
                    //MovimientoActual.Saldo = double.Parse(txtMonto.Text);
                    //MovimientoActual.PlazoAnios = double.Parse(txtpla.Text);
                    MovimientoActual.PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                    MovimientoActual.Interes = double.Parse(txtInteres.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                    MovimientoActual.TipoInteres = ddlTipoInteres.SelectedItem.ToString();
                    MovimientoActual.EstadoTransaccion = "Activa";
                    //MovimientoActual.Moneda = "Cordoba";
                    MovimientoActual.FechaRegistro = DateTime.Now;
                    MovimientoActual.FechaProceso = DateTime.Now;
                    MovimientoActual.Usuario = user.Login;
                    MovimientoActual.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                    MovimientoActual.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request); 
                                       
                    if (new MovimientoBLL().Actualizar(MovimientoActual))
                    {
                        if (MovimientoActual.tMovimientos1.Count > 0)
                        {

                            //MovimientoActual.tMovimientos1.FirstOrDefault().IdMovimientoPadre = MovimientoActual.tMovimientos1.Where(x => x.IdMovimientoPadre != null).FirstOrDefault().IdMovimiento;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().IdPlastico = MovimientoActual.tMovimientos1.Where(x => x.IdMovimientoPadre != null).FirstOrDefault().IdPlastico;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().IdPromotor = Guid.Parse(ddlPromotor.SelectedValue);
                            MovimientoActual.tMovimientos1.FirstOrDefault().IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue);
                            //MovimientoActual.tMovimientos1.FirstOrDefault().NoMovimiento = MovimientoActual.tMovimientos1.Where(x=>x.IdMovimientoPadre != null).FirstOrDefault().NoMovimiento;
                            MovimientoActual.tMovimientos1.FirstOrDefault().FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            MovimientoActual.tMovimientos1.FirstOrDefault().IdTipoTransaccion = new TipoTransaccionBLL().ObtenerPorNombre("Comision por Desembolso").FirstOrDefault().IdTipoTransaccion;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().IdColector = Guid.Parse(ddlColector.SelectedValue);
                            MovimientoActual.tMovimientos1.FirstOrDefault().NoCuotas = Convert.ToInt16(txtNoCuotas.Text);
                            MovimientoActual.tMovimientos1.FirstOrDefault().IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue);
                            //MovimientoActual.tMovimientos1.FirstOrDefault().MontoTransaccion = double.Parse(txtComision.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                            //MovimientoActual.tMovimientos1.FirstOrDefault().Abono = 0;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().Saldo = double.Parse(txtComision.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                            //MovimientoActual.tMovimientos1.FirstOrDefault().ComisionDesembolso = 0;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                            //MovimientoActual.tMovimientos1.FirstOrDefault().Interes = 0;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().TipoInteres = string.Empty;
                            //MovimientoActual.tMovimientos1.FirstOrDefault().Moneda = "Cordoba";
                            MovimientoActual.tMovimientos1.FirstOrDefault().EstadoTransaccion = "Activa";
                            MovimientoActual.tMovimientos1.FirstOrDefault().FechaRegistro = DateTime.Now;
                            MovimientoActual.tMovimientos1.FirstOrDefault().FechaProceso = DateTime.Now;
                            MovimientoActual.tMovimientos1.FirstOrDefault().Usuario = user.Login;
                            MovimientoActual.tMovimientos1.FirstOrDefault().DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            MovimientoActual.tMovimientos1.FirstOrDefault().NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);

                            if (new MovimientoBLL().Actualizar(MovimientoActual.tMovimientos1.FirstOrDefault()))
                            {
                                MostrarMensaje("El registro se ha actualizado con exito!", TipoMensaje.Success);
                                var movimientos = new MovimientoBLL().ListarMovimientosXCuenta(txtBuscar.Text.Trim());
                                if (movimientos.Count > 0)
                                {
                                    MovimientosCalcular = movimientos;
                                    gvDatos.DataSource = movimientos;
                                    gvDatos.DataBind();
                                }
                            }
                        }

                        var movimientosBuscar = new MovimientoBLL().ListarMovimientosXCuenta(txtBuscar.Text.Trim());
                        if (movimientosBuscar.Count > 0)
                        {
                            MovimientosCalcular = movimientosBuscar;
                            gvDatos.DataSource = movimientosBuscar;
                            gvDatos.DataBind();                               
                        }
                        LimpiarControles();
                        MostrarMensaje("Movimiento se ha actualizado con exito!", TipoMensaje.Success);
                    }
                    else
                        MostrarMensaje("No se pudo actualizar el movimiento!", TipoMensaje.Success);

                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        


        private void LimpiarControles()
        {
            //ddlAutorizante.ClearSelection();
            //txtNombre.Text = string.Empty;
            //txtIdentificacion.Text = string.Empty;
            //txtDisponible.Text = string.Empty;
            //txtMontoAutorizado.Text = string.Empty;

            txtMonto.Text = string.Empty;
            txtNoCuotas.Text = string.Empty;
            txtInteres.Text = string.Empty;
            txtPlazoMeses.Text = string.Empty;
            txtComision.Text = string.Empty;
            //txtSaldo.Text = string.Empty;

            ddlPromotor.ClearSelection();
            ddlFrecuencia.ClearSelection();
            //ddlDias.ClearSelection();
            ddlTipoInteres.ClearSelection();
            ddlColector.ClearSelection();

        }

        private void LlenarFormulario()
        {
            try
            {

                //CargarCombo();
                txtMonto.Text = MovimientoActual.MontoTransaccion.ToString();
                txtNoCuotas.Text = MovimientoActual.NoCuotas.ToString();
                ddlFrecuencia.SelectedValue = MovimientoActual.IdFrecuencia != null ? MovimientoActual.IdFrecuencia.ToString() : ddlFrecuencia.SelectedValue;
                //ddlPromotor.SelectedValue = MovimientoActual.IdPromotor != null ? MovimientoActual.IdPromotor.ToString() : ddlPromotor.SelectedValue;
                //ddlColector.SelectedValue = MovimientoActual.IdColector != null ? MovimientoActual.IdColector.ToString() : ddlColector.SelectedValue;
                txtInteres.Text = MovimientoActual.Interes.ToString();
                //ddlDesembolsa.SelectedValue = MovimientoActual.IdDesembolsa != null ? MovimientoActual.IdDesembolsa.ToString() : ddlDesembolsa.SelectedValue;
                ddlDesembolsa.SelectedIndex = ddlDesembolsa.Items.IndexOf(ddlDesembolsa.Items.FindByValue(MovimientoActual.IdDesembolsa.ToString()));
                txtPlazoMeses.Text = MovimientoActual.PlazoMeses.ToString();
                txtFechaMovimiento.Text = MovimientoActual.FechaEfectiva.Value.ToShortDateString();
                //txtComision.Text = MovimientoActual.ComisionDesembolso.ToString();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void CargarCombo()
        {
            //tipo de cuenta
            ddlFrecuencia.SelectedIndex = -1; //Limpia cualquier selección
            ddlFrecuencia.DataSource = new FrecuenciaBLL().ListarActivos();
            ddlFrecuencia.DataTextField = "Descripcion";
            ddlFrecuencia.DataValueField = "IdFrecuencia";
            ddlFrecuencia.DataBind();

            ////Combo usuarios
            //ddlAutorizante.SelectedIndex = -1; //Limpia cualquier selección
            //ddlAutorizante.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Autorizante");
            //ddlAutorizante.DataTextField = "Nombre";
            //ddlAutorizante.DataValueField = "IdUsuario";
            //ddlAutorizante.DataBind();

            //Combo desembolsador
            ddlDesembolsa.SelectedIndex = -1; //Limpia cualquier selección
            ddlDesembolsa.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Desembolsador");
            ddlDesembolsa.DataTextField = "Nombre";
            ddlDesembolsa.DataValueField = "IdUsuario";
            ddlDesembolsa.DataBind();

            //Combo promotor
            ddlPromotor.SelectedIndex = -1; //Limpia cualquier selección
            ddlPromotor.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Promotor");
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();

            //Combo colector
            ddlColector.SelectedIndex = -1; //Limpia cualquier selección
            ddlColector.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Cobro");
            ddlColector.DataTextField = "Nombre";
            ddlColector.DataValueField = "IdUsuario";
            ddlColector.DataBind();

        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdMovimiento.Value = string.Empty;
            MovimientoActual = null;
            LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            string tipoMensaje = "alert-" + tipo.ToString().ToLower();
            litmensaje.Text = "";
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