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
    public partial class ucReferenciasCrediticias : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioReferenciac"] == null)
                    ViewState["EstadoFormularioReferenciac"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioReferenciac"];
            }
            set
            {
                ViewState["EstadoFormularioReferenciac"] = value;
            }
        }

        public ReferenciaCrediticia RefCrediticiaActuales
        {
            get
            {
                if (Session["ReferenciaCredActual"] == null)
                    Session["ReferenciaCredActual"] = new ReferenciaCrediticia();
                return (ReferenciaCrediticia)Session["ReferenciaCredActual"];
            }
            set
            {
                Session["ReferenciaCredActual"] = value;
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
            Session.Remove("ReferenciaCredActual");
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
                RefCrediticiaActuales = new ReferenciaCrediticiaBLL().ObtenerPorIdReferenciaCrediticia(Guid.Parse(indice.Value.ToString()));
                if (new ReferenciaCrediticiaBLL().Eliminar(RefCrediticiaActuales))
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

                RefCrediticiaActuales = new ReferenciaCrediticiaBLL().ObtenerPorIdReferenciaCrediticia(Guid.Parse(indice.Value.ToString()));

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
            var dsContactos = Session["idCliente"] != null ? new ReferenciaCrediticiaBLL().ObtenerReferenciaCrediticiasPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsContactos;
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

            RefCrediticiaActuales = new ReferenciaCrediticia();

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
                    RefCrediticiaActuales = new ReferenciaCrediticiaBLL().ObtenerPorIdReferenciaCrediticia(Guid.Parse(dataKey.Value.ToString()));

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
                    RefCrediticiaActuales = new ReferenciaCrediticiaBLL().ObtenerPorIdReferenciaCrediticia(Guid.Parse(dataKey.Value.ToString()));
                    if (new ReferenciaCrediticiaBLL().Eliminar(RefCrediticiaActuales))
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

                if (!string.IsNullOrEmpty(txtBanco.Text.Trim()))
                {
                    decimal monto = 0;

                    monto = decimal.Parse(txtMonto.Text, System.Globalization.NumberStyles.Number, Generales.cultura);

                    ReferenciaCrediticia nuevaReferenciaCrediticia = new ReferenciaCrediticia()
                    {
                        IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                        Banco = txtBanco.Text.Trim(),
                        Monto = monto,
                        Plazo = txtPlazo.Text.Trim()
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevaReferenciaCrediticia.IdReferenciaCrediticia = Guid.NewGuid();
                            nuevaReferenciaCrediticia.EsActivo = chkActivo.Checked;
                            nuevaReferenciaCrediticia.FechaRegistro = DateTime.Now;
                            nuevaReferenciaCrediticia.UsuarioIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request); ;
                            nuevaReferenciaCrediticia.UsuarioPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request); ;
                            nuevaReferenciaCrediticia.Usuario = user.Login;
                            if (new ReferenciaCrediticiaBLL().Insertar(nuevaReferenciaCrediticia))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevaReferenciaCrediticia.IdReferenciaCrediticia = RefCrediticiaActuales.IdReferenciaCrediticia;
                            nuevaReferenciaCrediticia.EsActivo = chkActivo.Checked;
                            nuevaReferenciaCrediticia.FechaRegistro = RefCrediticiaActuales.FechaRegistro;
                            nuevaReferenciaCrediticia.Usuario = RefCrediticiaActuales.Usuario;
                            nuevaReferenciaCrediticia.UsuarioIP = RefCrediticiaActuales.UsuarioIP;
                            nuevaReferenciaCrediticia.UsuarioPC = RefCrediticiaActuales.UsuarioPC;
                            if (new ReferenciaCrediticiaBLL().Actualizar(nuevaReferenciaCrediticia))
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
            txtPlazo.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtBanco.Text = string.Empty;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            //string monto = string.Format("{0:0.00}", RefCrediticiaActuales.Monto.ToString());
            txtPlazo.Text = RefCrediticiaActuales.Plazo;
            txtMonto.Text = string.Format("{0:0,0.00}", Convert.ToDouble(RefCrediticiaActuales.Monto)); 
            txtBanco.Text = RefCrediticiaActuales.Banco;
            chkActivo.Checked = (bool)RefCrediticiaActuales.EsActivo;
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