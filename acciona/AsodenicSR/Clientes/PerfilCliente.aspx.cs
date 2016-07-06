using Acciona.App_Code;
using AccionaSR.Negocio;
using Newtonsoft.Json;
using SisSegLT.Datos;
using SisSegLT.Negocio;
using SisSegLT.Seguridad;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Acciona.Clientes
{
    public partial class PerfilCliente : Pagina
    {
        #region Propiedades

        public Generales.EstadoFormulario EstadoFormulario
        {
            get
            {
                if (ViewState["EstadoFormularioDatosCliente"] == null)
                    ViewState["EstadoFormularioDatosCliente"] = Generales.EstadoFormulario.Inicial;
                return (Generales.EstadoFormulario)ViewState["EstadoFormularioDatosCliente"];
            }
            set
            {
                ViewState["EstadoFormularioDatosCliente"] = value;
            }
        }

        public Cliente DatosCliente
        {
            get
            {
                if (Session["DatosClienteActual"] == null)
                    Session["DatosClienteActual"] = new Cliente();
                return (Cliente)Session["DatosClienteActual"];
            }
            set
            {
                Session["DatosClienteActual"] = value;
            }
        }

        public DetalleCliente DetalleClienteActual
        {
            get
            {
                if (Session["DatosDetalleCliente"] == null)
                    Session["DatosDetalleCliente"] = new DetalleCliente();
                return (DetalleCliente)Session["DatosDetalleCliente"];
            }
            set
            {
                Session["DatosDetalleCliente"] = value;
            }
        }
        //public Cliente DatosCliente
        //{
        //    get
        //    {
        //        if (ViewState["DatosClienteActual"] == null)
        //            ViewState["DatosClienteActual"] = new Cliente();
        //        return JsonConvert.DeserializeObject<Cliente>(ViewState["DatosClienteActual"].ToString());
        //    }
        //    set
        //    {
        //        string json = JsonConvert.SerializeObject(value, Formatting.Indented,
        //                           new JsonSerializerSettings
        //                           {
        //                               PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //                           });
        //        ViewState["DatosClienteActual"] = json;
        //    }
        //}

        //public DetalleCliente DetalleClienteActual
        //{
        //    get
        //    {
        //        if (ViewState["DatosDetalleCliente"] == null)
        //            ViewState["DatosDetalleCliente"] = new DetalleCliente();
        //        return JsonConvert.DeserializeObject<DetalleCliente>(ViewState["DatosDetalleCliente"].ToString());
        //    }
        //    set
        //    {
        //        string json = JsonConvert.SerializeObject(value, Formatting.Indented,
        //                            new JsonSerializerSettings
        //                            {
        //                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //                            });
        //        ViewState["DatosDetalleCliente"] = json;
        //    }
        //}

        private bool ImportacionActivada
        {
            get
            {
                if (ViewState["ImportacionActivadaDatosCliente"] == null)
                    ViewState["ImportacionActivadaDatosCliente"] = false;
                return (bool)ViewState["ImportacionActivadaDatosCliente"];
            }
            set
            {
                ViewState["ImportacionActivadaDatosCliente"] = value;
            }
        }

        #endregion Propiedades

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarSesion();
                ModoInicial();
                txtIdentificacion.Attributes.Add("OnBlur", "return ValidarCedula()");
            }
            //Setear el tab seleccionado
            hfSelectedTab.Value = hfSelectedTab.Value;
            hfNombreCliente.Value = this.MiUsuario.Nombre;
        }

        #region Botones de acción

        protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        {
            hfTabContacto.Value = string.Empty;
            ModoAgregar();
        }

        protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        {
            hfTabContacto.Value = string.Empty;
            ModoEditar();
        }

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
            hfTabContacto.Value = string.Empty;
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();
            hfTabContacto.Value = "contactos";
        }

        protected void imbImportar_Click(object sender, ImageClickEventArgs e)
        {
            ModoImportar();
            hfTabContacto.Value = string.Empty;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfTabContacto.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                if (cliente.Count > 0)
                {
                    hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                    //DatosCliente = cliente.FirstOrDefault();
                    txtIdentificacion.Text = txtBuscar.Text;
                    ModoEditar();

                    Domicilio.ModoInicial();
                    Contactos.ModoInicial();
                    ReferenciaPersonal.ModoInicial();
                    ReferenciasCrediticia.ModoInicial();
                    DatosNegocios.ModoInicial();
                    Aprobacion.ModoInicial();
                    ResumenTransaccion.ModoInicial();
                    ImportacionActivada = false; // Indica que no se ha utilizado la importacion
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }
            }
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            hfTabContacto.Value = string.Empty;
            if (txtBuscar.Text != string.Empty)
            {
                Response.Redirect("~/Reportes/ReportePIC.aspx?identificacion=" + txtBuscar.Text, true);
            }
        }

        #endregion Botones de acción

        #region Métodos

        #region Modos

        private void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            imbAgregar.Enabled =
            imbAgregar.Visible = true;

            imbEditar.Enabled =
            imbEditar.Visible =
            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbImportar.Enabled =
            imbImportar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = false;

            txtIdentificacion.Enabled =
            txtAlias.Enabled =
            txtFechaNacimiento.Enabled =
            txtFechaPerfil.Enabled =
            txtIngresos.Enabled =
            txtMiembrosFamilia.Enabled =
            txtNacionalidad.Enabled =
            txtNombre.Enabled =
            txtApellidos.Enabled =
            txtOcupacion.Enabled =
            ddlEstadoCivil.Enabled =
            ddlPais.Enabled =
            txtProfesion.Enabled =
            txtFechaEmision.Enabled =
            txtFechaVencimiento.Enabled =
            ddlPaisEmisor.Enabled =
            ddlSexo.Enabled = false;
            LimpiarControles();
            LimpiarSesion();
        }

        private void ModoImportar()
        {
            List<DatosClienteImp> datosCliente = new DatosClienteImpBLL().ObtenerPorCedula(txtIdentificacion.Text);
            if (datosCliente.Count > 0)
            {
                DatosClienteImp cliente = datosCliente.FirstOrDefault();
                ListItem itemSeleccionado = null;

                LimpiarControles();

                txtIdentificacion.Text = cliente.NoCedula;
                txtNombre.Text = cliente.Nombres;
                txtApellidos.Text = cliente.Apellidos;
                txtFechaNacimiento.Text = ((DateTime)cliente.FechaNacimiento).ToShortDateString();
                txtFechaPerfil.Text = cliente.FechaActualizacion.ToShortDateString();
                txtMiembrosFamilia.Text = string.IsNullOrEmpty(cliente.NoMiembros) ? "0" : cliente.NoMiembros;
                txtOcupacion.Text = cliente.Profesion;
                txtProfesion.Text = cliente.Profesion;
                switch (cliente.EstadoCivil.ToLower())
                {
                    case "c":
                        itemSeleccionado = ddlEstadoCivil.Items.FindByText("Casado");
                        break;

                    case "s":
                        itemSeleccionado = ddlEstadoCivil.Items.FindByText("Soltero");
                        break;

                    case "d":
                        itemSeleccionado = ddlEstadoCivil.Items.FindByText("Divorciado");
                        break;
                }
                if (itemSeleccionado != null)
                {
                    ddlEstadoCivil.ClearSelection();
                    itemSeleccionado.Selected = true;
                }

                switch (cliente.Sexo.ToLower())
                {
                    case "f":
                        itemSeleccionado = ddlSexo.Items.FindByText("Femenino");
                        break;

                    case "m":
                        itemSeleccionado = ddlSexo.Items.FindByText("Masculino");
                        break;
                }
                if (itemSeleccionado != null)
                {
                    ddlSexo.ClearSelection();
                    itemSeleccionado.Selected = true;
                }

                //itemSeleccionado = ddlProfesion.Items.FindByText(cliente.Profesion);
                //if (itemSeleccionado != null)
                //{
                //    ddlProfesion.ClearSelection();
                //    itemSeleccionado.Selected = true;
                //}
                ImportacionActivada = true;
            }
        }

        private void ModoAgregar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Agregar;

            DatosCliente = new Cliente();
            DetalleClienteActual = new DetalleCliente();

            imbEditar.Enabled =
            imbEditar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = false;

            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbImportar.Enabled =
            imbImportar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = true;

            txtIdentificacion.Enabled =
            txtAlias.Enabled =
            txtFechaNacimiento.Enabled =
            txtFechaPerfil.Enabled =
            txtIngresos.Enabled =
            txtMiembrosFamilia.Enabled =
            txtNacionalidad.Enabled =
            txtNombre.Enabled =
            txtApellidos.Enabled =
            txtOcupacion.Enabled =
            ddlEstadoCivil.Enabled =
            ddlPais.Enabled =
            txtProfesion.Enabled =
            txtFechaEmision.Enabled =
            txtFechaVencimiento.Enabled =
            ddlPaisEmisor.Enabled =
            ddlSexo.Enabled = true;

            LimpiarControles();
            hfIdCliente.Value = string.Empty;
            CargarCombos();
            txtNacionalidad.Text = "Nicaraguense";
            ddlPais.SelectedIndex = ddlPais.Items.IndexOf(ddlPais.Items.FindByText("Nicaragua"));
            ddlPaisEmisor.SelectedIndex = ddlPaisEmisor.Items.IndexOf(ddlPaisEmisor.Items.FindByText("Nicaragua"));
        }

        private void ModoEditar()
        {
            EstadoFormulario = Generales.EstadoFormulario.Editar;
            
            var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtIdentificacion.Text.Trim());

            DatosCliente = new Cliente();
            DatosCliente = cliente.FirstOrDefault();

            DetalleClienteActual = new DetalleClienteBLL().ObtenerDetalleClientePorIdCliente(Guid.Parse(hfIdCliente.Value));

            imbEditar.Enabled =
            imbEditar.Visible =
            imbAgregar.Enabled =
            imbAgregar.Visible = false;

            imbGuardar.Enabled =
            imbGuardar.Visible =
            imbImportar.Enabled =
            imbImportar.Visible =
            imbCancelar.Enabled =
            imbCancelar.Visible = true;

            txtIdentificacion.Enabled =
            txtAlias.Enabled =
            txtFechaNacimiento.Enabled =
            txtFechaPerfil.Enabled =
            txtIngresos.Enabled =
            txtMiembrosFamilia.Enabled =
            txtNacionalidad.Enabled =
            txtNombre.Enabled =
            txtApellidos.Enabled =
            txtOcupacion.Enabled =
            ddlEstadoCivil.Enabled =
            ddlPais.Enabled =
            txtProfesion.Enabled =
            txtFechaEmision.Enabled =
            txtFechaVencimiento.Enabled =
            ddlPaisEmisor.Enabled =
            ddlSexo.Enabled = true;

            LimpiarControles();
            CargarCombos();
            LlenarFormulario();
            ddlPais.SelectedIndex = ddlPais.Items.IndexOf(ddlPais.Items.FindByValue("Nicaragua"));
            ddlPaisEmisor.SelectedIndex = ddlPaisEmisor.Items.IndexOf(ddlPaisEmisor.Items.FindByValue("Nicaragua"));
        }

        private void ModoEliminar()
        {
            //if (gvDatos.SelectedIndex >= 0)
            //{
            //    var dataKey = gvDatos.DataKeys[gvDatos.SelectedIndex];
            //    if (dataKey != null)
            //    {
            //        ProfesionActual = new ProfesionBLL().ObtenerPorIdProfesion(Guid.Parse(dataKey.Value);
            //        if (new ProfesionBLL().Eliminar(ProfesionActual))
            //        {
            //            ModoInicial();
            //        }
            //    }
            //}
        }

        #endregion Modos

        #region Otros métodos

        private void Guardar()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
                if (!string.IsNullOrEmpty(txtIdentificacion.Text.Trim()))
                {
                    List<Cliente> clienteExiste = new ClienteBLL().ObtenerPorIdentificacion(txtIdentificacion.Text.Trim());


                    Cliente nuevoCliente = LlenarObjetoCliente();
                    nuevoCliente.IdUsuario = user.IdUsuario;
                    nuevoCliente.IdSucursal = user.IdSucursal;
                    nuevoCliente.Usuario = user.Login;

                    DetalleCliente nuevoDetalleCliente = LlenarObjetoDetalleCliente();
                    if (nuevoDetalleCliente != null)
                    {
                        nuevoDetalleCliente.IdCliente = nuevoCliente.IdCliente;
                        nuevoDetalleCliente.Usuario = user.Login;
                    }

                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Agregar:
                            if (clienteExiste.Count <= 0)
                            {
                                //nuevoCliente.IdCliente = Guid.NewGuid();
                                hfIdCliente.Value = nuevoCliente.IdCliente.ToString();
                                Session["idCliente"] = nuevoCliente.IdCliente;
                                nuevoCliente.EsActivo = true;
                                if (new ClienteBLL().Insertar(nuevoCliente))
                                {
                                    if (nuevoDetalleCliente != null)
                                    {
                                        var success = new DetalleClienteBLL().Insertar(nuevoDetalleCliente);
                                    }
                                    bool exito = !ImportacionActivada;
                                    if (ImportacionActivada)
                                    {
                                        List<DatosClienteImp> datosCliente = new DatosClienteImpBLL().ObtenerPorCedula(txtIdentificacion.Text);
                                        List<DetallePrestamosClienteImp> detalleCliente = new DetallePrestamosClienteImpBLL().ObtenerPorCedula(txtIdentificacion.Text);

                                        DateTime fechaRegistro = DateTime.Now;
                                        string userIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request);
                                        string userPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request);

                                        if (datosCliente.Count > 0)
                                        {
                                            List<Domicilio> listaDomicilios = new DomicilioBLL().ObtenerDomicilioPorIdCliente(nuevoCliente.IdCliente);
                                            List<DatosNegocio> listaDatosNegocio = new DatosNegocioBLL().ObtenerDatosNegocioPorIdCliente(nuevoCliente.IdCliente);
                                            //List<ResumenTransaccion> listaTransacciones = new ResumenTransaccionBLL().ObtenerResumenTransaccionPorIdCliente(nuevoCliente.IdCliente);

                                            foreach (DatosClienteImp dato in datosCliente)
                                            {
                                                //Domicilios

                                                #region Domicilios

                                                if (!listaDomicilios.Any(l => l.Descripcion.Equals(dato.Domicilio)))
                                                {
                                                    Domicilio nuevoDomicilio = new Domicilio()
                                                    {
                                                        IdDomicilio = Guid.NewGuid(),
                                                        IdCliente = nuevoCliente.IdCliente,
                                                        Descripcion = dato.Domicilio,
                                                        EsAlquilada = !string.IsNullOrEmpty(dato.Casa) ? dato.Casa.ToLower().Equals("a") : false,
                                                        EsPropia = !string.IsNullOrEmpty(dato.Casa) ? dato.Casa.ToLower().Equals("p") : false,
                                                        Familiar = !string.IsNullOrEmpty(dato.Casa) ? dato.Casa.ToLower().Equals("f") : false,
                                                        TiempoResidir = 0,
                                                        EsActivo = true,
                                                        FechaRegistro = fechaRegistro,
                                                        UserIP = userIP,
                                                        UserPC = userPC,
                                                        Usuario = user.Login
                                                    };
                                                    exito = new DomicilioBLL().Insertar(nuevoDomicilio);
                                                    if (exito) listaDomicilios.Add(nuevoDomicilio);
                                                }

                                                #endregion Domicilios

                                                //DatosNegocio

                                                #region DatosNegocio

                                                if (!listaDatosNegocio.Any(l => l.TipoNegocio.Equals(dato.NombreTipoNegocio) && l.UbicacionNegocio.Equals(dato.DireccionNegocio)))
                                                {
                                                    DatosNegocio nuevoDatosNegocio = new DatosNegocio()
                                                    {
                                                        IdDatosNegocio = Guid.NewGuid(),
                                                        IdCliente = nuevoCliente.IdCliente,
                                                        UbicacionNegocio = dato.DireccionNegocio,
                                                        Tiempo = "0",
                                                        TipoNegocio = dato.NombreTipoNegocio,
                                                        EsPropio = !string.IsNullOrEmpty(dato.CasaNegocio) ? dato.CasaNegocio.ToLower().Equals("p") : false,
                                                        Alquila = !string.IsNullOrEmpty(dato.CasaNegocio) ? dato.CasaNegocio.ToLower().Equals("a") : false,
                                                        Familiar = !string.IsNullOrEmpty(dato.CasaNegocio) ? dato.CasaNegocio.ToLower().Equals("f") : false,
                                                        IngresoVolumen = 0,
                                                        EsActivo = true,
                                                        FechaRegistro = fechaRegistro,
                                                        UserIP = userIP,
                                                        UserPC = userPC,
                                                        Usuario = user.Login
                                                    };
                                                    exito = new DatosNegocioBLL().Insertar(nuevoDatosNegocio);
                                                    if (exito) listaDatosNegocio.Add(nuevoDatosNegocio);
                                                }

                                                #endregion DatosNegocio
                                            }
                                            Domicilio.ModoInicial();
                                            DatosNegocios.ModoInicial();
                                        }
                                        else
                                        {
                                            exito = true;
                                        }

                                        if (exito && (detalleCliente.Count > 0))
                                        {
                                            List<ResumenTransaccion> listaTransacciones = new ResumenTransaccionBLL().ObtenerResumenTransaccionPorIdCliente(nuevoCliente.IdCliente);

                                            foreach (DetallePrestamosClienteImp detalle in detalleCliente)
                                            {
                                                //ResumenTransaccion

                                                #region ResumenTransaccion

                                                if (!listaTransacciones.Any(l => l.NoPrestamo.Equals(detalle.NoPrestamo) && l.FechaInicioCredito.Equals(detalle.FechaAprobacion)))
                                                {
                                                    ResumenTransaccion nuevoResumenTransaccion = new ResumenTransaccion()
                                                    {
                                                        IdTransaccionesInstitucion = Guid.NewGuid(),
                                                        IdCliente = nuevoCliente.IdCliente,
                                                        NoPrestamo = detalle.NoPrestamo,
                                                        FechaInicioCredito = detalle.FechaAprobacion,
                                                        FechaFinCredito = detalle.FechaCancelacion,
                                                        MontoPromedio = detalle.MontoAprobado != null ? Convert.ToDecimal((double)detalle.MontoAprobado) : 0,
                                                        MaximoDiasMora = 0,
                                                        Observaciones = "Ninguna",
                                                        EsActivo = true,
                                                        FechaRegistro = fechaRegistro,
                                                        UserIP = userIP,
                                                        UserPC = userPC,
                                                        Usuario = user.Login
                                                    };

                                                    DatosClienteImp dato = datosCliente.FirstOrDefault(c => c.NoExpediente.Trim().Equals(detalle.CodigoExpediente));
                                                    if (dato != null)
                                                    {
                                                        string metodologia = dato.Metodologia.Trim().ToLower();
                                                        Metodologia metodologiaUsada = new MetodologiaBLL().Listar().FirstOrDefault(m => m.Nombre.Trim().ToLower().Equals(metodologia));
                                                        if (metodologiaUsada != null)
                                                        {
                                                            nuevoResumenTransaccion.IdMetodologia = metodologiaUsada.IdMetodologia;
                                                            exito = new ResumenTransaccionBLL().Insertar(nuevoResumenTransaccion);
                                                            if (exito) listaTransacciones.Add(nuevoResumenTransaccion);
                                                        }
                                                    }
                                                }

                                                #endregion ResumenTransaccion
                                            }
                                            ResumenTransaccion.ModoInicial();
                                        }
                                        else
                                        {
                                            exito = true;
                                        }
                                    }

                                    //if (exito)
                                    //ModoInicial();
                                }
                                MostrarMensaje("Se ha guardado con éxito!", TipoMensaje.Success);
                            }
                            else
                            {
                                MostrarMensaje("Ya existe un perfil bajo el número de indentificación '" + txtIdentificacion.Text.Trim() + "'", TipoMensaje.Danger);
                            }
                            break;

                        case Generales.EstadoFormulario.Editar:
                            var exito1 = false;
                            nuevoCliente.IdCliente = Guid.Parse(hfIdCliente.Value);
                            nuevoCliente.EsActivo = true;
                            Session["idCliente"] = nuevoCliente.IdCliente;

                            if (exito1 = new ClienteBLL().Actualizar(nuevoCliente))
                            {
                                
                                if (nuevoDetalleCliente != null)
                                {
                                    nuevoDetalleCliente.IdCliente = nuevoCliente.IdCliente;

                                    if (DatosCliente.DetalleCliente.Count > 0)
                                    {
                                        nuevoDetalleCliente.IdDetalleCliente = DetalleClienteActual.IdDetalleCliente;
                                        exito1 = new DetalleClienteBLL().Actualizar(nuevoDetalleCliente);
                                    }
                                    else
                                    {
                                        exito1 = new DetalleClienteBLL().Insertar(nuevoDetalleCliente);
                                    }
                                }
                                if (exito1)
                                    MostrarMensaje("Se ha guardado con éxito!", TipoMensaje.Success);
                                else
                                    MostrarMensaje("No se pudo completar la accion!", TipoMensaje.Danger);

                                //ModoInicial();
                            }

                            break;
                    }
                    //MostrarMensaje("Se ha guardado con éxito!", TipoMensaje.Success);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo completar la accion!", TipoMensaje.Danger);
            }
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

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtAlias.Text = string.Empty;
            ddlSexo.ClearSelection();
            ddlEstado.ClearSelection();
            txtFechaNacimiento.Text = string.Empty;
            txtFechaPerfil.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtIngresos.Text = string.Empty;
            txtMiembrosFamilia.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
            txtOcupacion.Text = string.Empty;
            txtFechaEmision.Text = string.Empty;
            txtFechaVencimiento.Text = string.Empty;
            txtProfesion.Text = string.Empty;
            ddlPaisEmisor.ClearSelection();
            ddlEstadoCivil.ClearSelection();
            ddlPais.ClearSelection();
            Contactos.LimpiarSesion();
            Domicilio.LimpiarSesion();
            DatosNegocios.LimpiarSesion();
            ResumenTransaccion.LimpiarSesion();
            ReferenciasCrediticia.LimpiarSesion();
            ReferenciaPersonal.LimpiarSesion();
            Aprobacion.LimpiarSesion();
        }

        private void LlenarFormulario()
        {
            try
            {
                //Llenas datos generales clientes
                txtNombre.Text = DatosCliente.Nombres;
                txtApellidos.Text = DatosCliente.Apellidos;
                txtIdentificacion.Text = DatosCliente.NoIdentificacion;
                txtFechaPerfil.Text = DatosCliente.FechaPerfil.ToString("dd/MM/yyyy");
                ddlEstado.SelectedIndex = DatosCliente.EstadoPerfil == 1 ? 0 : 1;
                txtFechaEmision.Text = DatosCliente.FechaEmision != null ? ((DateTime)DatosCliente.FechaEmision).ToString("dd/MM/yyyy") : string.Empty;
                txtFechaVencimiento.Text = DatosCliente.FechaVencimiento != null ? ((DateTime)DatosCliente.FechaVencimiento).ToString("dd/MM/yyyy") : string.Empty;
                ddlPaisEmisor.SelectedValue = DatosCliente.IdPais != null ? DatosCliente.IdPais.ToString() : ddlPaisEmisor.SelectedValue;

                //Llenar formulario detalle Cliente
                if (DetalleClienteActual != null)
                {
                    ddlEstadoCivil.SelectedValue = DetalleClienteActual.EstadoCivil;
                    txtFechaNacimiento.Text = DetalleClienteActual.FechaNacimiento.ToString("dd/MM/yyyy");
                    ddlPais.SelectedIndex = ddlPais.Items.IndexOf(ddlPais.Items.FindByValue(DetalleClienteActual.IdPaisNacimiento.ToString()));
                    //DetalleClienteActual.IdPaisNacimiento != null ? DetalleClienteActual.IdPaisNacimiento.ToString() : ddlPais.SelectedValue;
                    //DatosCliente.IdPais != null ? DatosCliente.IdPais.ToString() : ddlPaisEmisor.SelectedValue;
                    txtNacionalidad.Text = DetalleClienteActual.Nacionalidad;
                    txtProfesion.Text = DetalleClienteActual.Profesion;
                    txtOcupacion.Text = DetalleClienteActual.Ocupacion;
                    txtMiembrosFamilia.Text = DetalleClienteActual.MiembrosFamilia != null ? DetalleClienteActual.MiembrosFamilia.ToString() : string.Empty;
                    txtIngresos.Text = DetalleClienteActual.Ingresos != null ? DetalleClienteActual.Ingresos.ToString() : string.Empty;
                    txtAlias.Text = DetalleClienteActual.Alias;
                    ddlSexo.SelectedValue = DetalleClienteActual.Sexo != null ? DetalleClienteActual.Sexo : ddlSexo.SelectedValue;
                }

                Session["idCliente"] = DatosCliente.IdCliente;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private Cliente LlenarObjetoCliente()
        {
            try
            {
                Cliente nuevoCliente = new Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nombres = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    NombreCompleto = txtNombre.Text.Trim() + " " + txtApellidos.Text.Trim(),
                    NoIdentificacion = txtIdentificacion.Text.Trim().ToUpper(),
                    FechaPerfil = DateTime.ParseExact(txtFechaPerfil.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    FechaEmision = txtFechaEmision.Text == "" ? DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(txtFechaEmision.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    FechaVencimiento = txtFechaVencimiento.Text == "" ? DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(txtFechaVencimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IdPais = ddlPaisEmisor.SelectedValue == "" ? Guid.Parse("AF62E549-7291-4456-9840-A21F4BC4449E") : Guid.Parse(ddlPaisEmisor.SelectedValue),
                    //ddlPais.SelectedIndex = ddlPais.Items.IndexOf(ddlPais.Items.FindByText("Nicaragua"));
                    //ddlPaisEmisor.SelectedIndex = ddlPaisEmisor.Items.IndexOf(ddlPaisEmisor.Items.FindByText("Nicaragua"));
                    EstadoPerfil = ddlEstado.SelectedItem.Text == "En Proceso" ? 1 : 2,
                    FechaRegistro = DateTime.Now,
                    UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                    UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
                };

                return nuevoCliente;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        private DetalleCliente LlenarObjetoDetalleCliente()
        {
            DetalleCliente nuevoDetalleCliente = null;
            try
            {
                nuevoDetalleCliente = new DetalleCliente()
                {
                    IdDetalleCliente = Guid.NewGuid(),
                    EstadoCivil = ddlEstadoCivil.SelectedValue,
                    FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IdPaisNacimiento = Guid.Parse(ddlPais.SelectedValue),
                    Nacionalidad = txtNacionalidad.Text,
                    Profesion = txtProfesion.Text,
                    Ocupacion = txtOcupacion.Text,
                    MiembrosFamilia = Convert.ToInt32(txtMiembrosFamilia.Text),
                    Ingresos = Convert.ToDouble(txtIngresos.Text),
                    Alias = txtAlias.Text.Trim(),
                    Sexo = ddlSexo.SelectedValue,
                    EsActivo = true,
                    FechaRegistro = DateTime.Now,
                    UserIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                    UserPC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
                };
            }
            catch (Exception ex)
            {
            }

            return nuevoDetalleCliente;
        }

        private void CargarCombos()
        {
            //ddlProfesion.SelectedIndex = -1; //Limpia cualquier selección
            //ddlProfesion.DataSource = new ProfesionBLL().Listar();
            //ddlProfesion.DataTextField = "Nombre";
            //ddlProfesion.DataValueField = "IdProfesion";
            //ddlProfesion.DataBind();

            ddlPais.SelectedIndex = -1; //Limpia cualquier selección
            ddlPais.DataSource = new PaisBLL().Listar();
            ddlPais.DataTextField = "Nombre";
            ddlPais.DataValueField = "IdPais";
            ddlPais.DataBind();

            ddlPaisEmisor.SelectedIndex = -1; //Limpia cualquier selección
            ddlPaisEmisor.DataSource = new PaisBLL().Listar();
            ddlPaisEmisor.DataTextField = "Nombre";
            ddlPaisEmisor.DataValueField = "IdPais";
            ddlPaisEmisor.DataBind();
        }

        private void LimpiarSesion()
        {
            Session.Remove("idCliente");
        }

        #endregion Otros métodos

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfIdentificacion.Value))
            {
                var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                if (cliente.Count > 0)
                {
                    hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                    txtIdentificacion.Text = hfIdentificacion.Value;
                    ModoEditar();

                    Domicilio.ModoInicial();
                    Contactos.ModoInicial();
                    ReferenciaPersonal.ModoInicial();
                    ReferenciasCrediticia.ModoInicial();
                    DatosNegocios.ModoInicial();
                    Aprobacion.ModoInicial();
                    ResumenTransaccion.ModoInicial();
                    ImportacionActivada = false; // Indica que no se ha utilizado la importacion
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }

                hfIdentificacion.Value = string.Empty;
            }
        }

        #endregion Métodos
    }
}