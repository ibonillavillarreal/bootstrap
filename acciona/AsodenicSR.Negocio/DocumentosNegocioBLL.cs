using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisSegLT.Datos;

namespace AccionaSR.Negocio
{
    public class DocumentosNegocioBLL
    {
        public bool Insertar(DocumentosNegocio entidad)
        {
            return new DocumentosNegocioDAO().Insertar(entidad);
        }

        public bool Actualizar(DocumentosNegocio entidad)
        {
            return new DocumentosNegocioDAO().Actualizar(entidad);
        }

        public bool Eliminar(DocumentosNegocio entidad)
        {
            return new DocumentosNegocioDAO().Eliminar(entidad);
        }

        public DocumentosNegocio CopiarEntidad(DocumentosNegocio entidad)
        {
            return new DocumentosNegocioDAO().CopiarEntidad(entidad);
        }

        public List<DocumentosNegocio> Listar()
        {
            return new DocumentosNegocioDAO().Listar();
        }

        public DocumentosNegocio ObtenerPorIdDocumentosNegocio(Guid idDocumentosNegocio)
        {
            return new DocumentosNegocioDAO().ObtenerPorIdDocumentosNegocio(idDocumentosNegocio);
        }

        public List<DocumentosNegocio> ObtenerPorNombre(string nombre)
        {
            return new DocumentosNegocioDAO().ObtenerPorNombre(nombre);
        }

        public List<DocumentosNegocio> ObtenerDocumentosNegocioPorIdDocumentosNegocio(Guid idDatosNegocio)
        {
            return new DocumentosNegocioDAO().ObtenerDocumentosNegocioPorIdDatosNegocio(idDatosNegocio);
        }

    }
}
