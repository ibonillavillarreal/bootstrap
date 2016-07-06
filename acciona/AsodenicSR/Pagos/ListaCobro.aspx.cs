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

    public partial class ListaCobro : Pagina
    {

        //List<SP_ListadeCobroXColector_Result> Lista = new List<SP_ListadeCobroXColector_Result>();    
         

        public List<SP_ListadeCobroXColector_Result> Lista
        {
            get
            {
                if (Session["MovimientoActuales"] == null)
                    Session["MovimientoActuales"] = new List<SP_ListadeCobroXColector_Result>();
                return (List<SP_ListadeCobroXColector_Result>)Session["MovimientoActuales"];
            }
            set
            {
                Session["MovimientoActuales"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombo();
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            Lista.Clear();
            pnlGrid.Visible = false;
            litmensaje.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //string fecha = txtFechaCobro.Text;
            //GenerarReporte(Guid.Parse(ddlColector.SelectedValue), fecha);
            try
            {
                if (!string.IsNullOrEmpty(txtFechaCobro.Text))
                {
                    var ListaExiste = new ListaCobroDAO().ListarRutadeCobro(txtFechaCobro.Text, ddlColector.SelectedItem.ToString());
                  

                    if (ListaExiste.Count() > 0)
                    {
                        MostrarMensaje("Ya se ha impreso esta ruta de cobro para el colector!", TipoMensaje.Danger);
                    }
                    else
                    {
                        var resultado = new CuotaBLL().ObtenerListadeCobroxColector(Guid.Parse(ddlColector.SelectedValue), txtFechaCobro.Text);
                        if (resultado.Count() > 0)
                        {

                            gvDatos.DataSource = null;
                            gvDatos.DataBind();

                            DataTable dt = resultado.ToDataTable();
                            pnlGrid.Visible = true;
                            Lista = resultado;

                            gvDatos.DataSource = resultado;
                            gvDatos.DataBind();

                            gvDatos.FooterRow.Cells[7].Text = string.Format("{0:N2}", dt.Compute("sum(SaldoTotal)", ""));
                            gvDatos.FooterRow.Cells[8].Text = string.Format("{0:N2}", dt.Compute("sum(CuotadelDia)", ""));
                            gvDatos.FooterRow.Cells[9].Text = string.Format("{0:N2}", dt.Compute("sum(Mora)", ""));
                            gvDatos.FooterRow.Cells[11].Text = string.Format("{0:N2}", dt.Compute("sum(CuotaIdeal)", ""));
                        }
                        else
                        {
                            MostrarMensaje("No se encontro ningun resultado", TipoMensaje.Info);
                            gvDatos.DataSource = null;
                            gvDatos.DataBind();

                        }
                    }

                }
                else
                {
                    gvDatos.DataSource = null;
                    gvDatos.DataBind();
                    MostrarMensaje("Por favor seleccione los parametros necesarios!", TipoMensaje.Danger);
                }
            }
            catch (Exception ex)
            {
               MostrarMensaje(ex.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void GenerarReporte()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                string DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                string NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);

                if (Lista.Count() > 0)
                {
                    DataTable dt = Lista.ToDataTable();

                    List<RutaCobro> Listado = new List<RutaCobro>();
                    RutaCobro ruta = new RutaCobro();
                    RutaCobroPrincipal rutaMain = new RutaCobroPrincipal();

                    rutaMain.IdRutaCobro = Guid.NewGuid();
                    rutaMain.IdColector = Guid.Parse(ddlColector.SelectedValue);
                    rutaMain.Nombre = ddlColector.SelectedItem.ToString();
                    rutaMain.Procesado = false;
                    rutaMain.Impreso = true;
                    rutaMain.FechaRegistro = DateTime.Now;
                    rutaMain.Usuario = user.Login;
                    rutaMain.DireccionIP = DireccionIP;
                    rutaMain.DireccionPC = NombrePC;


                    bool exito1 = new ListaCobroDAO().InsertarRutaPrincipal(rutaMain);

                    if (!exito1)
                    {         
                        MostrarMensaje("Error al guardar Principal!", TipoMensaje.Danger);
                        return;
                    }

                    foreach (var item in Lista)
                    {
                        ruta = new RutaCobro();
                        ruta.IdCobro = Guid.NewGuid();
                        ruta.IdRutaCobro = rutaMain.IdRutaCobro;
                        ruta.IdCuenta = new ClienteCuentaBLL().ObtenerPorNoCuenta(item.NoCuenta).FirstOrDefault().IdCuenta;
                        ruta.NombreCompleto = item.NombreCompleto;
                        ruta.NoIdentificacion = item.NoIdentificacion;
                        ruta.Direccion = item.Direccion;
                        ruta.Frecuencia = item.Frecuencia;
                        ruta.SaldoTotal = Convert.ToDouble(item.SaldoTotal);
                        ruta.NoCuenta = item.NoCuenta;
                        ruta.CuotadelDia = Convert.ToDouble(item.CuotadelDia);
                        ruta.Mora = Convert.ToDouble(item.Mora);
                        ruta.CuotasPendientes = Convert.ToDouble(item.Cantidad);
                        ruta.CuotaIdeal = Convert.ToDouble(item.CuotaIdeal);
                        ruta.Procesado = false;
                        ruta.FechaCobro = Convert.ToDateTime(txtFechaCobro.Text);
                        ruta.MontoRecibido = Convert.ToDouble(item.CuotadelDia);
                        ruta.NoRecibo = "";
                        ruta.Diferencia = 0;
                        ruta.FechaRegistro = DateTime.Now;
                        ruta.Colector = item.Colector;
                        ruta.Usuario = user.Login;
                        ruta.DireccionIP = DireccionIP;
                        ruta.NombrePC = NombrePC;
                        Listado.Add(ruta);
                    }

                    bool exito = new ListaCobroDAO().Insertar(Listado);

                    if (!exito)
                    {
                        MostrarMensaje("Error al guardar detalle!", TipoMensaje.Danger);
                        return;
                    }

                    rvListaCobro.SizeToReportContent = true;
                    rvListaCobro.LocalReport.DataSources.Clear();

                    ReportParameter Fecha = new ReportParameter("Fecha", txtFechaCobro.Text);
                    rvListaCobro.LocalReport.SetParameters(new ReportParameter[] { Fecha });

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

                    rvListaCobro.LocalReport.DataSources.Add(datasource);
                    rvListaCobro.BorderStyle = BorderStyle.None;

                    pnlGrid.Visible = false;
                    pnlReporte.Visible = true;
                    rvListaCobro.LocalReport.Refresh();

                    Lista.Clear();
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje(ex.ToString(), TipoMensaje.Danger);
            }
        }

        //private void GenerarReporte(Guid idColector, string fecha)
        //{
        //    try
        //    {

        //        var resultado = new CuotaBLL().ObtenerListadeCobroxColector(idColector, fecha);

        //        if (resultado.Count() > 0)
        //        {
        //            DataTable dt = resultado.ToDataTable();                     

        //            rvListaCobro.SizeToReportContent = true;
        //            rvListaCobro.LocalReport.DataSources.Clear();

        //            ReportParameter Fecha = new ReportParameter("Fecha", txtFechaCobro.Text);
        //            rvListaCobro.LocalReport.SetParameters(new ReportParameter[] { Fecha });

        //            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

        //            rvListaCobro.LocalReport.DataSources.Add(datasource);
        //            rvListaCobro.BorderStyle = BorderStyle.None;

        //            pnlReporte.Visible = true;
        //            rvListaCobro.LocalReport.Refresh();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        protected void btnReimpresion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFechaCobro.Text))
            {
                var ListaExiste = new ListaCobroDAO().ListarRutadeCobro(txtFechaCobro.Text, ddlColector.SelectedItem.ToString());
                var procesados = ListaExiste.Where(x=>x.Procesado == true).ToList();
                if (procesados.Count() == 0)
                {
                    DataTable dt = ListaExiste.ToDataTable();

                    rvListaCobro.SizeToReportContent = true;
                    rvListaCobro.LocalReport.DataSources.Clear();

                    ReportParameter Fecha = new ReportParameter("Fecha", txtFechaCobro.Text);
                    rvListaCobro.LocalReport.SetParameters(new ReportParameter[] { Fecha });

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

                    rvListaCobro.LocalReport.DataSources.Add(datasource);
                    rvListaCobro.BorderStyle = BorderStyle.None;

                    pnlGrid.Visible = false;
                    pnlReporte.Visible = true;
                    rvListaCobro.LocalReport.Refresh();

                    Lista.Clear();
                }
                else
                {
                    MostrarMensaje("Este listado ya ha sido procesado y tiene recibos asociados", TipoMensaje.Info);
                }

            }
            else
            {
                MostrarMensaje("Por favor seleccione una fecha", TipoMensaje.Info);
            }
                  

        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //string fecha = txtFechaCobro.Text;
            //GenerarReporte(Guid.Parse(ddlColector.SelectedValue), fecha);
            if (Lista.Count() > 0)
                GenerarReporte();
            else
                MostrarMensaje("Por favor primero verifique el listado de clientes", TipoMensaje.Danger);
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
    }
}