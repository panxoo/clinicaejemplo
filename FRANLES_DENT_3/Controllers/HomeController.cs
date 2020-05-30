using FRANLES_DENT_3.Areas.Principal.Controllers;
using FRANLES_DENT_3.Data;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models;
using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
            lstGnrl._signInManager = signInManager;
            lstGnrl._roleManager = roleManager;
            lstGnrl._userManager = userManager;
            lstGnrl._usuarios = new LibUsuario(lstGnrl);
            _lstGnrl._context = context;

        }
        IListGeneral _lstGnrl;

        public async Task<IActionResult> Index()
        { 
            if (_lstGnrl._signInManager.IsSignedIn(User))
            {
                string idus = _lstGnrl._userManager.GetUserId(User);
                await _lstGnrl._usuarios.DatosSession(HttpContext, idus);
                return RedirectToAction(nameof(PrincipalController.Index), "Principal");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                IdentityError _identityError = await _lstGnrl._usuarios.userLogin(model);


                if (_identityError.Code == "0")
                {
                    IdentityUser _user = await _lstGnrl._userManager.FindByNameAsync(model.Input.Login_Usuario);
                    await _lstGnrl._usuarios.DatosSession(HttpContext, _user.Id);
                    
                    return RedirectToAction(nameof(PrincipalController.Index), "Principal");

                }
                else
                {
                    return View(model);
                }

            }
            return View(model);

 

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }








        public async Task<IActionResult> Setup()
        {
            await CrearRoleDefault();
            await CrearUsuario();
            return View();

        }

        private async Task CrearRoleDefault()
        {
            try
            {


                if (await _lstGnrl._userManager.FindByNameAsync("fco.dl@hotmail.cl") == null)
                {
                    var usr = new IdentityUser
                    {
                        UserName = "fco.dl@hotmail.cl",
                        Email = "fco.dl@hotmail.cl"
                    };
                    var res = await _lstGnrl._userManager.CreateAsync(usr, "123Qaz+");
                }



                string[] roles = { "Admin", "User", "Medico", "Pruebas 1", "Pruebas 2", "Pruebas 3", "Pruebas 4", "Pruebas 5", "Pruebas 6", "Pruebas 7", "Pruebas 8", "Pruebas 9", "Pruebas 10", "Pruebas 11", "Pruebas 12", "Pruebas 13", "Pruebas 14", "Pruebas 15" };

                foreach (string item in roles)
                {
                    bool result = await _lstGnrl._roleManager.RoleExistsAsync(item);
                    if (!result)
                    {
                        await _lstGnrl._roleManager.CreateAsync(new IdentityRole(item));

                    }
                }


                var user = await _lstGnrl._userManager.FindByNameAsync("fco.dl@hotmail.cl");

                bool results = await _lstGnrl._userManager.IsInRoleAsync(user, "Admin");

                if (!results)
                    await _lstGnrl._userManager.AddToRoleAsync(user, "Admin");

            }
            catch (Exception ex)
            {

            }
        }

        private async Task CrearUsuario()
        {
            try
            {

                if (_lstGnrl._context.Clinicas.FirstOrDefault(p => p.Codigo.Equals("DENT4SMILE")) == null)
                {
                    Clinica clinica = new Clinica
                    {
                        ClinicaID = Ulid.NewUlid().ToString(),
                        Nombre = "Dentist 4Smile",
                        Codigo = "DENT4SMILE",
                        Dominio = "asdd",
                        Extencion = "as",
                        MultiSucursal = true
                    };

                    _lstGnrl._context.Add(clinica);
                    _lstGnrl._context.SaveChanges();

                }
                var clinicaId = _lstGnrl._context.Clinicas.Where(p => p.Codigo == "DENT4SMILE").ToList()[0].ClinicaID;


                if (_lstGnrl._context.Perfils.FirstOrDefault(p => p.Nombre.Equals("Administrador")) == null)
                {
                    Perfil perfil = new Perfil
                    {
                        PerfilId = Ulid.NewUlid().ToString(),
                        Nombre = "Administrador",
                        Activo = true,
                        ClinicaId = clinicaId,
                        Descripcion = "Administrador"

                    };
                    _lstGnrl._context.Add(perfil);
                    _lstGnrl._context.SaveChanges();
                }

                var PerfilId = _lstGnrl._context.Perfils.FirstOrDefault(p => p.Nombre.Equals("Administrador")).PerfilId;
                var NetUsr = _lstGnrl._userManager.FindByNameAsync("fco.dl@hotmail.cl").Result.Id;


                if (_lstGnrl._context.Usuarios.FirstOrDefault(p => p.UserId.Equals(NetUsr)) == null)
                {
                    Usuario usr = new Usuario
                    {
                        UsuarioId = Ulid.NewUlid().ToString(),
                        Nombre = "Francisco",
                        Apellido_Paterno = "Duran",
                        Apellido_Materno = "Letelier",
                        ClinicaId = clinicaId,
                        UserId = NetUsr,
                        Nombre_Cuenta = "fco.dl@hotmail.cl",
                        Fecha_Nac = DateTime.Now,
                        PerfilId = PerfilId,
                        Correo = "fco.dl@hotmail.cl",
                        Sexo = "M",
                        Movil = "12313",
                        Acceso = true,
                        Activo = true,
                        DistritoId = "070102",
                        NumDocumento = "asdadasd",
                        TipoDocumento = 1                       

                    };

                    _lstGnrl._context.Add(usr);
                    await _lstGnrl._context.SaveChangesAsync();
                }

                //if (_lstGnrl._context.Perfil_Rol.FirstOrDefault(p => p.PerfilId.Equals(PerfilId)) == null)
                //{
                //    var assd = _lstGnrl._roleManager.Roles.Select(p => p.Id).ToList();

                //    Perfil_Rol pr;

                //    foreach (var ad in assd)
                //    {
                //        pr = new Perfil_Rol
                //        {
                //            PerfilId = PerfilId,
                //            RolId = ad
                //        };

                //        _lstGnrl._context.Add(pr);
                //        _lstGnrl._context.SaveChanges();

                //    }
                //}

                if (_lstGnrl._context.Clinica_Rols.FirstOrDefault(p => p.ClinicaId.Equals(clinicaId)) == null)
                {
                    var assd = _lstGnrl._roleManager.Roles.Select(p => p.Id).ToList();

                    Clinica_Rol cr;

                    foreach (var ad in assd)
                    {
                        cr = new Clinica_Rol
                        {
                            ClinicaId = clinicaId,
                            RolId = ad,
                            Active = true
                        };

                        _lstGnrl._context.Add(cr);
                        _lstGnrl._context.SaveChanges();

                    }
                }



            } 
            catch (Exception ed) 
            {

            }
        }
    }
}