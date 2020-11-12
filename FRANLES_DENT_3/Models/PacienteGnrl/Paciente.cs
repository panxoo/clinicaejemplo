using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.Marca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRANLES_DENT_3.Models.PacienteGnrl
{
    public class Paciente
    {
        public string PacienteId { get; set; }
        [Display(Name ="Historia Clinica")]
        public int  HistClinica { get; set; }

        [Required(ErrorMessage ="Se debe seleccionar el tipo de documento.")]
        public int TipoDocumento { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessage = "Se debe ingresar el numero de documento.")]
        public string NumeroDocumento { get; set; }
        
        [StringLength(255)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name ="Nombres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(255)]
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(255)]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el sexo")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Column(TypeName = "Date")]
        public DateTime FechaNacimiento { get; set; }

    [DataType(DataType.EmailAddress, ErrorMessage = "El formato no corresponde a un correo")]
        public string Correo { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato del dato ingresado no corresponde a un Telefono")]
        public string Telefono { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato del dato ingresado no corresponde a un Movil")]
        [Required(ErrorMessage = "Se requiere ingresar numero del movil")]
        public string Movil { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        [Required(ErrorMessage ="Se debe seleccionar el distrito.")]
        public string DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        
       

        public DateTime Fecha_Add { get; set; }

        public string TipoRecomendacionId { get; set; }
        public TipoRecomendacion TipoRecomendacion { get; set; }

    }
}