using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.MedicoDato;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Personal
{
    public class HorarioMedico
    {
        [StringLength(255)]
        public string HorarioMedicoId { get; set; }

        public Int16 DiaWeekId { get; set; }

        [StringLength(255)]
        public string Tipo_HorarioId { get; set; }

        public Tipo_Horario Tipo_Horario { get; set; }

        [StringLength(255)]
        public string SucursalId { get; set; }

        public Sucursal Sucursal { get; set; }

        [StringLength(255)]
        public string UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public List<HorarioMedicoAreaAtencion> HorarioMedicoAreaAtencions { get; set; }
    }
}