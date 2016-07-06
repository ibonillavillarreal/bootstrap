using Acciona.App_Code;
using AccionaSR.Negocio;
using ControlesPersonalizados;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Clientes.Controles
{
    public partial class ucSuplidores : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioSuplidor"] == null)
                    ViewState["EstadoFormularioSuplidor"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioSuplidor"];
            }
            set
            {
                ViewState["EstadoFormularioSuplidor"] = value;
            }
        }

        public NegocioProveedores NegocioProveedorActual
        {
            get
            {
                if (Session["NegocioProveedorActuales"] == null)
                    Session["NegocioProveedorActuales"] = new NegocioProveedores();
                return (NegocioProveedores)Session["NegocioProveedorActuales"];
            }
            set
            {
                Session["NegocioProveedorActuales"] = value;
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
            Session.Remove("NegocioProveedorActuales");
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
                NegocioProveedorActual = new ProveedoresBLL().ObtenerPorIdNegocioProveedores(Guid.Parse(indice.Value.ToString()));
                if (new ProveedoresBLL().Eliminar(NegocioProveedorActual))
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

                NegocioProveedorActual = new ProveedoresBLL().ObtenerPorIdNegocioProveedores(Guid.Parse(indice.Value.ToString()));

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

        #endregion Eventos

        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            var dsDatosNegocio = Session["idDatosNegocio"] != null ? new ProveedoresBLL().ObtenerNegocioProveedoresPorIdDatosNegocio(Guid.Parse(Session["idDatosNegocio"].ToString())) : null;
            gvDatos.DataSource = dsDatosNegocio;
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

            NegocioProveedorActual = new NegocioProveedores();

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
                    NegocioProveedorActual = new ProveedoresBLL().ObtenerPorIdNegocioProveedores(Guid.Parse(dataKey.Value.ToString()));

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
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    NegocioProveedorActual = new ProveedoresBLL().ObtenerPorIdNegocioProveedores(Guid.Parse(dataKey.Value.ToString()));
                    if (new ProveedoresBLL().Eliminar(NegocioProveedorActual))
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

                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    NegocioProveedores nuevoNegocioProveedor = new NegocioProveedores()
                    {
                        Nombre = txtNombre.Text.Trim(),
                        EsCliente = chkCliente.Checked,
                        EsActivo = chkActivo.Checked
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoNegocioProveedor.IdDatosNegocio = Guid.Parse(Session["idDatosNegocio"].ToString());
                            nuevoNegocioProveedor.IdNegocioProveedor = Guid.NewGuid();
                            nuevoNegocioProveedor.Usuario = user.Login;
                            nuevoNegocioProveedor.FechaRegistro = DateTime.Now;
                            nuevoNegocioProveedor.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevoNegocioProveedor.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                            if (new ProveedoresBLL().Insertar(nuevoNegocioProveedor))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoNegocioProveedor.IdNegocioProveedor = NegocioProveedorActual.IdNegocioProveedor;
                            nuevoNegocioProveedor.IdDatosNegocio = NegocioProveedorActual.IdDatosNegocio;
                            nuevoNegocioProveedor.FechaRegistro = DateTime.Now;
                            nuevoNegocioProveedor.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevoNegocioProveedor.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                            nuevoNegocioProveedor.Usuario = user.Login;
                            if (new ProveedoresBLL().Actualizar(nuevoNegocioProveedor))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.ToString(), TipoMensaje.Danger);
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
            chkCliente.Checked = false;
            txtNombre.Text = string.Empty;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtNombre.Text = NegocioProveedorActual.Nombre;
            chkCliente.Checked = NegocioProveedorActual.EsCliente;
            chkActivo.Checked = NegocioProveedorActual.EsActivo;
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