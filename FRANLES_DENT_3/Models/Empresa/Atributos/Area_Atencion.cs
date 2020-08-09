using FRANLES_DENT_3.Models.MedicoDato;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Empresa.Atributos
{
    public class Area_Atencion
    {
        public string Area_AtencionId { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "El campo Nombre de Area es obligatorio")]
        [Display(Name = "Nombre Area")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        public List<Sucursal_Area_Atencion> Sucursal_Area_Atencions { get; set; }
        public List<HorarioMedicoAreaAtencion> HorarioMedicoAreaAtencions { get; set; }
    }
}