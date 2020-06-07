using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Models.Sistema;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo
{
    public class EspecialidadDetalleInput : DataInputDato<Especialidad>
    {
        public EspecialidadDetalleInput()
        {
            Input = new Especialidad();
        }
    }
}