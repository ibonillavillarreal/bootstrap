using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AsodenicSR.App_Code;
using AsodenicSR.Negocio;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System.Reflection;
using System.Collections;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

 
namespace AsodenicSR.Reportes
{
    public partial class Reportes : Pagina
    {
        public List<SP_DatosCliente_Result> Lista { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarReporte();
        }

        public void CargarReporte()
        {
            //crPerfilCliente rptCliente = new crPerfilCliente();

            ParameterFields paramFields = new ParameterFields();


            List<SP_DatosCliente_Result> dsCliente = new spDatosClienteBLL().ObtenerspDatosClientePorIdCliente((Guid)Session["idCliente"]);
            List<SP_DatosDocumentosCliente_Result> dsDocCliente = new spDatosClienteBLL().ObtenerspDocClientePorIdCliente((Guid)Session["idCliente"]);

            var resultado = dsCliente.ToList();
            //DataTable dt = MetodosExtensiones.ToDataTable<SP_DatosCliente_Result>(resultado);
            DataTable dtResults = new DataTable();
            DataTable dtResultDocCliente = new DataTable();

            dtResults = dsCliente.ToDataTable();
            dtResults.TableName = "Clientes";

            dtResultDocCliente = dsDocCliente.ToDataTable();
            dtResultDocCliente.TableName = "DocClientes";

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Reportes/crPerfilCliente.rpt"));

            ParameterField paramField = new ParameterField();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = "@IdCliente";
            paramDiscreteValue.Value = Session["idCliente"].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramFields.Add(paramField);                            
            //crystalReport.SetParameterValue(0, Session["idCliente"].ToString());

            //CrystalReportViewer1.ParameterFieldInfo = paramFields;
            //crystalReport.SetDataSource(dtResults);
            //CrystalReportViewer1.ReportSource = crystalReport;
            //CrystalReportViewer1.DataBind();
            
            //CrystalReportViewer1.RefreshReport();
        }


        

    }
}