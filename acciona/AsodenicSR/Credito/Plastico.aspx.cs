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

namespace Acciona.Credito
{
    public partial class Plastico : Pagina
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

        public tPlastico PlasticoActual
        {
            get
            {
                if (Session["PlasticoActuales"] == null)
                    Session["PlasticoActuales"] = new tPlastico();
                return (tPlastico)Session["PlasticoActuales"];
            }
            set
            {
                Session["PlasticoActuales"] = value;
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
                ModoInicial();
                imbAgregar.Visible = false;
            }
        }

        protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                  
                var tipoCuenta = new ClienteCuentaBLL().ObtenerPorNoCuenta(ddlCuenta.SelectedItem.Text).FirstOrDefault();
                txtLimite.Text = tipoCuenta.Limite.ToString();
                txtTipoCuenta.Text = tipoCuenta.tTipoCuenta.Descripcion;

                string prefijo = ddlCuenta.SelectedItem.Text.Substring(0,ddlCuenta.SelectedItem.Text.Length - 7);
                string fmt = "00000000";
                var tipoCuenta2 = new TipoCuentaBLL().ObtenerPorPrefijoCuenta(prefijo).FirstOrDefault();
                int consecutivo = Convert.ToInt16(tipoCuenta2.ConsecutivoPlastico) + 1;
                string numero = consecutivo.ToString(fmt);
                txtNoPlastico.Text = tipoCuenta2.PrefijoPlastico + numero;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
        }

        #endregion Otros eventos

        #region Botones de acción

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModoAgregar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModoEditar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
           
        }

        protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModoEliminar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ModoInicial();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
           
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
           
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        protected void btn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btna = sender as ImageButton;
                GridViewRow row = (GridViewRow)btna.NamingContainer;
                var indice = gvDatos.DataKeys[row.RowIndex];


                if (indice != null)
                {
                    PlasticoActual = new PlasticoBLL().ObtenerPorIdPlastico(Guid.Parse(indice.Value.ToString()));
                    if (new PlasticoBLL().Eliminar(PlasticoActual))
                    {
                        ModoInicial();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btna = sender as ImageButton;
                GridViewRow row = (GridViewRow)btna.NamingContainer;
                var indice = gvDatos.DataKeys[row.RowIndex];


                if (indice != null)
                {
                    EstadoFormulario = Generales.EstadoFormulario.Editar;

                    PlasticoActual = new PlasticoBLL().ObtenerPorIdPlastico(Guid.Parse(indice.Value.ToString()));

                    //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                    pnlAgregar.Visible = true;

                    gvDatos.Visible = false;

                    imbActualizar.Enabled =
                    imbActualizar.Visible =
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible =
                    imbAgregar.Enabled =
                    imbAgregar.Visible = false;

                    imbGuardar.Enabled =
                    imbGuardar.Visible =
                    imbCancelar.Enabled =
                    imbCancelar.Visible = true;

                    LimpiarControles();
                    CargarCombo();
                    LlenarFormulario();

                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                hfIdCliente.Value = string.Empty;
                if (!String.IsNullOrEmpty(txtBuscar.Text))
                {
                    var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                    if (cliente.Count > 0)
                    {
                        hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                        //txtCliente.Text = txtBuscar.Text;
                        LitCliente.Text = "Cliente" + ": " + cliente.FirstOrDefault().NombreCompleto.ToString();
                        ModoInicial();
                    }
                    else
                    {
                        MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Info);
            }
            
        }


        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            pnlAgregar.Visible = false;

            imbActualizar.Enabled =
            imbActualizar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = true;

            imbEditar.Enabled =
            imbEditar.Visible =
            imbEliminar.Enabled =
            imbEliminar.Visible =
            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = false;

            if (hfIdCliente.Value != string.Empty || hfIdCliente.Value != "")
            {
                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;
                var dsCuentas = new spPlasticoListaBLL().ObtenerspListaPlasticoPorIdCliente(Guid.Parse(hfIdCliente.Value.ToString()));
                gvDatos.DataSource = dsCuentas;
                gvDatos.DataBind();
            }


        }

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            PlasticoActual = new tPlastico();

            //litAyuda.Text = "Escriba el nombre para la sucursal y guarde los cambios";

            pnlAgregar.Visible = true;

            gvDatos.Visible = false;

            imbActualizar.Enabled =
            imbActualizar.Visible =
            imbEditar.Enabled =
            imbEditar.Visible =
            imbEliminar.Enabled =
            imbEliminar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = false;

            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = true;

            //LimpiarControles();
            CargarCombo();

            ddlCuenta.Enabled = true;
            txtNombrePlastico.ReadOnly = false;


        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    PlasticoActual = new PlasticoBLL().ObtenerPorIdPlastico(Guid.Parse(dataKey.Value.ToString()));

                    //litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

                    pnlAgregar.Visible = true;

                    gvDatos.Visible = false;

                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbAgregar.Enabled =
                    imbAgregar.Visible = false;

                    imbGuardar.Enabled =
                    imbGuardar.Visible =
                    imbCancelar.Enabled =
                    imbCancelar.Visible = true;

                    LimpiarControles();
                    CargarCombo();
                    LlenarFormulario();
                }
            }
        }

        private void ModoEliminar()
        {
            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    PlasticoActual = new PlasticoBLL().ObtenerPorIdPlastico(Guid.Parse(dataKey.Value.ToString()));
                    if (new PlasticoBLL().Eliminar(PlasticoActual))
                    {
                        ModoInicial();
                    }
                }
            }
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                if (!string.IsNullOrEmpty(txtNoPlastico.Text.Trim()))
                {
                    tPlastico nuevoPlastico = LlenarObjeto();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoPlastico.IdPlastico = Guid.NewGuid();
                            nuevoPlastico.EsActivo = true;
                            nuevoPlastico.FechaRegistro = DateTime.Now;
                            nuevoPlastico.Usuario = user.Login;
                            if (new PlasticoBLL().Insertar(nuevoPlastico))
                            {
                                if (actualizarConsecutivo(ddlCuenta.SelectedItem.Text, nuevoPlastico))
                                {
                                    ModoInicial();
                                    MostrarMensaje("El registro se ha agregado con exito!", TipoMensaje.Success);
                                }
                                else
                                    MostrarMensaje("No se pudo actualizar el consecutivo", TipoMensaje.Danger);
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevoPlastico.IdPlastico = PlasticoActual.IdPlastico;
                            nuevoPlastico.IdCuenta = PlasticoActual.IdCuenta;
                            nuevoPlastico.FechaRegistro = PlasticoActual.FechaRegistro;
                            nuevoPlastico.EsActivo = PlasticoActual.EsActivo;
                            nuevoPlastico.Usuario = user.Login;
                            if (new PlasticoBLL().Actualizar(nuevoPlastico))
                            {
                                MostrarMensaje("El registro se ha actualizado con exito!", TipoMensaje.Success);
                                //if (actualizarConsecutivo(ddlCuenta.SelectedItem.Text, nuevoPlastico))
                                //{
                                //    ModoInicial();
                                //    MostrarMensaje("El registro se ha actualizado con exito!", TipoMensaje.Success);
                                //}
                                //else
                                //    MostrarMensaje("No se pudo actualizar el consecutivo", TipoMensaje.Danger);
                                
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private bool actualizarConsecutivo(string tCuenta, tPlastico plasticoNuevo)
        {
            string prefijo = ddlCuenta.SelectedItem.Text.Substring(0, ddlCuenta.SelectedItem.Text.Length - 7);
            var tipoCuenta = new TipoCuentaBLL().ObtenerPorPrefijoCuenta(prefijo).FirstOrDefault();
            tTipoCuenta actTipoCuenta = new tTipoCuenta();
            actTipoCuenta.IdTipoCuenta = tipoCuenta.IdTipoCuenta;
            actTipoCuenta.Descripcion = tipoCuenta.Descripcion;
            actTipoCuenta.Prefijo = tipoCuenta.Prefijo;
            actTipoCuenta.Numero = tipoCuenta.Numero;
            actTipoCuenta.PrefijoPlastico = tipoCuenta.PrefijoPlastico;
            actTipoCuenta.ConsecutivoPlastico = plasticoNuevo.NoTarjeta.Substring(plasticoNuevo.NoTarjeta.Length - 8);
            actTipoCuenta.EsActivo = tipoCuenta.EsActivo;
            actTipoCuenta.DireccionIP = tipoCuenta.DireccionIP;
            actTipoCuenta.NombrePC = tipoCuenta.NombrePC;
            actTipoCuenta.Usuario = tipoCuenta.Usuario;
            if (new TipoCuentaBLL().Actualizar(actTipoCuenta))
                return true;
            else
                return false;

        }

        private tPlastico LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tPlastico nuevaCuenta = new tPlastico()
            {                   
                IdCuenta = Guid.Parse(ddlCuenta.SelectedValue),
                NoTarjeta = txtNoPlastico.Text,
                NombrePlastico = txtNombrePlastico.Text,                
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            return nuevaCuenta;
        }


        private void Seleccionar(object sender)
        {
            int indice;
            LinkButton linkButton = (LinkButton)sender;
            int.TryParse(linkButton.CommandArgument, out indice);
            if (indice >= 0)
            {
                if (gvDatos.SelectedIndex == indice)
                {
                    gvDatos.SelectedIndex = -1;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = false;
                }
                else
                {
                    gvDatos.SelectedIndex = indice;
                    imbEditar.Enabled =
                    imbEditar.Visible =
                    imbEliminar.Enabled =
                    imbEliminar.Visible = true;
                }
            }
        }

        private void LimpiarControles()
        {
            //LitCliente.Text = string.Empty;
            PlasticoActual = null;
            ddlCuenta.SelectedIndex = -1;
            ddlCuenta.ClearSelection();
            txtTipoCuenta.Text = string.Empty;
            txtLimite.Text = string.Empty;
            txtNombrePlastico.Text = string.Empty;
            txtNombrePlastico.Text = string.Empty;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            txtLimite.Text = string.Empty;
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdPlastico.Value = string.Empty;            
        }

        private void LlenarFormulario()
        {
            hfIdCuenta.Value = PlasticoActual.IdCuenta.ToString();
            hfIdPlastico.Value = PlasticoActual.IdPlastico.ToString();
            ddlCuenta.SelectedValue = PlasticoActual.IdCuenta != null ? PlasticoActual.IdCuenta.ToString(): ddlCuenta.SelectedValue;

            var tipoCuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(hfIdCuenta.Value));
            txtLimite.Text = string.Format("{0:0,0.00}", Convert.ToDouble(tipoCuenta.Limite)); 
            txtTipoCuenta.Text = tipoCuenta.tTipoCuenta.Descripcion;

            txtNoPlastico.Text = PlasticoActual.NoTarjeta;
            txtNombrePlastico.Text = PlasticoActual.NombrePlastico;
            chkActivo.Checked = PlasticoActual.EsActivo == true ? true : false;

            txtNombrePlastico.ReadOnly = true;
            ddlCuenta.Enabled = false;

        }

        private void CargarCombo()
        {
            //tipo de cuenta
            //ddlCuenta.SelectedIndex = -1; //Limpia cualquier selección
            ddlCuenta.DataSource = new ClienteCuentaBLL().ObtenerCuentaPorIdCliente(Guid.Parse(hfIdCliente.Value));
            ddlCuenta.DataTextField = "NoCuenta";
            ddlCuenta.DataValueField = "IdCuenta";
            ddlCuenta.DataBind();
        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdPlastico.Value = string.Empty;
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            litmensaje.Text = string.Empty; 
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