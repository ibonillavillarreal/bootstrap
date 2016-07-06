using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class TipoTransaccionBLL
    {
        public bool Insertar(TipoTransaccion entidad)
        {
            return new TipoTransaccionDAO().Insertar(entidad);
        }

        public bool Actualizar(TipoTransaccion entidad)
        {
            return new TipoTransaccionDAO().Actualizar(entidad);
        }

        public bool Eliminar(TipoTransaccion entidad)
        {
            return new TipoTransaccionDAO().Eliminar(entidad);
        }

        public TipoTransaccion CopiarEntidad(TipoTransaccion entidad)
        {
            return new TipoTransaccionDAO().CopiarEntidad(entidad);
        }

        public List<TipoTransaccion> Listar()
        {
            return new TipoTransaccionDAO().Listar();
        }
        public List<TipoTransaccion> ListarActivos()
        {
            return new TipoTransaccionDAO().ListarActivos();
        }

        public TipoTransaccion ObtenerPorIdTipoTransaccion(Guid idTipoTransaccion)
        {
            return new TipoTransaccionDAO().ObtenerPorIdTipoTransaccion(idTipoTransaccion);
        }

        public List<TipoTransaccion> ObtenerPorNombre(string nombre)
        {
            return new TipoTransaccionDAO().ObtenerPorNombre(nombre);
        }

        public List<TipoTransaccion> ObtenerListaTransacciones()
        {
            return new TipoTransaccionDAO().ListarHijos();
        }
    }
}
