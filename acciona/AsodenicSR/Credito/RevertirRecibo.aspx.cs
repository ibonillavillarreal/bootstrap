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
    public partial class RevertirRecibo : Pagina
    {
        public tCredito CreditoActual
        {
            get
            {
                if (Session["CreditoActuales"] == null)
                    Session["CreditoActuales"] = new tCredito();
                return (tCredito)Session["CreditoActuales"];
            }
            set
            {
                Session["CreditoActuales"] = value;
            }
        }

        public List<tCredito> ListaCreditos
        {
            get
            {
                if (Session["ListaCreditoActuales"] == null)
                    Session["ListaCreditoActuales"] = new List<tCredito>();
                return (List<tCredito>)Session["ListaCreditoActuales"];
            }
            set
            {
                Session["ListaCreditoActuales"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarControles();
                ModoInicial();

            }
        }

        #region Botones de acción


        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMotivo.Text))
                    MostrarMensaje("Por favor ingrese el motivo de reversion!", TipoMensaje.Warning); 
                else
                    Guardar();
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

                if (!String.IsNullOrEmpty(txtSerie.Text) && !String.IsNullOrEmpty(txtNoRecibo.Text))
                {
                    LlenarFormulario();
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Info);
            }

        }


        #endregion Botones de acción
        #region Métodos

        #region Modos

        public void ModoInicial()
        {

            imbGuardar.Enabled =
            imbGuardar.Visible = true;

        }

        protected void btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btna = sender as ImageButton;
            GridViewRow row = (GridViewRow)btna.NamingContainer;
            var indice = gvDatos.DataKeys[row.RowIndex];


            if (indice != null)
            {
                CreditoActual = new RecibosDAO().ObtenerPorIdPago(Guid.Parse(indice.Value.ToString()));
                //if (new ContactoBLL().Eliminar(ContactosActuales))
                //{
                //    ModoInicial();
                //}
            }
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                bool exito = false;
                Reversion reversionActual = new Reversion();

                if (new RecibosBLL().RevertirRecibos(CreditoActual))
                {
                    if (CreditoActual != null)
                    {
                        reversionActual.IdReversion = Guid.NewGuid();
                        reversionActual.IdCuenta = CreditoActual.IdCuenta;
                        reversionActual.Serie = CreditoActual.Serie;
                        reversionActual.NoReferencia = CreditoActual.NoReferencia;
                        reversionActual.Monto = CreditoActual.MontoRecibido;
                        reversionActual.MotivoReversion = txtMotivo.Text;
                        reversionActual.FechaRecibo = CreditoActual.FechaEfectiva;
                        reversionActual.FechaReversion = DateTime.Now;
                        reversionActual.Usuario = user.Login;
                        reversionActual.DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                        reversionActual.NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);
                        exito = new RecibosDAO().InsertarReversion(reversionActual);

                        if (exito)
                            MostrarMensaje("Se ha ingresado la reversion!", TipoMensaje.Success);
                        else
                            MostrarMensaje("Error al guardar la reversion!", TipoMensaje.Danger);
                            

                    }
                    else
                    {
                        MostrarMensaje("Por favor seleccione un recibo", TipoMensaje.Danger);
                    }

                    LimpiarControles();
                }


            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private void LimpiarControles()
        {
            //txtSerie.Text = "";
            ListaCreditos = null;
            //txtNoRecibo.Text = "";
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            CreditoActual = null;
        }

        private void LlenarFormulario()
        {
            ListaCreditos = new RecibosDAO().ObtenerListaNoReciboySerie(txtNoRecibo.Text.Trim(), txtSerie.Text.Trim());

            if (ListaCreditos.Count > 0)
            {
                gvDatos.DataSource = ListaCreditos;
                gvDatos.DataBind();
            }
            else
            {
                MostrarMensaje("No se encontraron resultados", TipoMensaje.Danger);
            }
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