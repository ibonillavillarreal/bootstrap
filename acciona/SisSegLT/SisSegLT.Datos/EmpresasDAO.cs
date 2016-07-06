using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class EmpresasDAO
    {
        private SisSegDB db = new SisSegDB();

        public bool Insertar(Empresas entidad)
        {
            try
            {
                bool exito = false;
                db.Empresas.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Empresas entidad)
        {
            try
            {
                bool exito = false;

                Empresas modificado = CopiarEntidad(entidad);
                db.Empresas.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Empresas entidad)
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

        public Empresas CopiarEntidad(Empresas entidad)
        {
            Empresas nuevo = new Empresas()
            {
                IdEmpresa = entidad.IdEmpresa,
                Codigo = entidad.Codigo,
                Descripcion = entidad.Descripcion,
                EsActivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro,
                Usuario = entidad.Usuario,
                DireccionIP = entidad.DireccionIP,
                NombrePC = entidad.NombrePC
            };
            return nuevo;
        }

        public List<Empresas> Listar()
        {
            return (from r in db.Empresas
                    orderby r.Descripcion
                    select r).ToList();
        }

        public Empresas ObtenerPorIdEmpresas(Guid idEmpresas)
        {
            return (from r in db.Empresas
                    where r.IdEmpresa == idEmpresas && r.EsActivo == true
                    select r).FirstOrDefault();
        }

        public List<Empresas> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Empresas
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<Empresas> ObtenerEmpresasPorIdCliente(Guid IdEmpresa)
        {
            return (from r in db.Empresas
                    where r.IdEmpresa.Equals(IdEmpresa) && r.EsActivo == true
                    select r).ToList();
        }
    }
}
