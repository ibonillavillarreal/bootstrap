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
using System.IO;
using Aspose.Words;

namespace Acciona.Credito
{
    public partial class MovimientosPyme : Pagina
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

        public tMovimientos MovimientoActual
        {
            get
            {
                if (Session["MovimientoActuales"] == null)
                    Session["MovimientoActuales"] = new tMovimientos();
                return (tMovimientos)Session["MovimientoActuales"];
            }
            set
            {
                Session["MovimientoActuales"] = value;
            }
        }

        public tMovimientos MovimientoCanon
        {
            get
            {
                if (Session["MovimientosCanon"] == null)
                    Session["MovimientosCanon"] = new tMovimientos();
                return (tMovimientos)Session["MovimientosCanon"];
            }
            set
            {
                Session["MovimientosCanon"] = value;
            }
        }

        public tMovimientos MovimientoSalvamento
        {
            get
            {
                if (Session["MovimientosSalvamento"] == null)
                    Session["MovimientosSalvamento"] = new tMovimientos();
                return (tMovimientos)Session["MovimientosSalvamento"];
            }
            set
            {
                Session["MovimientosSalvamento"] = value;
            }
        }

        public tVehiculos VehiculoActual
        {
            get
            {
                if (Session["VehiculoActuales"] == null)
                    Session["VehiculoActuales"] = new tVehiculos();
                return (tVehiculos)Session["VehiculoActuales"];
            }
            set
            {
                Session["VehiculoActuales"] = value;
            }
        }

        public InformacionCheque DesembolsoActual
        {
            get
            {
                if (Session["DesembolsoActuales"] == null)
                    Session["DesembolsoActuales"] = new InformacionCheque();
                return (InformacionCheque)Session["DesembolsoActuales"];
            }
            set
            {
                Session["DesembolsoActuales"] = value;
            }
        }

        public tClienteCuenta ClienteCuentaActual
        {
            get
            {
                if (Session["ClienteCuentaActuales"] == null)
                    Session["ClienteCuentaActuales"] = new tClienteCuenta();
                return (tClienteCuenta)Session["ClienteCuentaActuales"];
            }
            set
            {
                Session["ClienteCuentaActuales"] = value;
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
                CargarCombo();
                ModoInicial();

            }
        }

        #endregion Otros eventos

        #region Botones de acción

        //protected void imbAgregar_Click(object sender, ImageClickEventArgs e)
        //{
        //    ModoAgregar();
        //}

        //protected void imbEditar_Click(object sender, ImageClickEventArgs e)
        //{
        //    ModoEditar();
        //}

        //protected void imbEliminar_Click(object sender, ImageClickEventArgs e)
        //{
        //    ModoEliminar();
        //}

        protected void imbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            ModoInicial();
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();

        }
        //protected void imbImprimir_Click(object sender, ImageClickEventArgs e)
        //{
        //    Button b = sender as Button;
        //    if (b != null)
        //    {
        //        LitNoCuenta.Text = "clicked!!";
        //    }
        //}


        //protected void btn_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton btna = sender as ImageButton;
        //    GridViewRow row = (GridViewRow)btna.NamingContainer;
        //    var indice = gvDatos.DataKeys[row.RowIndex];


        //    if (indice != null)
        //    {
        //        Cuenta = new ClienteCuentaBLL().ObtenerPorIdCuenta(Guid.Parse(indice.Value.ToString()));
        //        if (new ClienteCuentaBLL().Eliminar(Cuenta))
        //        {
        //            ModoInicial();
        //        }
        //    }
        //}
        protected void btImprimir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Reportes/Crystal/Reporte.aspx?idmovimiento=" + Session["MovimientoId"].ToString(), true);

            // For complete examples and data files, please go to https://github.com/asposewords/Aspose_Words_NET
            // The path to the documents directory.
            string dataDir = "C:\\";
            string fileName = "CONTRATO DE LEASING.docx";

            // Open the stream. Read only access is enough for Aspose.Words to load a document.
            Stream stream = File.OpenRead(dataDir + fileName);

            // Load the entire document into memory.
            Document doc = new Document(stream);

            // You can close the stream now, it is no longer needed because the document is in memory.
            stream.Close();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            hfIdCliente.Value = string.Empty;
            if (!String.IsNullOrEmpty(txtBuscar.Text))
            {
                var cliente = new ClienteBLL().ObtenerPorIdentificacion(txtBuscar.Text);
                if (cliente.Count > 0)
                {
                    if (cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().tPlastico.Count == 0)
                    {
                        MostrarMensaje("Este cliente no posee un plastico activo asociado!", TipoMensaje.Danger);
                        return;
                    }

                    hfIdCliente.Value = cliente.FirstOrDefault().IdCliente.ToString();
                    hfIdCuenta.Value = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdCuenta.ToString();
                    //LitNoCuenta.Text = "Cuenta #" + ": " + cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().NoCuenta.ToString();
                    ClienteCuentaActual = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault();

                    txtNombre.Text = cliente.FirstOrDefault().NombreCompleto;
                    txtIdentificacion.Text = cliente.FirstOrDefault().NoIdentificacion;
                    txtDisponible.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                    txtMontoAutorizado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                    //txtMontoEntregado.Text = string.Format("{0:0,0.00}", Convert.ToDouble(cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().Limite));
                    ddlAutorizante.SelectedValue = cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado != null ? cliente.FirstOrDefault().tClienteCuenta.FirstOrDefault().IdAprobado.ToString() : ddlAutorizante.SelectedValue;

                    //Combo plastico
                    ddlPlastico.SelectedIndex = -1; //Limpia cualquier selección
                    ddlPlastico.DataSource = new PlasticoBLL().ObtenerTarjetaPorIdCliente(Guid.Parse(hfIdCliente.Value));
                    ddlPlastico.DataTextField = "NoTarjeta";
                    ddlPlastico.DataValueField = "IdPlastico";
                    ddlPlastico.DataBind();

                    ModoInicial();
                }
                else
                {
                    MostrarMensaje("No se encontraron resultados.", TipoMensaje.Info);
                }
            }
        }

        protected void btnCuota_Click(object sender, EventArgs e)
        {
            double monto = Convert.ToDouble(txtMonto2.Text);
            double interesi = Convert.ToDouble(txtInteres.Text);
            double plazo = Convert.ToDouble(12);
            double plazoanios = Convert.ToDouble(txtPlazoAnios.Text) / 12;


            interesi = interesi / 100;

            var tip = interesi * 12;
            var tip2 = (tip * plazoanios) / Convert.ToDouble(txtNoCuotas.Text);
            var i = tip2.ToString("#.###");
            double interes = CalcularCuota(monto, Convert.ToDouble(i), 12, plazoanios);
            txtCuota.Text = interes.ToString("#.##");

        }
        #endregion Botones de acción
        #endregion Eventos

        #region Métodos

        #region Modos

        /// <summary>
        /// Calcula el valor de la cuota para un crédito
        /// </summary>
        /// <param name="montoFinanciar">Monto real a financiar, obviando la prima</param>
        /// <param name="tasaInteresPeriodo">Tasa de interés para cada período (decimales)</param>
        /// <param name="cantCuotasPorAnio">Cantidad de cuotas o plazo del crédito</param>
        /// <returns>Valor calculado de la cuota mensual</returns>
        public static double CalcularCuota(double montoFinanciar, double tasaInteresPeriodo, double cantCuotasPorAnio, double plazoAnio)
        {
            double valor = 0;

            if (tasaInteresPeriodo > 0)
            {
                valor = montoFinanciar * (tasaInteresPeriodo / (1 - (Math.Pow(1 + tasaInteresPeriodo, ((cantCuotasPorAnio * plazoAnio) * -1)))));
            }
            else
            {
                valor = montoFinanciar / (cantCuotasPorAnio * plazoAnio);
            }
            return valor;
        }

        public void CargarDatosGenerales()
        {

        }

        public void ModoInicial()
        {
            EstadoFormulario = Generales.EstadoFormulario.Inicial;
            //litAyuda.Text = "Agregue, edite o deshabilite sucursales";

            txtMonto2.Text = "0";
            txtPrima.Text = "0";
            txtSalvamento.Text = "0";

            if (hfIdCliente.Value != string.Empty || hfIdCliente.Value != "")
            {
                //gvDatos.Visible = true;
                //gvDatos.SelectedIndex = -1;
                var dsCuentas = new ClienteCuentaBLL().ObtenerCuentaPorIdCliente(Guid.Parse(hfIdCliente.Value.ToString()));
                //gvDatos.DataSource = dsCuentas;
                //gvDatos.DataBind();
            }
        }

        //private void ModoAgregar()
        //{
        //    EstadoFormulario = Generales.EstadoFormulario.Agregar;

        //    MovimientoActual = new tMovimientos();


        //    LimpiarControles();
        //    CargarCombo();

        //}

        //private void ModoEditar()
        //{
        //    EstadoFormulario = Generales.EstadoFormulario.Editar;


        //    Guid dataKey = Guid.Parse(hfIdMovimiento.Value);
        //    if (dataKey != null)
        //    {
        //        MovimientoActual = new MovimientoBLL().ObtenerPorIdMovimiento(dataKey);  

        //        LimpiarControles();
        //        CargarCombo();
        //        LlenarFormulario();
        //    }

        //}



        #endregion Modos

        #region Otros métodos

        private void GuardarSobreSaldo()
        {
            try
            {
                Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                if (!string.IsNullOrEmpty(txtMonto.Text.Trim()))
                {
                    tMovimientos nuevoMovimiento = LlenarObjetoSobreSaldo();
                    InformacionCheque nuevoDesembolso = LlenarObjetoDesembolso();
                    switch (EstadoFormulario)
                    {
                        case Generales.EstadoFormulario.Inicial:
                            nuevoMovimiento.IdMovimiento = Guid.NewGuid();
                            nuevoMovimiento.FechaRegistro = DateTime.Now;
                            nuevoMovimiento.Usuario = user.Login;
                            if (new MovimientoBLL().Insertar(nuevoMovimiento))
                            {
                                if (new MovimientoBLL().GenerarCuotasNuevas(nuevoMovimiento, nuevoMovimiento.IdMovimiento))
                                {   //lblNoMovimiento.Text = nuevoMovimiento.NoMovimiento;
                                    Session["MovimientoId"] = nuevoMovimiento.IdMovimiento;
                                    MostrarMensaje("El registro se ha agregado con exito! Numero de Movimiento: " + nuevoMovimiento.NoMovimiento + "", TipoMensaje.Success);
                                    LimpiarSesion();
                                }
                            }
                            else
                                MostrarMensaje("No se pudo guardar el movimiento!", TipoMensaje.Success);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private tMovimientos LlenarObjetoSobreSaldo()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tMovimientos movimientoNuevo = new tMovimientos()
            {
                IdMovimientoPadre = null,
                IdPlastico = Guid.Parse(ddlPlastico.SelectedValue),
                IdPromotor = Guid.Parse(ddlPromotor.SelectedValue),
                IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue),
                NoMovimiento = NoMovimientoMaximo(),
                FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaVencimiento = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(Convert.ToInt16(txtNoCuotas.Text)),
                IdColector = Guid.Parse(ddlColector.SelectedValue),
                IdTipoTransaccion = Guid.Parse(ddlTipoTransaccion.SelectedValue),
                Flujo = true,
                FechaCorte = DateTime.ParseExact(txtFechaCorte.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                NoCuotas = Convert.ToInt16(txtNoCuotas.Text),
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                IdOrigen = Guid.Parse(ddlOrigen.SelectedValue),
                MontoTransaccion = double.Parse(txtMonto2.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Canon = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Salvamento = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                CuotaProgramada = double.Parse(txtCuota.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoMeses = double.Parse(txtNoCuotas.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Saldo = double.Parse(txtMonto2.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoAnios = double.Parse(txtPlazoAnios.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //Saldo = double.Parse("0", System.Globalization.NumberStyles.Currency, Generales.cultura),
                Interes = double.Parse(txtInteres.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Abono = 0,
                TipoInteres = ddlTipoInteres.SelectedItem.ToString(),
                EstadoTransaccion = "Activa",
                Moneda = "Dolares",
                Observaciones = txtComentarios.Text,
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            MovimientoActual = movimientoNuevo;

            return MovimientoActual;
        }

        /// <summary>
        /// Guarda transacciones para creditos de leasing con valor de salvamento y canon extraordinario
        /// </summary>
        private void Guardar()
        {
            try
            {
                if (Convert.ToDouble(txtMonto2.Text) <= 0)
                {
                    MostrarMensaje("El monto a financiar debe ser mayor a cero!", TipoMensaje.Danger);
                    return;
                }



                var suma = Convert.ToDouble(txtMonto2.Text) + Convert.ToDouble(txtPrima.Text) + Convert.ToDouble(txtSalvamento.Text) - Convert.ToDouble(txtMonto.Text);
                if (suma != 0)
                    MostrarMensaje("No se pudo guardar el movimiento. Verifique los montos!", TipoMensaje.Danger);
                else
                {

                    Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;

                    

                    if (!string.IsNullOrEmpty(txtMonto.Text.Trim()))
                    {
                        InformacionCheque nuevoDesembolso = LlenarObjetoDesembolso();

                        tMovimientos nuevoMovimiento = LlenarObjetoSobreSaldo();

                        //validacion para agregar vehiculo
                        if (!string.IsNullOrEmpty(txtCodCarro.Text) && !string.IsNullOrEmpty(txtMarca.Text) && !string.IsNullOrEmpty(txtModelo.Text))
                        {
                            tVehiculos nuevoVehiculo = LlenarObjetoVehiculo();
                            bool exitoveh = new VehiculoBLL().Insertar(nuevoVehiculo); // guardar vehiculo
                            if (exitoveh)
                                nuevoMovimiento.IdVehiculo = nuevoVehiculo.IdVehiculo;
                        }

                        switch (EstadoFormulario)
                        {
                            case Generales.EstadoFormulario.Inicial:
                                nuevoMovimiento.IdMovimiento = Guid.NewGuid();
                                nuevoMovimiento.FechaRegistro = DateTime.Now;
                                nuevoMovimiento.Usuario = user.Login;


                                tMovimientos nuevoCanon = LlenarObjetoCanon();
                                tMovimientos nuevoSalvamento = LlenarObjetoSalvamento();

                                if (string.IsNullOrEmpty(nuevoDesembolso.NoCheque))
                                {
                                    MostrarMensaje("Por favor verifique los datos de desembolso!", TipoMensaje.Success);
                                    return;
                                }

                                if (new DetalleDesembolsoBLL().Insertar(nuevoDesembolso))  //guardar datos de desembolso
                                {
                                    nuevoMovimiento.IdDetalleCheque = nuevoDesembolso.IdDetalleCheque;
                                    //nuevoMovimiento.InformacionCheque = nuevoDesembolso;
                                    if (new MovimientoBLL().Insertar(nuevoMovimiento))   //guardar movimiento principal
                                    {
                                        nuevoCanon.IdDetalleCheque = nuevoDesembolso.IdDetalleCheque;
                                        //nuevoCanon.IdMovimientoPadre = nuevoMovimiento.IdMovimientoPadre;
                                        //nuevoCanon.InformacionCheque = nuevoDesembolso;

                                        //verificar si monto canon es mayor a cero  gurdar transaccion
                                        if (nuevoCanon.MontoTransaccion > 0)
                                        {

                                            if (new MovimientoBLL().Insertar(nuevoCanon))    // guardar movimiento canon
                                            {
                                                if (nuevoMovimiento.Canon != null || nuevoMovimiento.Canon > 0)
                                                {
                                                    bool exito = new MovimientoBLL().GenerarCuotaPrima(nuevoCanon, nuevoCanon.IdMovimiento); //generar cuota unica de canon

                                                }
                                            }
                                            else
                                                MostrarMensaje("No se pudo guardar el movimiento!", TipoMensaje.Success);

                                        }
                                        nuevoSalvamento.IdDetalleCheque = nuevoDesembolso.IdDetalleCheque;
                                        //nuevoSalvamento.IdMovimientoPadre = nuevoMovimiento.IdMovimientoPadre;
                                        //nuevoSalvamento.InformacionCheque = nuevoDesembolso;
                                        //nuevoSalvamento.IdDetalleCheque = nuevoDesembolso.IdDetalleCheque;

                                        // si monto salvamento es mayor a cero guardar transaccion
                                        if (nuevoSalvamento.MontoTransaccion > 0)
                                        {
                                            if (!new MovimientoBLL().Insertar(nuevoSalvamento)) //movimiento valor de salvamento
                                            {
                                                MostrarMensaje("No se pudo guardar el movimiento de salvamento!", TipoMensaje.Danger);
                                            }

                                        }

                                        Session["MovimientoId"] = nuevoMovimiento.IdMovimiento;
                                        MostrarMensaje("El registro se ha agregado con exito! Numero de Movimiento: " + nuevoMovimiento.NoMovimiento + "", TipoMensaje.Success);
                                        LimpiarSesion();

                                    }
                                    else
                                        MostrarMensaje("No se pudo guardar el movimiento principal!", TipoMensaje.Danger);
                                }
                                else
                                    MostrarMensaje("No se pudo guardar el detalle de desembolso!", TipoMensaje.Danger);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString(), TipoMensaje.Danger);
            }

        }

        private InformacionCheque LlenarObjetoDesembolso()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            InformacionCheque nuevoDesembolso = new InformacionCheque()
            {
                IdDetalleCheque = Guid.NewGuid(),
                NoCheque = txtNoCheque.Text.Trim(),
                Banco = ddlBanco.SelectedItem.ToString(),
                TipoDocumento = ddlTipoDocumento.SelectedItem.ToString(),
                FechaRegistro = DateTime.Now,
                Moneda = "Dolares",
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                DireccionMAC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            DesembolsoActual = nuevoDesembolso;

            return DesembolsoActual;
        }

        private tVehiculos LlenarObjetoVehiculo()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tVehiculos nuevoVehiculo = new tVehiculos()
            {
                IdVehiculo = Guid.NewGuid(),
                Codigo = txtCodCarro.Text.Trim(),
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Placa = txtPlaca.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Anio = txtAnio.Text.Trim(),
                Tipo = txtTipoVehiculo.Text.Trim(),
                Chasis = txtChasis.Text.Trim(),
                Motor = txtMotor.Text.Trim(),
                Circulacion = txtCirculacion.Text.Trim(),
                Usuario = user.Login,
                FechaRegistro = DateTime.Now,
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request)
            };

            VehiculoActual = nuevoVehiculo;

            return VehiculoActual;
        }

        private tMovimientos LlenarObjetoCanon()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");                       
            tMovimientos movimientoNuevo = new tMovimientos()
            {
                IdMovimiento = Guid.NewGuid(),
                IdMovimientoPadre = MovimientoActual.IdMovimiento,
                IdPlastico = Guid.Parse(ddlPlastico.SelectedValue),
                IdPromotor = Guid.Parse(ddlPromotor.SelectedValue),
                IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue),
                NoMovimiento = MovimientoActual.NoMovimiento + "-0001",
                FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                IdColector = Guid.Parse(ddlColector.SelectedValue),
                IdTipoTransaccion = Guid.Parse("A21DC37F-718C-4AE2-BAC5-4E732966C4AC"), //canon extraordinario
                Flujo = false,
                //FechaCorte = DateTime.ParseExact(txtFechaCorte.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                NoCuotas = 1,
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                IdOrigen = Guid.Parse(ddlOrigen.SelectedValue),
                MontoTransaccion = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //Canon = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //Salvamento = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                CuotaProgramada = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoMeses = double.Parse("0", System.Globalization.NumberStyles.Currency, Generales.cultura),
                Saldo = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoAnios = double.Parse("0", System.Globalization.NumberStyles.Currency, Generales.cultura),
                //PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                FechaVencimiento = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Interes = double.Parse("0", System.Globalization.NumberStyles.Currency, Generales.cultura),
                TipoInteres = ddlTipoInteres.SelectedItem.ToString(),
                Abono = 0,
                EstadoTransaccion = "Activa",
                Moneda = "Dolares",
                Observaciones = txtComentarios.Text,
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                FechaRegistro = DateTime.Now,
                Usuario = user.Login
            };

            MovimientoCanon = movimientoNuevo;

            return MovimientoCanon;
        }

        private tMovimientos LlenarObjetoSalvamento()
        {
            Usuario user = ((SisSegLT.Seguridad.Pagina)Page).MiUsuario;
            //var cultureInfo = new System.Globalization.CultureInfo("es-NI");
            string cv = "";
            var i = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura) * 0.0125;
            if (i > 0)
                cv = i.ToString("#.###");
            else
                cv = "0";


            //string cv = i.ToString("#.###");

            tMovimientos movimientoNuevo = new tMovimientos()
            {
                IdMovimiento = Guid.NewGuid(),
                IdMovimientoPadre = MovimientoActual.IdMovimiento,
                IdPlastico = Guid.Parse(ddlPlastico.SelectedValue),
                IdPromotor = Guid.Parse(ddlPromotor.SelectedValue),
                IdDesembolsa = Guid.Parse(ddlDesembolsa.SelectedValue),
                NoMovimiento = MovimientoActual.NoMovimiento + "-0002",
                FechaEfectiva = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaVencimiento = DateTime.ParseExact(txtFechaMovimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(Convert.ToInt16(txtNoCuotas.Text)),
                IdColector = Guid.Parse(ddlColector.SelectedValue),
                IdTipoTransaccion = Guid.Parse("13566452-4881-43C3-A01E-003F9681DDEB"), //valor salvamento
                Flujo = true,
                FechaCorte = DateTime.ParseExact(txtFechaCorte.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                NoCuotas = Convert.ToInt16(txtNoCuotas.Text),
                IdFrecuencia = Guid.Parse(ddlFrecuencia.SelectedValue),
                IdOrigen = Guid.Parse(ddlOrigen.SelectedValue),
                MontoTransaccion = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //Canon = double.Parse(txtPrima.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //Salvamento = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),

                CuotaProgramada = double.Parse(cv, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoMeses = double.Parse(txtNoCuotas.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Saldo = double.Parse(txtSalvamento.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                PlazoAnios = double.Parse(txtPlazoAnios.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                //PlazoMeses = double.Parse(txtPlazoMeses.Text, System.Globalization.NumberStyles.Currency, Generales.cultura),
                Interes = double.Parse("1.25", System.Globalization.NumberStyles.Currency, Generales.cultura),
                TipoInteres = ddlTipoInteres.SelectedItem.ToString(),
                Abono = 0,
                EstadoTransaccion = "Activa",
                Moneda = "Dolares",
                Observaciones = txtComentarios.Text,
                DireccionIP = MetodosExtensiones.ObtenerUsuarioIP(this.Page.Request),
                NombrePC = MetodosExtensiones.ObtenerUsuarioPC(this.Page.Request),
                FechaRegistro = DateTime.Now,
                Usuario = user.Login
            };

            MovimientoSalvamento = movimientoNuevo;

            return MovimientoSalvamento;
        }


        public string NoMovimientoMaximo()
        {

            string fmt = "000000";
            int consecutivo = new MovimientoBLL().ObtenerMaxMovimiento() + 1;
            string numero = consecutivo.ToString(fmt);

            return numero;

        }


        private void LimpiarControles()
        {
            ddlAutorizante.ClearSelection();
            txtNombre.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtDisponible.Text = string.Empty;
            txtMontoAutorizado.Text = string.Empty;

            txtMonto2.Text = string.Empty;
            txtSalvamento.Text = string.Empty;
            txtPrima.Text = string.Empty;

            txtMonto.Text = string.Empty;
            txtNoCuotas.Text = string.Empty;
            txtInteres.Text = string.Empty;
            txtFechaCorte.Text = string.Empty;
            txtPlazoAnios.Text = string.Empty;
            txtCuota.Text = string.Empty;
            txtComentarios.Text = string.Empty;
            txtFechaMovimiento.Text = string.Empty;

            txtCodCarro.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtAnio.Text = string.Empty;
            txtTipoVehiculo.Text = string.Empty;
            txtChasis.Text = string.Empty;
            txtMotor.Text = string.Empty;
            txtCirculacion.Text = string.Empty;


            ddlPromotor.ClearSelection();
            ddlFrecuencia.ClearSelection();
            //ddlDias.ClearSelection();
            ddlTipoInteres.ClearSelection();
            ddlColector.ClearSelection();
            ddlTipoTransaccion.ClearSelection();
            ddlDesembolsa.ClearSelection();
            ddlOrigen.ClearSelection();
        }

        private void LlenarFormulario()
        {
            //hfIdCliente.Value = Cuenta.IdCliente.ToString();
            //hfIdCuenta.Value = Cuenta.IdCuenta.ToString();
            //ddlAprobado.SelectedValue = Cuenta.IdAprobado != null ? Cuenta.IdAprobado.ToString() : ddlAprobado.SelectedValue;
            //ddlTipoCuenta.SelectedValue = Cuenta.IdTipoCuenta != null ? Cuenta.IdTipoCuenta.ToString() : ddlTipoCuenta.SelectedValue;
            //txtNoCuenta.Text = Cuenta.NoCuenta;
            //txtFechaAprobacion.Text = Cuenta.FechaAprobacion != null ? ((DateTime)Cuenta.FechaAprobacion).ToString("dd/MM/yyyy") : string.Empty;
            //txtLimite.Text = string.Format("{0:0,0.00}", Convert.ToDouble(Cuenta.Limite));
            //ddlEstadoCuenta.Text = Cuenta.IdEstadoCuenta != null ? Cuenta.IdEstadoCuenta.ToString() : ddlEstadoCuenta.SelectedValue;

        }

        private void CargarCombo()
        {
            //tipo de FRECUENCIA
            ddlFrecuencia.SelectedIndex = -1; //Limpia cualquier selección
            ddlFrecuencia.DataSource = new FrecuenciaBLL().ListarActivos();
            ddlFrecuencia.DataTextField = "Descripcion";
            ddlFrecuencia.DataValueField = "IdFrecuencia";
            ddlFrecuencia.DataBind();

            //Combo usuarios
            ddlAutorizante.SelectedIndex = -1; //Limpia cualquier selección
            ddlAutorizante.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Autorizante");
            ddlAutorizante.DataTextField = "Nombre";
            ddlAutorizante.DataValueField = "IdUsuario";
            ddlAutorizante.DataBind();

            //Combo desembolsador
            ddlDesembolsa.SelectedIndex = -1; //Limpia cualquier selección
            ddlDesembolsa.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Desembolsador");
            ddlDesembolsa.DataTextField = "Nombre";
            ddlDesembolsa.DataValueField = "IdUsuario";
            ddlDesembolsa.DataBind();

            //Combo promotor
            ddlPromotor.SelectedIndex = -1; //Limpia cualquier selección
            ddlPromotor.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Promotor");
            ddlPromotor.DataTextField = "Nombre";
            ddlPromotor.DataValueField = "IdUsuario";
            ddlPromotor.DataBind();

            //Combo colector
            ddlColector.SelectedIndex = -1; //Limpia cualquier selección
            ddlColector.DataSource = new UsuarioDAO().ObtenerUsuariosPorNombreRol("Cobro");
            ddlColector.DataTextField = "Nombre";
            ddlColector.DataValueField = "IdUsuario";
            ddlColector.DataBind();

            //Combo TipoTransaccion
            ddlTipoTransaccion.SelectedIndex = -1; //Limpia cualquier selección
            ddlTipoTransaccion.DataSource = new TipoTransaccionBLL().ObtenerListaTransacciones();
            ddlTipoTransaccion.DataTextField = "Descripcion";
            ddlTipoTransaccion.DataValueField = "IdTipoTransaccion";
            ddlTipoTransaccion.DataBind();

            //Combo colector
            ddlOrigen.SelectedIndex = -1; //Limpia cualquier selección
            ddlOrigen.DataSource = new MovimientoBLL().ListarOrigenFondos();
            ddlOrigen.DataTextField = "Descripcion";
            ddlOrigen.DataValueField = "IdOrigen";
            ddlOrigen.DataBind();

            

        }

        public void LimpiarSesion()
        {
            //Session.Remove("TipoCuentaActual");
            hfIdCliente.Value = string.Empty;
            hfIdCuenta.Value = string.Empty;
            hfIdMovimiento.Value = string.Empty;
            MovimientoActual = null;
            LimpiarControles();
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo)
        {
            litmensaje.Text = "";
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