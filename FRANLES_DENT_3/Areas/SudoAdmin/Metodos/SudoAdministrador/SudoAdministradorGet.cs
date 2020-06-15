using FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador.Metodo;
using FRANLES_DENT_3.Areas.SudoAdmin.Models.SudoAdministrador;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador
{
    public class SudoAdministradorGet
    {
        public SudoAdministradorGet(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<DataCollect<Rol_ClienteMantList>> GetRol_ClienteMant()
        {
            DataCollect<Rol_ClienteMantList> model = new DataCollect<Rol_ClienteMantList>();
               model.ListDatos = await _lstGnrl._context.Clinicas.Include(i => i.Clinica_Rols)
                                                                              .Select(s => new Rol_ClienteMantList
                                                                              {
                                                                                  ClinicaId = s.ClinicaID,
                                                                                  Nombre = s.Nombre,
                                                                                  CantRoles = s.Clinica_Rols.Count
                                                                              })
                                                                              .IgnoreQueryFilters().ToListAsync();

            model.Metodo = VarGnrl.GetModuloKey("Root_CliRol");            
            return model;
        }

        public async Task<Rol_ClienteDetalleInput> GetRol_ClienteDetalle(string id,  string moduloAcc)
        {

            Rol_ClienteDetalleInput model = new Rol_ClienteDetalleInput();

            if (await _lstGnrl._context.Roles.AnyAsync(a =>
                                               !(_lstGnrl._context.Clinica_Rols.Where(w => w.ClinicaId.Equals(id)).Select(s => s.RolId).IgnoreQueryFilters()
                                                ).Contains(a.Id)))
            {
                await new AdminClinica(_lstGnrl).AddRolClinica(id);
            }

            model.Input = await _lstGnrl._context.Clinicas.FirstOrDefaultAsync(w => w.ClinicaID.Equals(id));


            model.Metodo =  VarGnrl.GetModuloKey("Root_CliRol"); 
            model.Action = moduloAcc;
            model.ModAct = VarGnrl.GetModuloActionKey("Root_CliRol", moduloAcc);

            return model;
        }

        public async Task<List<TreeViewTemp>> GetRolesAdmin(string id)
        {
            List<TreeViewTemp> _model = await _lstGnrl._context.Clinica_Rols.Where(w => w.ClinicaId.Equals(id))
                                                                               .Select(s => new TreeViewTemp
                                                                               {
                                                                                   Id = s.Rol.AtributoRolId,
                                                                                   Name = s.Rol.NameRol,
                                                                                   Hijo = s.Rol.Hijos,
                                                                                   FatherId = s.Rol.FatherId,
                                                                                   Check = s.Active
                                                                               }).IgnoreQueryFilters().ToListAsync();

            return _model;
        }
    }
}
