using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.ConfigAtributo
{
    public class ConfigAtributoSave
    {
        public ConfigAtributoSave(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> AddPerfil(PerfilDetallePost _model)
        {
            try
            {
                Perfil _perfil = new Perfil
                {
                    PerfilId = Ulid.NewUlid().ToString(),
                    Nombre = _model.Nombre,
                    Descripcion = _model.Descripcion,
                    Activo = true,
                    ClinicaId = _lstGnrl._datosUsuario.ClinicaId
                };

                await _lstGnrl._context.AddAsync(_perfil);
                await _lstGnrl._context.SaveChangesAsync();

                await AddRolPerfil(_perfil.PerfilId, _model.Rols);

                _perfil.IsMedic = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil).AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Role.IsMedic);
                _perfil.IsAsistente = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil).AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Role.IsAsistente);

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 1, Mensaje = "Error Al guardar los datos" };
            }
        }

        public async Task<RetornoAction> UpdPerfil(PerfilDetallePost _model)
        {
            try
            {
                Perfil _perfil = await _lstGnrl._context.Perfils.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.PerfilId.Equals(_model.PerfilId));

                await AddRolPerfil(_model.PerfilId, _model.Rols);
                await DelRolPerfil(_model.PerfilId, _model.Rols);

                _perfil.Nombre = _model.Nombre;
                _perfil.Descripcion = _model.Descripcion;
                _perfil.IsMedic = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil).AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Role.IsMedic);
                _perfil.IsAsistente = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil).AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Role.IsAsistente);

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 1, Mensaje = "Error Al guardar los datos" };
            }
        }

        public async Task AddRolPerfil(string _prfl, List<string> _rols)
        {
            List<Perfil_Rol> pr = new List<Perfil_Rol>();
            List<string> rolAux = new List<string>();

            rolAux = _rols.Except(await _lstGnrl._context.Perfil_Rols.Include(i => i.Perfil).Where(w => w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).Select(s => s.RolId).ToListAsync()).ToList();

            if (rolAux.Count() > 0)
            {
                foreach (string rol in rolAux)
                {
                    pr.Add(new Perfil_Rol { PerfilId = _prfl, RolId = rol });
                }

                await _lstGnrl._context.AddRangeAsync(pr);
                await _lstGnrl._context.SaveChangesAsync();
                await AddRolPerfilUsr(_prfl, rolAux);
            }
        }

        public async Task DelRolPerfil(string _prfl, List<string> _rols)
        {
            List<Perfil_Rol> pr = new List<Perfil_Rol>();

            var rolAux0 = await _lstGnrl._context.Perfil_Rols.Include(i => i.Perfil).Where(w => w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).Select(s => new { Id = s.RolId, Text = s.Role.NameRol }).ToListAsync();
            var rolAux = rolAux0.Where(w => !_rols.Contains(w.Id)).ToList();

            if (rolAux.Count() > 0)
            {
                foreach (string rol in rolAux.Select(s => s.Id))
                {
                    pr.Add(new Perfil_Rol { PerfilId = _prfl, RolId = rol });
                }

                _lstGnrl._context.RemoveRange(pr);
                await _lstGnrl._context.SaveChangesAsync();
                await DelRolPerfilUsr(_prfl, rolAux.Select(s => s.Text).ToList());
            }
        }

        public async Task AddRolPerfilUsr(string _prfl, List<string> _rols)
        {
            List<Usuario> usuarios = new List<Usuario>();

            var rols = await _lstGnrl._context.Perfil_Rols.Include(i => i.Role).Include(i => i.Perfil)
                                                            .Where(w => w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl))
                                                            .Select(s => new { Id = s.RolId, Text = s.Role.NameRol }).ToListAsync();

            var rolAux = rols.Where(s => _rols.Contains(s.Id)).Select(s => s.Text).ToList();

            usuarios = await _lstGnrl._context.Usuarios.IgnoreQueryFilters().Include(i => i.User).Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).ToListAsync();

            if (usuarios.Count() > 0)
            {
                foreach (var usr in usuarios)
                {
                    var aasd = await _lstGnrl._userManager.AddToRolesAsync(usr.User, rolAux);
                }
            }
        }

        public async Task DelRolPerfilUsr(string _prfl, List<string> _rol)
        {
            List<Usuario> usuarios = new List<Usuario>();

            usuarios = await _lstGnrl._context.Usuarios.IgnoreQueryFilters().Include(i => i.User).Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).ToListAsync();

            if (usuarios.Count() > 0)
            {
                foreach (var usr in usuarios)
                {
                    await _lstGnrl._userManager.RemoveFromRolesAsync(usr.User, _rol);
                }
            }
        }
    }
}