using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acciona
{
    public class Generales
    {
        #region Constantes

        public static IFormatProvider cultura = new System.Globalization.CultureInfo("es-NI", true);

        #endregion

        #region Enumeraciones
        public enum EstadoFormulario
        {
            Inicial,
            Detalle,
            Agregar,
            Editar,
            Busqueda
        }

        #endregion
    }
}