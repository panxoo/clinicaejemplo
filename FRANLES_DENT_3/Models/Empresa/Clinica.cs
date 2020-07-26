using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Personal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Empresa
{
    public class Clinica
    {
        [StringLength(255)]
        public string ClinicaID { get; set; }

        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool MultiSucursal { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Se debe ingresar el dominio para la generacion de usuarios")]
        public string Dominio { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Se debe ingresar extencion de dominio para la generacion de usuarios")]
        public string Extencion { get; set; }

        [StringLength(6)]
        public string DistritoId { get; set; }

        public Distrito Distrito { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        public List<Clinica_Rol> Clinica_Rols { get; set; }

        public ICollection<Perfil> Perfils { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Area_Atencion> Area_Atencions { get; set; }
        public ICollection<Especialidad> Especialidads { get; set; }
        public ICollection<Sucursal> Sucursals { get; set; }
        public ICollection<Tipo_Horario> Tipo_Horarios { get; set; }
    }
}