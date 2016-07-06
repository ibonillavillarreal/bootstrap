using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class vwPerfilIngresado
    {
        public string Cedula { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string Sucursal { get; set; }

        public string Promotor { get; set; }

        public string Metodologia { get; set; }

        public Nullable<int> Total { get; set; }
    }
}