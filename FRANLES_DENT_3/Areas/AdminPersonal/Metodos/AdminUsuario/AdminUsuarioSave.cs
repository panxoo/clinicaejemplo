using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Metodos.Permisos;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminUsuario
{
    public class AdminUsuarioSave
    {
        public AdminUsuarioSave(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> SaveCreaUsuarioAdd(CreaUsuarioInputPost _model)
        {
            try
            {

                IdentityUser _user = new IdentityUser();

                if (_model.Acceso)
                {
                    RetornoAction retornoAction = new RetornoAction();

                    retornoAction = await AddUser(_model.Nombre_Cuenta, _model.Correo, _model.PerfilId, _model.Contrasena);

                    if (retornoAction.Code != 0)
                    {
                        return retornoAction;
                    }

                    _user = await _lstGnrl._userManager.FindByNameAsync(_model.Nombre_Cuenta.ToString().Trim());

                    var objPrf = await _lstGnrl._context.Perfils.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_model.PerfilId))
                                                                .Select(s => new { isMedic = s.IsMedic, isAssistente = s.IsAsistente })
                                                                .FirstOrDefaultAsync();

                    _model.IsMedic = objPrf.isMedic;
                    _model.IsAsist = objPrf.isAssistente;
                }

                Usuario _usuario = new Usuario
                {
                    UsuarioId = Ulid.NewUlid().ToString(),
                    Nombre_Cuenta = _model.Acceso ? _model.Nombre_Cuenta.Trim() : null,
                    Nombre = _model.Nombre.Trim(),
                    Apellido_Paterno = _model.Apellido_Paterno.Trim(),
                    Apellido_Materno = string.IsNullOrEmpty(_model.Apellido_Materno) ? "" : _model.Apellido_Materno.Trim(),
                    Correo = _model.Correo.Trim(),
                    Direccion = string.IsNullOrEmpty(_model.Direccion) ? "" : _model.Direccion.Trim(),
                    Telefono = string.IsNullOrEmpty(_model.Telefono) ? "" : _model.Telefono.Trim(),
                    Movil = _model.Movil.Trim(),
                    Sexo = _model.Sexo,
                    ClinicaId = _lstGnrl._datosUsuario.ClinicaId,
                    Fecha_Nac = _model.Fecha_Nac,
                    FechaActuali = DateTime.Now.Date,
                    FechaCreacio = DateTime.Now.Date,
                    UserId = _model.Acceso ? _user.Id : null,
                    PerfilId = _model.PerfilId,
                    Activo = true,
                    DistritoId = _model.DistritoId.Trim(),
                    Acceso = _model.Acceso,
                    IsAsist = _model.IsAsist,
                    IsMedic = _model.IsMedic,
                    TipoDocumento = _model.TipoDocumento,
                    NumDocumento = _model.NumDocumento.Trim()
                };

               await _lstGnrl._context.AddAsync(_usuario);

                if (!string.IsNullOrEmpty(_model.DtEmUsr.Nombre))
                {
                    DatosEmergenciaUsuario _dtEmerg = new DatosEmergenciaUsuario
                    {
                        Nombre = _model.DtEmUsr.Nombre.Trim(),
                        Apellido_Paterno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Paterno) ? "" : _model.DtEmUsr.Apellido_Paterno.Trim(),
                        Apellido_Materno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Materno) ? "" : _model.DtEmUsr.Apellido_Materno.Trim(),
                        Movil = string.IsNullOrEmpty(_model.DtEmUsr.Movil) ? "" : _model.DtEmUsr.Movil.Trim(),
                        Parentesco = _model.DtEmUsr.Parentesco,
                        UsuarioId = _usuario.UsuarioId
                    };

                    _lstGnrl._context.Add(_dtEmerg);
                }

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 1, Mensaje = "Error del almacenamiento" };
            }
        }

        public async Task<RetornoAction> SaveCreaUsuarioUpd(CreaUsuarioInputPost _model)
        {
            try
            {
                Usuario _usuario = await _lstGnrl._context.Usuarios.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.UsuarioId.Equals(_model.UsuarioId));
                RetornoAction retornoAction = new RetornoAction();
                IdentityUser _user = new IdentityUser();

                if (_model.Acceso && _model.UserNomExist)
                {
                    _user = await _lstGnrl._userManager.FindByNameAsync(_model.Nombre_Cuenta.ToString().Trim());
                    if (!_model.Accesact)
                    {
                        await ActivaUsr(_user);
                        await Cambiocontraseña(_user, _model.Contrasena);
                    }
                    await new PermisosSave(_lstGnrl).SavePermisosUsr(_model.PerfilId, _lstGnrl._datosUsuario.ClinicaId, _user);
                }

                if (_model.Acceso && !_model.UserNomExist)
                {
                    retornoAction = await AddUser(_model.Nombre_Cuenta, _model.Correo, _model.PerfilId, _model.Contrasena);

                    if (retornoAction.Code != 0)
                    {
                        return retornoAction;
                    }

                    _user = await _lstGnrl._userManager.FindByNameAsync(_model.Nombre_Cuenta.ToString().Trim());
                }

                if (!_model.Acceso && _model.Accesact)
                {
                    _user = await _lstGnrl._userManager.FindByNameAsync(_usuario.Nombre_Cuenta.ToString().Trim());
                    await DesactUsr(_user);
                }

                if (_model.Acceso)
                {
                    var objPrf = await _lstGnrl._context.Perfils.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.PerfilId.Equals(_model.PerfilId))
                                                               .Select(s => new { isMedic = s.IsMedic, isAssistente = s.IsAsistente })
                                                               .FirstOrDefaultAsync();

                    _model.IsMedic = objPrf.isMedic;
                    _model.IsAsist = objPrf.isAssistente;
                }

                bool cambioPrfl = false;

                if (_model.Acceso)
                {
                    if (_usuario.PerfilId != _model.PerfilId)
                    {
                        cambioPrfl = true;
                    }
                }


                _usuario.Nombre = _model.Nombre.Trim();
                _usuario.Apellido_Paterno = _model.Apellido_Paterno.Trim();
                _usuario.Apellido_Materno = string.IsNullOrEmpty(_model.Apellido_Materno) ? "" : _model.Apellido_Materno.Trim();
                _usuario.Correo = _model.Correo.Trim();
                _usuario.Direccion = string.IsNullOrEmpty(_model.Direccion) ? "" : _model.Direccion.Trim();
                _usuario.Movil = string.IsNullOrEmpty(_model.Movil) ? "" : _model.Movil.Trim();
                _usuario.Sexo = _model.Sexo;
                _usuario.Fecha_Nac = _model.Fecha_Nac;
                _usuario.FechaActuali = DateTime.Now.Date;
                _usuario.DistritoId = _model.DistritoId.Trim();
                _usuario.IsAsist = _model.IsAsist;
                _usuario.IsMedic = _model.IsMedic;
                _usuario.Acceso = _model.Acceso;

                if (_model.Acceso)
                {
                    
                    _usuario.PerfilId = _model.PerfilId;
                   
                    if (!_model.UserNomExist)
                    {
                        _usuario.UsuarioId = _user.Id;
                    }
                }

                DatosEmergenciaUsuario _dtEmerg = await _lstGnrl._context.DatosEmergenciaUsuarios.FirstOrDefaultAsync(f => f.UsuarioId.Equals(_model.UsuarioId));

                if (_dtEmerg == null)
                {
                    if (!string.IsNullOrEmpty(_model.DtEmUsr.Nombre))
                    {
                        _dtEmerg = new DatosEmergenciaUsuario
                        {
                            Nombre = _model.DtEmUsr.Nombre.Trim(),
                            Apellido_Paterno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Paterno) ? "" : _model.DtEmUsr.Apellido_Paterno.Trim(),
                            Apellido_Materno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Materno) ? "" : _model.DtEmUsr.Apellido_Materno.Trim(),
                            Movil = string.IsNullOrEmpty(_model.DtEmUsr.Movil) ? "" : _model.DtEmUsr.Movil.Trim(),
                            Parentesco = _model.DtEmUsr.Parentesco,
                            UsuarioId = _usuario.UsuarioId
                        };

                        _lstGnrl._context.Add(_dtEmerg);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_model.DtEmUsr.Nombre))
                    {
                        _dtEmerg.Nombre = _model.DtEmUsr.Nombre.Trim();
                        _dtEmerg.Apellido_Paterno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Paterno) ? "" : _model.DtEmUsr.Apellido_Paterno.Trim();
                        _dtEmerg.Apellido_Materno = string.IsNullOrEmpty(_model.DtEmUsr.Apellido_Materno) ? "" : _model.DtEmUsr.Apellido_Materno.Trim();
                        _dtEmerg.Movil = string.IsNullOrEmpty(_model.DtEmUsr.Movil) ? "" : _model.DtEmUsr.Movil.Trim();
                        _dtEmerg.Parentesco = _model.DtEmUsr.Parentesco;
                    }
                    else
                    {
                        _lstGnrl._context.Remove(_dtEmerg);
                    }
                }

                await _lstGnrl._context.SaveChangesAsync();

                if (_model.Acceso && cambioPrfl )
                {
                    await new PermisosSave(_lstGnrl).SavePermisosUsr(_usuario.PerfilId, _lstGnrl._datosUsuario.ClinicaId, _user);
                }

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 1, Mensaje = "Error del almacenamiento" };
            }
        }

        private async Task<RetornoAction> AddUser(string nombre_cuenta, string correo, string idPerfil, string contrasena)
        {
            IdentityUser usr = new IdentityUser
            {
                UserName = nombre_cuenta.ToString().Trim(),
                Email = correo.ToString().Trim(),
                LockoutEnabled = false
            };

            IdentityResult _resUser = await _lstGnrl._userManager.CreateAsync(usr, contrasena);

            if (!_resUser.Succeeded)
            {
                foreach (IdentityError erro in _resUser.Errors)
                {
                    System.Diagnostics.Debug.WriteLine("Error " + erro.Description);
                }
                return new RetornoAction { Code = 1, Mensaje = "Error en la creación del usuario, favor volver a intentar." };
            }

            IdentityUser _user = await _lstGnrl._userManager.FindByNameAsync(nombre_cuenta.ToString().Trim());

            await AddRolesUsr(_user, idPerfil);

            return new RetornoAction { Code = 0, Mensaje = "" };
        }

        private async Task<bool> AddRolesUsr(IdentityUser usr, string prfl)
        {
            List<string> roles = await _lstGnrl._context.Perfil_Rols.Where(w => w.PerfilId.Equals(prfl) && w.Perfil.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => s.Role.NameRol).ToListAsync();

            await _lstGnrl._userManager.AddToRolesAsync(usr, roles);

            return true;
        }

        private async Task Cambiocontraseña(IdentityUser _user, string _password)
        {
            var _token = await _lstGnrl._userManager.GeneratePasswordResetTokenAsync(_user);
            await _lstGnrl._userManager.ResetPasswordAsync(_user, _token, _password);
        }

        private async Task DesactUsr(IdentityUser _user)
        {
            await _lstGnrl._userManager.SetLockoutEnabledAsync(_user, true);
            await _lstGnrl._userManager.SetLockoutEndDateAsync(_user, DateTime.Now.AddYears(200));
        }

        private async Task ActivaUsr(IdentityUser _user)
        {
            await _lstGnrl._userManager.SetLockoutEnabledAsync(_user, false);
        }
    }
}