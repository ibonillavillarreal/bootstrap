using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class DomicilioBLL
    {
        public bool Insertar(Domicilio entidad)
        {
            return new DomicilioDAO().Insertar(entidad);
        }

        public bool Actualizar(Domicilio entidad)
        {
            return new DomicilioDAO().Actualizar(entidad);
        }

        public bool Eliminar(Domicilio entidad)
        {
            return new DomicilioDAO().Eliminar(entidad);
        }

        public Domicilio CopiarEntidad(Domicilio entidad)
        {
            return new DomicilioDAO().CopiarEntidad(entidad);
        }

        public List<Domicilio> Listar()
        {
            return new DomicilioDAO().Listar();
        }

        public Domicilio ObtenerPorIdDomicilio(Guid idDomicilio)
        {
            return new DomicilioDAO().ObtenerPorIdDomicilio(idDomicilio);
        }

        public List<Domicilio> ObtenerPorNombre(string nombre)
        {
            return new DomicilioDAO().ObtenerPorNombre(nombre);
        }

        public List<Domicilio> ObtenerDomicilioPorIdCliente(Guid idCliente)
        {
            return new DomicilioDAO().ObtenerDomicilioPorIdCliente(idCliente);
        }
    }
}