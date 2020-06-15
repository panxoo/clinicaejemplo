using FRANLES_DENT_3.Libreria.Validation;
using FRANLES_DENT_3.Models.Personal;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class CreaUsuarioInputPost : Usuario
    {
        public bool Accesact { get; set; }
        public bool UserNomExist { get; set; }

        [RequiredIfDobleBoolTF(nameof(Acceso), nameof(Accesact), ErrorMessage = "Se debe ingresar la contraseña")]
        public string Contrasena { get; set; }

        public DatosEmergenciaUsuario DtEmUsr { get; set; }
        public string ModAct { get; set; }
    }
}