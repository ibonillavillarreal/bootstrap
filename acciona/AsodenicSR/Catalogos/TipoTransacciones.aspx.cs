using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acciona.App_Code;
using AccionaSR.Negocio;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;

namespace Acciona.Catalogos
{
    public partial class TipoTransacciones : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioContactos"] == null)
                    ViewState["EstadoFormularioContactos"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioContactos"];
            }
            set
            {
                ViewState["EstadoFormularioContactos"] = value;
            }
        }

        public TipoTransaccion TipoTransaccionActuales
        {
            get
            {
                if (Session["TipoTransaccionActual"] == null)
                    Session["TipoTransaccionActual"] = new TipoTransaccion();
                return (TipoTransaccion)Session["TipoTransaccionActual"];
            }
            set
            {
                Session["TipoTransaccionActual"] = value;
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
            //Session.Remove("TipoCuentaActual");
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
                TipoTransaccionActuales = new TipoTransaccionBLL().ObtenerPorIdTipoTransaccion(Guid.Parse(indice.Value.ToString()));
                if (new TipoTransaccionBLL().Eliminar(TipoTransaccionActuales))
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

                TipoTransaccionActuales = new TipoTransaccionBLL().ObtenerPorIdTipoTransaccion(Guid.Parse(indice.Value.ToString()));

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
                //CargarCombo();
                LlenarFormulario();

            }
        }



        #endregion Botones de acción

        #endregion Eventos

        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            var dsTipoCuentas = new TipoTransaccionBLL().Listar();
            gvDatos.DataSource = dsTipoCuentas;
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

            TipoTransaccionActuales = new TipoTransaccion();

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
            //CargarCombo();
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
                    TipoTransaccionActuales = new TipoTransaccionBLL().ObtenerPorIdTipoTransaccion(Guid.Parse(dataKey.Value.ToString()));

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
                    //CargarCombo();
                    LlenarFormulario();
                }
            }
        }

        private void ModoEliminar()
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    TipoTransaccionActuales = new TipoTransaccionBLL().ObtenerPorIdTipoTransaccion(Guid.Parse(dataKey.Value.ToString()));
                    if (new TipoTransaccionBLL().Eliminar(TipoTransaccionActuales))
                    {
                        ModoInicial();
                    }
                }
            }
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                if (!string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                {
                    TipoTransaccion nuevoTipoTransaccion = new TipoTransaccion()
                    {
                        CuentaContable = txtCuentaContable.Text.Trim(),
                        ContraCuenta = txtContraCuenta.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        Concepto = txtConcepto.Text.Trim(),
                        EsActivo = chkActivo.Checked
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoTipoTransaccion.IdTipoTransaccion = Guid.NewGuid();
                            //nuevoContacto.EsActivo = chkActivo.Checked;
                            nuevoTipoTransaccion.FechaRegistro = DateTime.Now;
                            nuevoTipoTransaccion.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevoTipoTransaccion.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                            nuevoTipoTransaccion.Usuario = user.Login;

                            if (new TipoTransaccionBLL().Insertar(nuevoTipoTransaccion))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoTipoTransaccion.IdTipoTransaccion = TipoTransaccionActuales.IdTipoTransaccion;

                            //nuevoContacto.EsActivo = chkActivo.Checked;
                            nuevoTipoTransaccion.FechaRegistro = DateTime.Now;
                            nuevoTipoTransaccion.Usuario = TipoTransaccionActuales.Usuario;
                            nuevoTipoTransaccion.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevoTipoTransaccion.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                            nuevoTipoTransaccion.Usuario = user.Login;
                            if (new TipoTransaccionBLL().Actualizar(nuevoTipoTransaccion))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
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

            txtDescripcion.Text = string.Empty;
            txtCuentaContable.Text = string.Empty;
            txtContraCuenta.Text = string.Empty;
            txtConcepto.Text = string.Empty;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtCuentaContable.Text = TipoTransaccionActuales.CuentaContable;
            txtContraCuenta.Text = TipoTransaccionActuales.ContraCuenta;
            txtConcepto.Text = TipoTransaccionActuales.Concepto;
            txtDescripcion.Text = TipoTransaccionActuales.Descripcion;
            chkActivo.Checked = TipoTransaccionActuales.EsActivo == true ? true : false;
        }

        private void CargarCombo()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
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