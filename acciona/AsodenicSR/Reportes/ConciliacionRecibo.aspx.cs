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
    public partial class ConciliacionRecibo : Pagina
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSerie.Text != string.Empty || txtFechaEfectiva.Text != string.Empty)
            {
                GenerarReporte(txtFechaEfectiva.Text, txtSerie.Text);
            }
        }

        private void GenerarReporte(string fecha, string serie)
        {
            try
            {

                var resultado = new ListaCobroDAO().ListarConciliacionRecibos(fecha, serie);
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

                    ReportViewer1.LocalReport.DataSources.Add(datasource);

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
