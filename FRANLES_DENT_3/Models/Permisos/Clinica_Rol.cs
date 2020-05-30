using FRANLES_DENT_3.Models.Empresa;

namespace FRANLES_DENT_3.Models.Permisos
{
    public class Clinica_Rol
    {
        public string ClinicaId { get; set; }
        public virtual Clinica Clinica { get; set; }
        public string RolId { get; set; }
        public virtual AtributoRol Rol { get; set; }

        public bool Active { get; set; }
    }
}