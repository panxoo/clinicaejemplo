using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Models.SudoAdministrador
{
    public class Rol_ClienteDetalleInput : DataInputDato<Clinica>
    {
        public Rol_ClienteDetalleInput()
        {
            Input = new Clinica();
        }
    }
}
