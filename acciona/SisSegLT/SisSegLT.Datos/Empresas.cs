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
    
    public partial class Empresas
    {
        public Empresas()
        {
            this.tClienteCuenta = new HashSet<tClienteCuenta>();
        }
    
        public System.Guid IdEmpresa { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool EsActivo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string DireccionIP { get; set; }
        public string NombrePC { get; set; }
    
        public virtual ICollection<tClienteCuenta> tClienteCuenta { get; set; }
    }
}
