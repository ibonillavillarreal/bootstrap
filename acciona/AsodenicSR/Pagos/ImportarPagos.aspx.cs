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
using System.Data.OleDb;
using System.IO;
using System.Data.Odbc;


namespace Acciona.Pagos
{
    public partial class ImportarPagos : Pagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            //Verificar si el FileUpload con tiene un Archivo
            if (FileUpload1.HasFile)
            {

                //Colocar el nombre del Archivo en una Variable String
                string filename = FileUpload1.FileName;

                //Enviar el Archivo a un Directorio de forma Temporal
                FileUpload1.SaveAs(Server.MapPath("/Uploads/" + filename));

                txtSerie.Text = FileUpload1.PostedFile.FileName;
                Response.Write("El archivo se ha cargado exitosamente");
                //Importar el Archivo Excel a un Gridview con el Metodo ExportToGrid
                ExportToGrid(Server.MapPath("/Uploads/" + filename), Path.GetExtension(Server.MapPath("/Uploads/" + filename)));
            }
        }

        void ExportToGrid(String path, String Extension)
        {


            OleDbConnection MiConexion = null;
            DataSet DtSet = null;
            OleDbDataAdapter MiComando = null;

            if (Extension == ".xls")
            {
                //Conexion para Formato .xls 2003
                MiConexion = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=Excel 8.0;IMEX=1;");
            }

            else if (Extension == ".xlsx")
            {
                //Conexion para Formato .xlsx 2007 o 2010
                MiConexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;");
            }


            //Seleccionar el archivo Excel
            MiConexion.Open();
            DataTable Datable = new DataTable();
            DataTable dt1 = new DataTable();

            //Seleccionar la Hoja que Esta Activa
            Datable = MiConexion.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            String Nombre_Hoja = Datable.Rows[0][2].ToString();


            MiComando = new System.Data.OleDb.OleDbDataAdapter("select * from [" + Nombre_Hoja + "]", MiConexion);
            DtSet = new System.Data.DataSet();
            //Bindear todo el Contenido del Excel a un Dataset
            MiComando.Fill(DtSet, "[" + Nombre_Hoja + "]");
            dt1 = DtSet.Tables[0];
            MiConexion.Close();
            //Verificar si el Datatable Contiene Valores
            if (dt1.Rows.Count > 0)
            {
                //GridView GridView2 = new GridView();
                gvPagos.DataSource = dt1;
                gvPagos.DataBind();
                Literal1.Text = "Cantidad de Cuentas a Actualizar: <b><font color=red>" + gvPagos.Rows.Count.ToString() + "</font></b>";
                //Panel_Modificaciones.Controls.Add(GridView2);
            }
            //Eliminar el Archivo Excel del Directorio Temporal
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            //Vaciar El Dataset y los Datatable
            dt1 = null;
            DtSet = null;
            Datable = null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool exito;

            try
            {

                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                string DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                string NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);

                foreach (GridViewRow row in gvPagos.Rows)
                {
                    string idCuenta = row.Cells[2].Text;
                    string col1 = row.Cells[1].Text;  //cliente                           
                    string col3 = row.Cells[3].Text;  //nocuenta               
                    string col4 = row.Cells[6].Text;  //serie
                    double colum5 = Convert.ToDouble(row.Cells[4].Text);  // monto recibido
                    string colum6 = row.Cells[5].Text; //no recibo
                    string fecha = row.Cells[7].Text;


                    exito = new CuotaBLL().AbonarCuota(col3, colum5, colum6, idCuenta, col4, col1, fecha, user.Login, DireccionIP, NombrePC);
                    //actualizar el procesado por cada linea de rutacobro
                    if (!exito)
                    {
                        Response.Write("Error al guardar el recibo no" + colum6);
                    }



                }
                Response.Write("Los pagos se han guardado satisfactoriamente!");
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }


    }
}