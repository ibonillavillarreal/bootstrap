using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class ContactoDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Contacto entidad)
        {
            try
            {
                bool exito = false;
                db.Contacto.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Actualizar(Contacto entidad)
        {
            try
            {
                bool exito = false;

                Contacto modificado = CopiarEntidad(entidad);
                db.Contacto.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Eliminar(Contacto entidad)
        {
            try
            {
                bool exito = false;
                entidad.EsActivo = false;
                exito = Actualizar(entidad);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public Contacto CopiarEntidad(Contacto entidad)
        {
            Contacto nuevo = new Contacto()
            {
                IdContacto = entidad.IdContacto,
                IdCliente = entidad.IdCliente,
                TipoContacto = entidad.TipoContacto,
                Descripcion = entidad.Descripcion,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<Contacto> Listar()
        {
            return (from r in db.Contacto
                    where r.EsActivo
                    orderby r.TipoContacto
                    select r).ToList();
        }

        public Contacto ObtenerPorIdContacto(Guid idContacto)
        {
            return (from r in db.Contacto
                    where (r.IdContacto == idContacto) && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<Contacto> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Contacto
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<Contacto> ObtenerContactosPorIdCliente(Guid idCliente)
        {
            return (from r in db.Contacto
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }
    }
}