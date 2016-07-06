using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    internal class NavigationProperties
    {
    }

    //public partial class Contacto
    //{
    //    //public override System.String ToString()
    //    //{
    //    //    return (this.TipoContacto);
    //    //}
    //}

    public partial class Referencias
    {
        public override System.String ToString()
        {
            return (this.Profesion);
        }
    }
}