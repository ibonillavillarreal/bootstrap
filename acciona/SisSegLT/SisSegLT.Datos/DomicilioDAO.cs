using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DomicilioDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Domicilio entidad)
        {
            try
            {
                bool exito = false;
                db.Domicilio.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Domicilio entidad)
        {
            try
            {
                bool exito = false;

                Domicilio modificado = CopiarEntidad(entidad);
                db.Domicilio.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Domicilio entidad)
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

        public Domicilio CopiarEntidad(Domicilio entidad)
        {
            Domicilio nuevo = new Domicilio()
            {
                IdCliente = entidad.IdCliente,
                IdDomicilio = entidad.IdDomicilio,
                EsAlquilada = entidad.EsAlquilada,
                EsPropia = entidad.EsPropia,
                Familiar = entidad.Familiar,
                TiempoResidir = entidad.TiempoResidir,
                Descripcion = entidad.Descripcion,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC
            };
            return nuevo;
        }

        public List<Domicilio> Listar()
        {
            return (from r in db.Domicilio                    
                    orderby r.Descripcion
                    select r).ToList();
        }

        public Domicilio ObtenerPorIdDomicilio(Guid idDomicilio)
        {
            return (from r in db.Domicilio
                    where r.IdDomicilio == idDomicilio && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<Domicilio> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Domicilio
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<Domicilio> ObtenerDomicilioPorIdCliente(Guid idCliente)
        {
            return (from r in db.Domicilio
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }
    }
}