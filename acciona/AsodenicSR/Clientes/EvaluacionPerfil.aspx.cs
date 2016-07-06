using Acciona.App_Code;
using AccionaSR.Negocio;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SisSegLT.Datos;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Acciona.Clientes
{
    public partial class EvaluacionPerfil : Pagina
    {
        #region Propiedades

        private decimal? totalPonderacion = 0;

        public Guid IdCliente
        {
            get
            {
                if (ViewState["IdCliente"] == null)
                {
                    ViewState["IdCliente"] = new Guid();
                }
                return (Guid)ViewState["IdCliente"];
            }
            set { ViewState["IdCliente"] = value; }
        }

        #endregion Propiedades

        #region Eventos

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                LimpiarControles();
                if (cliente.Count > 0)
                {
                    imbAgregar.Visible = true;
                    IdCliente = cliente.FirstOrDefault().IdCliente;
                    var evaluaciones = new ClienteEvaluacionBLL().ObtenerEvaluacionesPorIdCliente(cliente.FirstOrDefault().IdCliente);
                    if (evaluaciones.Count > 0)
                    {
                        //Si tiene evaluaciones se cargan en el grid para ser editadas
                        grvEvaluaciones.DataSource = evaluaciones;
                        grvEvaluaciones.DataBind();
                    }
                }
                else
                {
                    MostrarMensaje("No se encontraron Resultados", TipoMensaje.Warning);
                }
            }
        }

        protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Session["Categoria"] = e.Row.DataItem;
                Categoria categoria = (Categoria)Session["Categoria"];//JsonConvert.DeserializeObject<Categoria>(rowView);
                Session.Remove("Categoria");

                //Find the DropDownList in the Row
                DropDownList ddlClasificacion = (e.Row.FindControl("ddlClasificacion") as DropDownList);
                //var dsClasificacion = new ClasificacionBLL().ObtenerPorIdCategoria(categoria.IdCategoria);
                ddlClasificacion.DataSource = categoria.Clasificacion.Where(x => x.EsActivo).ToList();
                ddlClasificacion.DataTextField = "Nombre";
                ddlClasificacion.DataValueField = "IdClasificacion";
                ddlClasificacion.DataBind();

                if (Session["evaluacion"] != null)
                {
                    ClienteEvaluacion evaluacion = (ClienteEvaluacion)Session["evaluacion"];

                    foreach (var evaluacionCategoria in evaluacion.EvaluacionCategoria)
                    {
                        if (evaluacionCategoria.IdCategoria == categoria.IdCategoria)
                        {
                            foreach (var clasificacion in categoria.Clasificacion)
                            {
                                var evaluacionClasificacion = evaluacionCategoria.EvaluacionCategoriaClasificacion.Where(x => x.IdClasificacion == clasificacion.IdClasificacion).FirstOrDefault();
                                if (evaluacionClasificacion != null)
                                {
                                    ddlClasificacion.SelectedValue = evaluacionClasificacion.IdClasificacion.ToString();

                                    HiddenField hfEditarEvaluacionCategoria = (e.Row.FindControl("hfEditarEvaluacionCategoria") as HiddenField);
                                    var jsonEvaluacionCategoria =
                                        new
                                        {
                                            IdClienteEvaluacion = evaluacionCategoria.IdClienteEvaluacion,
                                            IdEvaluacionCategoria = evaluacionClasificacion.IdEvaluacionCategoria,
                                            IdEvaluacionCategoriaClasificacion = evaluacionClasificacion.IdEvaluacionCategoriaCategoria,
                                            IdClasificacion = evaluacionClasificacion.IdClasificacion,
                                            IdCategoria = categoria.IdCategoria
                                        };
                                    var json = JsonConvert.SerializeObject(jsonEvaluacionCategoria);
                                    hfEditarEvaluacionCategoria.Value = System.Web.HttpUtility.HtmlDecode(json);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //Add Default Item in the DropDownList
                ddlClasificacion.Items.Insert(0, new ListItem("Seleccione una Opción"));

                totalPonderacion += categoria.Ponderacion;

                categoria.Clasificacion.ToList().ForEach(x => x.Categoria = null);
                HiddenField hfClasificacion = (e.Row.FindControl("hfClasificacion") as HiddenField);
                var jsonText = JsonConvert.SerializeObject(categoria.Clasificacion);
                hfClasificacion.Value = System.Web.HttpUtility.HtmlDecode(jsonText);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPonderacion = (Label)e.Row.FindControl("lblTotalPonderacion");
                lblTotalPonderacion.Text = totalPonderacion.ToString();
                Session.Remove("evaluacion");
            }
        }

        protected void imbActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(hfEvaluacionCliente.Value) && !string.IsNullOrEmpty(hfEvaluacionCategoria.Value))
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                ClienteEvaluacion evaluacionCliente = new ClienteEvaluacion();
                evaluacionCliente = JsonConvert.DeserializeObject<ClienteEvaluacion>(hfEvaluacionCliente.Value);
                evaluacionCliente.IdClienteEvaluacion = evaluacionCliente.IdClienteEvaluacion;
                evaluacionCliente.IdMetodologia = Guid.Parse(ddlMetodologia.SelectedValue);
                evaluacionCliente.IdUsuario = Guid.Parse(ddlPromotor.SelectedValue);
                evaluacionCliente.NoCredito = txtNoCredito.Text;
                evaluacionCliente.NoExpediente = txtNoExpediente.Text;
                evaluacionCliente.FechaRegistro = DateTime.Now;
                evaluacionCliente.FechaHoraEvaluacion = DateTime.Parse(txtFecha.Text);
                evaluacionCliente.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                evaluacionCliente.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                evaluacionCliente.Usuario = user.Login;

                List<EvaluacionCategoria> evaluacionCategoria = new List<EvaluacionCategoria>();

                var arrayCategorias = JsonConvert.DeserializeObject<List<RootObject>>(hfEvaluacionCategoria.Value);
                foreach (var item in arrayCategorias)
                {
                    EvaluacionCategoria evaluacioncategoria = new EvaluacionCategoria();
                    evaluacioncategoria.IdEvaluacionCategoria = Guid.Parse(item.IdEvaluacionCategoria);
                    evaluacioncategoria.IdCategoria = Guid.Parse(item.IdCategoria);
                    evaluacioncategoria.IdClienteEvaluacion = evaluacionCliente.IdClienteEvaluacion;
                    evaluacioncategoria.CalculoRiesgo = Decimal.Parse(item.CalculoRiesgo.ToString());
                    evaluacioncategoria.ClienteEvaluacion = evaluacionCliente;
                    evaluacioncategoria.EsActivo = item.EsActivo;
                    evaluacioncategoria.FechaRegistro = DateTime.Now;
                    evaluacioncategoria.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                    evaluacioncategoria.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                    evaluacioncategoria.Usuario = user.Login;

                    SisSegLT.Datos.EvaluacionCategoriaClasificacion evaluacionClasificacion = new SisSegLT.Datos.EvaluacionCategoriaClasificacion();
                    evaluacionClasificacion.IdEvaluacionCategoriaCategoria = Guid.Parse(item.EvaluacionCategoriaClasificacion.IdEvaluacionCategoriaCategoria);
                    evaluacionClasificacion.IdClasificacion = Guid.Parse(item.EvaluacionCategoriaClasificacion.IdClasificacion);
                    evaluacionClasificacion.IdEvaluacionCategoria = evaluacioncategoria.IdEvaluacionCategoria;
                    evaluacionClasificacion.EsActivo = true;
                    evaluacionClasificacion.FechaRegistro = DateTime.Now;
                    evaluacionClasificacion.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                    evaluacionClasificacion.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                    evaluacionClasificacion.Usuario = user.Login;

                    evaluacioncategoria.EvaluacionCategoriaClasificacion.Add(evaluacionClasificacion);

                    evaluacionCategoria.Add(evaluacioncategoria);
                }
                evaluacionCliente.EvaluacionCategoria = evaluacionCategoria;

                AprobacionInstitucionBLL aprobacionBLL = new AprobacionInstitucionBLL();
                var aprobacion = aprobacionBLL.ObtenerAprobacionInstitucionPorIdCliente(evaluacionCliente.IdCliente);

                if (aprobacion.Count > 0)
                {
                    var matriz = new MatrizCalificacionBLL().Listar();
                    foreach (var item in matriz)
                    {
                        if (evaluacionCliente.Puntaje >= item.ValorMin && evaluacionCliente.Puntaje <= item.ValorMax)
                        {
                            aprobacion.FirstOrDefault().NivelRiesgo = item.Nombre;
                            break;
                        }
                    }
                    ClienteEvaluacionBLL evaluacionBLL = new ClienteEvaluacionBLL();
                    bool exito = false;
                    exito = evaluacionBLL.ActualizarEvaluacion(evaluacionCliente);

                    if (exito)
                    {
                        var cliente = new ClienteBLL().ObtenerPorIdCliente(aprobacion.FirstOrDefault().IdCliente);
                        MostrarMensaje("Evaluacion agregada satisfactoriamente", TipoMensaje.Info);
                        aprobacion.FirstOrDefault().Usuario = null;
                        aprobacion.FirstOrDefault().Cliente = null;
                        aprobacionBLL.Actualizar(aprobacion.FirstOrDefault());
                        cliente.EstadoPerfil = 2;
                        ClienteBLL clienteBLL = new ClienteBLL();
                        clienteBLL.Actualizar(cliente);
                        LimpiarControles();
                        imbGuardar.Visible = false;
                        imbCancelar.Visible = false;
                        imbActualizar.Visible = false;
                        imbAgregar.Visible = true;
                        pnlGridview.Visible = true;
                        pnlDatos.Visible = false;
                        var evaluaciones = new ClienteEvaluacionBLL().ObtenerEvaluacionesPorIdCliente(IdCliente);
                        if (evaluaciones.Count > 0)
                        {
                            //Si tiene evaluaciones se cargan en el grid para ser editadas
                            grvEvaluaciones.DataSource = evaluaciones;
                            grvEvaluaciones.DataBind();
                        }
                    }
                }
                else
                {
                    MostrarMensaje("Por favor complete la aprobación de la institución en el perfil del cliente.", TipoMensaje.Danger);
                }
            }
        }

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            imbAgregar.Visible = false;
            imbActualizar.Visible = false;
            imbCancelar.Visible = true;
            imbGuardar.Visible = true;
            var cliente = new ClienteBLL().ObtenerPorIdCliente(IdCliente);
            if (cliente != null)
            {
                pnlGridview.Visible = false;
                pnlDatos.Visible = true;

                txtNombre.Text = cliente.NombreCompleto;
                hfIdCliente.Value = cliente.IdCliente.ToString();

                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                var sucursal = new SucursalDAO().ObtenerPorIdSucursal(user.IdSucursal);
                txtCodAgencia.Text = sucursal.Codigo;

                var factores = new CategoriaBLL().Listar();
                factores.ForEach(x => x.Factor = null);
                gvDatos.DataSource = factores.Where(x => x.EsActivo == true).ToList();
                gvDatos.DataBind();
            }
        }

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            imbGuardar.Visible = false;
            imbCancelar.Visible = false;
            imbActualizar.Visible = false;
            imbAgregar.Visible = true;
            pnlGridview.Visible = true;
            pnlDatos.Visible = false;
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(hfEvaluacionCliente.Value) && !string.IsNullOrEmpty(hfEvaluacionCategoria.Value))
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                ClienteEvaluacion evaluacionCliente = new ClienteEvaluacion();
                evaluacionCliente = JsonConvert.DeserializeObject<ClienteEvaluacion>(hfEvaluacionCliente.Value);
                evaluacionCliente.IdClienteEvaluacion = Guid.NewGuid();
                evaluacionCliente.FechaRegistro = DateTime.Now;
                evaluacionCliente.FechaHoraEvaluacion = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                evaluacionCliente.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                evaluacionCliente.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                evaluacionCliente.Usuario = user.Login;

                List<EvaluacionCategoria> evaluacionCategoria = new List<EvaluacionCategoria>();

                var arrayCategorias = JsonConvert.DeserializeObject<List<RootObject>>(hfEvaluacionCategoria.Value);
                foreach (var item in arrayCategorias)
                {
                    EvaluacionCategoria evaluacioncategoria = new EvaluacionCategoria();
                    evaluacioncategoria.IdEvaluacionCategoria = Guid.NewGuid();
                    evaluacioncategoria.IdCategoria = Guid.Parse(item.IdCategoria);
                    evaluacioncategoria.IdClienteEvaluacion = evaluacionCliente.IdClienteEvaluacion;
                    evaluacioncategoria.CalculoRiesgo = Decimal.Parse(item.CalculoRiesgo.ToString());
                    evaluacioncategoria.ClienteEvaluacion = evaluacionCliente;
                    evaluacioncategoria.EsActivo = item.EsActivo;
                    evaluacioncategoria.FechaRegistro = DateTime.Now;
                    evaluacioncategoria.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                    evaluacioncategoria.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                    evaluacioncategoria.Usuario = user.Login;

                    SisSegLT.Datos.EvaluacionCategoriaClasificacion evaluacionClasificacion = new SisSegLT.Datos.EvaluacionCategoriaClasificacion();
                    evaluacionClasificacion.IdEvaluacionCategoriaCategoria = Guid.NewGuid();
                    evaluacionClasificacion.IdClasificacion = Guid.Parse(item.EvaluacionCategoriaClasificacion.IdClasificacion);
                    evaluacionClasificacion.IdEvaluacionCategoria = evaluacioncategoria.IdEvaluacionCategoria;
                    evaluacionClasificacion.EsActivo = true;
                    evaluacionClasificacion.FechaRegistro = DateTime.Now;
                    evaluacionClasificacion.UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Request);
                    evaluacionClasificacion.UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Request);
                    evaluacionClasificacion.Usuario = user.Login;

                    evaluacioncategoria.EvaluacionCategoriaClasificacion.Add(evaluacionClasificacion);

                    evaluacionCategoria.Add(evaluacioncategoria);
                }
                evaluacionCliente.EvaluacionCategoria = evaluacionCategoria;

                AprobacionInstitucionBLL aprobacionBLL = new AprobacionInstitucionBLL();
                var aprobacion = aprobacionBLL.ObtenerAprobacionInstitucionPorIdCliente(evaluacionCliente.IdCliente);

                if (aprobacion.Count > 0)
                {
                    var matriz = new MatrizCalificacionBLL().Listar();
                    foreach (var item in matriz)
                    {
                        if (evaluacionCliente.Puntaje >= item.ValorMin && evaluacionCliente.Puntaje <= item.ValorMax)
                        {
                            aprobacion.FirstOrDefault().NivelRiesgo = item.Nombre;
                            break;
                        }
                    }
                    ClienteEvaluacionBLL evaluacionBLL = new ClienteEvaluacionBLL();
                    bool exito = false;
                    exito = evaluacionBLL.GuardarEvaluacion(evaluacionCliente);

                    if (exito)
                    {
                        MostrarMensaje("Evaluacion agregada satisfactoriamente", TipoMensaje.Info);
                        aprobacion.FirstOrDefault().Cliente.EstadoPerfil = 2;
                        aprobacionBLL.Actualizar(aprobacion.FirstOrDefault());
                        LimpiarControles();
                        imbGuardar.Visible = false;
                        imbCancelar.Visible = false;
                        imbActualizar.Visible = false;
                        imbAgregar.Visible = true;
                        pnlGridview.Visible = true;
                        pnlDatos.Visible = false;
                        var evaluaciones = new ClienteEvaluacionBLL().ObtenerEvaluacionesPorIdCliente(IdCliente);
                        if (evaluaciones.Count > 0)
                        {
                            //Si tiene evaluaciones se cargan en el grid para ser editadas
                            grvEvaluaciones.DataSource = evaluaciones;
                            grvEvaluaciones.DataBind();
                        }
                    }
                }
                else
                {
                    MostrarMensaje("Por favor complete la aprobación de la institución en el perfil del cliente.", TipoMensaje.Danger);
                }
            }
        }

        protected void lnbSeleccionar_Click(object sender, EventArgs e)
        {
            LinkButton lbEditar = (LinkButton)sender;
            CargarFormularioPorIdClienteEvaluacion(lbEditar.CommandArgument);
        }

        protected void lbImprimir_Click(object sender, EventArgs e)
        {
            LinkButton lbEditar = (LinkButton)sender;
            Response.Redirect("~/Reportes/ReporteEvaluacion.aspx?cliente=" + lbEditar.CommandArgument, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
            }
        }

        #endregion Eventos

        #region Metodos

        private void CargarControles()
        {
            var ddlDataSource = new MetodologiaBLL().Listar();
            ddlMetodologia.DataSource = ddlDataSource;
            ddlMetodologia.DataTextField = "Nombre";
            ddlMetodologia.DataValueField = "IdMetodologia";
            ddlMetodologia.DataBind();

            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            List<Usuario> promotores = new List<Usuario>();

            promotores = new UsuarioDAO().ObtenerPorIdSucursal(user.IdSucursal);

            ddlPromotor.DataSource = promotores;
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();

            ddlPromotor.Items.Insert(0, new ListItem("Seleccione una Opción"));

            List<dynamic> prom = new List<dynamic>();
            foreach (var item in promotores)
            {
                var jsonUsuario = new
                {
                    IdUsuario = item.IdUsuario,
                    Codigo = item.Codigo
                };

                prom.Add(jsonUsuario);
            }

            hfUsuario.Value = JsonConvert.SerializeObject(prom);
            //, Newtonsoft.Json.Formatting.Indented,
            //                       new JsonSerializerSettings
            //                       {
            //                           PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //                       });
        }

        private void CargarFormularioPorIdClienteEvaluacion(string idEvaluacionCliente)
        {
            var evaluacion = new ClienteEvaluacionBLL().ObtenerEvaluacionPorIdEvaluacion(Guid.Parse(idEvaluacionCliente));
            if (evaluacion != null)
            {
                pnlDatos.Visible = true;
                pnlGridview.Visible = false;
                imbAgregar.Visible = false;
                imbCancelar.Visible = true;
                imbGuardar.Visible = false;
                imbActualizar.Visible = true;

                txtNombre.Text = evaluacion.Cliente.NombreCompleto;
                hfIdCliente.Value = evaluacion.Cliente.IdCliente.ToString();

                txtNoCredito.Text = evaluacion.NoCredito;
                txtNoExpediente.Text = evaluacion.NoExpediente;
                txtFecha.Text = evaluacion.FechaHoraEvaluacion.ToString();
                txtCodigo.Text = evaluacion.Usuario1.Codigo;
                ddlPromotor.SelectedValue = evaluacion.Usuario1.IdUsuario.ToString();

                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                var sucursal = new SucursalDAO().ObtenerPorIdSucursal(user.IdSucursal);
                txtCodAgencia.Text = sucursal.Codigo;

                hfIdUsuario.Value = evaluacion.IdUsuario.ToString();

                Session["evaluacion"] = evaluacion.Cliente.ClienteEvaluacion.FirstOrDefault(x => x.IdClienteEvaluacion == Guid.Parse(idEvaluacionCliente));

                var factores = new CategoriaBLL().Listar();
                factores.ForEach(x => x.Factor = null);
                gvDatos.DataSource = factores.Where(x => x.EsActivo == true).ToList();
                gvDatos.DataBind();
            }
        }

        private void LimpiarControles()
        {
            txtBuscar.Text = string.Empty;
            txtCodAgencia.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtNoCredito.Text = string.Empty;
            txtNoExpediente.Text = string.Empty;
            txtNombre.Text = string.Empty;
            ddlPromotor.SelectedIndex = -1;
            ddlMetodologia.SelectedIndex = -1;
            hfEvaluacionCategoria.Value = string.Empty;
            hfEvaluacionCliente.Value = string.Empty;
            hfIdCliente.Value = string.Empty;
            hfIdUsuario.Value = string.Empty;
            gvDatos.DataSource = null;
            gvDatos.DataBind();
            grvEvaluaciones.DataSource = null;
            grvEvaluaciones.DataBind();
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

        #endregion Metodos

        #region Enumeraciones

        internal enum TipoMensaje
        {
            Info,
            Success,
            Warning,
            Danger
        }

        #endregion Enumeraciones
    }

    #region Clases Auxiliares

    public class EvaluacionCategoriaClasificacion
    {
        public bool EsActivo { get; set; }

        public string FechaRegistro { get; set; }

        public string IdClasificacion { get; set; }

        public string IdEvaluacionCategoria { get; set; }

        public string IdEvaluacionCategoriaCategoria { get; set; }

        public string UserIP { get; set; }

        public string UserPC { get; set; }

        public string Usuario { get; set; }
    }

    public class RootObject
    {
        public double CalculoRiesgo { get; set; }

        public bool EsActivo { get; set; }

        public EvaluacionCategoriaClasificacion EvaluacionCategoriaClasificacion { get; set; }

        public string FechaRegistro { get; set; }

        public string IdCategoria { get; set; }

        public string IdClienteEvaluacion { get; set; }

        public string IdEvaluacionCategoria { get; set; }

        public string UserIP { get; set; }

        public string UserPC { get; set; }

        public string Usuario { get; set; }
    }

    #endregion Clases Auxiliares
}