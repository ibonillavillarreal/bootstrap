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
    public partial class Paises : System.Web.UI.Page
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioPaises"] == null)
                    ViewState["EstadoFormularioPaises"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioPaises"];
            }
            set
            {
                ViewState["EstadoFormularioPaises"] = value;
            }
        }

        public Generales.EstadoFormulario EstadoFormulario2
        {
            get
            {
                if (ViewState["EstadoFormulario2Paises"] == null)
                    ViewState["EstadoFormulario2Paises"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormulario2Paises"];
            }
            set
            {
                ViewState["EstadoFormulario2Paises"] = value;
            }
        }

        public Pais PaisActual
        {
            get
            {
                if (Session["PaisActualPaises"] == null)
                    Session["PaisActualPaises"] = new Pais();
                return (Pais)Session["PaisActualPaises"];
            }
            set
            {
                Session["PaisActualPaises"] = value;
            }
        }

        public Ciudad CiudadActual
        {
            get
            {
                if (Session["CiudadActualPaises"] == null)
                    Session["CiudadActualPaises"] = new Pais();
                return (Ciudad)Session["CiudadActualPaises"];
            }
            set
            {
                Session["CiudadActualPaises"] = value;
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

        #region País
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

        protected void imbCiudad_Click(object sender, ImageClickEventArgs e)
        {
            pnlCiudad.Visible = true;
            ModoInicial2();
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar(sender);
        }

        #endregion

        #region Ciudad
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
            pnlCiudad.Visible = false;
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

        #region País
        private void ModoInicial()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Inicial;
                litAyuda.Text = "Agregue, edite o deshabilite países";

                gvDatos.Visible = true;
                gvDatos.SelectedIndex = -1;
                gvDatos.DataSource = new PaisBLL().Listar();
                gvDatos.DataBind();

                pnlAgregar.Visible =
                pnlCiudad.Visible = false;

                imbActualizar.Enabled =
                imbActualizar.Visible =
                imbAgregar.Enabled =
                imbAgregar.Visible = true;

                imbCiudad.Enabled =
                imbCiudad.Visible =
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
                MostrarMensaje("Por favor verifique!" + ex.Message.ToString(), TipoMensaje.Warning);
            }


        }

        private void ModoAgregar()
        {
            try
            {
                EstadoFormulario = Generales.EstadoFormulario.Agregar;

                PaisActual = new Pais();

                litAyuda.Text = "Escriba el nombre para el país y guarde los cambios";

                pnlAgregar.Visible = true;

                gvDatos.Visible =
                pnlCiudad.Visible = false;

                imbCiudad.Enabled =
                imbCiudad.Visible =
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
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error al agregar!" + ex.Message.ToString(), TipoMensaje.Warning);
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
                        PaisActual = new PaisBLL().ObtenerPorIdPais((Guid)dataKey.Value);

                        litAyuda.Text = "Modifique el nombre del país y guarde los cambios";

                        pnlAgregar.Visible = true;

                        gvDatos.Visible =
                        pnlCiudad.Visible = false;

                        imbCiudad.Enabled =
                        imbCiudad.Visible =
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

            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error al editar!" + ex.Message.ToString(), TipoMensaje.Warning);
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
                        PaisActual = new PaisBLL().ObtenerPorIdPais((Guid)dataKey.Value);
                        if (new PaisBLL().Eliminar(PaisActual))
                        {
                            ModoInicial();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        #endregion

        #region Ciudad
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
                        PaisActual = new PaisBLL().ObtenerPorIdPais((Guid)dataKey.Value);

                        litAyuda2.Text = "Ciudades de " + PaisActual.Nombre;

                        gvHijo.Visible = true;
                        gvHijo.SelectedIndex = -1;
                        gvHijo.DataSource = new CiudadBLL().ObtenerPorIdPais(PaisActual.IdPais);
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

                MostrarMensaje("Ocurrio un error!" + ex.Message.ToString(), TipoMensaje.Danger);
            }
        }

        private void ModoAgregar2()
        {
            try
            {
                EstadoFormulario2 = Generales.EstadoFormulario.Agregar;
                litAyuda2.Text = "Escriba el nombre para la ciudad y guarde los cambios";
                if (gvDatos.SelectedIndex >= 0)
                {
                    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
                    if (dataKey != null)
                    {
                        PaisActual = new PaisBLL().ObtenerPorIdPais((Guid)dataKey.Value);

                        CiudadActual = new Ciudad();

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
                    }
                }
                else
                {
                    ModoInicial();
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error!" + ex.Message.ToString(), TipoMensaje.Danger);
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
                        PaisActual = new PaisBLL().ObtenerPorIdPais((Guid)dataKeyDatos.Value);
                        if (gvHijo.SelectedIndex >= 0)
                        {
                            var dataKey = gvHijo.DataKeys[gvHijo.SelectedIndex];
                            if (dataKey != null)
                            {
                                CiudadActual = new CiudadBLL().ObtenerPorIdCiudad((Guid)dataKey.Value);

                                litAyuda2.Text = "Modifique el nombre de la ciudad y guarde los cambios";

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
                                LlenarFormulario2();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error!" + ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void ModoEliminar2()
        {
            try
            {
                if (gvHijo.SelectedIndex >= 0)
                {
                    var dataKey = gvHijo.DataKeys[gvHijo.SelectedIndex];
                    if (dataKey != null)
                    {
                        CiudadActual = new CiudadBLL().ObtenerPorIdCiudad((Guid)dataKey.Value);
                        if (new CiudadBLL().Eliminar(CiudadActual))
                        {
                            ModoInicial();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error!" + ex.Message.ToString(), TipoMensaje.Danger);
            }
        }

        #endregion

        #endregion

        #region Otros métodos

        #region Pais

        private void Guardar()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    Pais nuevoPais = new Pais()
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Nacionalidad = txtNacionalidad.Text.Trim()
                    };
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevoPais.IdPais = Guid.NewGuid();
                            nuevoPais.EsActivo = true;
                            if (new PaisBLL().Insertar(nuevoPais))
                            {
                                ModoInicial();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevoPais.IdPais = PaisActual.IdPais;
                            nuevoPais.EsActivo = PaisActual.EsActivo;
                            if (new PaisBLL().Actualizar(nuevoPais))
                            {
                                ModoInicial();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error al guardar!" + ex.Message.ToString(), TipoMensaje.Danger);
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
                        imbCiudad.Enabled =
                        imbCiudad.Visible =
                        imbEditar.Enabled =
                        imbEditar.Visible =
                        imbEliminar.Enabled =
                        imbEliminar.Visible = false;
                    }
                    else
                    {
                        gvDatos.SelectedIndex = indice;
                        imbCiudad.Enabled =
                        imbCiudad.Visible =
                        imbEditar.Enabled =
                        imbEditar.Visible =
                        imbEliminar.Enabled =
                        imbEliminar.Visible = true;

                        if (pnlCiudad.Visible)
                        {
                            ModoInicial2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error al seleccionar el elemento!" + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
        }

        private void LlenarFormulario()
        {
            try
            {
                txtNombre.Text = PaisActual.Nombre;
                txtNacionalidad.Text = PaisActual.Nacionalidad;
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
            }
            
        }

        #endregion

        #region Ciudad

        private void Guardar2()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre2.Text.Trim()))
                {
                    Ciudad nuevaCiudad = new Ciudad()
                    {
                        Nombre = txtNombre2.Text.Trim()
                    };
                    switch (EstadoFormulario2)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            nuevaCiudad.IdCiudad = Guid.NewGuid();
                            nuevaCiudad.IdPais = PaisActual.IdPais;
                            nuevaCiudad.EsActivo = true;
                            if (new CiudadBLL().Insertar(nuevaCiudad))
                            {
                                ModoInicial2();
                            }
                            break;
                        case Generales.EstadoFormulario.Editar:
                            nuevaCiudad.IdCiudad = CiudadActual.IdCiudad;
                            nuevaCiudad.IdPais = PaisActual.IdPais;
                            nuevaCiudad.EsActivo = PaisActual.EsActivo;
                            if (new CiudadBLL().Actualizar(nuevaCiudad))
                            {
                                ModoInicial2();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Ocurrio un error! " + ex.Message.ToString(), TipoMensaje.Danger);
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
            txtNombre2.Text = string.Empty;
        }

        private void LlenarFormulario2()
        {
            txtNombre2.Text = CiudadActual.Nombre;
        }

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

        #endregion

        #endregion
    }
}