using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Models.PacienteGnrl
{
    public class ContactosPaciente
    {
        [StringLength(255)]
        public string ContactosPacienteId { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el tipo de documento.")]
        public int TipoDocumento { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Se debe ingresar el numero de documento.")]
        public string NumeroDocumento { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(255)]
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(255)]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Column(TypeName = "Date")]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato del dato ingresado no corresponde a un Movil")]
        [Required(ErrorMessage = "Se requiere ingresar numero del movil")]
        public string Movil { get; set; }

        public int Parentesco { get; set; }



    }
}
