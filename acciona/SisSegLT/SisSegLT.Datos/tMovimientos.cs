//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SisSegLT.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tMovimientos
    {
        public tMovimientos()
        {
            this.tMovimientos1 = new HashSet<tMovimientos>();
            this.CargosMensuales = new HashSet<CargosMensuales>();
            this.tCuotas = new HashSet<tCuotas>();
        }
    
        public System.Guid IdMovimiento { get; set; }
        public Nullable<System.Guid> IdMovimientoPadre { get; set; }
        public System.Guid IdPlastico { get; set; }
        public Nullable<System.Guid> IdPromotor { get; set; }
        public Nullable<System.Guid> IdColector { get; set; }
        public System.Guid IdOrigen { get; set; }
        public Nullable<System.Guid> IdDesembolsa { get; set; }
        public System.Guid IdDetalleCheque { get; set; }
        public Nullable<System.Guid> IdVehiculo { get; set; }
        public string NoMovimiento { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaEfectiva { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
        public Nullable<System.Guid> IdTipoTransaccion { get; set; }
        public Nullable<bool> Flujo { get; set; }
        public Nullable<int> NoCuotas { get; set; }
        public Nullable<System.Guid> IdFrecuencia { get; set; }
        public Nullable<double> MontoTransaccion { get; set; }
        public Nullable<double> Salvamento { get; set; }
        public Nullable<double> Canon { get; set; }
        public Nullable<double> Abono { get; set; }
        public Nullable<double> Saldo { get; set; }
        public Nullable<double> PlazoAnios { get; set; }
        public Nullable<double> CuotaProgramada { get; set; }
        public Nullable<double> PlazoMeses { get; set; }
        public Nullable<double> Interes { get; set; }
        public Nullable<System.DateTime> FechaCorte { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public string TipoInteres { get; set; }
        public string EstadoTransaccion { get; set; }
        public string Observaciones { get; set; }
        public string Moneda { get; set; }
        public string Usuario { get; set; }
        public string DireccionIP { get; set; }
        public string NombrePC { get; set; }
    
        public virtual InformacionCheque InformacionCheque { get; set; }
        public virtual OrigenFondos OrigenFondos { get; set; }
        public virtual tFrecuencia tFrecuencia { get; set; }
        public virtual TipoTransaccion TipoTransaccion { get; set; }
        public virtual ICollection<tMovimientos> tMovimientos1 { get; set; }
        public virtual tMovimientos tMovimientos2 { get; set; }
        public virtual tPlastico tPlastico { get; set; }
        public virtual tVehiculos tVehiculos { get; set; }
        public virtual ICollection<CargosMensuales> CargosMensuales { get; set; }
        public virtual ICollection<tCuotas> tCuotas { get; set; }
    }
}
