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
    public partial class Usuarios : System.Web.UI.Page
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioUsuarios"] == null)
                    ViewState["EstadoFormularioUsuarios"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioUsuarios"];
            }
            set
            {
                ViewState["EstadoFormularioUsuarios"] = value;
            }
        }

        public Generales.EstadoFormulario EstadoFormulario2
        {
            get
            {
                if (ViewState["EstadoFormulario2Roles"] == null)
                    ViewState["EstadoFormulario2Roles"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormulario2Roles"];
            }
            set
            {
                ViewState["EstadoFormulario2Roles"] = value;
            }
        }

        public Usuario UsuarioActual
        {
            get
            {
                if (Session["UsuarioActualUsuarios"] == null)
                    Session["UsuarioActualUsuarios"] = new Usuario();
                return (Usuario)Session["UsuarioActualUsuarios"];
            }
            set
            {
                Session["UsuarioActualUsuarios"] = value;
            }
        }

        public UsuarioRol UsuarioRolActual
        {
            get
            {
                if (Session["UsuarioRolActualMenu"] == null)
                    Session["UsuarioRolActualMenu"] = new UsuarioRol();
                return (UsuarioRol)Session["UsuarioRolActualMenu"];
            }
            set
            {
                Session["UsuarioRolActualMenu"] = value;
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

        

        protected void imbRoles_Click(object sender, ImageClickEventArgs e)
        {
            
            pnlRol.Visible = true;
            ModoInicial2();
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        #endregion

        #region Rol
        protected void imbAgregar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoAgregar2();
        }

        protected void imbEditar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoEditar2();
        }

        protected void imbEliminar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoEliminar2();
        }

        protected void imbCancelar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial2();
        }

        protected void imbGuardar2_Click(object sender, ImageClickEventArgs e)
        {
            Guardar2();
        }

        protected void imbCerrar2_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial2();
            pnlRol.Visible = false;
        }

        protected void lnbSeleccionar2_Click(object sender, EventArgs e)
        {
            Seleccionar2(sender);
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
                UsuarioActual = new SisSegLT.Datos.Usuario();

                litAyuda.Text = "Agregue, edite o deshabilite usuarios";

                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;

                gvDatos.DataSource = new UsuarioBLL().Listar();
                gvDatos.DataBind();

                pnlAgregar.Visible = false;
                pnlRol.Visible = false;

                
                
                imbActualizar.Enabled =
                imbActualizar.Visible =
                imbAgregar.Enabled =
                imbAgregar.Visible = true;

                imbRoles.Enabled =
                imbRoles.Visible =
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

                UsuarioActual = new Usuario();

                litAyuda.Text = "Escriba los datos del usuario y guarde los cambios";

                pnlAgregar.Visible = true;

                gvDatos.Visible = false;
                pnlRol.Visible = false;


                imbRoles.Enabled =
                imbRoles.Visible =
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
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
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
                        UsuarioActual = new UsuarioBLL().ObtenerPorIdUsuario((Guid)dataKey.Value);

                        litAyuda.Text = "Modifique los datos del usuario y guarde los cambios";

                        pnlAgregar.Visible = true;

                        gvDatos.Visible = false;

                        imbRoles.Enabled =
                        imbRoles.Visible =
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
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
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
                        UsuarioActual = new UsuarioBLL().ObtenerPorIdUsuario((Guid)dataKey.Value);
                        if (new UsuarioBLL().Eliminar(UsuarioActual))
                        {
                            ModoInicial();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        #endregion

        #region Rol
        private void ModoInicial2()
        {
            try
            {
                EstadoFormulario2 = Generales.EstadoFormulario.Inicial;
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        UsuarioActual = new UsuarioBLL().ObtenerPorIdUsuario((Guid)dataKey.Value);

                        litAyuda2.Text = "Roles permitidos para '" + UsuarioActual.Nombre + "'";

                        gvHijo.Visible = true;
                        gvHijo.SelectedIndex = -1;
                        gvHijo.DataSource = new UsuarioRolDAO().ObtenerPorIdUsuario(UsuarioActual.IdUsuario);
                        gvHijo.DataBind();

                        pnlAgregar2.Visible = false;

                        imbActualizar2.Enabled =
                        imbActualizar2.Visible =
                        imbAgregar2.Enabled =
                        imbAgregar2.Visible = true;

                        imbEditar2.Enabled =
                        imbEditar2.Visible =
                        imbEliminar2.Enabled =
                        imbEliminar2.Visible =
                        imbGuardar2.Enabled =
                        imbGuardar2.Visible =
                        imbCancelar2.Enabled =
                        imbCancelar2.Visible = false;
                    }
                }
                else
                {
                    ModoInicial();
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }


        }

        private void ModoAgregar2()
        {
            try
            {
                EstadoFormulario2 = Generales.EstadoFormulario.Agregar;
                litAyuda2.Text = "Elija los roles que podrán acceder a este menú y guarde los cambios";
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        UsuarioActual = new UsuarioBLL().ObtenerPorIdUsuario((Guid)dataKey.Value);

                        UsuarioRolActual = new UsuarioRol();

                        pnlAgregar2.Visible = true;

                        gvHijo.Visible = false;

                        imbActualizar2.Enabled =
                        imbActualizar2.Visible =
                        imbEditar2.Enabled =
                        imbEditar2.Visible =
                        imbEliminar2.Enabled =
                        imbEliminar2.Visible =
                        imbAgregar2.Enabled =
                        imbAgregar2.Visible = false;

                        imbGuardar2.Enabled =
                        imbGuardar2.Visible =
                        imbCancelar2.Enabled =
                        imbCancelar2.Visible = true;

                        LimpiarControles2();
                        CargarCombo2();
                    }
                }
                else
                {
                    ModoInicial();
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }



        }

        private void ModoEditar2()
        {
            try
            {
                EstadoFormulario2 = Generales.EstadoFormulario.Editar;
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKeyDatos = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKeyDatos != null)
                    {
                        UsuarioActual = new UsuarioBLL().ObtenerPorIdUsuario((Guid)dataKeyDatos.Value);
                        if (gvHijo.SelectedIndex >= 0)
                        {
                            var dataKey = gvHijo.DataKeys[gvHijo.SelectedIndex];
                            if (dataKey != null)
                            {
                                UsuarioRolActual = new UsuarioRolDAO().ObtenerPorIdUsuarioRol((Guid)dataKey.Value);

                                litAyuda2.Text = "Cambie el rol y guarde los cambios";

                                pnlAgregar2.Visible = true;

                                gvHijo.Visible = false;

                                imbActualizar2.Enabled =
                                imbActualizar2.Visible =
                                imbEditar2.Enabled =
                                imbEditar2.Visible =
                                imbEliminar2.Enabled =
                                imbEliminar2.Visible =
                                imbAgregar2.Enabled =
                                imbAgregar2.Visible = false;

                                imbGuardar2.Enabled =
                                imbGuardar2.Visible =
                                imbCancelar2.Enabled =
                                imbCancelar2.Visible = true;

                                LimpiarControles2();
                                CargarCombo2();
                                LlenarFormulario2();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }


        }

        private void ModoEliminar2()
        {
            try
            {
                if (gvHijo.SelectedIndex >= 0)
                {
                    var dataKeyDatos = gvHijo.DataKeys[gvHijo.SelectedIndex];
                    if (dataKeyDatos != null)
                    {
                        UsuarioRol UsuarioRolActual = new UsuarioRolDAO().ObtenerPorIdUsuarioRol((Guid)dataKeyDatos.Value);
                        if (new UsuarioRolDAO().Eliminar(UsuarioRolActual))
                        {
                            ModoInicial2();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
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
                    Usuario nuevoUsuario = new Usuario()
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Codigo = txtCodigo.Text.Trim(),
                        Login = txtLogin.Text.Trim(),
                        Pass = txtPass.Text,
                        Cargo = txtCargo.Text.Trim(),
                        Sexo = ddlSexo.SelectedValue,
                        //IdRol = Guid.Parse(ddlRol.SelectedValue),
                        IdSucursal = Guid.Parse(ddlSucursal.SelectedValue)
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoUsuario.IdUsuario = Guid.NewGuid();
                            nuevoUsuario.FechaRegistro = DateTime.Now;
                            nuevoUsuario.EsActivo = true;
                            if (new UsuarioBLL().Insertar(nuevoUsuario))
                            {
                                ModoInicial();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoUsuario.IdUsuario = UsuarioActual.IdUsuario;
                            nuevoUsuario.FechaRegistro = UsuarioActual.FechaRegistro;
                            nuevoUsuario.EsActivo = UsuarioActual.EsActivo;
                            if (new UsuarioBLL().Actualizar(nuevoUsuario))
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
                        imbRoles.Enabled =
                        imbRoles.Visible =
                        imbEditar.Enabled =
                        imbEditar.Visible =
                        imbEliminar.Enabled =
                        imbEliminar.Visible = false;
                    }
                    else
                    {
                        gvDatos.SelectedIndex = indice;
                        imbRoles.Enabled =
                        imbRoles.Visible =
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
            txtLogin.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtCargo.Text = string.Empty;
            //ddlRol.SelectedIndex = -1;
            ddlSexo.SelectedIndex = -1;
            ddlSucursal.SelectedIndex = -1;
        }

        private void LlenarFormulario()
        {
            try
            {
                txtNombre.Text = UsuarioActual.Nombre;
                txtCodigo.Text = UsuarioActual.Codigo;
                txtLogin.Text = UsuarioActual.Login;
                txtPass.Text = UsuarioActual.Pass;
                txtCargo.Text = UsuarioActual.Cargo;
                //ddlRol.SelectedValue = UsuarioActual.IdRol.ToString();
                ddlSexo.SelectedValue = UsuarioActual.Sexo;
                ddlSucursal.SelectedValue = UsuarioActual.IdSucursal.ToString();
            }
            catch (Exception ex)
            {
                
                MostrarMensaje("Ocurrio un error al guardar! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void CargarCombo()
        {
            //ddlRol.SelectedIndex = -1; //Limpia cualquier selección
            //ddlRol.DataSource = new RolBLL().Listar();
            //ddlRol.DataBind();

            ddlSucursal.SelectedIndex = -1; //Limpia cualquier selección
            ddlSucursal.DataSource = new SucursalBLL().Listar();
            ddlSucursal.DataBind();
        }
        #endregion

        #region Rol

        private void Guardar2()
        {
            try
            {
                
                if (ddlIdRol.SelectedIndex >= 0)
                {
                    UsuarioRol nuevoUsuarioRol = new UsuarioRol()
                    {
                        IdUsuario = UsuarioActual.IdUsuario,
                        IdRol = Guid.Parse(ddlIdRol.SelectedValue)
                    };
                    switch (EstadoFormulario2)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoUsuarioRol.IdUsuarioRol = Guid.NewGuid();
                            nuevoUsuarioRol.FechaRegistro = DateTime.Now;
                            nuevoUsuarioRol.EsActivo = true;
                            //nuevoUsuarioRol.Usuario = user.Login;
                            if (new UsuarioRolDAO().Insertar(nuevoUsuarioRol))
                            {
                                ModoInicial2();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoUsuarioRol.IdUsuarioRol = UsuarioRolActual.IdUsuarioRol;
                            nuevoUsuarioRol.FechaRegistro = UsuarioRolActual.FechaRegistro;
                            nuevoUsuarioRol.EsActivo = UsuarioRolActual.EsActivo;
                            //nuevoUsuarioRol.Usuario = UsuarioRolActual.Usuario;
                            if (new UsuarioRolDAO().Actualizar(nuevoUsuarioRol))
                            {
                                ModoInicial2();
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

        private void Seleccionar2(object sender)
        {
            try
            {
                int indice;
                LinkButton linkButton = (LinkButton)sender;
                int.TryParse(linkButton.CommandArgument, out indice);
                if (indice >= 0)
                {
                    if (gvHijo.SelectedIndex == indice)
                    {
                        gvHijo.SelectedIndex = -1;
                        imbEditar2.Enabled =
                        imbEditar2.Visible =
                        imbEliminar2.Enabled =
                        imbEliminar2.Visible = false;
                    }
                    else
                    {
                        gvHijo.SelectedIndex = indice;
                        imbEditar2.Enabled =
                        imbEditar2.Visible =
                        imbEliminar2.Enabled =
                        imbEliminar2.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void LimpiarControles2()
        {
            ddlIdRol.SelectedIndex = -1;
        }

        private void LlenarFormulario2()
        {

            ddlIdRol.SelectedValue = UsuarioRolActual.IdRol.ToString();
        }

        private void CargarCombo2()
        {
            try
            {
                ddlIdRol.SelectedIndex = -1; //Limpia cualquier selección
                ddlIdRol.DataSource = new RolBLL().Listar();
                ddlIdRol.DataBind();
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
    }
}