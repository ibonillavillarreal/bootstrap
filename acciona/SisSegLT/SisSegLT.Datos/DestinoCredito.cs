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
    
    public partial class DestinoCredito
    {
        public DestinoCredito()
        {
            this.DatosNegocio = new HashSet<DatosNegocio>();
        }
    
        public System.Guid IdDestinoCredito { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> EsActivo { get; set; }
    
        public virtual ICollection<DatosNegocio> DatosNegocio { get; set; }
    }
}