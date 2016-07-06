using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ResumenTransaccionBLL
    {
        public bool Insertar(ResumenTransaccion entidad)
        {
            return new ResumenTransaccionDAO().Insertar(entidad);
        }

        public bool Actualizar(ResumenTransaccion entidad)
        {
            return new ResumenTransaccionDAO().Actualizar(entidad);
        }

        public bool Eliminar(ResumenTransaccion entidad)
        {
            return new ResumenTransaccionDAO().Eliminar(entidad);
        }

        public ResumenTransaccion CopiarEntidad(ResumenTransaccion entidad)
        {
            return new ResumenTransaccionDAO().CopiarEntidad(entidad);
        }

        public List<ResumenTransaccion> Listar()
        {
            return new ResumenTransaccionDAO().Listar();
        }

        public ResumenTransaccion ObtenerPorIdResumenTransaccion(Guid idResumenTransaccion)
        {
            return new ResumenTransaccionDAO().ObtenerPorIdResumenTransaccion(idResumenTransaccion);
        }

        public List<ResumenTransaccion> ObtenerPorNombre(string nombre)
        {
            return new ResumenTransaccionDAO().ObtenerPorNombre(nombre);
        }

        public List<ResumenTransaccion> ObtenerResumenTransaccionPorIdCliente(Guid idCliente)
        {
            return new ResumenTransaccionDAO().ObtenerResumenTransaccionPorIdCliente(idCliente);
        }

        public List<vwResumenTransacciones> ObtenervwResumenTransaccionPorIdCliente(Guid idCliente)
        {
            return new ResumenTransaccionDAO().ObtenervwResumenTransaccionPorIdCliente(idCliente);
        }
    }
}