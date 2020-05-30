using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Localization
{
    public class Pais
    {
        public int PaisId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(10)]
        public string Codigo { get; set; }

        public ICollection<Departamento> Departamentos { get; set; }
    }
}