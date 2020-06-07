using FRANLES_DENT_3.Models.Historico;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador
{
    public class SudoAdministradorPost
    {
        public SudoAdministradorPost(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

       private IListGeneral _lstGnrl;



        public async Task<RetornoAction> PostAddRolClinica(string id, List<string> rols)
        {
            try
            {
                RetornoAction retornoAction = new RetornoAction();

                retornoAction = await new SudoAdministradorVal(_lstGnrl).ValAddRolClinica(id, rols);

                if(retornoAction.Code != 0)
                {
                    return retornoAction;
                }


                List<Perfil_Rol> perfil_Rols = new List<Perfil_Rol>();
                List<PerfilRolRemove> perfilRolRemoves = new List<PerfilRolRemove>();

                List<Clinica_Rol> clinica_Rols = await _lstGnrl._context.Clinica_Rols.IgnoreQueryFilters().Where(w => w.ClinicaId.Equals(id)).ToListAsync();


                clinica_Rols.ForEach(f =>
                {
                    f.Active = rols.Contains(f.RolId);
                });


                await _lstGnrl._context.SaveChangesAsync();

                perfil_Rols = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil).Where(w => w.Perfil.ClinicaId.Equals(id)).ToListAsync();


                perfil_Rols = await (from p in _lstGnrl._context.Perfils
                               join pr in _lstGnrl._context.Perfil_Rols on p.PerfilId equals pr.PerfilId
                               join cr in _lstGnrl._context.Clinica_Rols on new { c1 = pr.RolId, c2 = p.ClinicaId } equals new { c1 = cr.RolId, c2 = cr.ClinicaId }                               
                               where !cr.Active && p.ClinicaId.Equals(id)
                               select new Perfil_Rol
                               {
                                   PerfilId = p.PerfilId,
                                   RolId = pr.RolId
                               }
                              ).IgnoreQueryFilters().ToListAsync();


     

            foreach (Perfil_Rol pr in perfil_Rols)
            {
                perfilRolRemoves.Add(new PerfilRolRemove
                {
                    Clinica = id,
                    Perfil = pr.PerfilId,
                    Rol = pr.RolId,
                    Fecha = DateTime.Now
                });

                List<IdentityUser> usuarios = await _lstGnrl._context.Usuarios.IgnoreQueryFilters().Where(w => w.PerfilId.Equals(pr.PerfilId) && w.ClinicaId.Equals(id)).Select(p => p.User).ToListAsync();

                foreach (IdentityUser user in usuarios)
                {
                    await _lstGnrl._userManager.RemoveFromRoleAsync(user, pr.Role.NameRol);
                }

            }

            _lstGnrl._context.RemoveRange(perfil_Rols);
            await _lstGnrl._context.AddRangeAsync(perfilRolRemoves);


            await _lstGnrl._context.SaveChangesAsync();


            return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch(Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = ex.Message };

            }

        }
    }
}
