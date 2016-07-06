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
    public partial class wucCategoria : System.Web.UI.UserControl
    {
        #region Categoria

        #region Propiedades

        public Generales.EstadoFormulario EstadoFormularioCategoria
        {
            get
            {
                if (ViewState["EstadoFormularioCategorias"] == null)
                    ViewState["EstadoFormularioCategorias"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioCategorias"];
            }
            set
            {
                ViewState["EstadoFormularioCategorias"] = value;
            }
        }

        public Categoria CategoriaActual
        {
            get
            {
                if (Session["CategoriaActualCategorias"] == null)
                    Session["CategoriaActualCategorias"] = new Categoria();
                return (Categoria)Session["CategoriaActualCategorias"];
            }
            set
            {
                Session["CategoriaActualCategorias"] = value;
            }
        }

        public Guid? IdFactorActual
        {
            get
            {
                if (ViewState["IdFactorActualCategorias"] == null)
                    return null;
                return Guid.Parse(ViewState["IdFactorActualCategorias"].ToString());
            }
            set
            {
                ViewState["IdFactorActualCategorias"] = value;
            }
        }

        #endregion Propiedades

        #region Eventos

        #region Botones de acción

        protected void imbAgregar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoAgregar2();
        }

        protected void imbEditar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoEditar2();
        }

        protected void imbEliminar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoEliminar2();
        }

        protected void imbCancelar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial2();
        }

        protected void imbGuardar2_Click(object sender, ImageClickEventArgs e)
        {
            Guardar2();
        }

        protected void imbItems2_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    catClasificacion.Visible = true;
                    catClasificacion.Mostrar3(Guid.Parse(dataKey.Value.ToString()));
                }
            }
        }

        protected void lnbSeleccionar2_Click(object sender, EventArgs e)
        {
            Seleccionar2(sender);
        }

        #endregion Botones de acción

        #endregion Eventos

        #region Métodos

        #region Modos

        public void Mostrar2(Guid? idFactor = null)
        {
            IdFactorActual = idFactor;
            ModoInicial2();
        }

        public void ModoInicial2()
        {
            EstadoFormularioCategoria = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            if (IdFactorActual != Guid.Empty)
            {
                if (IdFactorActual == null)
                    gvDatos.DataSource = new CategoriaBLL().Listar();
                else
                    gvDatos.DataSource = new CategoriaBLL().ObtenerPorIdFactor(Guid.Parse(IdFactorActual.ToString()));
            }
            else
            {
                gvDatos.DataSource = null;
            }
            gvDatos.DataBind();

            catClasificacion.Visible =
            pnlAgregar.Visible = false;

            imbActualizar.Enabled =
            imbActualizar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = true;

            imbItems.Enabled =
            imbItems.Visible =
            imbEditar.Enabled =
            imbEditar.Visible =
            imbEliminar.Enabled =
            imbEliminar.Visible =
            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = false;
        }

        private void ModoAgregar2()
        {
            EstadoFormularioCategoria = Generales.EstadoFormulario.Agregar;

            CategoriaActual = new Categoria();

            //litAyuda.Text = "Escriba el nombre para la sucursal y guarde los cambios";

            pnlAgregar.Visible = true;

            catClasificacion.Visible =
            gvDatos.Visible = false;

            imbItems.Enabled =
            imbItems.Visible =
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

            LimpiarControles2();
            CargarCombo2();
        }

        private void ModoEditar2()
        {
            EstadoFormularioCategoria = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    CategoriaActual = new CategoriaBLL().ObtenerPorIdCategoria(Guid.Parse(dataKey.Value.ToString()));

                    //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                    pnlAgregar.Visible = true;

                    catClasificacion.Visible =
                    gvDatos.Visible = false;

                    imbItems.Enabled =
                    imbItems.Visible =
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

                    LimpiarControles2();
                    CargarCombo2();
                    LlenarFormulario2();
                }
            }
        }

        private void ModoEliminar2()
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    CategoriaActual = new CategoriaBLL().ObtenerPorIdCategoria(Guid.Parse(dataKey.Value.ToString()));
                    if (new CategoriaBLL().Eliminar(CategoriaActual))
                    {
                        ModoInicial2();
                    }
                }
            }
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar2()
        {
            if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                Categoria nuevoCategoria = new Categoria()
                {
                    Nombre = txtNombre.Text.Trim(),
                    Ponderacion = Convert.ToDecimal(txtPoderacion.Text)
                };
                switch (EstadoFormularioCategoria)
                {
                    case Generales.EstadoFormulario.Agregar:
                        nuevoCategoria.IdCategoria = Guid.NewGuid();
                        nuevoCategoria.IdFactor = Guid.Parse(IdFactorActual.ToString());
                        nuevoCategoria.EsActivo = chkEsActivo.Checked;
                        if (new CategoriaBLL().Insertar(nuevoCategoria))
                        {
                            ModoInicial2();
                        }
                        break;

                    case Generales.EstadoFormulario.Editar:
                        nuevoCategoria.IdCategoria = CategoriaActual.IdCategoria;
                        nuevoCategoria.IdFactor = CategoriaActual.IdFactor;
                        nuevoCategoria.EsActivo = chkEsActivo.Checked;
                        if (new CategoriaBLL().Actualizar(nuevoCategoria))
                        {
                            ModoInicial2();
                        }
                        break;
                }
            }
        }

        private void Seleccionar2(object sender)
        {
            int indice;
            LinkButton linkButton = (LinkButton)sender;
            int.TryParse(linkButton.CommandArgument, out indice);
            if (indice >= 0)
            {
                if (gvDatos.SelectedIndex == indice)
                {
                    gvDatos.SelectedIndex = -1;
                    imbItems.Enabled =
                    imbItems.Visible =
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = false;
                }
                else
                {
                    gvDatos.SelectedIndex = indice;
                    imbItems.Enabled =
                    imbItems.Visible =
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = true;

                    if (catClasificacion.Visible)
                        if (gvDatos.SelectedIndex >= 0)
                        {
                            var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                            if (dataKey != null)
                            {
                                catClasificacion.Mostrar3(Guid.Parse(dataKey.Value.ToString()));
                            }
                        }
                }
            }
        }

        private void LimpiarControles2()
        {
            txtNombre.Text = string.Empty;
            txtPoderacion.Text = string.Empty;
        }

        private void LlenarFormulario2()
        {
            txtNombre.Text = CategoriaActual.Nombre;
            txtPoderacion.Text = CategoriaActual.Ponderacion.ToString();
            chkEsActivo.Checked = CategoriaActual.EsActivo;
        }

        private void CargarCombo2()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
        }

        #endregion Otros métodos

        #endregion Métodos

        #endregion Categoria
    }
}