using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class PlasticoBLL
    {
        public bool Insertar(tPlastico entidad)
        {
            return new PlasticoDAO().Insertar(entidad);
        }

        public bool Actualizar(tPlastico entidad)
        {
            return new PlasticoDAO().Actualizar(entidad);
        }

        public bool Eliminar(tPlastico entidad)
        {
            return new PlasticoDAO().Eliminar(entidad);
        }

        public tPlastico CopiarEntidad(tPlastico entidad)
        {
            return new PlasticoDAO().CopiarEntidad(entidad);
        }

        public List<tPlastico> Listar()
        {
            return new PlasticoDAO().Listar();
        }
        public List<tPlastico> ListarActivos()
        {
            return new PlasticoDAO().ListarActivos();
        }

        public tPlastico ObtenerPorIdPlastico(Guid idPlastico)
        {
            return new PlasticoDAO().ObtenerPorIdPlastico(idPlastico);
        }

        public tPlastico ObtenerPorNoTarjeta(string noTarjeta)
        {
            return new PlasticoDAO().ObtenerPorNoTarjeta(noTarjeta);
        }

        public List<tPlastico> ObtenerPorIdCuenta(Guid idCuenta)
        {
            return new PlasticoDAO().ObtenerPorIdCuenta(idCuenta);
        }

        public List<tPlastico> ObtenerTarjetaPorIdCliente(Guid idCliente)
        {
            return new PlasticoDAO().ObtenerPorIdCliente(idCliente);
        }
    }
}
