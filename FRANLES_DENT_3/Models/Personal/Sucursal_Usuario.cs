using FRANLES_DENT_3.Models.Empresa;

namespace FRANLES_DENT_3.Models.Personal
{
    public class Sucursal_Usuario
    {
        public string SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public bool Activo { get; set; }
    }
}