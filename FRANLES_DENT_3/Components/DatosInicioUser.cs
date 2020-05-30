using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Components
{
    public class DatosInicioUser : ViewComponent
    {

        public DatosInicioUser(IListGeneral lstGnrl, ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _lstGnrl = lstGnrl;
            _lstGnrl._context = context;
            _lstGnrl._signInManager = signInManager;
            _lstGnrl._usuarios = new Libreria.LibUsuario(_lstGnrl);
        }

        IListGeneral _lstGnrl;

        public  IViewComponentResult  Invoke()
        {
            LayoutSesion model = new LayoutSesion();
            _lstGnrl._datosUsuario =  _lstGnrl._usuarios.GetDatosSession(HttpContext);

            model.Clinica = _lstGnrl._datosUsuario.Clinica;

            return View(model);
        }
    }
    }
