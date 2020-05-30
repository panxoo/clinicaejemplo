using FRANLES_DENT_3.Models.MedicoDato;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Empresa.Atributos
{
    public class Sucursal_Area_Atencion
    {
        public string Sucursal_Area_AtencionId { get; set; }
        public string SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        public string Area_AtencionId { get; set; }
        public Area_Atencion Area_Atencion { get; set; }

        public bool Activo { get; set; }

        public List<Area_Medico> Area_Medicos { get; set; }
    }
}