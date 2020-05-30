using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FRANLES_DENT_3.Areas.SudoAdmin.Metodos.SudoAdministrador;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FRANLES_DENT_3.Areas.SudoAdmin.Controllers
{
    [Area("SudoAdmin")]
    [Authorize]
    public class SudoAdministradorController : Controller
    {

        public SudoAdministradorController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IListGeneral lstGnrl, ApplicationDbContext context)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._signInManager = signInManager;
            _lstGnrl._context = context;
            _lstGnrl._userManager = userManager;
        }

        IListGeneral _lstGnrl;

        public async Task<IActionResult> Rol_ClienteMant()
        {
            return View(await new SudoAdministradorGet(_lstGnrl).GetRol_ClienteMant());
        }


        public async Task<IActionResult> Rol_ClienteDetalle(string id, string actmtd)
        {

            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Root_CliRol");

            if (moduloAcc.Contains("not"))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");

            if (!(await _lstGnrl._context.Clinicas.AnyAsync(A => A.ClinicaID.Equals(id))))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");


            return View(await new SudoAdministradorGet(_lstGnrl).GetRol_ClienteDetalle(id, actmtd, moduloAcc));
        }


        public async Task<JsonResult> AddRolesAdmin(string id, string actmtd)
        {

            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Root_CliRol");

            List<TreeViewTemp> rol_Temps = await _lstGnrl._context.Clinica_Rols.Include(i => i.Rol)
                                                                     .Where(w => w.ClinicaId.Equals(id))
                                                                     .Select(s => new TreeViewTemp
                                                                     {
                                                                         Id = s.Rol.AtributoRolId,
                                                                         Name = s.Rol.NameRol,
                                                                         Hijo = s.Rol.Hijos,
                                                                         FatherId = s.Rol.FatherId,
                                                                         Check = s.Active
                                                                     }).IgnoreQueryFilters().ToListAsync();


            TreeViewModel tvm = new TreeViewModel();

            return new JsonResult( tvm.TreeViewRealiza(rol_Temps, moduloAcc));
        }





        [HttpPost]
        public async Task<IActionResult> AddRolClinica(string id, List<string> rols)
        {
            RetornoAction ra = await new SudoAdministradorPost(_lstGnrl).PostAddRolClinica(id, rols);

            if (ra.Code == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { redir = false });
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    redirectToUrl = Url.Action(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador"),
                    redir = true,
                    mnsj = "Error en el registro, volver abrir pantalla para registro."
                });
            }
        }
    }
}