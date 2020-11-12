using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.PacienteGnrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Models.Marca
{
    public class TipoRecomendacion
    {
        public string TipoRecomendacionId { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        public List<Paciente> Pacientes { get; set; }
    }
}
