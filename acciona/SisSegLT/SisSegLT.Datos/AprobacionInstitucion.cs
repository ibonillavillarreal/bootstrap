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
    
    public partial class AprobacionInstitucion
    {
        public System.Guid IdAprobacionInstitucion { get; set; }
        public System.Guid IdCliente { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.Guid> IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaHoraVerificacion { get; set; }
        public string NivelRiesgo { get; set; }
        public Nullable<bool> EsActivo { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string UsuarioPC { get; set; }
        public string UsuarioIP { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}
