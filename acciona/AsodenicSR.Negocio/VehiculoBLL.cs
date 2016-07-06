using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class VehiculoBLL
    {
        public bool Insertar(tVehiculos entidad)
        {
            return new VehiculosDAO().Insertar(entidad);
        }

        public bool Actualizar(tVehiculos entidad)
        {
            return new VehiculosDAO().Actualizar(entidad);
        }

        public bool Eliminar(tVehiculos entidad)
        {
            return new VehiculosDAO().Eliminar(entidad);
        }

        public tVehiculos CopiarEntidad(tVehiculos entidad)
        {
            return new VehiculosDAO().CopiarEntidad(entidad);
        }

        public List<tVehiculos> Listar()
        {
            return new VehiculosDAO().Listar();
        }

        public tVehiculos ObtenerPorIdtVehiculos(Guid idVehiculos)
        {
            return new VehiculosDAO().ObtenerPorIdVehiculos(idVehiculos);
        }
    }
}
