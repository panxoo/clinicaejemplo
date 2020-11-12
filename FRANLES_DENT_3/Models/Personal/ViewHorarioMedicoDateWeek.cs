using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Models.Personal
{
    public class ViewHorarioMedicoDateWeek
    {
        public string HorarioMedicoId { get; set; }
        public string UsuarioId { get; set; }
        public Int16 DiaWeekId { get; set; }
        public string SucursalId { get; set; }
        public string SucursalName { get; set; }
        public string ClinicaId { get; set; }
        public string Tipo_HorarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
    }
}
