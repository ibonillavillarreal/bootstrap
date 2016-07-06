using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class ProveedoresBLL
    {
        public bool Insertar(NegocioProveedores entidad)
        {
            return new ProveedoresDAO().Insertar(entidad);
        }

        public bool Actualizar(NegocioProveedores entidad)
        {
            return new ProveedoresDAO().Actualizar(entidad);
        }

        public bool Eliminar(NegocioProveedores entidad)
        {
            return new ProveedoresDAO().Eliminar(entidad);
        }

        public NegocioProveedores CopiarEntidad(NegocioProveedores entidad)
        {
            return new ProveedoresDAO().CopiarEntidad(entidad);
        }

        public List<NegocioProveedores> Listar()
        {
            return new ProveedoresDAO().Listar();
        }

        public NegocioProveedores ObtenerPorIdNegocioProveedores(Guid idNegocioProveedores)
        {
            return new ProveedoresDAO().ObtenerPorIdNegocioProveedores(idNegocioProveedores);
        }

        public List<NegocioProveedores> ObtenerPorNombre(string nombre)
        {
            return new ProveedoresDAO().ObtenerPorNombre(nombre);
        }

        public List<NegocioProveedores> ObtenerNegocioProveedoresPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return new ProveedoresDAO().ObtenerNegocioProveedoresPorIdDatosNegocio(idDatosNegocio);
        }
    }
}
