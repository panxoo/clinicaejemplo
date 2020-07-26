using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg
{
    public class SucursalConfInput : DataInput<Sucursal>
    {
        public string Departamento { get; set; }
        public List<AreaAtencionSucursal> AreaSucursal { get; set; }
        public List<SelectListItem> AreaAtencionSucursalInputs { get; set; }

        public class AreaAtencionSucursal
        {
            public string AreaId { get; set; }
            public string AreaNombre { get; set; }
            public bool Seleccion { get; set; }
            public int CantMedicos { get; set; }
        }

    }
}