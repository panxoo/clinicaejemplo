using System;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Empresa.Atributos
{
    public class Tipo_Horario
    {
        [StringLength(255)]
        public string Tipo_HorarioId { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Se debe ingresar el nombre del tipo de horario.")]
        public string Nombre { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Se debe ingresar la hora inicio del horario.")]
        [DataType(DataType.Time, ErrorMessage = "Se debe ingresar un valor de hora valido.")]
        public TimeSpan Hora_Inicio { get; set; }

        [Required(ErrorMessage = "Se debe ingresar la hora fin del horario.")]
        [DataType(DataType.Time, ErrorMessage = "Se debe ingresar un valor de hora valido.")]
        public TimeSpan Hora_Fin { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
    }
}