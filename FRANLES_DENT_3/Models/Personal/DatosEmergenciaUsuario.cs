using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Personal
{
    public class DatosEmergenciaUsuario
    {
        public int DatosEmergenciaUsuarioId { get; set; }

        [StringLength(255)]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(255)]
        public string Apellido_Paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(255)]
        public string Apellido_Materno { get; set; }

        public int Parentesco { get; set; }

        public string Movil { get; set; }

        public string UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}