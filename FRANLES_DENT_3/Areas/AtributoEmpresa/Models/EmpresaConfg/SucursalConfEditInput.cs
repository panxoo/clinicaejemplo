using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Localization;
using FRANLES_DENT_3.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg
{
    public class SucursalConfEditInput : DataInput<Sucursal>
    {
        public SucursalConfEditInput()
        {
            LocalizacionGnrl = new LocalizacionGnrl();
        }

        public LocalizacionGnrl LocalizacionGnrl { get; set; }

    }
}
