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
    public partial class CatEmpresas : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioEmpresa"] == null)
                    ViewState["EstadoFormularioEmpresa"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioEmpresa"];
            }
            set
            {
                ViewState["EstadoFormularioEmpresa"] = value;
            }
        }

        public Empresas EmpresaActual
        {
            get
            {
                if (Session["EmpresaActuales"] == null)
                    Session["EmpresaActuales"] = new Empresas();
                return (Empresas)Session["EmpresaActuales"];
            }
            set
            {
                Session["EmpresaActuales"] = value;
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
                imbAgregar.Visible = true;
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
                    EmpresaActual = new EmpresasBLL().ObtenerPorIdEmpresas(Guid.Parse(indice.Value.ToString()));
                    if (new EmpresasBLL().Eliminar(EmpresaActual))
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

                    EmpresaActual = new EmpresasBLL().ObtenerPorIdEmpresas(Guid.Parse(indice.Value.ToString()));
                    

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
                hfIdEmpresa.Value = string.Empty;
                if (!String.IsNullOrEmpty(txtBuscar.Text))
                {
                    var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                    if (cliente.Count > 0)
                    {
                        hfIdEmpresa.Value = cliente.FirstOrDefault().IdCliente.ToString();
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

            if (hfIdEmpresa.Value != string.Empty || hfIdEmpresa.Value != "")
            {
                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;
                var dsCuentas = new spPlasticoListaBLL().ObtenerspListaPlasticoPorIdCliente(Guid.Parse(hfIdEmpresa.Value.ToString()));
                gvDatos.DataSource = dsCuentas;
                gvDatos.DataBind();
            }


        }

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            EmpresaActual = new Empresas();

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
            //CargarCombo();

            //ddlCuenta.Enabled = true;
            //txtNombrePlastico.ReadOnly = false;


        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;

            if (gvDatos.SelectedIndex >= 0)
            {
                var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                if (dataKey != null)
                {
                    EmpresaActual = new EmpresasBLL().ObtenerPorIdEmpresas(Guid.Parse(dataKey.Value.ToString()));

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
                    //CargarCombo();
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
                    EmpresaActual = new EmpresasBLL().ObtenerPorIdEmpresas(Guid.Parse(dataKey.Value.ToString()));
                    if (new EmpresasBLL().Eliminar(EmpresaActual))
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

                if (!string.IsNullOrEmpty(txtCodigo.Text.Trim()))
                {
                    Empresas nuevaEmpresa = LlenarObjeto();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevaEmpresa.IdEmpresa = Guid.NewGuid();
                            nuevaEmpresa.EsActivo = true;
                            nuevaEmpresa.FechaRegistro = DateTime.Now;
                            nuevaEmpresa.Usuario = user.Login;
                            if (new EmpresasBLL().Insertar(nuevaEmpresa))
                            {
                               
                                    ModoInicial();
                                    MostrarMensaje("El registro se ha agregado con exito!", TipoMensaje.Success); 
                            }
                            else
                                MostrarMensaje("No se pudo actualizar el consecutivo", TipoMensaje.Danger);
                            break;

                        case Generales.EstadoFormulario.Editar:
                            nuevaEmpresa.IdEmpresa = Guid.NewGuid();
                            nuevaEmpresa.EsActivo = true;
                            nuevaEmpresa.FechaRegistro = DateTime.Now;
                            nuevaEmpresa.Usuario = user.Login;
                            if (new EmpresasBLL().Actualizar(nuevaEmpresa))
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

       

        private Empresas LlenarObjeto()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            Empresas nuevaCuenta = new Empresas()
            {                   
                Codigo = txtCodigo.Text,
                Descripcion = txtNombreEmpresa.Text,
                EsActivo = chkActivo.Checked,
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
            EmpresaActual = null;
            
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            txtNombreEmpresa.Text = string.Empty;
            hfIdEmpresa.Value = string.Empty;
            txtCodigo.Text = string.Empty;
            
        }

        private void LlenarFormulario()
        {
            hfIdEmpresa.Value = EmpresaActual.IdEmpresa.ToString();

            txtCodigo.Text = EmpresaActual.Codigo;
            txtNombreEmpresa.Text = EmpresaActual.Descripcion;
            chkActivo.Checked = EmpresaActual.EsActivo == true ? true : false;
        }

       

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            
            hfIdEmpresa.Value = string.Empty;
          
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