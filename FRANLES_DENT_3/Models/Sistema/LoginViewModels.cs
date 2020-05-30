using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class LoginViewModels
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo de usuario es obligatorio")]
            [EmailAddress(ErrorMessage = "El formato del nombre de usuario debe ser la de un correo")]
            public string Login_Usuario { get; set; }

            [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
            [DataType(DataType.Password)]
            public string Login_Contrasena { get; set; }
        }
    }
}