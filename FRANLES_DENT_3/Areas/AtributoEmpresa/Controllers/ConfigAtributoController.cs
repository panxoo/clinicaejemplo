using FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.AtributoEmpresa;
using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo;
using FRANLES_DENT_3.Controllers;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Controllers
{
    [Area("AtributoEmpresa")]
    [Authorize]
    public class ConfigAtributoController : Controller
    {
        public ConfigAtributoController(IListGeneral lstGnrl, ApplicationDbContext context)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
            _lstGnrl._usuarios = new Libreria.LibUsuario(_lstGnrl);
        }

        private IListGeneral _lstGnrl;

        #region Especialidad

        public async Task<IActionResult> EspecialidadMant()
        {
            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            return View(await new ConfigAtributoGet(_lstGnrl).GetEspecialidadMant());
        }

        [HttpPost]
        public async Task<IActionResult> EspecialidadMant(EspecialidadInputPost _model)
        {
            string moduloAcc = VarGnrl.AccionModulo(_model.Metodo, "Conf_Atribu");

            if (moduloAcc != "Lst")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    redirectToUrl = Url.Action(nameof(HomeController.Index), "Home"),
                    redir = true,
                    mnsj = "Error en el registro, volver abrir pantalla para registro."
                });
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Falta llenar los campos obligatorios" });
            }

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new ConfigAtributoPost(_lstGnrl).PostSaveEspecialidad(_model);

            switch (retornoAction.Code)
            {
                case 0:

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    List<Especialidad> modelret = await new ConfigAtributoGet(_lstGnrl).GetListEspecialidad();
                    return PartialView("Shared/_EspecialidadMantDat", modelret);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { redir = false, mnsj = retornoAction.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        redirectToUrl = Url.Action(nameof(HomeController.Index), "Home"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(retornoAction.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : retornoAction.Mensaje
                    });
            }
        }

        public async Task<IActionResult> EspecialidadDetalle(string id, string actmtd)
        {
            string moduloAcc = VarGnrl.AccionModulo(actmtd, "Conf_Atribu");

            if (moduloAcc != "Vie")
            {
                return RedirectToAction(nameof(ConfigAtributoController.EspecialidadMant), "ConfigAtributo");
            }

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            var _model = await new ConfigAtributoGet(_lstGnrl).GetEspecialidadDet(id);

            if (_model.Datos == null)
            {
                return RedirectToAction(nameof(ConfigAtributoController.EspecialidadMant), "ConfigAtributo");
            }
            else
            {
                return View(_model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EspecialidadDetalle(EspecialidadInputPost _model)
        {
            string moduloAcc = VarGnrl.AccionModulo(_model.Metodo, "Conf_Atribu");

            if (moduloAcc != "Vie")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    redirectToUrl = Url.Action(nameof(ConfigAtributoController.EspecialidadMant), "ConfigAtributo"),
                    redir = true,
                    mnsj = "Error en el registro, volver abrir pantalla para registro."
                });
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new RetornoActionView { mnsj = "Falta llenar los campos obligatorios" });
            }

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new ConfigAtributoPost(_lstGnrl).PostUpdEspecialidad(_model);

            switch (retornoAction.Code)
            {
                case 0:

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    Especialidad modelret = await new ConfigAtributoGet(_lstGnrl).GetEspecialidadDetDato(_model.EspecialidadId);
                    return PartialView("Shared/_EspecialidadDetalleGnrl", modelret);

                case 1:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { redir = false, mnsj = retornoAction.Mensaje });

                default:
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        redirectToUrl = Url.Action(nameof(ConfigAtributoController.EspecialidadMant), "ConfigAtributo"),
                        redir = true,
                        mnsj = string.IsNullOrEmpty(retornoAction.Mensaje) ? "Error en el registro, volver abrir pantalla para registro." : retornoAction.Mensaje
                    });
            }
        }

        #endregion Especialidad

        public async Task<IActionResult> PerfilMant()

        public IActionResult Index()
        {
            return View();
        }
    }
}