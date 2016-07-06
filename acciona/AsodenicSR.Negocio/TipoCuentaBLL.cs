using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class TipoCuentaBLL
    {
        public bool Insertar(tTipoCuenta entidad)
        {
            return new TipoCuentaDAO().Insertar(entidad);
        }

        public bool Actualizar(tTipoCuenta entidad)
        {
            return new TipoCuentaDAO().Actualizar(entidad);
        }

        public bool Eliminar(tTipoCuenta entidad)
        {
            return new TipoCuentaDAO().Eliminar(entidad);
        }

        public tTipoCuenta CopiarEntidad(tTipoCuenta entidad)
        {
            return new TipoCuentaDAO().CopiarEntidad(entidad);
        }

        public List<tTipoCuenta> Listar()
        {
            return new TipoCuentaDAO().Listar();
        }
        public List<tTipoCuenta> ListarActivos()
        {
            return new TipoCuentaDAO().ListarActivos();
        }

        public tTipoCuenta ObtenerPorIdTipoCuenta(Guid idTipoCuenta)
        {
            return new TipoCuentaDAO().ObtenerPorIdTipoCuenta(idTipoCuenta);
        }

        public List<tTipoCuenta> ObtenerPorNombre(string nombre)
        {
            return new TipoCuentaDAO().ObtenerPorNombre(nombre);
        }
        public List<tTipoCuenta> ObtenerPorPrefijoCuenta(string prefijoCuenta)
        {
            return new TipoCuentaDAO().ObtenerPorPrefijoCuenta(prefijoCuenta);
        }
    }
}
