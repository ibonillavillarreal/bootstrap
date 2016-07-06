using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisSegLT.Datos
{
    public class FeriadosDAO
    {
        private SisSegDB db = new SisSegDB();
        public bool Insertar(Feriados entidad)
        {
            try
            {
                bool exito = false;
                db.Feriados.Add(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(Feriados entidad)
        {
            try
            {
                bool exito = false;

                Feriados modificado = CopiarEntidad(entidad);
                db.Feriados.Attach(modificado);
                db.Entry(modificado).State = System.Data.EntityState.Modified;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(Feriados entidad)
        {
            try
            {
                bool exito = false;
                db.Feriados.Remove(entidad);
                //db.Entry(entidad).State = System.Data.EntityState.Added;
                exito = db.SaveChanges() > 0;
                return exito;
            }
            catch
            {
                return false;
            }
        }

        public Feriados CopiarEntidad(Feriados entidad)
        {
            Feriados nuevo = new Feriados()
            {
                IdFeriado = entidad.IdFeriado,
                Departamento = entidad.Departamento,
                Municipio = entidad.Municipio,
                Dia = entidad.Dia,
                Mes = entidad.Mes,
                Anio = entidad.Anio,
                Descripcion = entidad.Descripcion
                
            };
            return nuevo;
        }

        public List<Feriados> Listar()
        {
            return (from r in db.Feriados
                    orderby r.Descripcion
                    select r).ToList();
        }
        

        public Feriados ObtenerPorIdFeriado(Guid idFeriado)
        {
            return (from r in db.Feriados
                    where (r.IdFeriado == idFeriado)
                    select r).FirstOrDefault();
        }

        public List<Feriados> ObtenerPorNombre(string nombre)
        {
            return (from r in db.Feriados
                    where r.Descripcion.Equals(nombre)
                    select r).ToList();
        }

        public List<Feriados> ObtenerPorFecha(int dia, int mes, int anio)
        {
            return (from r in db.Feriados
                    where r.Dia == dia && r.Mes == mes && r.Anio == anio
                    select r).ToList();
        }
    }
}
