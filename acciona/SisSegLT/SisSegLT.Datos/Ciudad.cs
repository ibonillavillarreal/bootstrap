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
    
    public partial class Ciudad
    {
        public Ciudad()
        {
            this.Sucursal = new HashSet<Sucursal>();
        }
    
        public System.Guid IdCiudad { get; set; }
        public System.Guid IdPais { get; set; }
        public string Nombre { get; set; }
        public bool EsActivo { get; set; }
    
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }
    }
}
