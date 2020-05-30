using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Models.SessionUser;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Libreria
{
    public class LibUsuario : ListGeneral
    {
        public LibUsuario(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        public LibUsuario(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRole = new UsersRoles();
        }

        public LibUsuario(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext _context, IListGeneral lstGnrl)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usersRole = new UsersRoles();
            _lstGnrl = lstGnrl;
            _lstGnrl._context = _context;
        }

        private IListGeneral _lstGnrl;

        internal async Task<IdentityError> userLogin(LoginViewModels _model)
        {
            IdentityError identityError = new IdentityError();

            try
            {
                SignInResult result = await _lstGnrl._signInManager.PasswordSignInAsync(_model.Input.Login_Usuario, _model.Input.Login_Contrasena, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {

                    identityError.Code = "0";
                }
              else if(result.IsLockedOut)
                {
                    identityError.Code = "2";
                    identityError.Description = "El usuario se encuentra bloqueado";
                }
                else
                {
                    identityError.Code = "1";
                    identityError.Description = "Correo o contraseña invalidos";
                }
            }
            catch (Exception ex)
            {
                identityError.Code = "3";
                identityError.Description = "Error de sistema";
            }

           
            return identityError;
        }

        private async Task<DatosUsuarioSession> SessionUser(string iduser)
        {
            //DatosUsuario result = (from usrd in _lstGnrl._context.Usuarios
            //       join clnd in _lstGnrl._context.Clinicas on usrd.ClinicaId equals clnd.ClinicaID
            //       join prfl in _lstGnrl._context.Perfils on usrd.PerfilId equals prfl.PerfilId
            //       where usrd.UserId.Equals(iduser)
            //       select new DatosUsuario
            //       {
            //           Id = iduser,
            //           Perfil = prfl.Nombre,
            //           PerfilId = prfl.PerfilId,
            //           UserName = usrd.Nombre_Cuenta,
            //           Nombre = usrd.Nombre + ' ' + usrd.Apellido_Paterno,
            //           Clinica = clnd.Nombre,
            //           ClinicaId = clnd.ClinicaID
            //       }).First();

            DatosUsuarioSession result = await _lstGnrl._context.Usuarios.Include(i => i.Clinica)
                                                                        .Include(i => i.Perfil)
                                                                        .Where(w => w.UserId.Equals(iduser))
                                                                        .Select(s => new DatosUsuarioSession 
                                                                        {
                                                                            Id = s.UsuarioId,
                                                                            Perfil = s.Perfil.Nombre,
                                                                            PerfilId = s.PerfilId,
                                                                            UserName = s.Nombre_Cuenta,
                                                                            Nombre = s.Nombre + ' ' + s.Apellido_Paterno,
                                                                            Clinica = s.Clinica.Nombre,
                                                                            ClinicaId = s.ClinicaId,
                                                                            MultiSucursal = s.Clinica.MultiSucursal,
                                                                            CodClinica = s.Clinica.Codigo,
                                                                            Avatar = s.Avatar
                                                                        }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<DatosUsuarioSession> DatosSession(HttpContext httpContext)
        {
            if (httpContext.Session.Keys.Contains("Users"))
            {
                return JsonConvert.DeserializeObject<DatosUsuarioSession>(httpContext.Session.GetString("Users"));
            }
            else
            {
                var id = ((ClaimsIdentity)httpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier);
                DatosUsuarioSession dt = await SessionUser(id.Value);

                httpContext.Session.SetString("Users", JsonConvert.SerializeObject(dt));
                return dt;
            }
        }

        public async Task DatosSession(HttpContext httpContext, string id)
        {
            if (httpContext.Session.Keys.Contains("Users")) httpContext.Session.Remove("Users");

            DatosUsuarioSession dt =await SessionUser(id);

            httpContext.Session.SetString("Users", JsonConvert.SerializeObject(dt));
        }

        public DatosUsuarioSession GetDatosSession(HttpContext httpContext)
        {
            if (httpContext.Session.Keys.Contains("Users"))
            {
                return JsonConvert.DeserializeObject<DatosUsuarioSession>(httpContext.Session.GetString("Users"));
            }
            else
            {
               return new DatosUsuarioSession();
            }
        }
    }
}