using AccionaSR.Negocio;
using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Controles
{
    public partial class wucClasificacion : System.Web.UI.UserControl
    {
        #region Clasificacion

        #region Propiedades

        //private int EntryID;

        public Generales.EstadoFormulario EstadoFormularioClasificacion
        {
            get
            {
                if (ViewState["EstadoFormularioClasificaciones"] == null)
                    ViewState["EstadoFormularioClasificaciones"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioClasificaciones"];
            }
            set
            {
                ViewState["EstadoFormularioClasificaciones"] = value;
            }
        }

        public Clasificacion ClasificacionActual
        {
            get
            {
                if (Session["ClasificacionActualClasificaciones"] == null)
                    Session["ClasificacionActualClasificaciones"] = new Clasificacion();
                return (Clasificacion)Session["ClasificacionActualClasificaciones"];
            }
            set
            {
                Session["ClasificacionActualClasificaciones"] = value;
            }
        }

        public Guid? IdCategoriaActual
        {
            get
            {
                if (ViewState["IdCategoriaActualClasificaciones"] == null)
                    return null;
                return Guid.Parse(ViewState["IdCategoriaActualClasificaciones"].ToString());
            }
            set
            {
                ViewState["IdCategoriaActualClasificaciones"] = value;
            }
        }

        #endregion Propiedades

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ModoInicial3();
            }
        }

        #region Botones de acción

        protected void imbAgregar3_Click(object sender, ImageClickEventArgs e)
        {
            ModoAgregar3();
        }

        protected void imbEditar3_Click(object sender, ImageClickEventArgs e)
        {
            ModoEditar3();
        }

        protected void imbEliminar3_Click(object sender, ImageClickEventArgs e)
        {
            ModoEliminar3();
        }

        protected void imbCancelar3_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial3();
        }

        protected void imbGuardar3_Click(object sender, ImageClickEventArgs e)
        {
            Guardar3();
        }

        protected void lnbSeleccionar3_Click(object sender, EventArgs e)
        {
            Seleccionar3(sender);
        }

        protected void gvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatos.PageIndex = e.NewPageIndex;
            ModoInicial3();
        }

        #endregion Botones de acción

        #endregion Eventos

        #region Métodos

        #region Modos

        public void Mostrar3(Guid? idCategoria = null)
        {
            IdCategoriaActual = idCategoria;
            ModoInicial3();
        }

        public void ModoInicial3()
        {
            EstadoFormularioClasificacion = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            //gvDatos.SelectedIndex = -1;
            if (IdCategoriaActual != Guid.Empty)
            {
                if (IdCategoriaActual == null)
                    gvDatos.DataSource = new ClasificacionBLL().Listar();
                else
                    gvDatos.DataSource = new ClasificacionBLL().ObtenerPorIdCategoria(Guid.Parse(IdCategoriaActual.ToString()));
            }
            else
            {
                gvDatos.DataSource = null;
            }
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

        private void ModoAgregar3()
        {
            EstadoFormularioClasificacion = Generales.EstadoFormulario.Agregar;

            ClasificacionActual = new Clasificacion();

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

            LimpiarControles3();
            CargarCombo3();
        }

        private void ModoEditar3()
        {
            EstadoFormularioClasificacion = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    ClasificacionActual = new ClasificacionBLL().ObtenerPorIdClasificacion(Guid.Parse(dataKey.Value.ToString()));

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

                    LimpiarControles3();
                    CargarCombo3();
                    LlenarFormulario3();
                }
            }
        }

        private void ModoEliminar3()
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    ClasificacionActual = new ClasificacionBLL().ObtenerPorIdClasificacion(Guid.Parse(dataKey.Value.ToString()));
                    if (new ClasificacionBLL().Eliminar(ClasificacionActual))
                    {
                        ModoInicial3();
                    }
                }
            }
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar3()
        {
            if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                Clasificacion nuevoClasificacion = new Clasificacion()
                {
                    Nombre = txtNombre.Text.Trim(),
                    Puntuacion = Convert.ToInt32(txtPuntuacion.Text)
                };
                switch (EstadoFormularioClasificacion)
                {
                    case Generales.EstadoFormulario.Agregar:
                        nuevoClasificacion.IdClasificacion = Guid.NewGuid();
                        nuevoClasificacion.IdCategoria = Guid.Parse(IdCategoriaActual.ToString());
                        nuevoClasificacion.EsActivo = chkEsActivo.Checked;
                        if (new ClasificacionBLL().Insertar(nuevoClasificacion))
                        {
                            ModoInicial3();
                        }
                        break;

                    case Generales.EstadoFormulario.Editar:
                        nuevoClasificacion.IdClasificacion = ClasificacionActual.IdClasificacion;
                        nuevoClasificacion.IdCategoria = ClasificacionActual.IdCategoria;
                        nuevoClasificacion.EsActivo = chkEsActivo.Checked;
                        if (new ClasificacionBLL().Actualizar(nuevoClasificacion))
                        {
                            ModoInicial3();
                        }
                        break;
                }
            }
        }

        private void Seleccionar3(object sender)
        {
            int indice;
            LinkButton linkButton = (LinkButton)sender;

            int rowIndex = int.Parse(linkButton.CommandArgument.ToString());
            var val = this.gvDatos.DataKeys[rowIndex]["IdClasificacion"];

            int.TryParse(linkButton.CommandArgument, out indice);

            if (indice >= 0)
            {
                if (gvDatos.SelectedIndex == rowIndex)
                {
                    gvDatos.SelectedIndex = -1;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = false;
                }
                else
                {
                    gvDatos.SelectedIndex = rowIndex;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = true;
                }
            }
        }

        private void LimpiarControles3()
        {
            txtNombre.Text = string.Empty;
            txtPuntuacion.Text = string.Empty;
        }

        private void LlenarFormulario3()
        {
            txtNombre.Text = ClasificacionActual.Nombre;
            txtPuntuacion.Text = ClasificacionActual.Puntuacion.ToString();
            chkEsActivo.Checked = ClasificacionActual.EsActivo;
        }

        private void CargarCombo3()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
        }

        #endregion Otros métodos

        #endregion Métodos

        #endregion Clasificacion
    }
}