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
using System.Data;

namespace Acciona.Reportes
{
    public partial class DetallePagos : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {


                var ListaExiste = new EstadoCuentasDAO().ConsultaxCodigoVehiculo(txtBuscar.Text);

                if (ListaExiste.Count() <= 0)
                {
                    MostrarMensaje("No se encontro ningun resultado!", TipoMensaje.Info);
                }
                else
                {
                    LitCliente.Text = new EstadoCuentasDAO().ClienteXCodigoVehiculo(txtBuscar.Text).FirstOrDefault().NombreCompleto;

                    gvDatos.DataSource = null;
                    gvDatos.DataBind();

                    txtCodigo.Text = ListaExiste.FirstOrDefault().Codigo;
                    txtMarca.Text = ListaExiste.FirstOrDefault().Marca.ToString();
                    txtModelo.Text = ListaExiste.FirstOrDefault().Modelo;
                    txtAnio.Text = ListaExiste.FirstOrDefault().Anio;

                    gvDatos.DataSource = ListaExiste;
                    gvDatos.DataBind();

                }



            }
            else
            {
                MostrarMensaje("Por favor seleccione los parametros necesarios!", TipoMensaje.Danger);
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
    }
}