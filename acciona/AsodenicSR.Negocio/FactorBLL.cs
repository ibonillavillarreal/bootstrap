using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class FactorBLL
    {
        public bool Insertar(Factor entidad)
        {
            return new FactorDAO().Insertar(entidad);
        }

        public bool Actualizar(Factor entidad)
        {
            return new FactorDAO().Actualizar(entidad);
        }

        public bool Eliminar(Factor entidad)
        {
            return new FactorDAO().Eliminar(entidad);
        }

        public Factor CopiarEntidad(Factor entidad)
        {
            return new FactorDAO().CopiarEntidad(entidad);
        }

        public List<Factor> Listar()
        {
            return new FactorDAO().Listar();
        }

        public Factor ObtenerPorIdFactor(Guid idFactor)
        {
            return new FactorDAO().ObtenerPorIdFactor(idFactor);
        }

        public List<Factor> ObtenerPorNombre(string nombre)
        {
            return new FactorDAO().ObtenerPorNombre(nombre);
        }
    }
}
