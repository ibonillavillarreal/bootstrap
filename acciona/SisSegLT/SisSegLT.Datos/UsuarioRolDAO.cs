using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class UsuarioRolDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(UsuarioRol entidad)
        {
            try
            {
                bool exito = false;
                db.UsuarioRol.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(UsuarioRol entidad)
        {
            try
            {
                bool exito = false;

                UsuarioRol modificado = CopiarEntidad(entidad);
                db.UsuarioRol.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(UsuarioRol entidad)
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

        public UsuarioRol CopiarEntidad(UsuarioRol entidad)
        {
            UsuarioRol nuevo = new UsuarioRol()
            {
                IdUsuarioRol = entidad.IdUsuarioRol,
                IdUsuario = entidad.IdUsuario,
                IdRol = entidad.IdRol,
                FechaRegistro = entidad.FechaRegistro,
                EsActivo = entidad.EsActivo,
                UserIP = entidad.UserIP,
                UserPC = entidad.UserPC                
            };
            return nuevo;
        }

        public List<UsuarioRol> ListarActivos()
        {
            return (from r in db.UsuarioRol
                    where r.EsActivo == true
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public List<UsuarioRol> Listar()
        {
            return (from r in db.UsuarioRol                   
                    orderby r.FechaRegistro
                    select r).ToList();
        }


        public UsuarioRol ObtenerPorIdUsuarioRol(Guid idUsuarioRol)
        {
            return (from r in db.UsuarioRol
                    where (r.IdUsuarioRol == idUsuarioRol) &&
                          (r.EsActivo == true)
                    select r).FirstOrDefault();
        }

        public List<UsuarioRol> ObtenerPorIdRol(Guid IdRol)
        {
            return (from r in db.UsuarioRol
                    where (r.IdRol == IdRol) &&
                          (r.EsActivo == true)
                    orderby r.FechaRegistro
                    select r).ToList();
        }

        public List<UsuarioRol> ObtenerPorIdUsuario(Guid idUsuario)
        {
            return (from r in db.UsuarioRol
                    where (r.IdUsuario == idUsuario) &&
                          (r.EsActivo == true)
                    orderby r.FechaRegistro
                    select r).ToList();
        }
    }
}
