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
    public partial class ReportePIC : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["identificacion"] != null)
                {
                    string noIdentificacion = Request.QueryString["identificacion"].ToString();
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

        private void GenerarReporte(string numeroIdentificaion)
        {
            var resultado = new ClienteBLL().ObtenerDatosGeneralesPorIdentificacion(numeroIdentificaion);
            if (resultado.Count() > 0)
            {
                //var datosNegocio = new DatosNegocioBLL().ObtenervwDatosNegocioPorIdCliente(resultado.First().IdCliente);

                DataTable dt = resultado.ToDataTable();

                var referencias = new ReferenciasBLL().ObtenervwReferenciasPorIdCliente(resultado.First().IdCliente);
                DataTable dtreferencias = referencias.ToDataTable();

                var exclusivoInstitucion = new AprobacionInstitucionBLL().ObtenervwInstitucionPorIdCliente(resultado.First().IdCliente);
                DataTable dtExclusivo = exclusivoInstitucion.ToDataTable();

                var resumenTransacciones = new ResumenTransaccionBLL().ObtenervwResumenTransaccionPorIdCliente(resultado.First().IdCliente);
                DataTable dtResumen = resumenTransacciones.ToDataTable();

                var contacto = new ContactoBLL().ObtenerContactosPorIdCliente(resultado.First().IdCliente);
                DataTable dtContacto = contacto.ToDataTable();

                var domicilio = new DomicilioBLL().ObtenerDomicilioPorIdCliente(resultado.First().IdCliente);
                DataTable dtDomicilio = domicilio.ToDataTable();

                var datosNegocio = new DatosNegocioBLL().ObtenervwDatosNegocioPorIdCliente(resultado.First().IdCliente);
                DataTable dtNegocio = datosNegocio.ToDataTable();

                var idDatosNegocio = datosNegocio.Count > 0 ? datosNegocio.First().IdDatosNegocio : new Guid();

                var documentosNegocio = new DocumentosNegocioBLL().ObtenerDocumentosNegocioPorIdDocumentosNegocio(idDatosNegocio);
                DataTable dtDocumentos = documentosNegocio.ToDataTable();

                var negocioProveedores = new ProveedoresBLL().ObtenerNegocioProveedoresPorIdDatosNegocio(idDatosNegocio);
                DataTable dtProveedores = negocioProveedores.ToDataTable();

                var referenciasCrediticias = new ReferenciaCrediticiaBLL().ObtenerReferenciaCrediticiasPorIdCliente(resultado.First().IdCliente);
                DataTable dtReferenciasCrediticias = referenciasCrediticias.ToDataTable();

                var referenciasPersonales = new ReferenciasBLL().ObtenerReferenciasPorIdCliente(resultado.First().IdCliente);
                DataTable dtReferenciasPersonales = referenciasPersonales.ToDataTable();

                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                ReportDataSource datasourceNegocio = new ReportDataSource("DataSet3", dtNegocio);
                ReportDataSource datasourceReferencias = new ReportDataSource("DataSet2", dtreferencias);
                ReportDataSource datasourceExclusivo = new ReportDataSource("DataSet4", dtExclusivo);
                ReportDataSource datasourceResumen = new ReportDataSource("DataSet5", dtResumen);
                ReportDataSource datasourceContacto = new ReportDataSource("DataSet6", dtContacto);
                ReportDataSource datasourceDomicilio = new ReportDataSource("DataSet7", dtDomicilio);
                ReportDataSource datasourceDatosNegocio = new ReportDataSource("DataSet9", dtNegocio);
                ReportDataSource datasourceDocumentos = new ReportDataSource("DataSet8", dtDocumentos);
                ReportDataSource datasourceProveedores = new ReportDataSource("DataSet10", dtProveedores);
                ReportDataSource datasourceReferenciasCrediticias = new ReportDataSource("DataSet11", dtReferenciasCrediticias);
                ReportDataSource datasourceReferenciasPersonales = new ReportDataSource("DataSet12", dtReferenciasPersonales);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasourceNegocio);
                ReportViewer1.LocalReport.DataSources.Add(datasourceReferencias);
                ReportViewer1.LocalReport.DataSources.Add(datasourceExclusivo);
                ReportViewer1.LocalReport.DataSources.Add(datasourceResumen);
                ReportViewer1.LocalReport.DataSources.Add(datasourceContacto);
                ReportViewer1.LocalReport.DataSources.Add(datasourceDomicilio);
                ReportViewer1.LocalReport.DataSources.Add(datasourceDatosNegocio);
                ReportViewer1.LocalReport.DataSources.Add(datasourceDocumentos);
                ReportViewer1.LocalReport.DataSources.Add(datasourceProveedores);
                ReportViewer1.LocalReport.DataSources.Add(datasourceReferenciasCrediticias);
                ReportViewer1.LocalReport.DataSources.Add(datasourceReferenciasPersonales);

                ReportViewer1.BorderStyle = BorderStyle.None;

                pnlReporte.Visible = true;
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}