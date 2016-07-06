using Acciona.App_Code;
using AccionaSR.Negocio;
using Microsoft.Reporting.WebForms;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Reportes
{
    public partial class ReporteEvaluacion : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cliente"] != null)
                {
                    string idEvalucaion = Request.QueryString["cliente"].ToString();
                    GenerarReporte(Guid.Parse(idEvalucaion));
                }
            }
        }

        private void GenerarReporte(Guid idEvalucaion)
        {
            var resultado = new ClienteEvaluacionBLL().ObtenervwClienteEvaluacionPorIdEvaluacion(idEvalucaion);
            if (resultado != null)
            {
                var detalle = new ClienteEvaluacionBLL().ObtenervwClienteEvaluacionCategoriaPorIdEvaluacion(idEvalucaion);
                DataTable dtDetalle = detalle.ToDataTable();
                DataTable dt = resultado.ToDataTable();

                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                ReportDataSource datasourceDetalle = new ReportDataSource("DataSet2", dtDetalle);

                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasourceDetalle);

                pnlReporte.Visible = true;
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}