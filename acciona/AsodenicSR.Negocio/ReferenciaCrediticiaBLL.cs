using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ReferenciaCrediticiaBLL
    {
        public bool Insertar(ReferenciaCrediticia entidad)
        {
            return new ReferenciaCrediticiaDAO().Insertar(entidad);
        }

        public bool Actualizar(ReferenciaCrediticia entidad)
        {
            return new ReferenciaCrediticiaDAO().Actualizar(entidad);
        }

        public bool Eliminar(ReferenciaCrediticia entidad)
        {
            return new ReferenciaCrediticiaDAO().Eliminar(entidad);
        }

        public ReferenciaCrediticia CopiarEntidad(ReferenciaCrediticia entidad)
        {
            return new ReferenciaCrediticiaDAO().CopiarEntidad(entidad);
        }

        public List<ReferenciaCrediticia> Listar()
        {
            return new ReferenciaCrediticiaDAO().Listar();
        }

        public ReferenciaCrediticia ObtenerPorIdReferenciaCrediticia(Guid idReferenciaCrediticia)
        {
            return new ReferenciaCrediticiaDAO().ObtenerPorIdReferenciaCrediticia(idReferenciaCrediticia);
        }

        public List<ReferenciaCrediticia> ObtenerPorNombre(string nombre)
        {
            return new ReferenciaCrediticiaDAO().ObtenerPorNombreBanco(nombre);
        }

        public List<ReferenciaCrediticia> ObtenerReferenciaCrediticiasPorIdCliente(Guid idCliente)
        {
            return new ReferenciaCrediticiaDAO().ObtenerReferenciaCrediticiasPorIdCliente(idCliente);
        }
    }
}