using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.Personal;

namespace FRANLES_DENT_3.Models.MedicoDato
{
    public class Area_Medico
    {
        public string Area_MedicoId { get; set; }
        public string Sucursal_Area_AtencionId { get; set; }
        public Sucursal_Area_Atencion Sucursal_Area_Atencion { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public bool Activo { get; set; }
    }
}