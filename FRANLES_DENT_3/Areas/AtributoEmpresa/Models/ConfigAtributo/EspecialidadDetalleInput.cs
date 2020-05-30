using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo
{
    public class EspecialidadDetalleInput :DataInputDato<Especialidad>
    {
        public EspecialidadDetalleInput()
        {
            Input = new Especialidad();
        }
    }
}
