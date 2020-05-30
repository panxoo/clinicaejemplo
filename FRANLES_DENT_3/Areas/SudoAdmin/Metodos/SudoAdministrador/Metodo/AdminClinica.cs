using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador.Metodo
{
    public class AdminClinica
    {
        public AdminClinica(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        IListGeneral _lstGnrl;


        public async Task AddRolClinica(string id)
        {
            List<Clinica_Rol> cr = new List<Clinica_Rol>();

            cr = await _lstGnrl._context.Roles.Where(a => !(_lstGnrl._context.Clinica_Rols
                                                            .Where(w => w.ClinicaId.Equals(id))
                                                            .Select(s => s.RolId).IgnoreQueryFilters()).Contains(a.Id))
                                      .Select(s => new Clinica_Rol
                                      {
                                          ClinicaId = id,
                                          RolId = s.Id,
                                          Active = false
                                      }).ToListAsync();

            await _lstGnrl._context.AddRangeAsync(cr);
            await _lstGnrl._context.SaveChangesAsync();
        }
    }
}
