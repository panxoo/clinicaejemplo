using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Personal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Localization
{
    public class Distrito
    {
        [StringLength(6)]
        public string DistritoId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        public ICollection<Clinica> Clinicas { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}