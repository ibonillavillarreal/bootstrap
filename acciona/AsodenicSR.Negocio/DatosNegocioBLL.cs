using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class DatosNegocioBLL
    {
        public bool Insertar(DatosNegocio entidad)
        {
            return new DatosNegocioDAO().Insertar(entidad);
        }

        public bool Actualizar(DatosNegocio entidad)
        {
            return new DatosNegocioDAO().Actualizar(entidad);
        }

        public bool Eliminar(DatosNegocio entidad)
        {
            return new DatosNegocioDAO().Eliminar(entidad);
        }

        public DatosNegocio CopiarEntidad(DatosNegocio entidad)
        {
            return new DatosNegocioDAO().CopiarEntidad(entidad);
        }

        public List<DatosNegocio> Listar()
        {
            return new DatosNegocioDAO().Listar();
        }

        public DatosNegocio ObtenerPorIdDatosNegocio(Guid idDatosNegocio)
        {
            return new DatosNegocioDAO().ObtenerPorIdDatosNegocio(idDatosNegocio);
        }

        public List<DatosNegocio> ObtenerPorNombre(string nombre)
        {
            return new DatosNegocioDAO().ObtenerPorNombre(nombre);
        }

        public List<DatosNegocio> ObtenerDatosNegocioPorIdCliente(Guid idDatosNegocio)
        {
            return new DatosNegocioDAO().ObtenerDatosNegocioPorIdDatosNegocio(idDatosNegocio);
        }

        public List<vwDatosNegocio> ObtenervwDatosNegocioPorIdCliente(Guid idCliente)
        {
            return new DatosNegocioDAO().ObtenerDatosNegocioPorIdCliente(idCliente);
        }
    }
}