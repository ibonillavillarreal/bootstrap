using Acciona.App_Code;
using AccionaSR.Negocio;
using Microsoft.Reporting.WebForms;
using SisSegLT.Datos;
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
    public partial class ReportePICIngresados : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text != string.Empty && txtFechaFin.Text != string.Empty)
            {
                var fechainicio = DateTime.ParseExact(txtFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var fechafin = DateTime.ParseExact(txtFechaFin.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string sucursal = ddlSucursal.SelectedIndex != 0 ? ddlSucursal.SelectedItem.Text : string.Empty;
                string metodologia = ddlMetodogolia.SelectedIndex != 0 ? ddlMetodogolia.SelectedItem.Text : string.Empty;
                int estado = ddlEstado.SelectedIndex != 0 ? int.Parse(ddlEstado.SelectedValue) : -1;
                string promotor = ddlPromotor.SelectedIndex != 0 ? ddlPromotor.SelectedItem.Text : string.Empty;
                string textoEstado = ddlEstado.SelectedIndex != 0 ? ddlEstado.SelectedItem.Text : string.Empty;

                List<vwPerfilIngresado> resultado = new List<vwPerfilIngresado>();
                if (ddlTipoReporte.SelectedValue == "Resumido")
                {
                    ReportViewer1.LocalReport.ReportPath = "Reportes\\rldc\\PICIngresados.rdlc";
                    resultado = new ClienteBLL().ObtenervwPicIngresados(fechainicio, fechafin, estado);
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = "Reportes\\rldc\\PICIngresadosDetallado.rdlc";
                    resultado = new ClienteBLL().ObtenervwPicIngresadosDetallado(fechainicio, fechafin, estado);
                }

                if (!string.IsNullOrEmpty(sucursal))
                {
                    resultado = resultado.Where(x => x.Sucursal == sucursal).OrderBy(x => x.Sucursal).ToList();
                }

                if (!string.IsNullOrEmpty(promotor))
                {
                    resultado = resultado.Where(x => x.Promotor == promotor).OrderBy(x => x.Promotor).ToList();
                }

                DataTable dt = resultado.ToDataTable();
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportParameter[] parameters = new ReportParameter[7];
                parameters[0] = new ReportParameter("FechaInicio", fechainicio.ToString("dd/MM/yyyy"));
                parameters[1] = new ReportParameter("Sucursal", sucursal);
                parameters[2] = new ReportParameter("Metodologia", metodologia);
                parameters[3] = new ReportParameter("Estado", textoEstado.ToString());
                parameters[4] = new ReportParameter("FechaFin", fechafin.ToString("dd/MM/yyyy"));
                parameters[5] = new ReportParameter("Promotor", promotor);
                parameters[6] = new ReportParameter("Tipo", ddlTipoReporte.SelectedValue);

                pnlReporte.Visible = true;
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.SetParameters(parameters.ToList());
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