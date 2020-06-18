using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Sistema;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg
{
    public class SucursalConfInput : DataInput<Sucursal>
    {
        public SucursalConfInput()
        {
            AreaSucursal = new List<AreaAtencionSucursal>();
            AreaAtencionSucursalInputs = new List<AreaAtencionSucursalInput>();
        }

        public string Departamento { get; set; }
        public List<AreaAtencionSucursal> AreaSucursal { get; set; }
        public List<AreaAtencionSucursalInput> AreaAtencionSucursalInputs { get; set; }

        public class AreaAtencionSucursal
        {
            public string AreaId { get; set; }
            public string AreaNombre { get; set; }
            public bool Seleccion { get; set; }
            public int CantMedicos { get; set; }
        }

        public class AreaAtencionSucursalInput
        {
            public string SucursalId { get; set; }
            public string AreaAtencionId { get; set; }
            public string AANombre { get; set; }
            public bool Activo { get; set; }
        }
    }
}