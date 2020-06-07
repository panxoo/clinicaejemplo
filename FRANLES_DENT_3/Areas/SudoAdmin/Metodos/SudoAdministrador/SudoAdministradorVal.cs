using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador
{
    public class SudoAdministradorVal
    {
        public SudoAdministradorVal(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> ValAddRolClinica(string id, List<string> rols)
        {
            try
            {
                if(!await _lstGnrl._context.Clinicas.IgnoreQueryFilters().AnyAsync(a => a.ClinicaID.Equals(id)))
                {
                    return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
                }

                if (rols.Count() == 0)
                {
                    return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar los roles para asignar al cliente." };
                }

                if (rols.Except(_lstGnrl._context.Roles.Select(s => s.Id)).Count() > 0)
                {
                    return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
                }

                return new RetornoAction { Code = 0, Mensaje = "" };

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.Message);

                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }
    }
}
