using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class ClienteCuentaBLL
    {
        public bool Insertar(tClienteCuenta entidad)
        {
            return new ClienteCuentaDAO().Insertar(entidad);
        }

        public bool Actualizar(tClienteCuenta entidad)
        {
            return new ClienteCuentaDAO().Actualizar(entidad);
        }

        public bool Eliminar(tClienteCuenta entidad)
        {
            return new ClienteCuentaDAO().Eliminar(entidad);
        }

        public tClienteCuenta CopiarEntidad(tClienteCuenta entidad)
        {
            return new ClienteCuentaDAO().CopiarEntidad(entidad);
        }

        public List<tClienteCuenta> Listar()
        {
            return new ClienteCuentaDAO().Listar();
        }

        public tClienteCuenta ObtenerPorIdCuenta(Guid idCuenta)
        {
            return new ClienteCuentaDAO().ObtenerPorIdCuenta(idCuenta);
        }

        public List<tClienteCuenta> ObtenerPorNoCuenta(string NoCuenta)
        {
            return new ClienteCuentaDAO().ObtenerPorNoCuenta(NoCuenta);
        }

        public List<tClienteCuenta> ObtenerCuentaPorIdCliente(Guid idCliente)
        {
            return new ClienteCuentaDAO().ObtenerCuentaPorIdCliente(idCliente);
        }

        //public List<tClienteCuenta> ObtenervwDatosNegocioPorIdCliente(Guid idCliente)
        //{
        //    return new ClienteCuentaDAO().ObtenerDatosNegocioPorIdCliente(idCliente);
        //}
    }
}
