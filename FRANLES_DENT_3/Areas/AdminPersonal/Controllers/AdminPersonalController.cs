using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Controllers
{
    [Area("AdminPersonal")]
    [Authorize]
    public class AdminPersonalController : Controller
    {
        public AdminPersonalController(IListGeneral lstGnrl, ApplicationDbContext context)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
            _lstGnrl._usuarios = new Libreria.LibUsuario(_lstGnrl);            
        }

        private IListGeneral _lstGnrl;

        public async Task<IActionResult> ProfilePersonal(string id)
        {

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
