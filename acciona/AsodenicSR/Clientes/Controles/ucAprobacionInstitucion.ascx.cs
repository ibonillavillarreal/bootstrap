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
    public partial class ucAprobacionInstitucion : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioAprobacion"] == null)
                    ViewState["EstadoFormularioAprobacion"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioAprobacion"];
            }
            set
            {
                ViewState["EstadoFormularioAprobacion"] = value;
            }
        }

        public AprobacionInstitucion AprobacionActual
        {
            get
            {
                if (Session["AprobacionActuales"] == null)
                    Session["AprobacionActuales"] = new AprobacionInstitucion();
                return (AprobacionInstitucion)Session["AprobacionActuales"];
            }
            set
            {
                Session["AprobacionActuales"] = value;
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
            Session.Remove("AprobacionActuales");
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
                AprobacionActual = new AprobacionInstitucionBLL().ObtenerPorIdAprobacionInstitucion(Guid.Parse(indice.Value.ToString()));
                if (new AprobacionInstitucionBLL().Eliminar(AprobacionActual))
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

                AprobacionActual = new AprobacionInstitucionBLL().ObtenerPorIdAprobacionInstitucion(Guid.Parse(indice.Value.ToString()));

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
            var dsAprobacionInstitucion = Session["idCliente"] != null ? new AprobacionInstitucionBLL().ObtenerAprobacionInstitucionPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsAprobacionInstitucion;
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

            AprobacionActual = new AprobacionInstitucion();

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
        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    AprobacionActual = new AprobacionInstitucionBLL().ObtenerPorIdAprobacionInstitucion(Guid.Parse(dataKey.Value.ToString()));

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
                    AprobacionActual = new AprobacionInstitucionBLL().ObtenerPorIdAprobacionInstitucion(Guid.Parse(dataKey.Value.ToString()));
                    if (new AprobacionInstitucionBLL().Eliminar(AprobacionActual))
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
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

            if (!string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
            {
                AprobacionInstitucion nuevaAprobacionInstitucion = new AprobacionInstitucion()
                {
                    IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                    Descripcion = txtDescripcion.Text.Trim(),
                    FechaHoraVerificacion = Convert.ToDateTime(txtFechaAprobacion.Text.Trim()),
                    IdUsuario = Guid.Parse(ddlPromotor.SelectedValue),                  
                    NivelRiesgo = txtNivelRiesgo.Text,
                    //IdUsuario = user.IdUsuario
                };
                switch (EstadoFormulario)
                {
                    case Generales.EstadoFormulario.Agregar:
                        nuevaAprobacionInstitucion.IdAprobacionInstitucion = Guid.NewGuid();
                        nuevaAprobacionInstitucion.EsActivo = true;
                        nuevaAprobacionInstitucion.FechaRegistro = DateTime.Now;
                        nuevaAprobacionInstitucion.UsuarioIP = MetodosExtensiones.ObtenerUsuarioIP((this.Page.Request));
                        nuevaAprobacionInstitucion.UsuarioPC = MetodosExtensiones.ObtenerUsuarioPC((this.Page.Request));
                        nuevaAprobacionInstitucion.Usuario = user.Login;
                        if (new AprobacionInstitucionBLL().Insertar(nuevaAprobacionInstitucion))
                        {
                            ModoInicial();
                        }
                        break;

                    case Generales.EstadoFormulario.Editar:
                        nuevaAprobacionInstitucion.IdAprobacionInstitucion = AprobacionActual.IdAprobacionInstitucion;
                        nuevaAprobacionInstitucion.EsActivo = AprobacionActual.EsActivo;
                        nuevaAprobacionInstitucion.FechaRegistro = AprobacionActual.FechaRegistro;
                        nuevaAprobacionInstitucion.Usuario = AprobacionActual.Usuario;
                        nuevaAprobacionInstitucion.UsuarioIP = AprobacionActual.UsuarioIP;
                        nuevaAprobacionInstitucion.UsuarioPC = AprobacionActual.UsuarioPC;
                        //nuevaAprobacionInstitucion.IdUsuario = AprobacionActual.IdUsuario;
                        if (new AprobacionInstitucionBLL().Actualizar(nuevaAprobacionInstitucion))
                        {
                            ModoInicial();
                        }
                        break;
                }
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
            txtFechaAprobacion.Text = string.Empty;
            txtNivelRiesgo.Text = string.Empty;
            gvDatos.DataSource = null;
            ddlPromotor.SelectedIndex = -1;
            gvDatos.DataBind();
        }

        private void LlenarFormulario()
        {
            txtDescripcion.Text = AprobacionActual.Descripcion;
            DateTime fechaAprobacion = (DateTime)AprobacionActual.FechaHoraVerificacion;
            txtFechaAprobacion.Text = fechaAprobacion.ToShortDateString();
            txtNivelRiesgo.Text = AprobacionActual.NivelRiesgo.ToString();
            ddlPromotor.SelectedValue = AprobacionActual.Usuario1.IdUsuario.ToString();
        }

        private void CargarCombo()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            List<Usuario> promotores = new List<Usuario>();

            promotores = new UsuarioDAO().ObtenerPorIdSucursal(user.IdSucursal);

            ddlPromotor.DataSource = promotores;
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();

            ddlPromotor.Items.Insert(0, new ListItem("Seleccione una Opción"));
        }

        #endregion Otros métodos

        #endregion Métodos
    }
}