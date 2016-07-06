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
    public partial class Cartera : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string anio = ddlAnio.SelectedValue.ToString();
            GenerarReporte(Convert.ToInt16(anio));

        }

        private void GenerarReporte(int anio)
        {
            try
            {

                var resultado = new ListaCobroDAO().ListarRecuperacionPorMes(anio).ToList();
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    //ReportViewer1.Reset();
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportParameter Anio = new ReportParameter("Anio", anio.ToString());

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Anio });

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