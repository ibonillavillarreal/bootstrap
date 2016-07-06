using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisSegLT.Seguridad;
using Acciona.App_Code;
using AccionaSR.Negocio;
using Newtonsoft.Json;
using SisSegLT.Datos;
using System.Globalization;
using System.Data;

namespace Acciona.Pagos
{
    public partial class PagosMultiples : Pagina
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

        //public tDetallePago DetallePagoActual
        //{
        //    get
        //    {
        //        if (Session["DetallePagoActuales"] == null)
        //            Session["DetallePagoActuales"] = new tDetallePago();
        //        return (tDetallePago)Session["DetallePagoActuales"];
        //    }
        //    set
        //    {
        //        Session["DetallePagoActuales"] = value;
        //    }
        //}

        public tClienteCuenta ClienteCuentaActual
        {
            get
            {
                if (Session["ClienteCuentaActuales"] == null)
                    Session["ClienteCuentaActuales"] = new tClienteCuenta();
                return (tClienteCuenta)Session["ClienteCuentaActuales"];
            }
            set
            {
                Session["ClienteCuentaActuales"] = value;
            }
        }

        //public vw_DatosCredito lstDatosCredito
        //{
        //    get
        //    {
        //        if (Session["vwDatosCredito"] == null)
        //            Session["vwDatosCredito"] = new vw_DatosCredito();
        //        return (vw_DatosCredito)Session["vwDatosCredito"];
        //    }
        //    set
        //    {
        //        Session["vwDatosCredito"] = value;
        //    }
        //}


        #endregion Propiedades

        #region Eventos

        #region Otros eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                  
                //Creamos el dataTable
                DataTable DT = new DataTable();
                DT.Columns.Add("Serie", Type.GetType("System.String"));
                DT.Columns.Add("NoRecibo", Type.GetType("System.String"));
                DT.Columns.Add("MontoRecibido", Type.GetType("System.String"));
                DT.Columns.Add("FechaCouta", Type.GetType("System.String"));

                //Guardamos la información en una variable de sesión
                Session["DT"] = DT;

                //Asignamos el DT al gridview (en este momento el DT esta vacio
                gvDatos.DataSource = DT;
                gvDatos.DataBind();

                LimpiarSesion();
                CargarCombo();
                ModoInicial();

            }
        }

        #endregion Otros eventos

        #region Botones de acción


        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            DataTable objDT = (DataTable)Session["DT"];
            if (objDT.Rows.Count > 0)
            {
                Guardar();
            }
            else
            {
                MostrarMensaje("Por favor ingrese al menos un recibo!", TipoMensaje.Info);
            }


        }
        //protected void imbImprimir_Click(object sender, ImageClickEventArgs e)
        //{
        //    Button b = sender as Button;
        //    if (b != null)
        //    {
        //        LitNoCuenta.Text = "clicked!!";
        //    }
        //}



        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Reportes/Crystal/Reporte.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                if (ddlTipo.SelectedValue == "0")  //placa
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorNoPlaca(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;
                        //txtNoTarjeta.Text = datosCredito.LastOrDefault().NoTarjeta;
                        //txtNocuenta.Text = datosCredito.LastOrDefault().NoCuenta;
                        //txtMonto.Text = string.Format("{0:0,0.00}", datosCredito.LastOrDefault().monto.ToString());
                        //txtNoCuotas.Text = datosCredito.LastOrDefault().NoCuotas.ToString();
                        //txtPlazo.Text = datosCredito.LastOrDefault().PlazoMeses.ToString();
                        //txtColector.Text = datosCredito.LastOrDefault().colector;

                        ////txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso.ToString();
                        //txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso != null ? ((DateTime)datosCredito.LastOrDefault().fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                        //txtFechaVencimiento.Text = datosCredito.LastOrDefault().fechav;

                        //var idCliente = new ClienteBLL().ObtenerPorIdentificacion(datosCredito.LastOrDefault().NoIdentificacion);

                        //var dsCuentas = new CuotaBLL().ObtenerCuotasPendientes(idCliente.FirstOrDefault().IdCliente);
                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else if (ddlTipo.SelectedValue == "1") //identificacion
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorIdentificacion(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;
                        //txtNoTarjeta.Text = datosCredito.LastOrDefault().NoTarjeta;
                        //txtNocuenta.Text = datosCredito.LastOrDefault().NoCuenta;
                        //txtMonto.Text = string.Format("{0:0,0.00}", datosCredito.LastOrDefault().monto.ToString());
                        //txtNoCuotas.Text = datosCredito.LastOrDefault().NoCuotas.ToString();
                        //txtPlazo.Text = datosCredito.LastOrDefault().PlazoMeses.ToString();
                        //txtColector.Text = datosCredito.LastOrDefault().colector;

                        ////txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso;
                        //txtFechaDesembolso.Text = datosCredito.LastOrDefault().fechadesembolso != null ? ((DateTime)datosCredito.LastOrDefault().fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                        //txtFechaVencimiento.Text = datosCredito.LastOrDefault().fechav;

                        //var idCliente = new ClienteBLL().ObtenerPorIdentificacion(datosCredito.LastOrDefault().NoIdentificacion);

                        //var dsCuentas = new CuotaBLL().ObtenerCuotasPendientes(idCliente.FirstOrDefault().IdCliente);
                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
                else //codigo
                {
                    var datosCredito = new MovimientoBLL().ObtenerDatosVehiculoPorCodigo(txtBuscar.Text.Trim());
                    if (datosCredito.Count > 0)
                    {
                        txtNombre.Text = datosCredito.LastOrDefault().NombreCompleto;
                        txtIdentificacion.Text = datosCredito.LastOrDefault().NoIdentificacion;

                        gvDetalle.DataSource = datosCredito;
                        gvDetalle.DataBind();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }


            }
        }

        protected void gvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //
                // Se obtiene indice de la row seleccionada
                //
                int index = Convert.ToInt32(e.CommandArgument);

                //
                // Obtengo el id de la entidad que se esta editando
                // en este caso de la entidad tmovimientos
                //
                Guid id = (Guid)gvDetalle.DataKeys[index].Values[0];
                hfIdMovimiento.Value = id.ToString();
                var listacredito = new MovimientoBLL().ObtenerDatosGeneralesPorIdMovimiento2(id).FirstOrDefault();

                txtNombre.Text = listacredito.NombreCompleto;
                txtIdentificacion.Text = listacredito.NoIdentificacion;
                txtNoTarjeta.Text = listacredito.NoTarjeta;
                txtNocuenta.Text = listacredito.NoCuenta;
                txtMonto.Text = string.Format("{0:0,0.00}", listacredito.monto.ToString());
                txtNoCuotas.Text = listacredito.NoCuotas.ToString();
                txtPlazo.Text = listacredito.PlazoMeses.ToString();
                txtColector.Text = listacredito.colector;

                //txtFechaDesembolso.Text = listacredito.fechadesembolso;
                txtFechaDesembolso.Text = listacredito.fechadesembolso != null ? ((DateTime)listacredito.fechadesembolso).ToString("dd/MM/yyyy") : string.Empty;

                //txtFechaVencimiento.Text = listacredito.fechav;

                var idCliente = new ClienteBLL().ObtenerPorIdentificacion(listacredito.NoIdentificacion);
                hfIdCliente.Value = idCliente.ToString();

                gvHistoria.DataSource = new RecibosBLL().ObtenerReciboPorIdMovimiento(Guid.Parse(hfIdMovimiento.Value));
                gvHistoria.DataBind();

            }

        }

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtNoTarjeta.Text == "")
                {
                    MostrarMensaje("Seleccione un Vehiculo", TipoMensaje.Warning);
                }
                else
                {
                    //Leemos la información
                    string strSerie = txtSerie.Text;
                    string strRecibo = txtNoRecibo.Text;
                    string strMonto = txtMontoRecibido.Text;
                    string strFecha = txtFechaEfectiva.Text;

                    //Leemos el datatable
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["DT"];

                    //Insertamos el registro
                    DT.Rows.Add(strSerie, strRecibo, strMonto, strFecha);

                    //Asignamos del DT al gridview
                    gvDatos.DataSource = DT;
                    gvDatos.DataBind();

                    //Actualizamos el DT de la variable de sessión
                    Session["DT"] = DT;

                    //Limpiamos la pantalla
                    txtSerie.Text = "";
                    txtNoRecibo.Text = "";
                    txtMontoRecibido.Text = "";
                    txtFechaEfectiva.Text = "";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataTable objDT = (DataTable)Session["DT"];
                //Dim rows1 As DataRow() = dt.Select("IdTarjeta = " & gvwPersonal.DataKeys(e.RowIndex).Value.ToString())
                //'Dim row As DataRow = Int32.Parse(gvwPersonal.DataKeys(e.RowIndex).Value.ToString())
                //dt.Rows.Remove(rows1(0))

                if (objDT.Rows.Count > 0)
                {
                    objDT.Rows[e.RowIndex].Delete();

                    gvDatos.DataSource = objDT;
                    gvDatos.DataBind();

                    Session["DT"] = objDT;

                }

                objDT = null;

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar recibo." + ex.Message.ToString(), TipoMensaje.Danger);
            }
        }

        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        public void CargarDatosGenerales()
        {

        }

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;

        }

        #endregion Modos

        #region Otros métodos

        private bool VerificarRecibo(string noRecibo, string serie)
        {
            var existe = new RecibosDAO().ObtenerPorNoReciboySerie(noRecibo, serie);

            if (existe != null)
                return true;
            else
                return false;
        }

        private void Guardar()
        {
            try
            {

                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                string DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                string NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                string fmt = "000000";
                string col1 = txtNombre.Text;  //cliente                           
                string col3 = txtNocuenta.Text;  //nocuenta   

                foreach (GridViewRow row in gvDatos.Rows)   //recorrrer gridview
                {

                    string col4 = row.Cells[0].Text.ToUpper();  //serie
                    double colum5 = Convert.ToDouble(row.Cells[2].Text);  // monto recibido
                    int colum6 = Convert.ToInt16(row.Cells[1].Text); //no recibo     
                    string fecha = row.Cells[3].Text;
                    string recibo = colum6.ToString(fmt);

                    if (VerificarRecibo(recibo, txtSerie.Text.Trim()))
                    {

                        MostrarMensaje("Se ha encontrado un recibo duplicado. Verifique!", TipoMensaje.Danger);
                        return;

                    }
                    else
                    {
                        var idCuenta = new ClienteCuentaBLL().ObtenerPorNoCuenta(col3).FirstOrDefault().IdCuenta;

                        string idCuentastr = idCuenta.ToString();

                        bool exito = new CuotaBLL().AbonarCuotaSoloCredito(col3, colum5, recibo, idCuentastr, hfIdMovimiento.Value, col4, col1, fecha, user.Login, DireccionIP, NombrePC);
                        //actualizar el procesado por cada linea de rutacobro
                        if (!exito)
                        {
                            MostrarMensaje("Error al gurdar recibo!", TipoMensaje.Danger);
                        }

                    }

                    
                }

                MostrarMensaje("El pago se ha guardado satisfactoriamente!", TipoMensaje.Success);

                Session["DT"] = "";
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }



        private void LimpiarControles()
        {
            txtBuscar.Text = string.Empty;
            txtColector.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtNocuenta.Text = string.Empty;
            txtNoTarjeta.Text = string.Empty;
            txtFechaDesembolso.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtNoCuotas.Text = string.Empty;
            //txtFechaVencimiento.Text = string.Empty;
            txtPlazo.Text = string.Empty;

            txtSerie.Text = string.Empty;
            txtNoRecibo.Text = string.Empty;
            txtMontoRecibido.Text = string.Empty;
            txtFechaEfectiva.Text = string.Empty;


            gvDetalle.DataSource = null;
            gvDetalle.DataBind();

            gvDatos.DataSource = null;
            gvDatos.DataBind();

            //Session.Remove("DT");


        }

        private void LlenarFormulario()
        {
            //hfIdCliente.Value = Cuenta.IdCliente.ToString();
            //hfIdCuenta.Value = Cuenta.IdCuenta.ToString();
            //ddlAprobado.SelectedValue = Cuenta.IdAprobado != null ? Cuenta.IdAprobado.ToString() : ddlAprobado.SelectedValue;
            //ddlTipoCuenta.SelectedValue = Cuenta.IdTipoCuenta != null ? Cuenta.IdTipoCuenta.ToString() : ddlTipoCuenta.SelectedValue;
            //txtNoCuenta.Text = Cuenta.NoCuenta;
            //txtFechaAprobacion.Text = Cuenta.FechaAprobacion != null ? ((DateTime)Cuenta.FechaAprobacion).ToString("dd/MM/yyyy") : string.Empty;
            //txtLimite.Text = string.Format("{0:0,0.00}", Convert.ToDouble(Cuenta.Limite));
            //ddlEstadoCuenta.Text = Cuenta.IdEstadoCuenta != null ? Cuenta.IdEstadoCuenta.ToString() : ddlEstadoCuenta.SelectedValue;

        }

        private void CargarCombo()
        {


        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdMovimiento.Value = string.Empty;
            //DetallePagoActual = null;
            LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            string tipoMensaje = "alert-" + tipo.ToString().ToLower();
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