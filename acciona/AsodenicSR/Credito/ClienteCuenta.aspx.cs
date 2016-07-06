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
    public partial class ClienteCuenta : Pagina
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

        public tClienteCuenta Cuenta
        {
            get
            {
                if (Session["Cuentas"] == null)
                    Session["Cuentas"] = new tClienteCuenta();
                return (tClienteCuenta)Session["Cuentas"];
            }
            set
            {
                Session["Cuentas"] = value;
            }
        }

        //public tClienteCuenta Cuenta
        //{
        //    get
        //    {
        //        if (ViewState["Cuentas"] == null)
        //            ViewState["Cuentas"] = new tClienteCuenta();
        //        return JsonConvert.DeserializeObject<tClienteCuenta>(ViewState["Cuentas"].ToString());
        //    }
        //    set
        //    {
        //        string json = JsonConvert.SerializeObject(value, Formatting.Indented,
        //                           new JsonSerializerSettings
        //                           {
        //                               PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //                           });
        //        ViewState["Cuentas"] = json;
        //    }
        //}

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

        #endregion Otros eventos

        #region Botones de acción

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            ModoAgregar();
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            ModoEditar();
        }

        protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        {
            ModoEliminar();
        }

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        protected void btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                Cuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(indice.Value.ToString()));
                if (new ClienteCuentaBLL().Eliminar(Cuenta))
                {
                    ModoInicial();
                }
            }
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                EstadoFormulario = Generales.EstadoFormulario.Editar;

                Cuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(indice.Value.ToString()));

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

        protected void btnBuscar_Click(object sender, EventArgs e)
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

        //protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fmt = "000000";
        //        var tipoCuenta = new TipoCuentaBLL().ObtenerPorNombre(ddlTipoCuenta.SelectedItem.Text).FirstOrDefault();
        //        int consecutivo = Convert.ToInt16(tipoCuenta.Numero) + 1;
        //        string numero = consecutivo.ToString(fmt);
        //        txtNoCuenta.Text = tipoCuenta.Prefijo + "-" + numero;
        //    }
        //    catch (Exception ex)
        //    {
        //        MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
        //    }
        //}
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
                var dsCuentas = new ClienteCuentaBLL().ObtenerCuentaPorIdCliente(Guid.Parse(hfIdCliente.Value.ToString()));
                gvDatos.DataSource = dsCuentas;
                gvDatos.DataBind();
            }

            
        }

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            Cuenta = new tClienteCuenta();

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

            LimpiarControles();
            CargarCombo();   
           
        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    Cuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(dataKey.Value.ToString()));

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
                    Cuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(dataKey.Value.ToString()));
                    if (new ClienteCuentaBLL().Eliminar(Cuenta))
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

                if (!string.IsNullOrEmpty(txtFechaAprobacion.Text.Trim()))
                {
                    tClienteCuenta nuevaCuenta = LlenarObjeto();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevaCuenta.IdCuenta = Guid.NewGuid();
                            nuevaCuenta.EsActivo = true;
                            nuevaCuenta.FechaRegistro = DateTime.Now;
                            nuevaCuenta.Usuario = user.Login;

                            string fmt = "000000";
                            var tipoCuenta = new TipoCuentaBLL().ObtenerPorNombre(ddlTipoCuenta.SelectedItem.Text).FirstOrDefault();
                            int consecutivo = Convert.ToInt16(tipoCuenta.Numero) + 1;
                            string numero = consecutivo.ToString(fmt);
                            txtNoCuenta.Text = tipoCuenta.Prefijo + "-" + numero;

                            nuevaCuenta.NoCuenta = txtNoCuenta.Text;

                            if (new ClienteCuentaBLL().Insertar(nuevaCuenta))
                            {
                                if (actualizarConsecutivo(ddlTipoCuenta.SelectedItem.Text, nuevaCuenta))
                                {
                                    ModoInicial();
                                    MostrarMensaje("El registro se ha agregado con exito!", TipoMensaje.Success);
                                } 
                                else
                                    MostrarMensaje("No se pudo actualizar el consecutivo", TipoMensaje.Danger);
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevaCuenta.IdCuenta = Cuenta.IdCuenta;
                            nuevaCuenta.IdCliente = Cuenta.IdCliente;
                            nuevaCuenta.FechaRegistro = Cuenta.FechaRegistro;
                            nuevaCuenta.EsActivo = Cuenta.EsActivo;
                            nuevaCuenta.Usuario = user.Login;
                            if (new ClienteCuentaBLL().Actualizar(nuevaCuenta))
                            {
                                MostrarMensaje("El registro se ha actualizado con exito!", TipoMensaje.Success);
                                //if (actualizarConsecutivo(ddlTipoCuenta.SelectedItem.Text, nuevaCuenta))
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

        private bool actualizarConsecutivo(string tCuenta, tClienteCuenta clienteCuenta)
        {
            var tipoCuenta = new TipoCuentaBLL().ObtenerPorNombre(tCuenta).FirstOrDefault();
            tTipoCuenta actTipoCuenta = new tTipoCuenta();
            actTipoCuenta.IdTipoCuenta = tipoCuenta.IdTipoCuenta;
            actTipoCuenta.Descripcion = tipoCuenta.Descripcion;
            actTipoCuenta.Prefijo = tipoCuenta.Prefijo;
            actTipoCuenta.Numero = clienteCuenta.NoCuenta.Substring(clienteCuenta.NoCuenta.Length - 6);
            actTipoCuenta.ConsecutivoPlastico = tipoCuenta.ConsecutivoPlastico;
            actTipoCuenta.PrefijoPlastico = tipoCuenta.PrefijoPlastico;
            actTipoCuenta.FechaRegistro = tipoCuenta.FechaRegistro;
            actTipoCuenta.EsActivo = tipoCuenta.EsActivo;
            actTipoCuenta.DireccionIP = tipoCuenta.DireccionIP;
            actTipoCuenta.NombrePC = tipoCuenta.NombrePC;
            actTipoCuenta.Usuario = tipoCuenta.Usuario;
            if (new TipoCuentaBLL().Actualizar(actTipoCuenta))
                return true;
            else
                return false;

        }

        private tClienteCuenta LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");   
                    
            tClienteCuenta nuevaCuenta = new tClienteCuenta()
            {
                IdCliente = Guid.Parse(hfIdCliente.Value),
                IdAprobado = Guid.Parse(ddlAprobado.SelectedValue),
                IdTipoCuenta = Guid.Parse(ddlTipoCuenta.SelectedValue),
                NoCuenta = txtNoCuenta.Text,
                Limite = txtLimite.Text == string.Empty ? 0 : double.Parse(txtLimite.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                FechaAprobacion = DateTime.ParseExact(txtFechaAprobacion.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                IdEstadoCuenta = Guid.Parse(ddlEstadoCuenta.SelectedValue),
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
            ddlTipoCuenta.SelectedIndex = -1;
            ddlAprobado.ClearSelection();
            ddlTipoCuenta.ClearSelection();
            txtNoCuenta.Text = string.Empty;
            txtFechaAprobacion.Text = string.Empty;   
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            txtLimite.Text = string.Empty;
            ddlEstadoCuenta.ClearSelection();
        }

        private void LlenarFormulario()
        {
            hfIdCliente.Value = Cuenta.IdCliente.ToString();
            hfIdCuenta.Value = Cuenta.IdCuenta.ToString();
            ddlAprobado.SelectedValue = Cuenta.IdAprobado != null ? Cuenta.IdAprobado.ToString() : ddlAprobado.SelectedValue;
            ddlTipoCuenta.SelectedValue = Cuenta.IdTipoCuenta != null ? Cuenta.IdTipoCuenta.ToString() : ddlTipoCuenta.SelectedValue;
            txtNoCuenta.Text = Cuenta.NoCuenta;
            txtFechaAprobacion.Text = Cuenta.FechaAprobacion != null ? ((DateTime)Cuenta.FechaAprobacion).ToString("dd/MM/yyyy") : string.Empty; 
            txtLimite.Text = string.Format("{0:0,0.00}", Convert.ToDouble(Cuenta.Limite));
            ddlEstadoCuenta.Text = Cuenta.IdEstadoCuenta != null ? Cuenta.IdEstadoCuenta.ToString() : ddlEstadoCuenta.SelectedValue;
            
        }

        private void CargarCombo()
        {
            //tipo de cuenta
            ddlTipoCuenta.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoCuenta.DataSource = new TipoCuentaBLL().ListarActivos();
            ddlTipoCuenta.DataTextField = "Descripcion";
            ddlTipoCuenta.DataValueField = "IdTipoCuenta";
            ddlTipoCuenta.DataBind();

            //Estado de cuenta
            ddlEstadoCuenta.SelectedIndex = -1; //Limpia cualquier selección
            ddlEstadoCuenta.DataSource = new EstadoCuentasBLL().ListarActivos();
            ddlEstadoCuenta.DataTextField = "Descripcion";
            ddlEstadoCuenta.DataValueField = "IdEstadoCuenta";
            ddlEstadoCuenta.DataBind();  

            //Combo usuarios
            //ddlAprobado.SelectedIndex = -1; //Limpia cualquier selección
            //ddlAprobado.DataSource = new UsuarioDAO().ObtenerPorNombreRol("Autorizante");
            //ddlAprobado.DataTextField = "Nombre";
            //ddlAprobado.DataValueField = "IdUsuario";
            //ddlAprobado.DataBind();

            //Combo usuarios
            ddlAprobado.SelectedIndex = -1; //Limpia cualquier selección
            ddlAprobado.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Autorizante");
            ddlAprobado.DataTextField = "Nombre";
            ddlAprobado.DataValueField = "IdUsuario";
            ddlAprobado.DataBind();
        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
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