using FRANLES_DENT_3.Libreria.Validation;
using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Personal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRANLES_DENT_3.Models.MedicoDato
{
    public class DatosMedico
    {
        [StringLength(255)]
        public string DatosMedicoId { get; set; }

        [Required(ErrorMessage = "Error, se debe ingresar dato COP")]
        public int COP { get; set; }

        [Display(Name = "R.N.E.")]
        public string RNE { get; set; }

        [Display(Name = "% Ganancia Asegurada")]
        [RangeIfBool(nameof(OpcRemFija), min: 1, max: 100, opc: false, ErrorMessage = "El monto debe ser mayor a 0 y menor a 100")]
        public int Porcentaje_ganancia_asegurada { get; set; }

        [Display(Name = "% Ganancia Interno")]
        [RangeIfBool(nameof(OpcRemFija), min: 1, max: 100, opc: false, ErrorMessage = "El monto debe ser mayor a 0 y menor a 100")]
        public int Porcentaje_ganancia_interno { get; set; }

        public bool OpcRemFija { get; set; }

        [RangeIfBool(nameof(OpcRemFija), 1, 1000000, opc: true, ErrorMessage = "El monto debe ser mayor a 0 y menor a 1.000.0000")]
        public int RemFijaMonto { get; set; }

        public bool OpcPorc_Ganancia_REM_fj { get; set; }

        [RangeIfDoubleBoolTT(nameof(OpcRemFija), nameof(OpcPorc_Ganancia_REM_fj), 1, 100, ErrorMessage = "El porcentaje ingresado debe ser entre 1 y 100%")]
        public int Porc_Ganancia_REM_FJ { get; set; }

        public bool Habilidad { get; set; }

        public DateTime FechaRegistroHab { get; set; }

        [Column(TypeName = "Date")]
        [Display(Name = "Fecha inicio habilidad")]
        public DateTime FechaIniHab { get; set; }

        [Column(TypeName = "Date")]
        [Display(Name = "Fecha termino habilidad")]
        public DateTime FechaTermHab { get; set; }

        [StringLength(255)]
        public string ClinicaId { get; set; }

        public Clinica Clinica { get; set; }

        [StringLength(255)]
        public string UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public ICollection<Especialidad_Medico> Especialidad_Medicos { get; } = new List<Especialidad_Medico>();
    }
}