using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ClienteBLL
    {
        public bool Insertar(Cliente entidad)
        {
            return new ClienteDAO().Insertar(entidad);
        }

        public bool Actualizar(Cliente entidad)
        {
            return new ClienteDAO().Actualizar(entidad);
        }

        public bool Eliminar(Cliente entidad)
        {
            return new ClienteDAO().Eliminar(entidad);
        }

        public Cliente CopiarEntidad(Cliente entidad)
        {
            return new ClienteDAO().CopiarEntidad(entidad);
        }

        public List<Cliente> Listar()
        {
            return new ClienteDAO().Listar();
        }

        public Cliente ObtenerPorIdCliente(Guid idCliente)
        {
            return new ClienteDAO().ObtenerPorIdCliente(idCliente);
        }

        public List<Cliente> ObtenerPorNombre(string nombre)
        {
            return new ClienteDAO().ObtenerPorNombre(nombre);
        }

        public List<Cliente> ObtenerPorIdentificacion(string identificacion)
        {
            return new ClienteDAO().ObtenerPorIdentificacion(identificacion);
        }

        public List<Cliente> ObtenerPorCuenta(string nocuenta)
        {
            return new ClienteDAO().ObtenerporCuenta(nocuenta);
        }

        public List<vwDatosGeneralesPerfil> ObtenerDatosGeneralesPorIdentificacion(string identificacion)
        {
            return new ClienteDAO().ObtenerDatosGeneralesPorIdentificacion(identificacion);
        }

        public List<vwPerfilIngresado> ObtenervwPicIngresados(DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            return new ClienteDAO().ObtenervwPicIngresados(fechaInicio, fechaFin, estado);
        }

        public List<vwPerfilIngresado> ObtenervwPicIngresadosDetallado(DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            return new ClienteDAO().ObtenervwPicIngresadosDetallado(fechaInicio, fechaFin, estado);
        }
    }
}