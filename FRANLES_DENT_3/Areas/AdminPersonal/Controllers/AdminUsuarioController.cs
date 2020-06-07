﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminUsuario;
using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Controllers
{
    [Area("AdminPersonal")]
    [Authorize]
    public class AdminUsuarioController : Controller
    {

        public AdminUsuarioController(IListGeneral lstGnrl, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
            _lstGnrl._usuarios = new Libreria.LibUsuario(_lstGnrl);
            _lstGnrl._userManager = userManager;
        }

        private IListGeneral _lstGnrl;

        public async Task<IActionResult> UsuarioMant()
        {
            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            return View(await new AdminUsuarioGet(_lstGnrl).GetUsuarioMant());
        }
        //    Mant_Usuari

        public async Task<IActionResult> CreaUsuario(string id, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Mant_Usuari");

            if (!(moduloAcc == "Add" || moduloAcc == "Upd"))
                return RedirectToAction(nameof(AdminUsuarioController.UsuarioMant), "AdminUsuario");

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            if (moduloAcc == "Upd" && string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(AdminUsuarioController.UsuarioMant), "AdminUsuario");


            var _model = await new AdminUsuarioGet(_lstGnrl).GetCreaUsuario(id, moduloAcc);

            if (_model == null)
                return RedirectToAction(nameof(AdminUsuarioController.UsuarioMant), "AdminUsuario");

            return View(_model);

        }

        [HttpPost]
        public async Task<IActionResult> CreaUsuario(CreaUsuarioInputPost _model)
        {
            string moduloAcc = VarGnrl.AccionModulo(_model.Metodo, "Mant_Usuari");

            if (!(moduloAcc == "Upd" || moduloAcc == "Add"))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    redirectToUrl = Url.Action(nameof(AdminUsuarioController.UsuarioMant), "AdminUsuario"),
                    redir = true,
                    mnsj = "Error en el registro, volver abrir pantalla para registro."
                });
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { redir = false, mnsj = "Se debe llenar todos los campos." });
            }

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);


            RetornoAction retornoAction = await new AdminUsuarioPost(_lstGnrl).PostCreaUsuario(_model, moduloAcc);


            switch (retornoAction.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    //return Json(new { redirectToUrl = Url.Action(nameof(AdminUsuarioController.UsuarioView), "AdmUsuario", new { id = _model.UsuarioId, actmtd = "4d616e745f557375617269566965" }), redir = true });
                    return View();
                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { redir = false, mnsj = retornoAction.Mensaje });
                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        redirectToUrl = Url.Action(nameof(AdminUsuarioController.UsuarioMant), "AdmUsuario"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(retornoAction.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : retornoAction.Mensaje
                    });
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}