using FRANLES_DENT_3.Models.Empresa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.MedicoDato.Atributo
{
    public class Especialidad
    {
        [StringLength(255)]
        public string EspecialidadId { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "El campo Nombre Departamento es obligatorio")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha_Upd { get; set; }
        public DateTime Fecha_Add { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        public ICollection<Especialidad_Medico> Especialidad_Medicos { get; } = new List<Especialidad_Medico>();
    }
}