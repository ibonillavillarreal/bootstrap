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
    
    public partial class Domicilio
    {
        public System.Guid IdDomicilio { get; set; }
        public System.Guid IdCliente { get; set; }
        public string Descripcion { get; set; }
        public bool EsAlquilada { get; set; }
        public bool EsPropia { get; set; }
        public bool Familiar { get; set; }
        public int TiempoResidir { get; set; }
        public bool EsActivo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string UserPC { get; set; }
        public string UserIP { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}