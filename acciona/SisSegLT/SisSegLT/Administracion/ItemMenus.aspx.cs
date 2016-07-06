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
    public partial class ItemMenus : System.Web.UI.Page
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioItemMenues"] == null)
                    ViewState["EstadoFormularioItemMenues"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioItemMenues"];
            }
            set
            {
                ViewState["EstadoFormularioItemMenues"] = value;
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

        public ItemMenu ItemMenuActual
        {
            get
            {
                if (Session["ItemMenuActualItemMenu"] == null)
                    Session["ItemMenuActualItemMenu"] = new ItemMenu();
                return (ItemMenu)Session["ItemMenuActualItemMenu"];
            }
            set
            {
                Session["ItemMenuActualItemMenu"] = value;
            }
        }

        public ItemRol ItemRolActual
        {
            get
            {
                if (Session["ItemRolActualMenu"] == null)
                    Session["ItemRolActualMenu"] = new ItemRol();
                return (ItemRol)Session["ItemRolActualMenu"];
            }
            set
            {
                Session["ItemRolActualMenu"] = value;
            }
        }

        public Guid? IdPadre
        {
            get
            {
                if (ViewState["IdPadre"] == null)
                    ViewState["IdPadre"] = null;
                return (Guid?)ViewState["IdPadre"];
            }
            set
            {
                ViewState["IdPadre"] = value;
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

        #region Menu
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
            IdPadre = null;
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();
        }

        protected void imbItems_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial(ItemMenuActual.IdItemMenu);
        }

        protected void imbRoles_Click(object sender, ImageClickEventArgs e)
        {
            IdPadre = null;
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

        #endregion

        #region Métodos

        #region Modos

        #region Menu
        private void ModoInicial(Guid? idItemMenuPadre = null)
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Inicial;
                litAyuda.Text = "Agregue, edite o deshabilite menúes";

                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;
                if (idItemMenuPadre == null || idItemMenuPadre == Guid.Empty)
                {
                    gvDatos.DataSource = new ItemMenuBLL().Listar();
                    IdPadre = null;
                    rutaMenu.Visible = false;
                }
                else
                {
                    gvDatos.DataSource = new ItemMenuBLL().ObtenerPorIdItemMenuPadre((Guid)idItemMenuPadre);
                    IdPadre = idItemMenuPadre;
                    rutaMenu.Visible = true;
                    litRutaMenu.Text = new ItemMenuBLL().ObtenerRutaMenu((Guid)IdPadre, true);
                }
                gvDatos.DataBind();

                pnlAgregar.Visible =
                pnlRol.Visible = false;

                imbActualizar.Enabled =
                imbActualizar.Visible =
                imbAgregar.Enabled =
                imbAgregar.Visible = true;

                imbItems.Enabled =
                imbItems.Visible =
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
                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Warning);
            }

        }

        private void ModoAgregar()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Agregar;

                ItemMenuActual = new SisSegLT.Datos.ItemMenu();

                litAyuda.Text = "Escriba el nombre para el menú y guarde los cambios";

                pnlAgregar.Visible = true;

                gvDatos.Visible =
                pnlRol.Visible = false;

                imbItems.Enabled =
                imbItems.Visible =
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
                        ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKey.Value);

                        litAyuda.Text = "Modifique el nombre de la itemMenu y guarde los cambios";

                        pnlAgregar.Visible = true;

                        gvDatos.Visible =
                        pnlRol.Visible = false;

                        imbItems.Enabled =
                        imbItems.Visible =
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
                        ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKey.Value);
                        if (new ItemMenuBLL().Eliminar(ItemMenuActual))
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
                        ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKey.Value);

                        litAyuda2.Text = "Roles permitidos para '" + ItemMenuActual.Texto + "'";

                        gvHijo.Visible = true;
                        gvHijo.SelectedIndex = -1;
                        gvHijo.DataSource = new ItemRolBLL().ObtenerPorIdItemMenu(ItemMenuActual.IdItemMenu);
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
                        ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKey.Value);

                        ItemRolActual = new ItemRol();

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
                        ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKeyDatos.Value);
                        if (gvHijo.SelectedIndex >= 0)
                        {
                            var dataKey = gvHijo.DataKeys[gvHijo.SelectedIndex];
                            if (dataKey != null)
                            {
                                ItemRolActual = new ItemRolBLL().ObtenerPorIdItemRol((Guid)dataKey.Value);

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
                        ItemRol itemRolActual = new ItemRolBLL().ObtenerPorIdItemRol((Guid)dataKeyDatos.Value);
                        if (new ItemRolBLL().Eliminar(itemRolActual))
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

        #endregion

        #region Otros métodos

        #region Menu

        private void Guardar()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTexto.Text.Trim()))
                {
                    SisSegLT.Datos.ItemMenu nuevoItemMenu = new SisSegLT.Datos.ItemMenu()
                    {
                        Texto = txtTexto.Text,
                        Ruta = txtRuta.Text,
                        Descripcion = txtDescripcion.Text,
                        Visible = chkVisible.Checked
                    };

                    if (IdPadre != null)
                        nuevoItemMenu.IdItemMenuPadre = IdPadre;

                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoItemMenu.IdItemMenu = Guid.NewGuid();
                            nuevoItemMenu.FechaRegistro = DateTime.Now;
                            nuevoItemMenu.EsActivo = true;
                            if (new ItemMenuBLL().Insertar(nuevoItemMenu))
                            {
                                ModoInicial(IdPadre);
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoItemMenu.IdItemMenu = ItemMenuActual.IdItemMenu;
                            nuevoItemMenu.FechaRegistro = ItemMenuActual.FechaRegistro;
                            nuevoItemMenu.EsActivo = ItemMenuActual.EsActivo;
                            if (new ItemMenuBLL().Actualizar(nuevoItemMenu))
                            {
                                ModoInicial(IdPadre);
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
                        ItemMenuActual = null;
                        imbItems.Enabled =
                        imbItems.Visible =
                        imbRoles.Enabled =
                        imbRoles.Visible =
                        imbEditar.Enabled =
                        imbEditar.Visible =
                        imbEliminar.Enabled =
                        imbEliminar.Visible =
                        rutaMenu.Visible = false;
                        if (IdPadre != null)
                        {
                            rutaMenu.Visible = true;
                            litRutaMenu.Text = new ItemMenuBLL().ObtenerRutaMenu((Guid)IdPadre, true);
                        }
                    }
                    else
                    {
                        gvDatos.SelectedIndex = indice;
                        var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                        if (dataKey != null)
                        {
                            ItemMenuActual = new ItemMenuBLL().ObtenerPorIdItemMenu((Guid)dataKey.Value);
                            imbItems.Enabled =
                            imbItems.Visible =
                            imbRoles.Enabled =
                            imbRoles.Visible =
                            imbEditar.Enabled =
                            imbEditar.Visible =
                            imbEliminar.Enabled =
                            imbEliminar.Visible =
                            rutaMenu.Visible = true;

                            litRutaMenu.Text = new ItemMenuBLL().ObtenerRutaMenu(ItemMenuActual.IdItemMenu, true);
                        }
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
            try
            {
                txtTexto.Text = string.Empty;
                txtRuta.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                chkVisible.Checked = false;
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }

           
        }

        private void LlenarFormulario()
        {
            try
            {
                txtTexto.Text = ItemMenuActual.Texto;
                txtRuta.Text = ItemMenuActual.Ruta;
                txtDescripcion.Text = ItemMenuActual.Descripcion;
                chkVisible.Checked = ItemMenuActual.Visible;
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void CargarCombo()
        {
            //ddlCiudad.SelectedIndex = -1; //Limpia cualquier selección
            //ddlCiudad.DataSource = new CiudadBLL().Listar();
            //ddlCiudad.DataBind();
        }

        #endregion

        #region Rol

        private void Guardar2()
        {
            try
            {
                if (ddlIdRol.SelectedIndex >= 0)
                {
                    ItemRol nuevoItemRol = new ItemRol()
                    {
                        IdItemMenu = ItemMenuActual.IdItemMenu,
                        IdRol = Guid.Parse(ddlIdRol.SelectedValue)
                    };
                    switch (EstadoFormulario2)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoItemRol.IdItemRol = Guid.NewGuid();
                            nuevoItemRol.FechaRegistro = DateTime.Now;
                            nuevoItemRol.EsActivo = true;
                            if (new ItemRolBLL().Insertar(nuevoItemRol))
                            {
                                ModoInicial2();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoItemRol.IdItemRol = ItemRolActual.IdItemRol;
                            nuevoItemRol.FechaRegistro = ItemRolActual.FechaRegistro;
                            nuevoItemRol.EsActivo = ItemRolActual.EsActivo;
                            if (new ItemRolBLL().Actualizar(nuevoItemRol))
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
            
            ddlIdRol.SelectedValue = ItemRolActual.IdRol.ToString();
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