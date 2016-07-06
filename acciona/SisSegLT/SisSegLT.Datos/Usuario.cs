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
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.AprobacionInstitucion = new HashSet<AprobacionInstitucion>();
            this.ClienteEvaluacion = new HashSet<ClienteEvaluacion>();
            this.UsuarioRol = new HashSet<UsuarioRol>();
        }
    
        public System.Guid IdUsuario { get; set; }
        public System.Guid IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Cargo { get; set; }
        public string Codigo { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public bool EsActivo { get; set; }
    
        public virtual ICollection<AprobacionInstitucion> AprobacionInstitucion { get; set; }
        public virtual ICollection<ClienteEvaluacion> ClienteEvaluacion { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}