using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace SisSegLT.Negocio
{
    public class SucursalBLL
    {
        public bool Insertar(Sucursal entidad)
        {
            return new SucursalDAO().Insertar(entidad);
        }

        public bool Actualizar(Sucursal entidad)
        {
            return new SucursalDAO().Actualizar(entidad);
        }

        public bool Eliminar(Sucursal entidad)
        {
            return new SucursalDAO().Eliminar(entidad);
        }

        public Sucursal CopiarEntidad(Sucursal entidad)
        {
            return new SucursalDAO().CopiarEntidad(entidad);
        }

        public List<Sucursal> Listar()
        {
            return new SucursalDAO().Listar();
        }

        public List<Sucursal> ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                return Listar();
            return new SucursalDAO().ObtenerPorNombre(nombre);
        }

        public Sucursal ObtenerPorIdSucursal(Guid idSucursal)
        {
            if (idSucursal == Guid.Empty)
                return null;
            return new SucursalDAO().ObtenerPorIdSucursal(idSucursal);
        }

        public List<Sucursal> ObtenerPorIdCiudad(Guid idCiudad)
        {
            if (idCiudad == Guid.Empty)
                return null;
            return new SucursalDAO().ObtenerPorIdCiudad(idCiudad);
        }
    }
}
