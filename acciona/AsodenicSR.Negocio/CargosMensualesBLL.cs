using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class CargosMensualesBLL
    {
        public bool Insertar(CargosMensuales entidad)
        {
            return new CargosMensualesDAO().Insertar(entidad);
        }

        public bool Actualizar(CargosMensuales entidad)
        {
            return new CargosMensualesDAO().Actualizar(entidad);
        }

        public bool Eliminar(CargosMensuales entidad)
        {
            return new CargosMensualesDAO().Eliminar(entidad);
        }

        public CargosMensuales CopiarEntidad(CargosMensuales entidad)
        {
            return new CargosMensualesDAO().CopiarEntidad(entidad);
        }

        public List<CargosMensuales> Listar()
        {
            return new CargosMensualesDAO().Listar();
        }

        public CargosMensuales ObtenerPorIdCargosMensuales(Guid idCargosMensuales)
        {
            return new CargosMensualesDAO().ObtenerPorIdCargosMensuales(idCargosMensuales);
        }

        public List<CargosMensuales> ObtenerPorTipoTransaccion(string tipoTransaccion)
        {
            return new CargosMensualesDAO().ObtenerPorTipoTransaccion(tipoTransaccion);
        }

        public List<vw_CargosMensuales> ObtenerCargosMensualessPorIdMovimiento(Guid idmovimiento)
        {
            return new CargosMensualesDAO().ObtenerCargosMensualessPorIdMovimiento(idmovimiento);
        }
    }
}
