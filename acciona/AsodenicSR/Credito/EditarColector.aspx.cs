using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Seguridad;
using Acciona.App_Code;
using AccionaSR.Negocio;
using Newtonsoft.Json;
using SisSegLT.Datos;
using System.Globalization;

namespace Acciona.Credito
{
    public partial class EditarColector : Pagina
    {


        #region Eventos

     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarControles();
                ModoInicial();
                CargarCombo();

            }
        }

     

        
        #endregion Eventos

        #region Botones de acción


        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!String.IsNullOrEmpty(txtBuscar.Text))
                {
                    LlenarFormulario();
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Info);
            }

        }


        #endregion Botones de acción
        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            
            imbGuardar.Enabled =
            imbGuardar.Visible = true;

        }



        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                bool exito = false;

                var listaMovimientos = new MovimientoBLL().ObtenerPorNoMovimiento(txtNoPrestamo.Text);
                if (listaMovimientos.Count > 0)
                {
                    //listaMovimientos.FirstOrDefault().IdColector = Guid.Parse(ddlColector.SelectedValue);
                    listaMovimientos.FirstOrDefault().Usuario = user.Login;
                    listaMovimientos.FirstOrDefault().DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                    listaMovimientos.FirstOrDefault().NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                    exito = new MovimientoBLL().Actualizar(listaMovimientos.FirstOrDefault()); 

                    if (exito)
                    {
                        //listaMovimientos.FirstOrDefault().tMovimientos1.FirstOrDefault().IdColector = Guid.Parse(ddlColector.SelectedValue);
                        listaMovimientos.FirstOrDefault().tMovimientos1.FirstOrDefault().Usuario = user.Login;
                        listaMovimientos.FirstOrDefault().tMovimientos1.FirstOrDefault().DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                        listaMovimientos.FirstOrDefault().tMovimientos1.FirstOrDefault().NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                        exito = new MovimientoBLL().Actualizar(listaMovimientos.FirstOrDefault().tMovimientos1.FirstOrDefault()); 
                        if (exito == false)
                            MostrarMensaje("Error al actualizar!", TipoMensaje.Danger);
                    }

                }

                LimpiarControles();
                MostrarMensaje("Se ha actualizado el colector!", TipoMensaje.Success);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void LimpiarControles()
        {
            txtNoPrestamo.Text = "";
            txtFechaAprobacion.Text = "";
            ddlColector.ClearSelection();
            txtCliente.Text = "";
        }

        private void LlenarFormulario()
        {
            var ClienteMovimiento = new ClienteBLL().ObtenerPorNombre(txtBuscar.Text.Trim());
            if (ClienteMovimiento.Count > 0)
            {
                txtCliente.Text = ClienteMovimiento.FirstOrDefault().NombreCompleto;
                var cuenta = new MovimientosDAO().ListarMovimientoParaCambio(ClienteMovimiento.FirstOrDefault().IdCliente);

                if (cuenta.Count > 0)
                {
                    txtNoPrestamo.Text = cuenta.LastOrDefault().NoMovimiento;
                    txtFechaAprobacion.Text = cuenta.LastOrDefault().FechaEfectiva.Value.ToShortDateString();
                    //ddlColector.SelectedValue = cuenta.LastOrDefault().IdColector != null ? cuenta.LastOrDefault().IdColector.ToString() : ddlColector.SelectedValue;
                }
            }
            else
            {
                MostrarMensaje("No se encontraron resultados", TipoMensaje.Danger);
            }
        }

        private void CargarCombo()
        {
            //Combo colector
            ddlColector.SelectedIndex = -1; //Limpia cualquier selección
            ddlColector.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Cobro");
            ddlColector.DataTextField = "Nombre";
            ddlColector.DataValueField = "IdUsuario";
            ddlColector.DataBind();
        }

        

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            litmensaje.Text = string.Empty;
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