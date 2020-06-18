using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo;
using FRANLES_DENT_3.Metodos.Permisos;
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

                _perfil.IsMedic = await _lstGnrl._context.Perfil_Rols.AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.PerfilId.Equals(_perfil.PerfilId) && a.Role.IsMedic);
                _perfil.IsAsistente = await _lstGnrl._context.Perfil_Rols.AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.PerfilId.Equals(_perfil.PerfilId) && a.Role.IsAsistente);

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0, Mensaje = "", Parametro = _perfil.PerfilId  };
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

                await DelRolPerfil(_model.PerfilId, _model.Rols);
                await AddRolPerfil(_model.PerfilId, _model.Rols);

                _perfil.Nombre = _model.Nombre;
                _perfil.Descripcion = _model.Descripcion;
                _perfil.IsMedic = await _lstGnrl._context.Perfil_Rols.AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.PerfilId.Equals(_perfil.PerfilId) && a.Role.IsMedic);
                _perfil.IsAsistente = await _lstGnrl._context.Perfil_Rols.AnyAsync(a => a.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.PerfilId.Equals(_perfil.PerfilId) && a.Role.IsAsistente);

                await _lstGnrl._context.SaveChangesAsync();

                await new PermisosSave(_lstGnrl).SavePermisosUsr(_perfil.PerfilId, _lstGnrl._datosUsuario.ClinicaId);


                return new RetornoAction { Code = 0, Mensaje = "", Parametro =   _perfil.PerfilId  };
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

            rolAux = _rols.Except(await _lstGnrl._context.Perfil_Rols.Where(w => w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).Select(s => s.RolId).ToListAsync()).ToList();

            if (rolAux.Count() > 0)
            {
                foreach (string rol in rolAux)
                {
                    pr.Add(new Perfil_Rol { PerfilId = _prfl, RolId = rol });
                }

                await _lstGnrl._context.AddRangeAsync(pr);
                await _lstGnrl._context.SaveChangesAsync();
            }
        }

        public async Task DelRolPerfil(string _prfl, List<string> _rols)
        {
            List<Perfil_Rol> pr = new List<Perfil_Rol>();

            List<string> rolAux0 = await _lstGnrl._context.Perfil_Rols.Where(w => w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_prfl)).Select(s => s.RolId).ToListAsync();
            List<string> rolAux = rolAux0.Where(w => !_rols.Contains(w)).ToList();

            if (rolAux.Count() > 0)
            {
                foreach (string rol in rolAux)
                {
                    pr.Add(new Perfil_Rol { PerfilId = _prfl, RolId = rol });
                }

                _lstGnrl._context.RemoveRange(pr);
                await _lstGnrl._context.SaveChangesAsync();
            }
        }


    }
}