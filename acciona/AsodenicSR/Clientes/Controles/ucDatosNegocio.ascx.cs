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
    public partial class ucDatosNegocio : System.Web.UI.UserControl
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
            hfSelectedTab1.Value = hfSelectedTab1.Value;
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
                DatosNegocioActual = new DatosNegocioBLL().ObtenerPorIdDatosNegocio(Guid.Parse(indice.Value.ToString()));
                if (new DatosNegocioBLL().Eliminar(DatosNegocioActual))
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

                DatosNegocioActual = new DatosNegocioBLL().ObtenerPorIdDatosNegocio(Guid.Parse(indice.Value.ToString()));

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
            var dsDatosNegocio = Session["idCliente"] != null ? new DatosNegocioBLL().ObtenerDatosNegocioPorIdCliente(Guid.Parse(Session["idCliente"].ToString())) : null;
            gvDatos.DataSource = dsDatosNegocio;

            if (dsDatosNegocio != null)
            {
                if (dsDatosNegocio.Count > 0)
                {
                    gvDatos.DataBind();
                    hfIdDatosNegocio.Value = dsDatosNegocio.FirstOrDefault().IdDatosNegocio.ToString();
                    Session["idDatosNegocio"] = hfIdDatosNegocio.Value;
                    DoumentoNegocio.ModoInicial();
                    Suplidores.ModoInicial();
                }
                else
                {
                    gvDatos.DataBind();
                    Session["idDatosNegocio"] = null;
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
                            hfIdDatosNegocio.Value = nuevoDatosNegocio.IdDatosNegocio.ToString();
                            nuevoDatosNegocio.EsActivo = chkActivo.Checked;
                            nuevoDatosNegocio.FechaRegistro = DateTime.Now;
                            if (new DatosNegocioBLL().Insertar(nuevoDatosNegocio))
                            {
                                ModoInicial();
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoDatosNegocio.IdDatosNegocio = DatosNegocioActual.IdDatosNegocio;
                            nuevoDatosNegocio.EsActivo = chkActivo.Checked;
                            nuevoDatosNegocio.FechaRegistro = DateTime.Now;
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
                MostrarMensaje(ex.ToString(), TipoMensaje.Danger);                
            }
            
        }

        private DatosNegocio LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            DatosNegocio nuevoDatosNegocio = new DatosNegocio()
            {
                IdCliente = Guid.Parse(Session["idCliente"].ToString()),
                TipoNegocio = txtTipoNegocio.Text.Trim(),
                UbicacionNegocio = txtUbicacion.Text.Trim(),
                //IngresoVolumen = Convert.ToDouble(Double.Parse(txtIngreso.Text, System.Globalization.NumberStyles.Currency).ToString()),  
                IngresoVolumen = txtIngreso.Text == string.Empty ? 0 : double.Parse(txtIngreso.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Tiempo = txtTiempo.Text.Trim(),
                Alquila = rblVivienda.SelectedItem.Text == "Alquilada",
                EsPropio = rblVivienda.SelectedItem.Text == "Propia",
                Familiar = rblVivienda.SelectedItem.Text == "Familiar",
                IdDestinoCredito = Guid.Parse(ddlDestinoCredito.SelectedValue),
                UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                Usuario = user.Login,
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
            rblVivienda.SelectedIndex = -1;
            txtTiempo.Text = string.Empty;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            DoumentoNegocio.LimpiarSesion();
            chkActivo.Checked = false;
            ddlDestinoCredito.ClearSelection();
            Suplidores.LimpiarSesion();
            txtIngreso.Text = string.Empty;
            txtTipoNegocio.Text = string.Empty;  
        }

        private void LlenarFormulario()
        {
            hfIdDatosNegocio.Value = DatosNegocioActual.IdDatosNegocio.ToString();
            txtTipoNegocio.Text = DatosNegocioActual.TipoNegocio;
            txtTiempo.Text = DatosNegocioActual.Tiempo;
            rblVivienda.Items.FindByText("Familiar").Selected = DatosNegocioActual.Familiar;
            rblVivienda.Items.FindByText("Propia").Selected = DatosNegocioActual.EsPropio;
            rblVivienda.Items.FindByText("Alquilada").Selected = DatosNegocioActual.Alquila;
            txtIngreso.Text = string.Format("{0:0,0.00}", Convert.ToDouble(DatosNegocioActual.IngresoVolumen)); 
            txtUbicacion.Text = DatosNegocioActual.UbicacionNegocio;
            ddlDestinoCredito.SelectedValue = DatosNegocioActual.IdDestinoCredito != null ? DatosNegocioActual.IdDestinoCredito.ToString() : ddlDestinoCredito.SelectedValue;
            chkActivo.Checked = DatosNegocioActual.EsActivo;
        }

        private void CargarCombo()
        {
            ddlDestinoCredito.SelectedIndex = -1; //Limpia cualquier selección
            ddlDestinoCredito.DataSource = new DestinoCreditoBLL().ListarActivos();
            ddlDestinoCredito.DataTextField = "Nombre";
            ddlDestinoCredito.DataValueField = "IdDestinoCredito";
            ddlDestinoCredito.DataBind();
        }

        public void LimpiarSesion()
        {
            Session.Remove("idDatosNegocio");
            LimpiarControles();
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