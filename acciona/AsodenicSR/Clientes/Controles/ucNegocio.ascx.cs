using Acciona.App_Code;
using AccionaSR.Negocio;
using ControlesPersonalizados;
using Newtonsoft.Json;
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
    public partial class ucNegocio : System.Web.UI.UserControl
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioNegocio"] == null)
                    ViewState["EstadoFormularioNegocio"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioNegocio"];
            }
            set
            {
                ViewState["EstadoFormularioNegocio"] = value;
            }
        }

        public DatosNegocio DatosNegocioActual
        {
            get
            {
                if (ViewState["NegocioActuales"] == null)
                    ViewState["NegocioActuales"] = new DatosNegocio();
                return JsonConvert.DeserializeObject<DatosNegocio>(ViewState["NegocioActuales"].ToString());
            }
            set
            {
                string json = JsonConvert.SerializeObject(value, Formatting.Indented,
                                   new JsonSerializerSettings
                                   {
                                       PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                   });
                ViewState["NegocioActuales"] = json;
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
            //Setear el tab seleccionado
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

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            gvDatos.Visible = true;
            gvDatos.SelectedIndex = -1;
            var dsDatosNegocio = Session["idCliente"] != null ? new DatosNegocioBLL().ObtenerDatosNegocioPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsDatosNegocio;

            if (dsDatosNegocio != null)
            {
                if (dsDatosNegocio.Count > 0)
                {
                    gvDatos.DataBind();
                }
            }

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

            DatosNegocioActual = new DatosNegocio();

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
                    DatosNegocioActual = new DatosNegocioBLL().ObtenerPorIdDatosNegocio(Guid.Parse(dataKey.Value.ToString()));

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
                    DatosNegocioActual = new DatosNegocioBLL().ObtenerPorIdDatosNegocio(Guid.Parse(dataKey.Value.ToString()));
                    if (new DatosNegocioBLL().Eliminar(DatosNegocioActual))
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
                if (!string.IsNullOrEmpty(txtUbicacion.Text.Trim()))
                {
                    DatosNegocio nuevoDatosNegocio = LlenarObjeto();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoDatosNegocio.IdDatosNegocio = Guid.NewGuid();
                            Session["idDatosNegocio"] = nuevoDatosNegocio.IdDatosNegocio;
                            nuevoDatosNegocio.EsActivo = chkActivo.Checked;
                            nuevoDatosNegocio.FechaRegistro = DateTime.Now;

                            if (new DatosNegocioBLL().Insertar(nuevoDatosNegocio))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoDatosNegocio.IdDatosNegocio = DatosNegocioActual.IdDatosNegocio;
                            //nuevoDatosNegocio.EsActivo = DatosNegocioActual.EsActivo;
                            if (new DatosNegocioBLL().Actualizar(nuevoDatosNegocio))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo completar la accion!", TipoMensaje.Danger);
            }
            
        }

        private DatosNegocio LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            double ingreso = 0;

            ingreso = Double.Parse(txtIngreso.Text, System.Globalization.NumberStyles.Number, Generales.cultura);

            DatosNegocio nuevoDatosNegocio = new DatosNegocio()
            {
                TipoNegocio = txtTipoNegocio.Text.Trim(),
                IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                UbicacionNegocio = txtUbicacion.Text.Trim(),
                IngresoVolumen = ingreso,
                Tiempo = txtTiempo.Text.Trim(),
                Alquila = rbAlquilada.Checked,
                EsPropio = rbPropia.Checked,
                Familiar = rbFamiliar.Checked,
                EsActivo = chkActivo.Checked,
                UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                Usuario = user.Login
            };
            return nuevoDatosNegocio;
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
            txtUbicacion.Text = string.Empty;
            rbFamiliar.Checked =
            rbAlquilada.Checked =
            rbPropia.Checked = false;
            chkActivo.Checked = false;
            txtTiempo.Text = string.Empty;
        }

        private void LlenarFormulario()
        {
            txtTipoNegocio.Text = DatosNegocioActual.TipoNegocio;
            txtTiempo.Text = DatosNegocioActual.Tiempo;
            rbPropia.Checked = DatosNegocioActual.EsPropio;
            rbFamiliar.Checked = DatosNegocioActual.Familiar;
            rbAlquilada.Checked = DatosNegocioActual.Alquila;
            txtIngreso.Text = DatosNegocioActual.IngresoVolumen.ToString();
            txtUbicacion.Text = DatosNegocioActual.UbicacionNegocio;
            chkActivo.Checked = DatosNegocioActual.EsActivo;
        }

        private void CargarCombo()
        {
            //ddlControl.SelectedIndex = -1; //Limpia cualquier selección
            //ddlControl.DataSource = new EntidadBLL().Listar();
            //ddlControl.DataBind();
        }

        private void LimpiarSesion()
        {
            Session.Remove("idDatosNegocio");
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