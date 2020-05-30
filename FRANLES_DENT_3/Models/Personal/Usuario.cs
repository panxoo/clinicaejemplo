using FRANLES_DENT_3.Libreria.Validation;
using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Permisos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRANLES_DENT_3.Models.Personal
{
    public class Usuario
    {
        [StringLength(255)]
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Documento es obligatorio")]
        [Display(Name = "Tipo de documento")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "El campo N° Documento es obligatorio")]
        [Display(Name = "Numero de documento")]
        [StringLength(20)]
        public string NumDocumento { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(255)]
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string Apellido_Paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(255)]
        public string Apellido_Materno { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el sexo")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Column(TypeName = "Date")]
        public Nullable<DateTime> Fecha_Nac { get; set; }

        [StringLength(6)]
        public string DistritoId { get; set; }

        public Distrito Distrito { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        public string Telefono { get; set; }

        [Required(ErrorMessage = "Se requiere ingresar numero del movil")]
        public string Movil { get; set; }

        [Display(Name = "Nombre Cuenta")]
        [StringLength(250)]
        [RequiredIfBool(nameof(Acceso), ErrorMessage = "Se debe ingresar nombre de usuario")]
        public string Nombre_Cuenta { get; set; }

        [EmailAddress(ErrorMessage = "Se debe ingresar un correo valido")]
        [Required(ErrorMessage = "Se debe ingresar un correo valido")]
        public string Correo { get; set; }

        public string Avatar { get; set; }
        public bool Acceso { get; set; }

        public bool IsMedic { get; set; }
        public bool IsAsist { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacio { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public DateTime FechaActuali { get; set; }

        //public string UsrCrea { get; set; }
        //public virtual Usuario UsuarioCre { get; set; }

        //public string UsrAct { get; set; }
        //public virtual Usuario UsuarioAct { get; set; }

        public string ClinicaId { get; set; }
        public Clinica Clinica { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [RequiredIfBool(nameof(Acceso), ErrorMessage = "Se debe seleccionar el perfil")]
        [Display(Name = "Perfil")]
        public string PerfilId { get; set; }

        public Perfil Perfil { get; set; }
        public DatosMedico DatosMedico { get; set; }

        public DatosEmergenciaUsuario DatosEmergenciaUsuario { get; set; }

        public List<Sucursal_Usuario> Sucursal_Usuarios { get; set; }
        public List<Area_Medico> Area_Medicos { get; set; }
    }
}