using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Metodos.Permisos
{
    public class PermisosSave
    {
        public PermisosSave(IListGeneral lstGntl)
        {
            _lstGnrl = lstGntl;
        }

        private IListGeneral _lstGnrl;

        public async Task SavePermisosUsr(string _prfl, string _clinica, IdentityUser _user = null)
        {
            List<string> rolPrfl = await _lstGnrl._context.Perfil_Rols.Where(w => w.PerfilId.Equals(_prfl) && w.Perfil.ClinicaId.Equals(_clinica)).Select(s => s.Role.NameRol).ToListAsync();

            if (_user != null)
            {
                await SaveRolesUsr(rolPrfl, _user);
            }
            else
            {
                List<IdentityUser> users = await _lstGnrl._context.Usuarios.IgnoreQueryFilters().Where(w => w.ClinicaId.Equals(_clinica) && w.PerfilId.Equals(_prfl)).Select(s => s.User).ToListAsync();

                foreach (IdentityUser user in users)
                {
                    await SaveRolesUsr(rolPrfl, user);
                }

                List<Usuario> usuarios = await _lstGnrl._context.Usuarios.IgnoreQueryFilters().Where(w => w.ClinicaId.Equals(_clinica) && w.PerfilId.Equals(_prfl)).ToListAsync<Usuario>();

                var prmprfl = await _lstGnrl._context.Perfils.Where(w => w.ClinicaId.Equals(_clinica) && w.PerfilId.Equals(_prfl))
                                                             .Select(s => new
                                                             {
                                                                 medic = s.IsMedic,
                                                                 asist = s.IsAsistente
                                                             }).FirstOrDefaultAsync();

                usuarios.ForEach(f =>
                {
                    f.IsMedic = prmprfl.medic;
                    f.IsAsist = prmprfl.asist;
                });

                await _lstGnrl._context.SaveChangesAsync();
            }
        }

        private async Task SaveRolesUsr(List<string> _rolPrfl, IdentityUser _user)
        {
            IList<string> rolUsr = await _lstGnrl._userManager.GetRolesAsync(_user);

            List<string> rolAdd = _rolPrfl.Except(rolUsr).ToList();

            if (rolAdd.Count() > 0)
                await _lstGnrl._userManager.AddToRolesAsync(_user, rolAdd);

            List<string> rolDel = rolUsr.Except(_rolPrfl).ToList();

            if (rolDel.Count() > 0)
            {
                await _lstGnrl._userManager.RemoveFromRolesAsync(_user, rolDel);
            }
        }
    }
}