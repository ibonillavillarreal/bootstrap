using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class AprobacionInstitucionDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(AprobacionInstitucion entidad)
        {
            try
            {
                bool exito = false;
                db.AprobacionInstitucion.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(AprobacionInstitucion entidad)
        {
            try
            {
                bool exito = false;

                AprobacionInstitucion modificado = CopiarEntidad(entidad);
                db.AprobacionInstitucion.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(AprobacionInstitucion entidad)
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

        public AprobacionInstitucion CopiarEntidad(AprobacionInstitucion entidad)
        {
            AprobacionInstitucion nuevo = new AprobacionInstitucion()
            {
                IdCliente = entidad.IdCliente,
                IdAprobacionInstitucion = entidad.IdAprobacionInstitucion,
                IdUsuario = entidad.IdUsuario,
                NivelRiesgo = entidad.NivelRiesgo,
                FechaHoraVerificacion = entidad.FechaHoraVerificacion,
                Descripcion = entidad.Descripcion,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                UsuarioIP = entidad.UsuarioIP,
                UsuarioPC = entidad.UsuarioPC
            };
            return nuevo;
        }

        public List<AprobacionInstitucion> Listar()
        {
            return (from r in db.AprobacionInstitucion
                    where r.EsActivo == true
                    orderby r.Descripcion
                    select r).ToList();
        }

        public AprobacionInstitucion ObtenerPorIdAprobacionInstitucion(Guid idAprobacionInstitucion)
        {
            return (from r in db.AprobacionInstitucion
                    where (r.IdAprobacionInstitucion == idAprobacionInstitucion) &&
                          (r.EsActivo == true)
                    select r).FirstOrDefault();
        }

        public List<AprobacionInstitucion> ObtenerPorNombre(string nombre)
        {
            return (from r in db.AprobacionInstitucion
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<AprobacionInstitucion> ObtenerAprobacionInstitucionPorIdCliente(Guid idCliente)
        {
            return (from r in db.AprobacionInstitucion
                    where r.IdCliente.Equals(idCliente) && r.EsActivo == true
                    select r).ToList();
        }

        public List<vwExclusivoInstitucion> ObtenervwInstitucionPorIdCliente(Guid idCliente)
        {
            return (from r in db.vwExclusivoInstitucion
                    where r.IdCliente.Equals(idCliente)
                    select r).ToList();
        }
    }
}