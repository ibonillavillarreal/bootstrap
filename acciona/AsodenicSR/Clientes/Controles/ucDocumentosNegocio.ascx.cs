using Acciona.App_Code;
using AccionaSR.Negocio;
using ControlesPersonalizados;
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
    public partial class ucDocumentosNegocio : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioDocNegocio"] == null)
                    ViewState["EstadoFormularioDocNegocio"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioDocNegocio"];
            }
            set
            {
                ViewState["EstadoFormularioDocNegocio"] = value;
            }
        }

        public DocumentosNegocio DocNegocioActual
        {
            get
            {
                if (Session["DocNegocioActuales"] == null)
                    Session["DocNegocioActuales"] = new DocumentosNegocio();
                return (DocumentosNegocio)Session["DocNegocioActuales"];
            }
            set
            {
                Session["DocNegocioActuales"] = value;
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
            Session.Remove("DocNegocioActuales");
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
                DocNegocioActual = new DocumentosNegocioBLL().ObtenerPorIdDocumentosNegocio(Guid.Parse(indice.Value.ToString()));
                if (new DocumentosNegocioBLL().Eliminar(DocNegocioActual))
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

                DocNegocioActual = new DocumentosNegocioBLL().ObtenerPorIdDocumentosNegocio(Guid.Parse(indice.Value.ToString()));

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
            var dsDatosNegocio = Session["idDatosNegocio"] != null ? new DocumentosNegocioBLL().ObtenerDocumentosNegocioPorIdDocumentosNegocio(Guid.Parse(Session["idDatosNegocio"].ToString())) : null;
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

            DocNegocioActual = new DocumentosNegocio();

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
                    DocNegocioActual = new DocumentosNegocioBLL().ObtenerPorIdDocumentosNegocio(Guid.Parse(dataKey.Value.ToString()));

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
                    DocNegocioActual = new DocumentosNegocioBLL().ObtenerPorIdDocumentosNegocio(Guid.Parse(dataKey.Value.ToString()));
                    if (new DocumentosNegocioBLL().Eliminar(DocNegocioActual))
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

                if (!string.IsNullOrEmpty(txtDocumento.Text.Trim()))
                {
                    DocumentosNegocio nuevoDocNegocio = LlenarObjeto();

                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoDocNegocio.IdDatosNegocio = Guid.Parse(Session["idDatosNegocio"].ToString());
                            nuevoDocNegocio.IdDocumentoNegocio = Guid.NewGuid();
                            nuevoDocNegocio.FechaRegistro = DateTime.Now;
                            nuevoDocNegocio.Usuario = user.Login;
                            if (new DocumentosNegocioBLL().Insertar(nuevoDocNegocio))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoDocNegocio.IdDatosNegocio = DocNegocioActual.IdDatosNegocio;
                            nuevoDocNegocio.IdDocumentoNegocio = DocNegocioActual.IdDocumentoNegocio;
                            nuevoDocNegocio.EsActivo = DocNegocioActual.EsActivo;
                            nuevoDocNegocio.FechaRegistro = DocNegocioActual.FechaRegistro;
                            nuevoDocNegocio.Usuario = user.Login;
                            if (new DocumentosNegocioBLL().Actualizar(nuevoDocNegocio))
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

        private DocumentosNegocio LlenarObjeto()
        {
            DocumentosNegocio nuevoDocNegocio = new DocumentosNegocio()
            {
                TipoRegistros = txtDocumento.Text.Trim(),
                Institucion = txtInstitucion.Text.Trim(),
                UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                FechaEmision = Convert.ToDateTime(txtFechaEmision.Text),
                FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text),
                EsActivo = chkActivo.Checked
            };
            return nuevoDocNegocio;
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
            txtDocumento.Text =
            txtInstitucion.Text =
            txtFechaVencimiento.Text =
            txtFechaEmision.Text = string.Empty;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtDocumento.Text = DocNegocioActual.TipoRegistros;
            txtInstitucion.Text = DocNegocioActual.Institucion;
            txtFechaVencimiento.Text = DocNegocioActual.FechaVencimiento.ToString("dd/MM/yyyy");
            DateTime fechaEmision = (DateTime)DocNegocioActual.FechaEmision;
            txtFechaEmision.Text = fechaEmision.ToShortDateString();
            chkActivo.Checked = DocNegocioActual.EsActivo;
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