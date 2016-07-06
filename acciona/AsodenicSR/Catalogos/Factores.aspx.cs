using AccionaSR.Negocio;
using SisSegLT.Datos;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Catalogos
{
    public partial class Factores : Pagina
    {
        #region Factores

        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioFactores"] == null)
                    ViewState["EstadoFormularioFactores"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioFactores"];
            }
            set
            {
                ViewState["EstadoFormularioFactores"] = value;
            }
        }

        public Factor FactorActual
        {
            get
            {
                if (Session["FactorActualFactores"] == null)
                    Session["FactorActualFactores"] = new Factor();
                return (Factor)Session["FactorActualFactores"];
            }
            set
            {
                Session["FactorActualFactores"] = value;
            }
        }

        #endregion Propiedades

        #region Eventos

        #region Otros eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarSession();
                ModoInicial();
            }
        }

        private void LimpiarSession()
        {
            Session.Remove("FactorActualFactores");
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

        protected void imbItems_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    catCategoria.Visible = true;
                    catCategoria.Mostrar2(Guid.Parse(dataKey.Value.ToString()));
                }
            }
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        #endregion Botones de acción

        #endregion Eventos

        #region Métodos

        #region Modos

        private void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            gvDatos.DataSource = new FactorBLL().Listar();
            gvDatos.DataBind();

            catCategoria.Visible =
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

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            FactorActual = new Factor();

            //litAyuda.Text = "Escriba el nombre para la sucursal y guarde los cambios";

            pnlAgregar.Visible = true;

            catCategoria.Visible =
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
                    FactorActual = new FactorBLL().ObtenerPorIdFactor(Guid.Parse(dataKey.Value.ToString()));

                    //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                    pnlAgregar.Visible = true;

                    catCategoria.Visible =
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
                    FactorActual = new FactorBLL().ObtenerPorIdFactor(Guid.Parse(dataKey.Value.ToString()));
                    if (new FactorBLL().Eliminar(FactorActual))
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
            if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                Factor nuevoFactor = new Factor()
                {
                    Nombre = txtNombre.Text.Trim()
                };
                switch (EstadoFormulario)
                {
                    case Generales.EstadoFormulario.Agregar:
                        nuevoFactor.IdFactor = Guid.NewGuid();
                        nuevoFactor.EsActivo = chkEsActivo.Checked;
                        if (new FactorBLL().Insertar(nuevoFactor))
                        {
                            ModoInicial();
                        }
                        break;

                    case Generales.EstadoFormulario.Editar:
                        nuevoFactor.IdFactor = FactorActual.IdFactor;
                        nuevoFactor.EsActivo = chkEsActivo.Checked;
                        if (new FactorBLL().Actualizar(nuevoFactor))
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

                    if (catCategoria.Visible)
                        if (gvDatos.SelectedIndex >= 0)
                        {
                            var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                            if (dataKey != null)
                            {
                                catCategoria.Mostrar2(Guid.Parse(dataKey.Value.ToString()));
                            }
                        }
                }
            }
        }

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
        }

        private void LlenarFormulario()
        {
            txtNombre.Text = FactorActual.Nombre;
            chkEsActivo.Checked = FactorActual.EsActivo;
        }

        private void CargarCombo()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
        }

        #endregion Otros métodos

        #endregion Métodos

        #endregion Factores
    }
}