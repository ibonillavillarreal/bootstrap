using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccionaSR.Negocio;
using SisSegLT.Datos;
using SisSegLT.Seguridad;

namespace Acciona.Catalogos
{
    public partial class DestinoCreditos : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioDestino"] == null)
                    ViewState["EstadoFormularioDestino"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioDestino"];
            }
            set
            {
                ViewState["EstadoFormularioDestino"] = value;
            }
        }

        public DestinoCredito DestinoActual
        {
            get
            {
                if (Session["DestinoActualSesion"] == null)
                    Session["DestinoActualSesion"] = new DestinoCredito();
                return (DestinoCredito)Session["DestinoActualSesion"];
            }
            set
            {
                Session["DestinoActualSesion"] = value;
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

        private void LimpiarSesion()
        {
            Session.Remove("DestinoActualSesion");
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

        #endregion Eventos

        #region Métodos

        #region Modos

        private void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            gvDatos.DataSource = new DestinoCreditoBLL().Listar();
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

            DestinoActual = new DestinoCredito();

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
                    DestinoActual = new DestinoCreditoBLL().ObtenerPorIdDestinoCredito(Guid.Parse(dataKey.Value.ToString()));

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
                    DestinoActual = new DestinoCreditoBLL().ObtenerPorIdDestinoCredito(Guid.Parse(dataKey.Value.ToString()));
                    if (new DestinoCreditoBLL().Eliminar(DestinoActual))
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
                DestinoCredito nuevoDestinoCredito = new DestinoCredito()
                {
                    Nombre = txtNombre.Text.Trim(),
                    EsActivo = chkActivo.Checked
                };
                switch (EstadoFormulario)
                {
                    case Generales.EstadoFormulario.Agregar:
                        nuevoDestinoCredito.IdDestinoCredito = Guid.NewGuid();
                        //nuevoMetodologia.EsActivo = true;
                        if (new DestinoCreditoBLL().Insertar(nuevoDestinoCredito))
                        {
                            ModoInicial();
                        }
                        break;

                    case Generales.EstadoFormulario.Editar:
                        nuevoDestinoCredito.IdDestinoCredito = DestinoActual.IdDestinoCredito;
                        //nuevoDestinoCredito.EsActivo = MetodologiaActual.EsActivo;
                        if (new DestinoCreditoBLL().Actualizar(nuevoDestinoCredito))
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
            txtNombre.Text = string.Empty;
            chkActivo.Checked = false;
        }

        private void LlenarFormulario()
        {
            txtNombre.Text = DestinoActual.Nombre;
            chkActivo.Checked = (bool)DestinoActual.EsActivo == null ? true : (bool)DestinoActual.EsActivo;
        }

        private void CargarCombo()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
        }

        #endregion Otros métodos

        #endregion Métodos
    }
}