using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using SisSegLT.Datos;
using SisSegLT.Negocio;

namespace SisSegLT.Administracion
{
    public partial class Sucursales : System.Web.UI.Page
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioSucursales"] == null)
                    ViewState["EstadoFormularioSucursales"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioSucursales"];
            }
            set
            {
                ViewState["EstadoFormularioSucursales"] = value;
            }
        }

        public Sucursal SucursalActual
        {
            get
            {
                if (Session["SucursalActualSucursales"] == null)
                    Session["SucursalActualSucursales"] = new Sucursal();
                return (Sucursal)Session["SucursalActualSucursales"];
            }
            set
            {
                Session["SucursalActualSucursales"] = value;
            }
        }

        #endregion

        #region Eventos

        #region Otros eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ModoInicial();
            }
        }

        #endregion

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

        #endregion

        #endregion

        #region Métodos

        #region Modos
        private void ModoInicial()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Inicial;
                litAyuda.Text = "Agregue, edite o deshabilite sucursales";

                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;
                gvDatos.DataSource = new SucursalBLL().Listar();
                gvDatos.DataBind();

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
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void ModoAgregar()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Agregar;

                SucursalActual = new Sucursal();

                litAyuda.Text = "Escriba el nombre para la sucursal y guarde los cambios";

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
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error al agregar! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void ModoEditar()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Editar;

                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        SucursalActual = new SucursalBLL().ObtenerPorIdSucursal((Guid)dataKey.Value);

                        litAyuda.Text = "Modifique el nombre de la sucursal y guarde los cambios";

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
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error al editar! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void ModoEliminar()
        {
            try
            {
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        SucursalActual = new SucursalBLL().ObtenerPorIdSucursal((Guid)dataKey.Value);
                        if (new SucursalBLL().Eliminar(SucursalActual))
                        {
                            ModoInicial();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error al eliminar! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        #endregion

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    Sucursal nuevoSucursal = new Sucursal()
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Codigo = txtCodigo.Text.Trim(),
                        Direccion = txtDireccion.Text.Trim(),
                        IdCiudad = Guid.Parse(ddlCiudad.SelectedValue)
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoSucursal.IdSucursal = Guid.NewGuid();
                            nuevoSucursal.EsActivo = true;
                            if (new SucursalBLL().Insertar(nuevoSucursal))
                            {
                                ModoInicial();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoSucursal.IdSucursal = SucursalActual.IdSucursal;
                            nuevoSucursal.EsActivo = SucursalActual.EsActivo;
                            if (new SucursalBLL().Actualizar(nuevoSucursal))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error al guardar! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void Seleccionar(object sender)
        {
            try
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
            catch (Exception ex)
            {
                
               MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            ddlCiudad.SelectedIndex = -1;
        }

        private void LlenarFormulario()
        {
            try
            {
                txtNombre.Text = SucursalActual.Nombre;
                txtCodigo.Text = SucursalActual.Codigo;
                txtDireccion.Text = SucursalActual.Direccion;
                ddlCiudad.SelectedValue = SucursalActual.IdCiudad.ToString();
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void CargarCombo()
        {
            try
            {
                ddlCiudad.SelectedIndex = -1; //Limpia cualquier selección
                ddlCiudad.DataSource = new CiudadBLL().Listar();
                ddlCiudad.DataBind();
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }
        #endregion

        #endregion
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
    }
}