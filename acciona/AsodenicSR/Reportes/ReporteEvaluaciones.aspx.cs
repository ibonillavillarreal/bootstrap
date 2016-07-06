using Acciona.App_Code;
using Acciona.ASODENICDBDataSetTableAdapters;
using AccionaSR.Negocio;
using Microsoft.Reporting.WebForms;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Reportes
{
    public partial class ReporteEvaluaciones : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlResultados.Visible = true;
            pnlReporte.Visible = false;

            if (txtFechaInicio.Text != string.Empty && txtFechaFin.Text != string.Empty)
            {
                var fechainicio = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fechafin = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string sucursal = ddlSucursal.SelectedIndex != 0 ? ddlSucursal.SelectedItem.Text : string.Empty;
                string metodologia = ddlMetodogolia.SelectedIndex != 0 ? ddlMetodogolia.SelectedItem.Text : string.Empty;
                string nivelRiesgo = ddlNivelRiesgo.SelectedIndex != 0 ? ddlNivelRiesgo.SelectedItem.Text : string.Empty;
                string promotor = ddlPromotor.SelectedIndex != 0 ? ddlPromotor.SelectedItem.Text : string.Empty;

                var resultado = new ClienteEvaluacionBLL().ObtenerEvaluacionesPorFiltro(fechainicio, fechafin, sucursal, metodologia, nivelRiesgo, promotor);
                if (resultado.Count() > 0)
                {
                    grvResultados.DataSource = resultado.ToList();
                    grvResultados.DataBind();

                    btnGenerar.Visible = true;
                }
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            pnlResultados.Visible = false;
            pnlReporte.Visible = true;

            if (txtFechaInicio.Text != string.Empty && txtFechaFin.Text != string.Empty)
            {
                var fechainicio = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fechafin = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string sucursal = ddlSucursal.SelectedIndex != 0 ? ddlSucursal.SelectedItem.Text : string.Empty;
                string metodologia = ddlMetodogolia.SelectedIndex != 0 ? ddlMetodogolia.SelectedItem.Text : string.Empty;
                string nivelRiesgo = ddlNivelRiesgo.SelectedIndex != 0 ? ddlNivelRiesgo.SelectedItem.Text : string.Empty;
                string promotor = ddlPromotor.SelectedIndex != 0 ? ddlPromotor.SelectedItem.Text : string.Empty;

                var resultado = new ClienteEvaluacionBLL().ObtenerEvaluacionesPorFiltro(fechainicio, fechafin, sucursal, metodologia, nivelRiesgo, promotor);
                if (resultado.Count() > 0)
                {
                    //ReportViewer1.Reset();
                    //ReportViewer1.LocalReport.ReportPath = "Reports/rptSalesSummary.rdlc";

                    DataTable dt = resultado.ToDataTable();
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportParameter[] parameters = new ReportParameter[6];
                    parameters[0] = new ReportParameter("FechaInicio", fechainicio.ToString("dd/MM/yyyy"));
                    parameters[1] = new ReportParameter("Sucursal", sucursal);
                    parameters[2] = new ReportParameter("Metodologia", metodologia);
                    parameters[3] = new ReportParameter("NivelRiesgo", nivelRiesgo);
                    parameters[4] = new ReportParameter("FechaFin", fechafin.ToString("dd/MM/yyyy"));
                    parameters[5] = new ReportParameter("Promotor", promotor);

                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.SetParameters(parameters.ToList());
                }
            }
        }

        private void CargarControles()
        {
            var metodologias = new MetodologiaBLL().Listar();
            ddlMetodogolia.DataSource = metodologias;
            ddlMetodogolia.DataTextField = "Nombre";
            ddlMetodogolia.DataValueField = "IdMetodologia";
            ddlMetodogolia.DataBind();
            ddlMetodogolia.Items.Insert(0, new ListItem("Seleccione una Opción"));

            var niveles = new MatrizCalificacionBLL().Listar();
            ddlNivelRiesgo.DataSource = niveles;
            ddlNivelRiesgo.DataTextField = "Nombre";
            ddlNivelRiesgo.DataValueField = "IdMatrizCalificacion";
            ddlNivelRiesgo.DataBind();
            ddlNivelRiesgo.Items.Insert(0, new ListItem("Seleccione una Opción"));

            var promotores = new UsuarioBLL().Listar();
            ddlPromotor.DataSource = promotores;
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();
            ddlPromotor.Items.Insert(0, new ListItem("Seleccione una Opción"));

            var sucursales = new SucursalBLL().Listar();
            ddlSucursal.DataSource = sucursales;
            ddlSucursal.DataTextField = "Nombre";
            ddlSucursal.DataValueField = "IdSucursal";
            ddlSucursal.DataBind();
            ddlSucursal.Items.Insert(0, new ListItem("Seleccione una Opción"));
        }
    }
}