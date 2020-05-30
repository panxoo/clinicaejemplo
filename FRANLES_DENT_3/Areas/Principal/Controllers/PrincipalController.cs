using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Areas.Principal.Controllers
{
    [Area("Principal")] 
    [Authorize(Roles =("Admin"))]
    public class PrincipalController : Controller
    {

        public PrincipalController(ApplicationDbContext context, IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
_lstGnrl._context = context;
            lstGnrl._usuarios = new LibUsuario(lstGnrl);
            
        }

        IListGeneral _lstGnrl;

        public async Task<IActionResult> Index()
        {

            _lstGnrl._datosUsuario = await _lstGnrl._usuarios.DatosSession(HttpContext);

            return View();
        }
    }
}