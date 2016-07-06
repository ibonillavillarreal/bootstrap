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


namespace Acciona.Credito
{
    public partial class RecalcularCartera : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioNegocio"] == null)
                    ViewState["EstadoFormularioNegocio"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioNegocio"];
            }
            set
            {
                ViewState["EstadoFormularioNegocio"] = value;
            }
        }

        public tMovimientos MovimientoActual
        {
            get
            {
                if (Session["MovimientoActuales"] == null)
                    Session["MovimientoActuales"] = new tMovimientos();
                return (tMovimientos)Session["MovimientoActuales"];
            }
            set
            {
                Session["MovimientoActuales"] = value;
            }
        }

        public List<tMovimientos> MovimientosCalcular
        {
            get
            {
                if (Session["MovimientoCalcular"] == null)
                    Session["MovimientoCalcular"] = new List<tMovimientos>();
                return (List<tMovimientos>)Session["MovimientoCalcular"];
            }
            set
            {
                Session["MovimientoCalcular"] = value;
            }
        }


        #endregion Propiedades

        #region Eventos

        #region Otros eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LimpiarSesion();
                

            }
        }

        #endregion Otros eventos

        #region Botones de acción


        

        

        




        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            

                var movimientos = new MovimientoBLL().ListarMovimientosXCartera();
                if (movimientos.Count > 0)
                {
                    MovimientosCalcular = movimientos;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert(" + MovimientosCalcular.Count + ");", true);
                    
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }

          
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
                litmensaje.Text = "Cantidad de Cuentas a Actualizar: <b><font color=red>" + gvPagos.Rows.Count.ToString() + "</font></b>";
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

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                string nocuenta;
                bool exito = false;
                foreach (GridViewRow row in gvPagos.Rows)
                {
                    exito = new RecibosBLL().RecalcularMovimientosxCuenta(Guid.Parse(row.Cells[0].Text), row.Cells[1].Text);
                    //litmensaje.Text = item.tPlastico.tClienteCuenta.NoCuenta;
                    //string jscript = "<script>alert(" + item.tPlastico.tClienteCuenta.NoCuenta + ")</script>";
                    //System.Type t = this.GetType();
                    //ClientScript.RegisterStartupScript(t, "k", jscript);

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert(" + item.tPlastico.tClienteCuenta.NoCuenta + ");", true);
                    if (exito == false)
                    {
                        nocuenta = row.Cells[1].Text;
                        break;
                    }  else
                    {
                        nocuenta = row.Cells[1].Text;
                    }
                }
                

                if (exito)
                {
                    MostrarMensaje("Se ha recalculado la cuenta correctamente!", TipoMensaje.Success);
                }
                else
                {
                    MostrarMensaje("Hubo un error al recalcular la cuenta", TipoMensaje.Danger);
                }

            }
            catch (Exception)
            {

                MostrarMensaje("Hubo un error al recalcular la cuenta!", TipoMensaje.Danger);
            }
        }

        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        




        #endregion Modos

        #region Otros métodos

        




       

       

       

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            //hfIdCliente.Value = string.Empty;
            //hfIdCuenta.Value = string.Empty;
            //hfIdMovimiento.Value = string.Empty;
            //MovimientoActual = null;
            //LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            string tipoMensaje = "alert-" + tipo.ToString().ToLower();
            //litmensaje.Text = "";
            litmensaje.Text = string.Format(@"<div class='alert {0} alert-dismissible' role='alert'>
              <button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>"
             + mensaje +
            @"</div>", tipoMensaje);

            ScriptManager.RegisterStartupScript(this, GetType(), ClientID, @"$(document).ready(function() {
                                                                                window.setTimeout(function() {
                                                                                    $('.alert').fadeTo(500, 0).slideUp(500, function() {
                                                                                        $(this).remove();
                                                                                    });
                                                                                }, 5000);
                                                                            });", true);
        }

        internal enum TipoMensaje
        {
            Info,
            Success,
            Warning,
            Danger
        }

        #endregion Otros métodos



        #endregion Métodos
    }
}