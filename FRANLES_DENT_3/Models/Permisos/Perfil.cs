using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Personal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Permisos
{
    public class Perfil
    {
        [StringLength(255)]
        public string PerfilId { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool IsMedic { get; set; }
        public bool IsAsistente { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        public ICollection<Perfil_Rol> Perfil_Rols { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}