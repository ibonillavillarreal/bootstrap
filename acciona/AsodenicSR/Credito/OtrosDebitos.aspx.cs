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
    public partial class OtrosDebitos : Pagina
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


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                if (ddlTipo.SelectedValue == "1")   //busqueda por cedula
                {
                    var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                    if (cliente.Count > 0)
                    {
                        if (cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().tPlastico.Count == 0)
                        {
                            MostrarMensaje("Este cliente no posee un plastico activo asociado!", TipoMensaje.Danger);
                            return;
                        }

                        hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                        hfIdCuenta.Value = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdCuenta.ToString();
                        //LitNoCuenta.Text = "Cuenta #" + ": " + cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().NoCuenta.ToString();
                        ClienteCuentaActual = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault();

                        txtNombre.Text = cliente.FirstOrDefault().NombreCompleto;
                        txtIdentificacion.Text = cliente.FirstOrDefault().NoIdentificacion;
                        txtDisponible.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        txtMontoAutorizado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        //txtMontoEntregado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        ddlAutorizante.SelectedValue = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado != null ? cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado.ToString() : ddlAutorizante.SelectedValue;
                        ModoInicial();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else if (ddlTipo.SelectedValue == "0")    //busqueda por cuenta
                {
                    var cliente = new ClienteBLL().ObtenerPorCuenta(txtBuscar.Text);
                    if (cliente.Count > 0)
                    {
                        if (cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().tPlastico.Count == 0)
                        {
                            MostrarMensaje("Este cliente no posee un plastico activo asociado!", TipoMensaje.Danger);
                            return;
                        }

                        hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                        hfIdCuenta.Value = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdCuenta.ToString();
                        //LitNoCuenta.Text = "Cuenta #" + ": " + cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().NoCuenta.ToString();
                        ClienteCuentaActual = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault();

                        txtNombre.Text = cliente.FirstOrDefault().NombreCompleto;
                        txtIdentificacion.Text = cliente.FirstOrDefault().NoIdentificacion;
                        txtDisponible.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        txtMontoAutorizado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        //txtMontoEntregado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                        ddlAutorizante.SelectedValue = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado != null ? cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado.ToString() : ddlAutorizante.SelectedValue;
                        ModoInicial();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }

                var ListaExiste = new EstadoCuentasDAO().ConsultaEncabezadoEC(ClienteCuentaActual.NoCuenta);

                if (ListaExiste.Count() <= 0)
                {
                    
                }
                else
                {
                    if (ListaExiste.FirstOrDefault().SaldoTotal < 0)
                    {
                        MostrarMensaje("Esta cuenta tiene un saldo a su favor de " + Math.Abs(ListaExiste.FirstOrDefault().SaldoTotal).ToString() + "Cordobas. Aplique una Devolucion Saldo a Favor!", TipoMensaje.Info);
                        txtMonto.Text = Math.Abs(ListaExiste.FirstOrDefault().SaldoTotal).ToString();
                        //txtMonto.ReadOnly = true;
                    }
                    else
                        txtMonto.ReadOnly = false;
                }
            }
        }

        protected void ddlTipoTransaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoTransaccion.SelectedValue == "4e9cea66-edf1-49a9-a20d-053312bc734c")
                {
                    if (ClienteCuentaActual != null)
                    {
                        var ListaExiste = new EstadoCuentasDAO().ConsultaEncabezadoEC(ClienteCuentaActual.NoCuenta);

                        if (ListaExiste.Count() <= 0)
                        {
                            //MostrarMensaje("No se encontro ningun resultado!", TipoMensaje.Info);
                        }
                        else
                        {
                            txtMonto.Text = Math.Abs(ListaExiste.FirstOrDefault().SaldoTotal).ToString();
                            //txtMonto.ReadOnly = true;
                        }
                    }
                }
                else
                    txtMonto.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }
        //protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fmt = "000000";
        //        var tipoCuenta = new TipoCuentaBLL().ObtenerPorNombre(ddlTipoCuenta.SelectedItem.Text).FirstOrDefault();
        //        int consecutivo = Convert.ToInt16(tipoCuenta.Numero) + 1;
        //        string numero = consecutivo.ToString(fmt);
        //        txtNoCuenta.Text = tipoCuenta.Prefijo + "-" + numero;
        //    }
        //    catch (Exception ex)
        //    {
        //        MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
        //    }
        //}
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
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales"; 

            if (hfIdCliente.Value != string.Empty || hfIdCliente.Value != "")
            {
                //gvDatos.Visible = true;
                //gvDatos.SelectedIndex = -1;
                //var dsCuentas = new ClienteCuentaBLL().ObtenerCuentaPorIdCliente(Guid.Parse(hfIdCliente.Value.ToString()));
                //gvDatos.DataSource = dsCuentas;
                //gvDatos.DataBind();
            }
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
                    tMovimientos nuevoMovimiento = LlenarObjetoPrincipal();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Inicial:
                            nuevoMovimiento.IdMovimiento = Guid.NewGuid();
                            nuevoMovimiento.FechaRegistro = DateTime.Now;
                            nuevoMovimiento.Usuario = user.Login;
                            switch (ddlTipoTransaccion.SelectedValue)
                            {
                                case "4e9cea66-edf1-49a9-a20d-053312bc734c":
                                    if (new MovimientoBLL().Insertar(nuevoMovimiento))
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
                                    else
                                        MostrarMensaje("No se pudo guardar el movimiento!", TipoMensaje.Success);

                                    break;
                                default:
                                    // guardar el restante de los movimientos

                                    if (new MovimientoBLL().Insertar(nuevoMovimiento))
                                    {

                                    }
                                    else
                                        MostrarMensaje("No se pudo guardar el movimiento!", TipoMensaje.Success);
                                    break;
                            }

                            break;
                    }
                    LimpiarControles();
                    LimpiarSesion();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private tMovimientos LlenarObjetoPrincipal()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tMovimientos movimientoNuevo = new tMovimientos()
            {
                IdMovimientoPadre = null,
                IdPlastico = ClienteCuentaActual.tPlastico.FirstOrDefault().IdPlastico,
                //IdPromotor = Guid.Parse(ddlPromotor.SelectedValue),
                //IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue),
                NoMovimiento = NoMovimientoMaximo(),
                FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                //IdColector = Guid.Parse(ddlColector.SelectedValue),
                IdTipoTransaccion = Guid.Parse(ddlTipoTransaccion.SelectedValue),

                NoCuotas = 1,
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                MontoTransaccion = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Abono = 0,
                Saldo = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoAnios = 0,
                PlazoMeses = 1,
                Interes = 0,
                //TipoInteres = ddlTipoInteres.SelectedItem.ToString(),
                EstadoTransaccion = "Activa",
                Moneda = "Cordoba",
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            MovimientoActual = movimientoNuevo;

            return MovimientoActual;
        }

        //private tMovimientos LlenarObjetoDetalle()
        //{
        //    Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
        //    //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
        //    tMovimientos movimientoNuevo = new tMovimientos()
        //    {
        //        IdMovimientoPadre = MovimientoActual.IdMovimiento,
        //        IdPlastico = ClienteCuentaActual.tPlastico.FirstOrDefault().IdPlastico,
        //        IdPromotor = Guid.Parse(ddlPromotor.SelectedValue),
        //        IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue),
        //        NoMovimiento = MovimientoActual.NoMovimiento + "-01",
        //        FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),

        //        IdTipoTransaccion = new TipoTransaccionBLL().ObtenerPorNombre("Comision por Desembolso").FirstOrDefault().IdTipoTransaccion,
        //        IdColector = Guid.Parse(ddlColector.SelectedValue),
        //        NoCuotas = Convert.ToInt16(txtNoCuotas.Text),
        //        IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
        //        MontoTransaccion = double.Parse(txtComision.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
        //        Abono = 0,
        //        Saldo = double.Parse(txtComision.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
        //        ComisionDesembolso = 0,
        //        PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
        //        Interes = 0,
        //        TipoInteres = string.Empty,
        //        Moneda = "Cordoba",
        //        EstadoTransaccion = "Activa",
        //        DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
        //        NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
        //    };
        //    return movimientoNuevo;
        //}

        public string NoMovimientoMaximo()
        {

            string fmt = "000000";
            int consecutivo = new MovimientoBLL().ObtenerMaxMovimiento() + 1;
            string numero = consecutivo.ToString(fmt);

            return numero;

        }


        private void LimpiarControles()
        {
            ddlAutorizante.ClearSelection();
            txtNombre.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtDisponible.Text = string.Empty;
            txtMontoAutorizado.Text = string.Empty;

            txtMonto.Text = string.Empty;
            //txtNoCuotas.Text = string.Empty;
            //txtInteres.Text = string.Empty;
            //txtPlazoMeses.Text = string.Empty;
            //txtComision.Text = string.Empty;
            //txtSaldo.Text = string.Empty;

            //ddlPromotor.ClearSelection();
            ddlFrecuencia.ClearSelection();
            //ddlDias.ClearSelection();
            //ddlTipoInteres.ClearSelection();
            ddlTipoTransaccion.ClearSelection();

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
            //tipo de cuenta
            ddlFrecuencia.SelectedIndex = -1; //Limpia cualquier selección
            ddlFrecuencia.DataSource = new FrecuenciaBLL().ListarActivos();
            ddlFrecuencia.DataTextField = "Descripcion";
            ddlFrecuencia.DataValueField = "IdFrecuencia";
            ddlFrecuencia.DataBind();

            ddlFrecuencia.SelectedValue = "884FB1BA-750F-4F7C-993A-D614502FE9C9";

            //Combo usuarios
            ddlAutorizante.SelectedIndex = -1; //Limpia cualquier selección
            ddlAutorizante.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Autorizante");
            ddlAutorizante.DataTextField = "Nombre";
            ddlAutorizante.DataValueField = "IdUsuario";
            ddlAutorizante.DataBind();

            

            //Combo desembolsador
            //ddlDesembolsa.SelectedIndex = -1; //Limpia cualquier selección
            //ddlDesembolsa.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Desembolsador");
            //ddlDesembolsa.DataTextField = "Nombre";
            //ddlDesembolsa.DataValueField = "IdUsuario";
            //ddlDesembolsa.DataBind();

            //Combo promotor
            //ddlPromotor.SelectedIndex = -1; //Limpia cualquier selección
            //ddlPromotor.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Promotor");
            //ddlPromotor.DataTextField = "Nombre";
            //ddlPromotor.DataValueField = "IdUsuario";
            //ddlPromotor.DataBind();

            //Combo colector
            ddlTipoTransaccion.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoTransaccion.DataSource = new RecibosBLL().ListarTipoDebitos();
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
            MovimientoActual = null;
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

        protected void ddlTipoTransaccion_TextChanged(object sender, EventArgs e)
        {

        }

        



        #endregion Métodos
    }
}