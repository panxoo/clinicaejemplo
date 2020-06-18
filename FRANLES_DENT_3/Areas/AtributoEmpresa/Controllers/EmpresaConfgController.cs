using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg;
using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Controllers
{
    [Area("AtributoEmpresa")]
    [Authorize]
    public class EmpresaConfgController : Controller
    {
      
        public EmpresaConfgController (IListGeneral lstGnrl, ApplicationDbContext context )
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
            _lstGnrl._usuarios = new Libreria.LibUsuario(_lstGnrl);
        }

        private IListGeneral _lstGnrl;
        private RetornoAction retornoAction;

        public async Task<IActionResult> SucursalConfMant()
        {
            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            return View(await new EmpresaConfgGet(_lstGnrl).GetSucursalConfMant());
        }

        public async Task<IActionResult> SucursalConfEdit(string id, string actmtd)
        {
            string moduloAcc =  VarGnrl.AccionModulo(actmtd, "Conf_Empres");


            if (moduloAcc != "Upd" )
                return RedirectToAction(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg");

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            RetornoAction retornoAction = await new EmpresaConfgGet(_lstGnrl).GetSucursalConfEdit(id, moduloAcc);

            if(retornoAction.Code == 0)
            {
                return View(retornoAction.Parametro);
            }
            else
            {
                return RedirectToAction(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg");
            }

        }

        [HttpPost]
        public async Task<IActionResult> SucursalConfEdit(SucursalConfEditPost _model)
        {
            string moduloAcc = VarGnrl.AccionModulo(_model.ModAct, "Conf_Empres");


            if (moduloAcc != "Upd")
                return RedirectToAction(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg");

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            retornoAction = await new EmpresaConfgPost(_lstGnrl).PostSucursalConfEdit(_model);

            switch (retornoAction.Code)
            {
                case 0:
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { redirectToUrl = Url.Action(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg"), redir = true });
                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { redir = false, mnsj = retornoAction.Mensaje });
                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        redirectToUrl = Url.Action(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(retornoAction.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : retornoAction.Mensaje
                    });
            }
        }

        public async Task<IActionResult> SucursalConf(string id, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Conf_Empres");


            if (moduloAcc != "Vie")
                return RedirectToAction(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg");

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            RetornoAction retornoAction = await new EmpresaConfgGet(_lstGnrl).GetSucursalConf(id, moduloAcc);

            if (retornoAction.Code == 0)
            {
                return View(retornoAction.Parametro);
            }
            else
            {
                return RedirectToAction(nameof(EmpresaConfgController.SucursalConfMant), "EmpresaConfg");
            }

        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
