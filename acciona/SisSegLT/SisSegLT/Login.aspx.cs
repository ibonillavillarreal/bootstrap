using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;

namespace Acciona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUser.Text;
                string pass = txtPass.Text;
                Usuario usuario = Consulta.ValidarCredenciales(user, pass);
                if (usuario != null)
                {
                    Session["UserAsodenicAutentication"] = usuario;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    MostrarMensaje("Por favor verifique sus credenciales!", TipoMensaje.Danger);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo completar la accion!" + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
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
                                                                                    $('.alert').fadeTo(50, 0).slideUp(50, function() {
                                                                                        $(this).remove();
                                                                                    });
                                                                                }, 50);
                                                                            });", true);
        }

        internal enum TipoMensaje
        {
            Info,
            Success,
            Warning,
            Danger
        }
    }
}