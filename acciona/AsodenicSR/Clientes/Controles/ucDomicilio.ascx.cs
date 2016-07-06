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
    public partial class ucDomicilio : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioDomicilio"] == null)
                    ViewState["EstadoFormularioDomicilio"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioDomicilio"];
            }
            set
            {
                ViewState["EstadoFormularioDomicilio"] = value;
            }
        }

        public Domicilio DomicilioActual
        {
            get
            {
                if (Session["DomicilioActuales"] == null)
                    Session["DomicilioActuales"] = new Domicilio();
                return (Domicilio)Session["DomicilioActuales"];
            }
            set
            {
                Session["DomicilioActuales"] = value;
            }
        }

        //public Guid IdCliente
        //{
        //    get
        //    {
        //        if (ViewState["EstadoFormularioDomicilio"] == null)
        //            ViewState["EstadoFormularioDomicilio"] = Generales.EstadoFormulario.Inicial;
        //        return (Generales.EstadoFormulario)ViewState["EstadoFormularioDomicilio"];
        //    }
        //    set
        //    {
        //        ViewState["EstadoFormularioDomicilio"] = value;
        //    }
        //}

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
            Session.Remove("DomicilioActuales");
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
                DomicilioActual = new DomicilioBLL().ObtenerPorIdDomicilio(Guid.Parse(indice.Value.ToString()));
                if (new DomicilioBLL().Eliminar(DomicilioActual))
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

                DomicilioActual = new DomicilioBLL().ObtenerPorIdDomicilio(Guid.Parse(indice.Value.ToString()));

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
            var dsDomicilio = Session["idCliente"] != null ? new DomicilioBLL().ObtenerDomicilioPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsDomicilio;
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

            DomicilioActual = new Domicilio();

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
                    DomicilioActual = new DomicilioBLL().ObtenerPorIdDomicilio(Guid.Parse(dataKey.Value.ToString()));

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
                    DomicilioActual = new DomicilioBLL().ObtenerPorIdDomicilio(Guid.Parse(dataKey.Value.ToString()));
                    if (new DomicilioBLL().Eliminar(DomicilioActual))
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

                if (!string.IsNullOrEmpty(txtDireccion.Text.Trim()))
                {
                    Domicilio nuevoDomicilio = LlenarObjeto();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoDomicilio.IdCliente = Guid.Parse(Session["idCliente"].ToString());
                            nuevoDomicilio.IdDomicilio = Guid.NewGuid();
                            nuevoDomicilio.EsActivo = true;
                            nuevoDomicilio.FechaRegistro = DateTime.Now;
                            nuevoDomicilio.Usuario = user.Login;
                            if (new DomicilioBLL().Insertar(nuevoDomicilio))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoDomicilio.IdDomicilio = DomicilioActual.IdDomicilio;
                            nuevoDomicilio.IdCliente = DomicilioActual.IdCliente;
                            nuevoDomicilio.FechaRegistro = DomicilioActual.FechaRegistro;
                            nuevoDomicilio.EsActivo = DomicilioActual.EsActivo;
                            nuevoDomicilio.Usuario = user.Login;
                            if (new DomicilioBLL().Actualizar(nuevoDomicilio))
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

        private Domicilio LlenarObjeto()
        {
            Domicilio nuevoDomicilio = new Domicilio()
             {
                 Descripcion = txtDireccion.Text.Trim(),
                 TiempoResidir = Convert.ToInt16(txtResidir.Text.Trim()),
                 EsAlquilada = rblVivienda.SelectedItem.Text == "Alquilada",
                 EsPropia = rblVivienda.SelectedItem.Text == "Propia",
                 Familiar = rblVivienda.SelectedItem.Text == "Familiar",
                 EsActivo = chkActivo.Checked,
                 UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                 UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
             };
            return nuevoDomicilio;
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
            txtDireccion.Text = string.Empty;
            rblVivienda.SelectedIndex = -1;
            txtResidir.Text = string.Empty;
            chkActivo.Checked = false;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtDireccion.Text = DomicilioActual.Descripcion;
            txtResidir.Text = DomicilioActual.TiempoResidir.ToString();
            rblVivienda.Items.FindByText("Familiar").Selected = DomicilioActual.Familiar;
            rblVivienda.Items.FindByText("Propia").Selected = DomicilioActual.EsPropia;
            rblVivienda.Items.FindByText("Alquilada").Selected = DomicilioActual.EsAlquilada;
            chkActivo.Checked = DomicilioActual.EsActivo;
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