using Acciona.App_Code;
using AccionaSR.Negocio;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Clientes.Controles
{
    public partial class ucResumenTransacciones : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioResumen"] == null)
                    ViewState["EstadoFormularioResumen"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioResumen"];
            }
            set
            {
                ViewState["EstadoFormularioResumen"] = value;
            }
        }

        public ResumenTransaccion ResumenActual
        {
            get
            {
                if (Session["ResumenActuales"] == null)
                    Session["ResumenActuales"] = new ResumenTransaccion();
                return (ResumenTransaccion)Session["ResumenActuales"];
            }
            set
            {
                Session["ResumenActuales"] = value;
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
                ModoInicial();
            }
        }

        public void LimpiarSesion()
        {
            Session.Remove("ResumenActuales");
            LimpiarControles();
        }

        #endregion Otros eventos

        #region Botones de acción

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            ModoAgregar();
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            ModoEditar();
        }

        protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        {
            ModoEliminar();
        }

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        protected void btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                ResumenActual = new ResumenTransaccionBLL().ObtenerPorIdResumenTransaccion(Guid.Parse(indice.Value.ToString()));
                if (new ResumenTransaccionBLL().Eliminar(ResumenActual))
                {
                    ModoInicial();
                }
            }
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                EstadoFormulario = Generales.EstadoFormulario.Editar;

                ResumenActual = new ResumenTransaccionBLL().ObtenerPorIdResumenTransaccion(Guid.Parse(indice.Value.ToString()));

                //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                pnlAgregar.Visible = true;

                gvDatos.Visible = false;

                imbActualizar.Enabled =
                imbActualizar.Visible =
                imbEditar.Enabled =
                imbEditar.Visible =
                imbEliminar.Enabled =
                imbEliminar.Visible =
                imbAgregar.Enabled =
                imbAgregar.Visible = false;

                imbGuardar.Enabled =
                imbGuardar.Visible =
                imbCancelar.Enabled =
                imbCancelar.Visible = true;

                LimpiarControles();
                CargarCombo();
                LlenarFormulario();

            }
        }

        #endregion Botones de acción

        protected void gvDatos_DataBound(object sender, EventArgs e)
        {
        }

        #endregion Eventos

        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            var dsReferencias = Session["idCliente"] != null ? new ResumenTransaccionBLL().ObtenerResumenTransaccionPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsReferencias;
            gvDatos.DataBind();

            pnlAgregar.Visible = false;

            imbActualizar.Enabled =
            imbActualizar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = true;

            imbEditar.Enabled =
            imbEditar.Visible =
            imbEliminar.Enabled =
            imbEliminar.Visible =
            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = false;
        }

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            ResumenActual = new ResumenTransaccion();

            //litAyuda.Text = "Escriba el nombre para la sucursal y guarde los cambios";

            pnlAgregar.Visible = true;

            gvDatos.Visible = false;

            imbActualizar.Enabled =
            imbActualizar.Visible =
            imbEditar.Enabled =
            imbEditar.Visible =
            imbEliminar.Enabled =
            imbEliminar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = false;

            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = true;

            LimpiarControles();
            CargarCombo();
            chkActivo.Checked = true;
        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    ResumenActual = new ResumenTransaccionBLL().ObtenerPorIdResumenTransaccion(Guid.Parse(dataKey.Value.ToString()));

                    //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                    pnlAgregar.Visible = true;

                    gvDatos.Visible = false;

                    imbActualizar.Enabled =
                    imbActualizar.Visible =
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible =
                    imbAgregar.Enabled =
                    imbAgregar.Visible = false;

                    imbGuardar.Enabled =
                    imbGuardar.Visible =
                    imbCancelar.Enabled =
                    imbCancelar.Visible = true;

                    LimpiarControles();
                    CargarCombo();
                    LlenarFormulario();
                }
            }
        }

        private void ModoEliminar()
        {
            try
            {
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        ResumenActual = new ResumenTransaccionBLL().ObtenerPorIdResumenTransaccion(Guid.Parse(dataKey.Value.ToString()));
                        if (new ResumenTransaccionBLL().Eliminar(ResumenActual))
                        {
                            ModoInicial();
                        }
                    }
                }
            }
            catch (Exception)
            {

                MostrarMensaje("Ocurrio un error. Por favor verifique!", TipoMensaje.Danger);
            }
            
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                if (!string.IsNullOrEmpty(txtNoCredito.Text.Trim()))
                {
                    decimal monto = 0;

                    monto = decimal.Parse(txtMonto.Text, System.Globalization.NumberStyles.Number, Generales.cultura);

                    ResumenTransaccion nuevoResumen = new ResumenTransaccion()
                    {
                        IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                        NoPrestamo = txtNoCredito.Text.Trim(),
                        IdMetodologia = Guid.Parse(ddlMetodologia.SelectedValue),
                        FechaInicioCredito = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        FechaFinCredito = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        MaximoDiasMora = Convert.ToInt16(txtMora.Text.Trim()),
                        MontoPromedio = monto,
                        Observaciones = txtObservaciones.Text.Trim(),
                        EsActivo = chkActivo.Checked
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoResumen.IdTransaccionesInstitucion = Guid.NewGuid();
                            nuevoResumen.FechaRegistro = DateTime.Now;
                            nuevoResumen.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevoResumen.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                            nuevoResumen.Usuario = user.Login;

                            if (new ResumenTransaccionBLL().Insertar(nuevoResumen))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoResumen.IdTransaccionesInstitucion = ResumenActual.IdTransaccionesInstitucion;
                            nuevoResumen.FechaRegistro = ResumenActual.FechaRegistro;
                            nuevoResumen.Usuario = ResumenActual.Usuario;
                            nuevoResumen.UserIP = ResumenActual.UserIP;
                            nuevoResumen.UserPC = ResumenActual.UserPC;
                            if (new ResumenTransaccionBLL().Actualizar(nuevoResumen))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
              MostrarMensaje("No se pudo completar la accion!", TipoMensaje.Danger);
            }
            
        }

        private void Seleccionar(object sender)
        {
            int indice;
            LinkButton linkButton = (LinkButton)sender;
            int.TryParse(linkButton.CommandArgument, out indice);
            if (indice >= 0)
            {
                if (gvDatos.SelectedIndex == indice)
                {
                    gvDatos.SelectedIndex = -1;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = false;
                }
                else
                {
                    gvDatos.SelectedIndex = indice;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = true;
                }
            }
        }

        private void LimpiarControles()
        {
            txtFechaFin.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtMora.Text = string.Empty;
            txtNoCredito.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            ddlMetodologia.SelectedIndex = -1;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtFechaFin.Text = ResumenActual.FechaFinCredito != null ? ((DateTime)ResumenActual.FechaFinCredito).ToString("d") : string.Empty;
            txtFechaInicio.Text = ResumenActual.FechaInicioCredito != null ? ((DateTime)ResumenActual.FechaInicioCredito).ToString("d") : string.Empty;
            txtMonto.Text = ResumenActual.MontoPromedio.ToString();
            txtMora.Text = ResumenActual.MaximoDiasMora.ToString();
            txtNoCredito.Text = ResumenActual.NoPrestamo;
            txtObservaciones.Text = ResumenActual.Observaciones;
            ddlMetodologia.SelectedValue = ResumenActual.IdMetodologia.ToString();
            chkActivo.Checked = ResumenActual.EsActivo;
        }

        private void CargarCombo()
        {
            ddlMetodologia.SelectedIndex = -1; //Limpia cualquier selección
            ddlMetodologia.DataSource = new MetodologiaBLL().Listar();
            ddlMetodologia.DataTextField = "Nombre";
            ddlMetodologia.DataValueField = "IdMetodologia";
            ddlMetodologia.DataBind();
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