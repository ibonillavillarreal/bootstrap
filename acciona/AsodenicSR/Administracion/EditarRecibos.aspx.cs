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

namespace Acciona.Administracion
{
    public partial class EditarRecibos : Pagina
    {

        public tCredito CreditoActual
        {
            get
            {
                if (Session["CreditoActuales"] == null)
                    Session["CreditoActuales"] = new tCredito();
                return (tCredito)Session["CreditoActuales"];
            }
            set
            {
                Session["CreditoActuales"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarControles();
                ModoInicial();

            }
        }

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

                if (!String.IsNullOrEmpty(txtSerieb.Text) && !String.IsNullOrEmpty(txtRecibob.Text))
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

                if (CreditoActual != null)
                {
                    CreditoActual.FechaEfectiva = Convert.ToDateTime(txtFechaEfectiva.Text);
                    //CreditoActual.IdMovimiento = Guid.Parse(Session["IdMovimiento"].ToString());
                    CreditoActual.Serie = txtSerie.Text;
                    CreditoActual.NoReferencia = txtNoRecibo.Text;
                    CreditoActual.MontoRecibido = double.Parse(txtMonto.Text, System.Globalization.NumberStyles.Currency, Generales.cultura);
                    CreditoActual.Usuario = user.Login;
                    CreditoActual.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                    CreditoActual.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                    exito = new RecibosDAO().Actualizar(CreditoActual);

                    if (exito)
                        MostrarMensaje("Se ha actualizado el recibo con exito!", TipoMensaje.Success);                  
                    else                         
                        MostrarMensaje("Error al actualizar!", TipoMensaje.Danger); 

                }

                LimpiarControles();
                
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void LimpiarControles()
        {
            txtSerie.Text = "";
            txtSerieb.Text = "";
            txtRecibob.Text = "";
            txtNoRecibo.Text = "";
            txtCliente.Text = "";
            txtFechaEfectiva.Text = "";
            txtMonto.Text = "";
            CreditoActual = null;
         
        }

        private void LlenarFormulario()
        {
            CreditoActual = new RecibosDAO().ObtenerPorNoReciboySerie(txtRecibob.Text.Trim(), txtSerieb.Text.Trim());

            if (CreditoActual != null)
            {
                txtCliente.Text = Page.Server.HtmlDecode(CreditoActual.Recibidode);
                txtMonto.Text = CreditoActual.MontoRecibido.ToString();
                txtFechaEfectiva.Text = CreditoActual.FechaEfectiva.ToString();
                txtSerie.Text = CreditoActual.Serie;
                txtNoRecibo.Text = CreditoActual.NoReferencia;
                Session["IdMovimiento"]= CreditoActual.IdMovimiento;
            }                                      
            else
            {
                MostrarMensaje("No se encontraron resultados", TipoMensaje.Danger);
            }
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