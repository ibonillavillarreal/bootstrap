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
    public partial class ConsolidadoPorTransacciones : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombo();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text != string.Empty || txtFechaFin.Text != string.Empty || ddlTipoTransaccion.SelectedItem.ToString() != string.Empty)
            {
                bool todo;

                todo = chkTodos.Checked == true ? true : false;

                GenerarReporte(txtFecha.Text, txtFechaFin.Text, ddlTipoTransaccion.SelectedItem.ToString(), todo);
            }

        }

        private void GenerarReporte(string fecha, string fechafin, string tipoTransaccion, bool todo)
        {
            try
            {

                var resultado = new MovimientosDAO().ObtenerCreditosPorTipoTransaccion(tipoTransaccion,fecha, fechafin, todo).ToList();
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    //ReportViewer1.Reset();
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportParameter Fecha = new ReportParameter("FechaI", txtFecha.Text);
                    ReportParameter FechaFin = new ReportParameter("FechaF", txtFechaFin.Text);
                    ReportParameter TipoTran = new ReportParameter();

                    if (todo)
                        TipoTran = new ReportParameter("TipoTran", "Todos");
                    else
                        TipoTran = new ReportParameter("TipoTran", ddlTipoTransaccion.SelectedItem.ToString());

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Fecha, FechaFin, TipoTran });

                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);

                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                    ReportViewer1.BorderStyle = BorderStyle.None;

                    pnlReporte.Visible = true;
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    ReportViewer1.LocalReport.DataSources.Clear();                        
                    ReportViewer1.LocalReport.Refresh();
                    pnlReporte.Visible = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void CargarCombo()
        {
            //Combo ddlPromotor
            ddlTipoTransaccion.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoTransaccion.DataSource = new TipoTransaccionBLL().ObtenerListaTransacciones();
            ddlTipoTransaccion.DataTextField = "Descripcion";
            ddlTipoTransaccion.DataValueField = "IdTipoTransaccion";
            ddlTipoTransaccion.DataBind();
        }
    }
}