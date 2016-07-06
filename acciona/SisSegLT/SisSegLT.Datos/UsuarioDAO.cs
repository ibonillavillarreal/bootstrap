using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class UsuarioDAO
    {
        private SisSegDB db = new SisSegDB();
        //private int largoPassNormal = 25;
        public bool Insertar(Usuario entidad)
        {
            try
            {
                bool exito = false;
                //Si la contraseña es corta inca que no está encriptada
                //if (entidad.Pass.Length <= largoPassNormal)
                //{
                //    //Encriptar la contraseña
                //    entidad.Pass = Encriptacion.EncriptarTexto(entidad.Pass);
                //}
                //Encriptar la contraseña
                entidad.Pass = Encriptacion.EncriptarTexto(entidad.Pass);
                db.Usuario.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Usuario entidad, bool encriptar = true)
        {
            try
            {
                bool exito = false;

                Usuario modificado = CopiarEntidad(entidad, encriptar);
                db.Usuario.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Usuario entidad)
        {
            try
            {
                bool exito = false;
                entidad.EsActivo = false;
                exito = Actualizar(entidad, false);
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public Usuario CopiarEntidad(Usuario entidad, bool encriptar = true)
        {
            Usuario nuevo = new Usuario()
            {
                IdUsuario = entidad.IdUsuario,
                IdSucursal = entidad.IdSucursal,
                //IdRol = entidad.IdRol,
                Nombre = entidad.Nombre,
                Login = entidad.Login,
                Pass = entidad.Pass,
                Cargo = entidad.Cargo,
                FechaRegistro = entidad.FechaRegistro,
                Sexo = entidad.Sexo,
                Codigo = entidad.Codigo,
                EsActivo = entidad.EsActivo
            };
            //Si la contraseña es corta inca que no está encriptada
            //if (nuevo.Pass.Length <= largoPassNormal)
            //{
            //    //Encriptar la contraseña
            //    nuevo.Pass = Encriptacion.EncriptarTexto(nuevo.Pass);
            //}

            //Encriptar la contraseña
            
            if (encriptar)
                nuevo.Pass = Encriptacion.EncriptarTexto(nuevo.Pass);

            return nuevo;
        }

        public List<Usuario> Listar()
        {
            return (from r in db.Usuario
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public List<Usuario> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Usuario
                    where (r.Nombre.Equals(nombre)) &&
                          (r.EsActivo)
                    orderby r.Nombre
                    select r).ToList();
        }

        public Usuario ObtenerPorUsuarioContrasena(string user, string pass)
        {
            string passEncriptada = pass;
            //Si la contraseña es corta inca que no está encriptada
            //if (pass.Length <= largoPassNormal)
            //{
            //    //Encriptar la contraseña
            //    passEncriptada = Encriptacion.EncriptarTexto(pass);
            //}
            //Encriptar la contraseña
            
            passEncriptada = Encriptacion.EncriptarTexto(pass);
            Usuario usuario = (from r in db.Usuario
                               where ((r.Login.Equals(user)) 
                               && (r.Pass.Equals(passEncriptada))
                               ) &&
                               (r.EsActivo)
                               select r).FirstOrDefault();
            if (usuario != null)
            {
                //Si la contraseña es corta inca que no está encriptada
                //if (usuario.Pass.Length > largoPassNormal)
                //{
                //    //Encriptar la contraseña
                //    usuario.Pass = Encriptacion.DesencriptarTexto(usuario.Pass);
                //}
                //Encriptar la contraseña
                usuario.Pass = Encriptacion.DesencriptarTexto(usuario.Pass);
            }
            return usuario;
        }

        public Usuario ObtenerPorIdUsuario(Guid idUsuario)
        {
            Usuario usuario = (from r in db.Usuario
                               where (r.IdUsuario == idUsuario) &&
                                     (r.EsActivo)
                               select r).FirstOrDefault();
            if (usuario != null)
            {
                //Si la contraseña es corta inca que no está encriptada
                //if (usuario.Pass.Length > largoPassNormal)
                //{
                //    //Encriptar la contraseña
                //    usuario.Pass = Encriptacion.DesencriptarTexto(usuario.Pass);
                //}
                usuario.Pass = Encriptacion.DesencriptarTexto(usuario.Pass);
            }
            return usuario;
        }

        public List<Usuario> ObtenerPorIdSucursal(Guid idSucursal)
        {
            return (from r in db.Usuario
                    where (r.IdSucursal == idSucursal) &&
                          (r.EsActivo)
                    select r).ToList();
        }

        public List<Usuario> ObtenerPorIdRol(Guid idRol)
        {
            return (from c in db.Usuario
                    join cn in db.UsuarioRol on c.IdUsuario equals cn.IdUsuario
                    join ct in db.Rol on cn.IdRol equals ct.IdRol
                     where (cn.IdRol == idRol)  && (c.EsActivo)
                    select c).ToList();
        }
        //public List<Usuario> ObtenerPorNombreRol(string nombreRol)
        //{
        //    return (from r in db.Usuario
        //            where (r.UsuarioRol.Nombre == nombreRol) &&
        //                  (r.EsActivo)
        //            select r).ToList();
        //}

        public List<Usuario> ObtenerUsuariosPorNombreRol(string nombreRol)
        {
            return (from c in db.Usuario
                    join cn in db.UsuarioRol on c.IdUsuario equals cn.IdUsuario
                    join ct in db.Rol on cn.IdRol equals ct.IdRol
                    where (ct.Nombre == nombreRol) && (cn.EsActivo == true)
                    select c).ToList();
        }
    }
}
