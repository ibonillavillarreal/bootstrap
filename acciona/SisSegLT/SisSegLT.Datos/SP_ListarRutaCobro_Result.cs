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
    
    public partial class SP_ListarRutaCobro_Result
    {
        public System.Guid IdCobro { get; set; }
        public Nullable<System.Guid> IdRutaCobro { get; set; }
        public Nullable<System.Guid> IdCuenta { get; set; }
        public string NombreCompleto { get; set; }
        public string NoIdentificacion { get; set; }
        public string NoCuenta { get; set; }
        public string Direccion { get; set; }
        public string Frecuencia { get; set; }
        public Nullable<double> SaldoTotal { get; set; }
        public Nullable<double> CuotadelDia { get; set; }
        public Nullable<double> Mora { get; set; }
        public Nullable<double> CuotasPendientes { get; set; }
        public Nullable<double> CuotaIdeal { get; set; }
        public string Colector { get; set; }
        public Nullable<double> MontoRecibido { get; set; }
        public string NoRecibo { get; set; }
        public Nullable<double> Diferencia { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> FechaCobro { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string DireccionIP { get; set; }
        public string NombrePC { get; set; }
    }
}
