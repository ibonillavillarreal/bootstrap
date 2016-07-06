using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DestinoCreditoBLL
    {
        public bool Insertar(DestinoCredito entidad)
        {
            return new DestinoCreditoDAO().Insertar(entidad);
        }

        public bool Actualizar(DestinoCredito entidad)
        {
            return new DestinoCreditoDAO().Actualizar(entidad);
        }

        public bool Eliminar(DestinoCredito entidad)
        {
            return new DestinoCreditoDAO().Eliminar(entidad);
        }

        public DestinoCredito CopiarEntidad(DestinoCredito entidad)
        {
            return new DestinoCreditoDAO().CopiarEntidad(entidad);
        }

        public List<DestinoCredito> Listar()
        {
            return new DestinoCreditoDAO().Listar();
        }
        public List<DestinoCredito> ListarActivos()
        {
            return new DestinoCreditoDAO().ListarActivos();
        }

        public DestinoCredito ObtenerPorIdDestinoCredito(Guid idDestinoCredito)
        {
            return new DestinoCreditoDAO().ObtenerPorIdDestinoCredito(idDestinoCredito);
        }

        public List<DestinoCredito> ObtenerPorNombre(string nombre)
        {
            return new DestinoCreditoDAO().ObtenerPorNombre(nombre);
        }
    }
}
