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
    
    public partial class tTipoCuenta
    {
        public tTipoCuenta()
        {
            this.tClienteCuenta = new HashSet<tClienteCuenta>();
        }
    
        public System.Guid IdTipoCuenta { get; set; }
        public string Descripcion { get; set; }
        public string Prefijo { get; set; }
        public string Numero { get; set; }
        public string PrefijoPlastico { get; set; }
        public string ConsecutivoPlastico { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<bool> EsActivo { get; set; }
        public string Usuario { get; set; }
        public string NombrePC { get; set; }
        public string DireccionIP { get; set; }
    
        public virtual ICollection<tClienteCuenta> tClienteCuenta { get; set; }
    }
}
