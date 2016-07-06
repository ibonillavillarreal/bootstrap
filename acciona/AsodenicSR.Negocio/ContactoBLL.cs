using SisSegLT.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccionaSR.Negocio
{
    public class ContactoBLL
    {
        public bool Insertar(Contacto entidad)
        {
            return new ContactoDAO().Insertar(entidad);
        }

        public bool Actualizar(Contacto entidad)
        {
            return new ContactoDAO().Actualizar(entidad);
        }

        public bool Eliminar(Contacto entidad)
        {
            return new ContactoDAO().Eliminar(entidad);
        }

        public Contacto CopiarEntidad(Contacto entidad)
        {
            return new ContactoDAO().CopiarEntidad(entidad);
        }

        public List<Contacto> Listar()
        {
            return new ContactoDAO().Listar();
        }

        public Contacto ObtenerPorIdContacto(Guid idContacto)
        {
            return new ContactoDAO().ObtenerPorIdContacto(idContacto);
        }

        public List<Contacto> ObtenerPorNombre(string nombre)
        {
            return new ContactoDAO().ObtenerPorNombre(nombre);
        }

        public List<Contacto> ObtenerContactosPorIdCliente(Guid idCliente)
        {
            return new ContactoDAO().ObtenerContactosPorIdCliente(idCliente);
        }
    }
}