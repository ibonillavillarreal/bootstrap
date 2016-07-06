using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class SucursalDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Sucursal entidad)
        {
            try
            {
                bool exito = false;
                db.Sucursal.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Sucursal entidad)
        {
            try
            {
                bool exito = false;

                Sucursal modificado = CopiarEntidad(entidad);
                db.Sucursal.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Sucursal entidad)
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

        public Sucursal CopiarEntidad(Sucursal entidad)
        {
            Sucursal nuevo = new Sucursal()
            {
                IdSucursal = entidad.IdSucursal,
                IdCiudad = entidad.IdCiudad,
                Nombre = entidad.Nombre,
                Direccion = entidad.Direccion,
                Codigo = entidad.Codigo,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<Sucursal> Listar()
        {
            return (from r in db.Sucursal
                    where r.EsActivo
                    orderby r.Nombre
                    select r).ToList();
        }

        public List<Sucursal> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Sucursal
                    where (r.Nombre.ToLowerInvariant().Equals(nombre.ToLowerInvariant())) &&
                          (r.EsActivo)
                    orderby r.Nombre
                    select r).ToList();
        }

        public Sucursal ObtenerPorIdSucursal(Guid idSucursal)
        {
            return (from r in db.Sucursal
                    where (r.IdSucursal == idSucursal) &&
                          (r.EsActivo)
                    select r).FirstOrDefault();
        }

        public List<Sucursal> ObtenerPorIdCiudad(Guid idCiudad)
        {
            return (from r in db.Sucursal
                    where (r.IdCiudad == idCiudad) &&
                          (r.EsActivo)
                    select r).ToList();
        }
    }
}
