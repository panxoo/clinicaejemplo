using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Localization
{
    public class Provincia
    {
        [StringLength(4)]
        public string ProvinciaId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public ICollection<Distrito> Distritos { get; set; }
    }
}