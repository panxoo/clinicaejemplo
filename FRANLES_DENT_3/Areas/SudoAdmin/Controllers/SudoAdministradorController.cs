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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        private IListGeneral _lstGnrl;

        public async Task<IActionResult> Rol_ClienteMant()
        {
            return View(await new SudoAdministradorGet(_lstGnrl).GetRol_ClienteMant());
        }

        public async Task<IActionResult> Rol_ClienteDetalle(string id, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Root_CliRol");

            if (!(moduloAcc == "Vie" || moduloAcc == "Upd"))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");

            if (!(await _lstGnrl._context.Clinicas.AnyAsync(A => A.ClinicaID.Equals(id))))
                return RedirectToAction(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador");

            return View(await new SudoAdministradorGet(_lstGnrl).GetRol_ClienteDetalle(id, moduloAcc));
        }

        public async Task<JsonResult> AddRolesAdmin(string id, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Root_CliRol");

            List<TreeViewTemp> _model = await new SudoAdministradorGet(_lstGnrl).GetRolesAdmin(id);

            TreeViewModel tvm = new TreeViewModel();

            return new JsonResult(tvm.TreeViewRealiza(_model, moduloAcc));
        }

        [HttpPost]
        public async Task<IActionResult> AddRolClinica(string id, List<string> rols, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Root_CliRol");

            if (moduloAcc != "Upd")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    redirectToUrl = Url.Action(nameof(SudoAdministradorController.Rol_ClienteMant), "SudoAdministrador"),
                    redir = true,
                    mnsj = "Error en el registro, volver abrir pantalla para registro."
                });
            }

            RetornoAction retornoAction = await new SudoAdministradorPost(_lstGnrl).PostAddRolClinica(id, rols);

            switch (retornoAction.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { redirectToUrl = Url.Action(nameof(SudoAdministradorController.Rol_ClienteDetalle), "SudoAdministrador", new { id = id, actmtd = VarGnrl.GetModuloActionKey("Root_CliRol", "Vie") }), redir = true });


                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { redir = false, mnsj = retornoAction.Mensaje });

                default:
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