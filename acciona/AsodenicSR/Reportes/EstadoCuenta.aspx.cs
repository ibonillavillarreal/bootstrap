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
    public partial class EstadoCuenta : Pagina
    {

        public string cedula
        {
            get
            {
                if (ViewState["cedula"] == null)
                {
                    ViewState["cedula"] = new object();
                }
                return (string)ViewState["cedula"];
            }
            set { ViewState["cedula"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {

                if (ddlTipo.SelectedValue == "0")
                {
                    var ListaExiste = new EstadoCuentasDAO().ConsultaEncabezadoEC(txtBuscar.Text);

                    if (ListaExiste.Count() <= 0)
                    {
                        MostrarMensaje("No se encontro ningun resultado!", TipoMensaje.Info);
                    }
                    else
                    {
                        LitCliente.Text = ListaExiste.FirstOrDefault().NombreCompleto;
                        txtSaldo.Text = ListaExiste.FirstOrDefault().SaldoTotal.ToString();
                        cedula = ListaExiste.FirstOrDefault().noidentificacion;

                        gvDatos.DataSource = null;
                        gvDatos.DataBind();

                        txtFechaFin.Text = "";
                        txtFechaInicio.Text = "";
                    }
                }

                
            }
            else
            {
                MostrarMensaje("Por favor seleccione los parametros necesarios!", TipoMensaje.Danger);
            }
        }

        protected void bntBuscarEstado_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFechaFin.Text) || string.IsNullOrEmpty(txtFechaInicio.Text) || string.IsNullOrEmpty(cedula))
            {
                MostrarMensaje("Por favor seleccione los parametros necesarios!", TipoMensaje.Danger);
            }
            else
            {
                var ListaEC = new EstadoCuentasDAO().ConsultaEC(txtBuscar.Text, cedula,Convert.ToDateTime(txtFechaInicio.Text),Convert.ToDateTime(txtFechaFin.Text));

                if (ListaEC.Count() <= 0)
                {
                    MostrarMensaje("No se encontro ningun resultado!", TipoMensaje.Info);
                }
                else
                {
                    double? celda;
                    double saldo1 = 0;
                    double resultado;

                    for (int i = 0; i < ListaEC.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (ListaEC[i].Descripcion == "Saldo Inicial" && ListaEC[i].Debitos == 0)
                            {
                               continue;
                            }
                            else
                            {
                                //saldo1 = Convert.ToDouble(gvDatos.Rows[i].Cells[3].Text);
                                if (string.IsNullOrEmpty(ListaEC[i].Debitos.ToString()))
                                {
                                    ListaEC[i].Debitos = 0;
                                    saldo1 = ListaEC[i].Debitos.Value;
                                }
                                else
                                {
                                    saldo1 = ListaEC[i].Debitos.Value;
                                }

                                ListaEC[0].Saldo = Convert.ToDecimal(saldo1);
                            }
                            
                        }
                        else if (i == 1)
                        {
                            if (ListaEC[1].Descripcion == "Saldo Inicial")
                            {
                                saldo1 = ListaEC[i].Debitos.Value;
                                ListaEC[1].Saldo = Convert.ToDecimal(saldo1);
                            }
                            else
                            {
                                celda = saldo1 + ListaEC[i].Debitos - ListaEC[i].Creditos;
                                ListaEC[i].Saldo = Convert.ToDecimal(celda);
                            }
                            
                        }
                        else
                        {
                            resultado = Convert.ToDouble(ListaEC[i-1].Saldo) + Convert.ToDouble(ListaEC[i].Debitos) - Convert.ToDouble(ListaEC[i].Creditos);
                            ListaEC[i].Saldo = Convert.ToDecimal(resultado);
                        } 
                    }

                    gvDatos.DataSource = ListaEC;
                    gvDatos.DataBind();
                }
            }
        }

        protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    { 

            //    }
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}

        }

        protected void gvDatos_DataBound(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in gvDatos.Rows)
            //{
            //    double saldo = 0;

            //    saldo = Convert.ToDouble(row.Cells[3].Text) - Convert.ToDouble(row.Cells[4].Text);
            //    row.Cells[5].Text = saldo.ToString();

                
            //}

            //double celda;             
            //double saldo1=0;
            //double resultado;

            //for (int i = 0; i < gvDatos.Rows.Count; i++)
            //{
            //    if (i == 0 || gvDatos.Rows[0].Cells[4].Text == "Retiro de Efectivo")
            //    {
            //        saldo1 = Convert.ToDouble(gvDatos.Rows[i].Cells[3].Text);
            //        gvDatos.Rows[0].Cells[5].Text = saldo1.ToString();
            //    }
            //    else if (i == 1)
            //    {
            //        celda = saldo1 + Convert.ToDouble(gvDatos.Rows[i].Cells[3].Text) - Convert.ToDouble(gvDatos.Rows[i].Cells[4].Text);
            //        gvDatos.Rows[0].Cells[5].Text = celda.ToString();
            //    }
            //    else
            //    {
            //        resultado = Convert.ToDouble(gvDatos.Rows[0].Cells[5].Text) + Convert.ToDouble(gvDatos.Rows[i].Cells[3].Text) - Convert.ToDouble(gvDatos.Rows[i].Cells[4].Text);
            //        gvDatos.Rows[0].Cells[5].Text = resultado.ToString();
            //    }
            //}
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