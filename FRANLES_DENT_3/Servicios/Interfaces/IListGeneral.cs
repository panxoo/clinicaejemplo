using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.SessionUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Servicios.Interfaces
{
    public interface IListGeneral
    {
        DatosUsuarioSession _datosUsuario { get; set; }
        ApplicationDbContext _context { get; set; }

        List<SelectListItem> _selectList { get; set; }
        IdentityError _identityError { get; set; }
        LibUsuario _usuarios { get; set; }

        UsersRoles _usersRole { get; set; }

        RoleManager<IdentityRole> _roleManager { get; set; }
        UserManager<IdentityUser> _userManager { get; set; }
        SignInManager<IdentityUser> _signInManager { get; set; }
    }
}