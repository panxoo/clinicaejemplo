using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Permisos
{
    public class AtributoRol
    {
        public string AtributoRolId { get; set; }
        public string NameRol { get; set; }
        public bool IsMedic { get; set; }
        public bool IsAsistente { get; set; }
        public bool Hijos { get; set; }

        public string FatherId { get; set; }
        public AtributoRol RolFather { get; set; }

        public List<Perfil_Rol> Perfil_Rols { get; set; }
        public List<Clinica_Rol> Clinica_Rols { get; set; }
        public List<AtributoRol> RolesHijos { get; set; }
    }
}