using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class CreaUsuarioInput : DataInput<Usuario>
    {
        public CreaUsuarioInput()
        {
            PerfilesSelect = new List<SelPerfil>();
            SexoSelect = TransfParam.ParamSexo();
            DatoEmergencia = new DatosEmergenciaUsuario();
            ParentescoSelect = TransfParam.ParamParentesco();
            LocalizacionGnrl = new LocalizacionGnrl();
            TipoDocumentoSelect = TransfParam.ParamTipoDocumento();
        }

        public string Alias { get; set; }

        [Display(Name = "Nombre Cuenta")]
        [StringLength(250)]
        [Required(ErrorMessage = "Se requiere ingresar el nombre de usuario")]
        [RegularExpression(@"^[A-Z0-9a-zñÑ\\.\\_]*$", ErrorMessage = "El nombre de usuario solo puede contener numeros, letras y los caracteres punto y guion bajo")]
        public string Nombre_CuentaT { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Se debe ingresar contraseña")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{6,}$)(?=.*\d)(?=.*\W+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "La contraseña solo puede contener numeros, letras y los caracteres punto y guion bajo")]
        public string Contrasena { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "Se debe repetir contraseña")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string Contrasena_Segunda { get; set; }

        public bool UserNomExist { get; set; }
        public bool Accesact { get; set; }

        public IFormFile AvatarImage { get; set; }

        public LocalizacionGnrl LocalizacionGnrl { get; set; }
        public List<SelPerfil> PerfilesSelect { get; set; }
        public List<SelectListItem> SexoSelect { get; set; }
        public List<SelectListItem> ParentescoSelect { get; set; }
        public List<SelectListItem> TipoDocumentoSelect { get; set; }

        public DatosEmergenciaUsuario DatoEmergencia { get; set; }

        public class SelPerfil
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public bool IsMedic { get; set; }
            public bool IsAsistent { get; set; }
        }
    }
}
