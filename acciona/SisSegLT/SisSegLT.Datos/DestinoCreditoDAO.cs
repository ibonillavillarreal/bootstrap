using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class DestinoCreditoDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(DestinoCredito entidad)
        {
            try
            {
                bool exito = false;
                db.DestinoCredito.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(DestinoCredito entidad)
        {
            try
            {
                bool exito = false;

                DestinoCredito modificado = CopiarEntidad(entidad);
                db.DestinoCredito.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(DestinoCredito entidad)
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

        public DestinoCredito CopiarEntidad(DestinoCredito entidad)
        {
            DestinoCredito nuevo = new DestinoCredito()
            {
                IdDestinoCredito = entidad.IdDestinoCredito,
                Nombre = entidad.Nombre,
                EsActivo = entidad.EsActivo
            };
            return nuevo;
        }

        public List<DestinoCredito> Listar()
        {
            return (from r in db.DestinoCredito                    
                    orderby r.Nombre
                    select r).ToList();
        }
        public List<DestinoCredito> ListarActivos()
        {
            return (from r in db.DestinoCredito
                    where r.EsActivo == true
                    orderby r.Nombre
                    select r).ToList();
        }

        public DestinoCredito ObtenerPorIdDestinoCredito(Guid idDestinoCredito)
        {
            return (from r in db.DestinoCredito
                    where (r.IdDestinoCredito == idDestinoCredito)                     
                    select r).FirstOrDefault();
        }

        public List<DestinoCredito> ObtenerPorNombre(string nombre)
        {
            return (from r in db.DestinoCredito
                    where r.Nombre.Equals(nombre)
                    select r).ToList();
        }
    }
}
