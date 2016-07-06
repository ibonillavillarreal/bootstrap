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

namespace Acciona.Reportes
{
    public partial class RecuperacionCartera : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
           {
               CargarCombo();
           }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSerie.Text != string.Empty || txtFechaInicio.Text != string.Empty || txtFechaFin.Text != string.Empty)
            {
                GenerarReporte(txtFechaInicio.Text, txtFechaInicio.Text, txtSerie.Text, ddlColector.SelectedValue);
            }
        }

        private void GenerarReporte(string fechaInicio, string FechaFin, string serie, string idusuario)
        {
            try
            {

                var resultado = new ListaCobroDAO().ListarRecuperacionCarteraXColector(fechaInicio, FechaFin, serie, idusuario);
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportParameter FechaI = new ReportParameter("FechaI", txtFechaInicio.Text);
                    ReportParameter FechaF = new ReportParameter("FechaF", txtFechaFin.Text);
                    ReportParameter Colector = new ReportParameter("Colector", ddlColector.SelectedItem.Text);

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { FechaI, FechaF, Colector });

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                    ReportViewer1.BorderStyle = BorderStyle.None;

                    pnlReporte.Visible = true;
                    ReportViewer1.LocalReport.Refresh();
                }

            }
            catch (Exception ex)
            {

                throw  new Exception(ex.ToString());
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

        
    }
}