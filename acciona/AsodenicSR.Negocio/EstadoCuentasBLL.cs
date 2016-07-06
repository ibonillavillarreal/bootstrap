using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class EstadoCuentasBLL
    {
        public bool Insertar(tEstadoCuentas entidad)
        {
            return new EstadoCuentasDAO().Insertar(entidad);
        }

        public bool Actualizar(tEstadoCuentas entidad)
        {
            return new EstadoCuentasDAO().Actualizar(entidad);
        }

        public bool Eliminar(tEstadoCuentas entidad)
        {
            return new EstadoCuentasDAO().Eliminar(entidad);
        }

        public tEstadoCuentas CopiarEntidad(tEstadoCuentas entidad)
        {
            return new EstadoCuentasDAO().CopiarEntidad(entidad);
        }

        public List<tEstadoCuentas> Listar()
        {
            return new EstadoCuentasDAO().Listar();
        }
        public List<tEstadoCuentas> ListarActivos()
        {
            return new EstadoCuentasDAO().ListarActivos();
        }

        public tEstadoCuentas ObtenerPorIdEstadoCuenta(Guid idEstadoCuenta)
        {
            return new EstadoCuentasDAO().ObtenerPorIdEstadoCuenta(idEstadoCuenta);
        }

        public List<tEstadoCuentas> ObtenerPorNombre(string nombre)
        {
            return new EstadoCuentasDAO().ObtenerPorNombre(nombre);
        }
    }
}
