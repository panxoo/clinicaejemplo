using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Models.SessionUser;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Libreria
{
    public class ListGeneral : IListGeneral
    {
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; set; }

        public List<SelectListItem> _selectList { get; set; }
        public DatosUsuarioSession _datosUsuario { get; set; }
        public ApplicationDbContext _context { get; set; }
        public IdentityError _identityError { get; set; }
        public LibUsuario _usuarios { get; set; }
        public UsersRoles _usersRole { get; set; }
    }
}