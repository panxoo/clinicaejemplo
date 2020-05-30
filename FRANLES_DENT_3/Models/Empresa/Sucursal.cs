using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.Personal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Empresa
{
    public class Sucursal
    {
        [StringLength(255)]
        public string SucursalId { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombre Sucursal")]
        public string Nombre { get; set; }

        public bool Principal { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el Distrito")]
        public string DistritoId { get; set; }

        public Distrito Distrito { get; set; }

        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }

        [Display(Name = "Segundo Telefono")]
        public string Telefono2 { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "Se debe ingresar un correo valido")]
        public string CorreoSucursal { get; set; }

        [Display(Name = "Segundo Correo")]
        [EmailAddress(ErrorMessage = "Se debe ingresar un correo valido")]
        public string CorreoSucursal2 { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
        public List<Sucursal_Area_Atencion> Sucursal_Area_Atencions { get; set; }
        public List<Sucursal_Usuario> Sucursal_Usuarios { get; set; }
    }
}