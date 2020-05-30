using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Controllers
{
    public class ComunController : Controller
    {
        public ComunController(ApplicationDbContext context, IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
        }

        IListGeneral _lstGnrl;


        [HttpPost]
        public async Task<IActionResult> CargaProvincia(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                return new JsonResult(await new CargaLocation().ObtenerLocalizacion(Id, _lstGnrl._context));
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult("Error de datos de localización");
            }
        }
    }
}