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
    
    public partial class Referencias
    {
        public System.Guid IdReferencia { get; set; }
        public System.Guid IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string NoIdentificacion { get; set; }
        public string Profesion { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string Tiempo { get; set; }
        public string CentroLaboral { get; set; }
        public string Direccion { get; set; }
        public bool EsActivo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string UserPC { get; set; }
        public string UserIP { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
