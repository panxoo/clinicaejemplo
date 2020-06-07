using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo;
using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.ConfigAtributo
{
    public class ConfigAtributoGet
    {
        public ConfigAtributoGet(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<DataCollectInput<Especialidad>> GetEspecialidadMant()
        {
            DataCollectInput<Especialidad> _model = new DataCollectInput<Especialidad>();

            _model.ListDatos = await GetListEspecialidad();
            _model.Input = new Especialidad();
            _model.Metodo = "1bj6WOlnNLZQ/Ka5Wixpp6RlgYBZc/QlSy4FUHVfOxo=4c7374";

            return _model;
        }

        public async Task<List<Especialidad>> GetListEspecialidad()
        {
            return await _lstGnrl._context.Especialidades.IgnoreQueryFilters().Where(p => p.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                 .Select(s => new Especialidad
                                                                                 {
                                                                                     EspecialidadId = s.EspecialidadId,
                                                                                     Nombre = s.Nombre,
                                                                                     Descripcion = s.Descripcion,
                                                                                     Activo = s.Activo
                                                                                 }).OrderBy(o => o.Activo).OrderBy(o => o.Nombre)
                                                                                 .ToListAsync();
        }

        public async Task<EspecialidadDetalleInput> GetEspecialidadDet(string id)
        {
            EspecialidadDetalleInput _model = new EspecialidadDetalleInput();
            _model.Datos = await GetEspecialidadDetDato(id);

            _model.Metodo = "1bj6WOlnNLZQ/Ka5Wixpp6RlgYBZc/QlSy4FUHVfOxo=566965";

            return _model;
        }

        public async Task<Especialidad> GetEspecialidadDetDato(string id)
        {
            return await _lstGnrl._context.Especialidades.Where(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.EspecialidadId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<List<PerfilMantenedor>> GetPerfilMant()
        {
            List<PerfilMantenedor> _model;

            _model = await _lstGnrl._context.Perfils.IgnoreQueryFilters().Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                         .Select(s => new PerfilMantenedor
                                                                         {
                                                                             Nombre = s.Nombre,
                                                                             Descripcion = s.Descripcion,
                                                                             Activo = s.Activo,
                                                                             PerfilId = s.PerfilId,
                                                                             IsMedic = s.IsMedic,
                                                                             IsAsistente = s.IsAsistente,
                                                                             CantUser = _lstGnrl._context.Usuarios.Count(c => c.PerfilId.Equals(s.PerfilId))
                                                                         }).ToListAsync();

            return _model;
        }

        public async Task<PerfilDetalleInput> GetPerfilDetalle(string id, string actmd, string accion)
        {
            PerfilDetalleInput _model = new PerfilDetalleInput();

            if (accion != "Add" && !string.IsNullOrEmpty(id))
            {
                _model.Input = await _lstGnrl._context.Perfils.IgnoreQueryFilters().FirstOrDefaultAsync(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(id));
            }

            if (accion == "Vie" && !string.IsNullOrEmpty(id))
            {
                _model.Usuarios = await _lstGnrl._context.Usuarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(id))
                                                                    .Select(s => new PerfilDetalleInput.UsuariosGrillaPerfil
                                                                    {
                                                                        Nombre = s.Nombre,
                                                                        Apellido = s.Apellido_Paterno + " " + s.Apellido_Materno,
                                                                        NombreUsuario = s.Nombre_Cuenta,
                                                                        UsuarioId = s.UsuarioId
                                                                    }).ToListAsync();
            }

            _model.Metodo = actmd;
            _model.Action = accion;

            return _model;
        }

        public async Task<List<TreeViewTemp>> GetRolesPerfil(string id, string accion)
        {
            List<TreeViewTemp> _model;

            if (accion == "Add")
            {
                _model = await _lstGnrl._context.Clinica_Rols.Include(t => t.Rol)
                                                          .Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                          .Select(s => new TreeViewTemp
                                                          {
                                                              Id = s.Rol.AtributoRolId,
                                                              Name = s.Rol.NameRol,
                                                              Hijo = s.Rol.Hijos,
                                                              FatherId = s.Rol.FatherId,
                                                              Check = false
                                                          }).ToListAsync();
            }
            else
            {
                List<string> PerfRlTemp = await _lstGnrl._context.Perfil_Rols.Include(i => i.Perfil)
                                                                       .Where(w => w.PerfilId.Equals(id) && w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                       .Select(s => s.RolId).ToListAsync();

                _model = await _lstGnrl._context.Clinica_Rols.Include(i => i.Rol)
                                                          .Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                          .Select(s => new TreeViewTemp
                                                          {
                                                              Id = s.Rol.AtributoRolId,
                                                              Name = s.Rol.NameRol,
                                                              Hijo = s.Rol.Hijos,
                                                              FatherId = s.Rol.FatherId,
                                                              Check = PerfRlTemp.Contains(s.RolId)
                                                          }).ToListAsync();
            }
            return _model;
        }
    }
}