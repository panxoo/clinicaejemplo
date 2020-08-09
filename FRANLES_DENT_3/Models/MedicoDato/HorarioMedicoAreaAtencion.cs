using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.Personal;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.MedicoDato
{
    public class HorarioMedicoAreaAtencion
    {
        //public string HorarioMedicoAreaAtencionId { get; set; }
        [StringLength(255)]
        public string Area_AtencionId { get; set; }

        public Area_Atencion Area_Atencion { get; set; }

        [StringLength(255)]
        public string HorarioMedicoId { get; set; }

        public HorarioMedico HorarioMedico { get; set; }
    }
}