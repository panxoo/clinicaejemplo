using FRANLES_DENT_3.Models.Permisos;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo
{
    public class PerfilDetallePost : Perfil
    {
        public List<string> Rols { get; set; }
        public string Metodo { get; set; }
    }
}