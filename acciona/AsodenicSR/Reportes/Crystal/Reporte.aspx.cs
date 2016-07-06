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

namespace Acciona.Reportes.Crystal
{
    public partial class Reporte : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if (Request.QueryString["idmovimiento"] != null)
                {
                    string noIdentificacion = Request.QueryString["idmovimiento"].ToString();
                    GenerarReporte(noIdentificacion);
                } 
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text != string.Empty)
            {
                GenerarReporte(txtIdentificacion.Text);
            }
        }
        private void GenerarReporte(string idmovimiento)
        {
            try
            {

                var resultado = new MovimientoBLL().ObtenerDatosGeneralesPorIdMovimiento(Guid.Parse(idmovimiento));
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    var calculoCuotas = new MovimientoBLL().ObtenerListaCuotasPorIdMovimiento(Guid.Parse(idmovimiento));
                    DataTable dtCuotas = calculoCuotas.ToDataTable();

                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                    ReportDataSource datasourceCuotas = new ReportDataSource("DataSet2", dtCuotas);

                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceCuotas);

                    ReportViewer1.BorderStyle = BorderStyle.None;

                    pnlReporte.Visible = true;
                    ReportViewer1.LocalReport.Refresh();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}