using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class EmpresasBLL
    {
        public bool Insertar(Empresas entidad)
        {
            return new EmpresasDAO().Insertar(entidad);
        }

        public bool Actualizar(Empresas entidad)
        {
            return new EmpresasDAO().Actualizar(entidad);
        }

        public bool Eliminar(Empresas entidad)
        {
            return new EmpresasDAO().Eliminar(entidad);
        }

        public Empresas CopiarEntidad(Empresas entidad)
        {
            return new EmpresasDAO().CopiarEntidad(entidad);
        }

        public List<Empresas> Listar()
        {
            return new EmpresasDAO().Listar();
        }

        public Empresas ObtenerPorIdEmpresas(Guid idEmpresas)
        {
            return new EmpresasDAO().ObtenerPorIdEmpresas(idEmpresas);
        }

        public List<Empresas> ObtenerPorNombre(string nombre)
        {
            return new EmpresasDAO().ObtenerPorNombre(nombre);
        }

        public List<Empresas> ObtenerEmpresasPorIdEmpresa(Guid IdEmpresa)
        {
            return new EmpresasDAO().ObtenerEmpresasPorIdCliente(IdEmpresa);
        }
    }
}
