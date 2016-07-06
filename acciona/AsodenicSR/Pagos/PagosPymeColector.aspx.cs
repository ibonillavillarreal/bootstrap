using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acciona.App_Code;
using AccionaSR.Negocio;
using Microsoft.Reporting.WebForms;
using SisSegLT.Seguridad;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SisSegLT.Datos;


namespace Acciona.Pagos
{
    public partial class PagosPymeColector : Pagina
    {

        public string IdRutaCobroPadre
        {
            get
            {
                if (Session["IdRutaCobroP"] == null)
                    Session["IdRutaCobroP"] = "";
                return (string)Session["IdRutaCobroP"];
            }
            set
            {
                Session["IdRutaCobroP"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombo();
                btnGuardar.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGuardar, null) + ";");
                gvDatos.DataSource = null;
                gvDatos.DataBind();
                btnGuardar.Enabled = true;
                
            }

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool exito;
            bool exitoRuta;
            string idCobro;
            try
            {
                if (string.IsNullOrEmpty(txtSerie.Text))
                {
                    MostrarMensaje("Por favor ingrese la serie del Recibo", TipoMensaje.Danger);
                    return;
                }
                else
                {
                    Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                    string DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                    string NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);

                    foreach (GridViewRow row in gvDatos.Rows)
                    {
                        
                        string idCuenta = gvDatos.DataKeys[row.RowIndex].Values[0].ToString();
                        idCobro = gvDatos.DataKeys[row.RowIndex].Values[1].ToString();
                        string col1 = row.Cells[2].Text;
                        string col2 = row.Cells[3].Text;
                        string col3 = row.Cells[4].Text;
                        string col4 = row.Cells[5].Text;
                        TextBox tb = (TextBox)row.Cells[6].FindControl("txtMontoRecibido");
                        double colum5 = Convert.ToDouble(tb.Text);
                        string col6 = row.Cells[6].Text;
                        TextBox tbroc = (TextBox)row.Cells[8].FindControl("txtRecibo");
                        string recibo = "";
                        if (!string.IsNullOrEmpty(tbroc.Text))                        
                        {
                            int colum6 = Convert.ToInt16(tbroc.Text.Trim());
                            string fmt = "000000";
                            recibo = colum6.ToString(fmt);
                        }

                        if (string.IsNullOrEmpty(tbroc.Text))
                            continue;
                        else
                        {
                            exito = new CuotaBLL().AbonarCuota(col3, colum5, recibo, idCuenta, txtSerie.Text.Trim().ToUpper(), col1, txtFechaCobro.Text, user.Login, DireccionIP, NombrePC);
                            //actualizar el procesado por cada linea de rutacobro
                            if (exito)
                            {
                                var ruta = new ListaCobroDAO().ObtenerPorIdCobro(Guid.Parse(idCobro));

                                ruta.Procesado = true;
                                ruta.NoRecibo = recibo;
                                ruta.MontoRecibido = colum5;

                                if (exitoRuta = new ListaCobroDAO().Actualizar(ruta) == false)
                                {
                                    MostrarMensaje("Ocurrio un error al guardar el recibo No.: " + recibo, TipoMensaje.Danger);
                                    break;
                                }

                            }

                        }

                    }

                    var principal = new ListaCobroDAO().ObtenerPorIdCobroPrincipal(Guid.Parse(IdRutaCobroPadre));

                    principal.Serie = txtSerie.Text.Trim();
                    principal.Procesado = true;

                    bool exitoPadre = new ListaCobroDAO().ActualizarRutaPadre(principal);
                    btnGuardar.Enabled = false;
                }


                MostrarMensaje("Los pagos se han guardado satisfactoriamente!", TipoMensaje.Success);

                
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.ToString(), TipoMensaje.Danger);
                btnGuardar.Enabled = false;
                gvDatos.DataSource = null;
                gvDatos.DataBind();
            }

        }

        protected void txtMontoRecibido_TextChanged(object sender, EventArgs e)
        {
            double total = 0;
            foreach (GridViewRow gvr in gvDatos.Rows)
            {
                double v1;
                double v2;
                double dif;
                TextBox tb = (TextBox)gvr.Cells[6].FindControl("txtMontoRecibido");
                TextBox tbroc = (TextBox)gvr.Cells[8].FindControl("txtRecibo");
                double sum;

                v1 = Convert.ToDouble(gvr.Cells[5].Text);
                v2 = Convert.ToDouble(tb.Text);
                dif = v1 - v2;
                gvr.Cells[7].Text = dif.ToString();
                if (double.TryParse(tb.Text.Trim(), out sum))
                {
                    total += sum;
                }
            }
            //Muestra  el Total en el pie del grid
            //gvDatos.FooterRow.Cells[6].Text = string.Format("{0:N2}", total.ToString(), "");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //string fecha = txtFechaCobro.Text;
            //GenerarReporte(Guid.Parse(ddlColector.SelectedValue), fecha);
            if (!string.IsNullOrEmpty(txtFechaCobro.Text))
            {

                var ListaExiste = new ListaCobroDAO().ListarRutadeCobro(txtFechaCobro.Text, ddlColector.SelectedItem.ToString());

                if (ListaExiste.Count() <= 0)
                {
                    MostrarMensaje("No hay facturacion pendiente para este dia en la ruta!", TipoMensaje.Danger);
                }
                else if (ListaExiste.FirstOrDefault().Procesado == true)
                {
                    MostrarMensaje("Esta ruta ya ha sido procesada en el dia indicado!", TipoMensaje.Danger);
                }
                else
                {
                    IdRutaCobroPadre = ListaExiste.FirstOrDefault().IdRutaCobro.ToString();
                    DataTable dt = ListaExiste.ToDataTable();

                    gvDatos.DataSource = dt;
                    gvDatos.DataBind();


                    gvDatos.FooterRow.Cells[5].Text = string.Format("{0:N2}", dt.Compute("sum(CuotadelDia)", ""));
                    //gvDatos.FooterRow.Cells[6].Text = string.Format("{0:N2}", dt.Compute("sum(MontoRecibido)", ""));
                }
            }
            else
            {
                MostrarMensaje("Por favor seleccione los parametros necesarios!", TipoMensaje.Danger);
            }
        }


        private void CargarCombo()
        {
            //Combo colector
            //ddlColector.SelectedIndex = -1; //Limpia cualquier selección
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




    }
}