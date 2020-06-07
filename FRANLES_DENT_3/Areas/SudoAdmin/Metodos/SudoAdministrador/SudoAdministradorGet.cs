using FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador.Metodo;
using FRANLES_DENT_3.Areas.SudoAdmin.Models.SudoAdministrador;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Rol_ClienteMantList>> GetRol_ClienteMant()
        {
            List<Rol_ClienteMantList> model = await _lstGnrl._context.Clinicas.Include(i => i.Clinica_Rols)
                                                                              .Select(s => new Rol_ClienteMantList
                                                                              {
                                                                                  ClinicaId = s.ClinicaID,
                                                                                  Nombre = s.Nombre,
                                                                                  CantRoles = s.Clinica_Rols.Count
                                                                              })
                                                                              .IgnoreQueryFilters().ToListAsync();

            return model;
        }

        public async Task<Rol_ClienteDetalleInput> GetRol_ClienteDetalle(string id, string actmtd, string moduloAcc)
        {

            Rol_ClienteDetalleInput model = new Rol_ClienteDetalleInput();

            if (await _lstGnrl._context.Roles.AnyAsync(a =>
                                               !(_lstGnrl._context.Clinica_Rols.Where(w => w.ClinicaId.Equals(id)).Select(s => s.RolId).IgnoreQueryFilters()
                                                ).Contains(a.Id))
               )
            {
                await new AdminClinica(_lstGnrl).AddRolClinica(id);
            }

            model.Input = await _lstGnrl._context.Clinicas.FirstOrDefaultAsync(w => w.ClinicaID.Equals(id));


            model.Metodo = actmtd;
            model.Action = moduloAcc;

            return model;
        }

        public async Task<List<TreeViewTemp>> GetRolesAdmin(string id)
        {
            List<TreeViewTemp> _model = await _lstGnrl._context.Clinica_Rols.Include(i => i.Rol)
                                                                               .Where(w => w.ClinicaId.Equals(id))
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
