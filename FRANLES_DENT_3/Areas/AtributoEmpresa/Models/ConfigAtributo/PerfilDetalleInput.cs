using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Sistema;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo
{
    public class PerfilDetalleInput : DataInput<Perfil>
    {
        public PerfilDetalleInput()
        {
            Input = new Perfil();
        }

        public List<UsuariosGrillaPerfil> Usuarios { get; set; }

        public class UsuariosGrillaPerfil
        {
            public string UsuarioId { get; set; }
            public string NombreUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
        }
    }
}