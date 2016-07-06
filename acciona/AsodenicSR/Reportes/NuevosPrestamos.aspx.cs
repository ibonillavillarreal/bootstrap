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
    public partial class NuevosPrestamos : Pagina
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
            if (txtFecha.Text != string.Empty || txtFechaFin.Text != string.Empty || ddlPromotor.Text != string.Empty)
            {
                bool todo;

                todo = chkTodos.Checked == true ? true : false;

                GenerarReporte(txtFecha.Text, txtFechaFin.Text, ddlPromotor.SelectedValue, todo);
            }
            
        }

        private void GenerarReporte(string fecha, string fechafin, string idusuario, bool todo)
        {
            try
            {

                var resultado = new ListaCobroDAO().ObtenerCreditosNuevos(fecha, fechafin, idusuario, todo).ToList();
                if (resultado.Count() > 0)
                {
                    DataTable dt = resultado.ToDataTable();

                    //ReportViewer1.Reset();
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    
                    ReportParameter Fecha = new ReportParameter("Fecha", txtFecha.Text);
                    ReportParameter FechaFin = new ReportParameter("FechaFin", txtFechaFin.Text);
                    ReportParameter Promotor = new ReportParameter();

                    if (todo)
                        Promotor = new ReportParameter("Promotor", "Todos");
                    else
                        Promotor = new ReportParameter("Promotor", ddlPromotor.SelectedItem.Text);

                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { Fecha, FechaFin ,Promotor });

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

        private void CargarCombo()
        {
            //Combo ddlPromotor
            ddlPromotor.SelectedIndex = -1; //Limpia cualquier selección
            ddlPromotor.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Promotor");
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();
        }
    }

}