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
    
    public partial class Reversion
    {
        public System.Guid IdReversion { get; set; }
        public Nullable<System.Guid> IdCuenta { get; set; }
        public string Serie { get; set; }
        public string NoReferencia { get; set; }
        public Nullable<System.DateTime> FechaRecibo { get; set; }
        public Nullable<double> Monto { get; set; }
        public string MotivoReversion { get; set; }
        public Nullable<System.DateTime> FechaReversion { get; set; }
        public string Usuario { get; set; }
        public string NombrePC { get; set; }
        public string DireccionIP { get; set; }
    }
}
