using Acciona.App_Code;
using AccionaSR.Negocio;
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
    public partial class ucReferenciasPersonales : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioReferencia"] == null)
                    ViewState["EstadoFormularioReferencia"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioReferencia"];
            }
            set
            {
                ViewState["EstadoFormularioReferencia"] = value;
            }
        }

        public Referencias ReferenciasActuales
        {
            get
            {
                if (Session["ReferenciaActual"] == null)
                    Session["ReferenciaActual"] = new Referencias();
                return (Referencias)Session["ReferenciaActual"];
            }
            set
            {
                Session["ReferenciaActual"] = value;
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
            Session.Remove("ReferenciaActual");
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

        #endregion Botones de acción

        protected void gvDatos_DataBound(object sender, EventArgs e)
        {
        }

        protected void btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                ReferenciasActuales = new ReferenciasBLL().ObtenerPorIdReferencias(Guid.Parse(indice.Value.ToString()));
                if (new ReferenciasBLL().Eliminar(ReferenciasActuales))
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

                ReferenciasActuales = new ReferenciasBLL().ObtenerPorIdReferencias(Guid.Parse(indice.Value.ToString()));

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

        #endregion Eventos

        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            var dsReferencias = Session["idCliente"] != null ? new ReferenciasBLL().ObtenerReferenciasPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
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

            ReferenciasActuales = new Referencias();

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
                    ReferenciasActuales = new ReferenciasBLL().ObtenerPorIdReferencias(Guid.Parse(dataKey.Value.ToString()));

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
                    ReferenciasActuales = new ReferenciasBLL().ObtenerPorIdReferencias(Guid.Parse(dataKey.Value.ToString()));
                    if (new ReferenciasBLL().Eliminar(ReferenciasActuales))
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
                    Referencias nuevaReferencia = new Referencias()
                    {
                        IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                        NombreCompleto = txtNombre.Text.Trim(),
                        NoIdentificacion = txtIdentificacion.Text.Trim(),
                        CentroLaboral = txtLugarTrabajo.Text.Trim(),
                        Tiempo = txtAntiguedad.Text.Trim(),
                        Profesion = txtProfesion.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Direccion = txtDireccion.Text.Trim(),
                        Sexo = ddlSexo.SelectedValue,
                        EsActivo = chkActivo.Checked
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevaReferencia.IdReferencia = Guid.NewGuid();
                            nuevaReferencia.FechaRegistro = DateTime.Now;
                            nuevaReferencia.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                            nuevaReferencia.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request); ;
                            nuevaReferencia.Usuario = user.Login;
                            if (new ReferenciasBLL().Insertar(nuevaReferencia))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevaReferencia.IdReferencia = ReferenciasActuales.IdReferencia;
                            nuevaReferencia.FechaRegistro = ReferenciasActuales.FechaRegistro;
                            nuevaReferencia.Usuario = ReferenciasActuales.Usuario;
                            nuevaReferencia.UserIP = ReferenciasActuales.UserIP;
                            nuevaReferencia.UserPC = ReferenciasActuales.UserPC;
                            if (new ReferenciasBLL().Actualizar(nuevaReferencia))
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
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtLugarTrabajo.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtAntiguedad.Text = string.Empty;
            txtProfesion.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            chkActivo.Checked = false;
            ddlSexo.SelectedIndex = -1;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtDireccion.Text = ReferenciasActuales.Direccion;
            txtIdentificacion.Text = ReferenciasActuales.NoIdentificacion;
            txtLugarTrabajo.Text = ReferenciasActuales.CentroLaboral;
            txtNombre.Text = ReferenciasActuales.NombreCompleto;
            txtTelefono.Text = ReferenciasActuales.Telefono;
            txtProfesion.Text = ReferenciasActuales.Profesion;
            //ddlProfesion.SelectedIndex = ddlProfesion.Items.IndexOf(ddlProfesion.Items.FindByValue(ReferenciasActuales.IdProfesion.ToString()));
            txtAntiguedad.Text = ReferenciasActuales.Tiempo;
            if (ReferenciasActuales.Sexo != null)
                ddlSexo.SelectedIndex = ddlSexo.Items.IndexOf(ddlSexo.Items.FindByValue(ReferenciasActuales.Sexo.ToString()));
            else
                ddlSexo.SelectedIndex = 1;
            chkActivo.Checked = ReferenciasActuales.EsActivo;
        }

        private void CargarCombo()
        {
            //ddlProfesion.SelectedIndex = -1; //Limpia cualquier selección
            //ddlProfesion.DataSource = new ProfesionBLL().Listar();
            //ddlProfesion.DataTextField = "Nombre";
            //ddlProfesion.DataValueField = "IdProfesion";
            //ddlProfesion.DataBind();
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