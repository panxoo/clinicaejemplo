using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Localization
{
    public class Departamento
    {
        [StringLength(2)]
        public string DepartamentoId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        public ICollection<Provincia> Provincias { get; set; }
    }
}