namespace FRANLES_DENT_3.Models.Permisos
{
    public class Perfil_Rol
    {
        public string PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        public string RolId { get; set; }
        public AtributoRol Role { get; set; }
    }
}