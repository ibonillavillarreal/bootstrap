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
    
    public partial class tClienteCuenta
    {
        public tClienteCuenta()
        {
            this.tPlastico = new HashSet<tPlastico>();
        }
    
        public System.Guid IdCuenta { get; set; }
        public Nullable<System.Guid> IdCliente { get; set; }
        public Nullable<System.Guid> IdAprobado { get; set; }
        public Nullable<System.Guid> IdTipoCuenta { get; set; }
        public string NoCuenta { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public Nullable<double> Limite { get; set; }
        public Nullable<System.Guid> IdEstadoCuenta { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<bool> EsActivo { get; set; }
        public string Usuario { get; set; }
        public string NombrePC { get; set; }
        public string DireccionIP { get; set; }
        public Nullable<System.Guid> IdEmpresa { get; set; }
        public Nullable<System.Guid> IdPromotor { get; set; }
        public Nullable<System.Guid> IdColector { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual tEstadoCuentas tEstadoCuentas { get; set; }
        public virtual ICollection<tPlastico> tPlastico { get; set; }
        public virtual tTipoCuenta tTipoCuenta { get; set; }
        public virtual Empresas Empresas { get; set; }
    }
}
