using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.MedicoDato
{
    public class Especialidad_Medico
    {
        [StringLength(255)]
        public string DatosMedicoId { get; set; }

        public DatosMedico DatosMedico { get; set; }

        [StringLength(255)]
        public string EspecialidadId { get; set; }

        public Especialidad Especialidad { get; set; }
    }
}