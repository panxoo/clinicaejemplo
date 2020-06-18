using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminUsuario
{
    public class AdminUsuarioVal
    {
        public AdminUsuarioVal(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> ValCreaUsuarioInicial(CreaUsuarioInputPost _model, string accion)
        {
            try
            {
                if (accion == "Upd")
                {
                    if (string.IsNullOrWhiteSpace(_model.UsuarioId))
                    {
                        return new RetornoAction { Code = 2, Mensaje = "Error en actualización, el registro de usuario no existe." };
                    }

                    if (!await _lstGnrl._context.Usuarios.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.UsuarioId.Equals(_model.UsuarioId)))
                    {
                        return new RetornoAction { Code = 2, Mensaje = "Error en actualización, el registro de usuario no existe." };
                    }
                }

                if (accion == "Add")
                {
                    if (await _lstGnrl._context.Usuarios.IgnoreQueryFilters().AnyAsync(a => a.NumDocumento.Equals(_model.NumDocumento.Trim()) && a.TipoDocumento.Equals(_model.TipoDocumento)))
                    {
                        return new RetornoAction { Code = 1, Mensaje = "El numero de documento ya existe registrado." };
                    }
                }

                if (_model.Acceso)
                {
                    if (!await _lstGnrl._context.Perfils.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.PerfilId.Equals(_model.PerfilId)))
                    {
                        return new RetornoAction { Code = 2, Mensaje = "" };
                    }
                }
                else
                {
                    if (!_model.IsAsist && !_model.IsMedic)
                    {
                        return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar el tipo de usuario que se creara, si es medico o/y asistente." };
                    }
                }

                if (!TransfParam.ValParmSexo(_model.Sexo) || (!string.IsNullOrEmpty(_model.DtEmUsr.Nombre) && !TransfParam.ValParmParentesc(_model.DtEmUsr.Parentesco.ToString())))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.Message);

                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }

        public async Task<RetornoAction> ValCreaUsuarioComplementaria(CreaUsuarioInputPost _model, string accion, string alias)
        {
            try
            {
                if (_model.Acceso)
                {
                    if (accion == "Add" || !_model.UserNomExist)
                    {
                        if (!System.Text.RegularExpressions.Regex.Match(_model.Nombre_Cuenta.Substring(0, _model.Nombre_Cuenta.Length - alias.Length - 1), @"^[A-Z0-9a-zñÑ\\.\\_]*$").Success)
                        {
                            return new RetornoAction { Code = 1, Mensaje = "El nombre de usuario solo debe contener letras, numeros y los caracteres (.-)" };
                        }

                        if (await _lstGnrl._context.Usuarios.IgnoreQueryFilters().AnyAsync(a => a.Nombre_Cuenta.Equals(_model.Nombre_Cuenta.ToString().Trim()) && a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
                        {
                            return new RetornoAction { Code = 1, Mensaje = "El nombre de cuenta ingresado ya existe." };
                        }

                        if (await _lstGnrl._userManager.FindByNameAsync(_model.Nombre_Cuenta.ToString().Trim()) != null)
                        {
                            return new RetornoAction { Code = 1, Mensaje = "El nombre de cuenta ingresado ya existe." };
                        }
                    }
                    if (accion == "Add" || !_model.Accesact)
                    {
                        if (!(await new PasswordValidator<IdentityUser>().ValidateAsync(_lstGnrl._userManager, null, _model.Contrasena)).Succeeded)
                        {
                            return new RetornoAction { Code = 1, Mensaje = "La contraseña no cumple ." };
                        }
                    }
                }

                return new RetornoAction { Code = 0, Mensaje = "" };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.Message);

                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }

        public async Task<RetornoAction> ValDetalleUsuarioMedUpd(UsuarioViewPost.UsuarioMedicoPost _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.UsuarioId))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (!await _lstGnrl._context.Usuarios.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.UsuarioId.Equals(_model.UsuarioId) && a.IsMedic))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (_model.EspecialIds == null || _model.EspecialIds.Count == 0)
                {
                    return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar especialidad del medico" };
                }

                if (_model.EspecialIds.Except(await _lstGnrl._context.Especialidades.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(a => a.EspecialidadId.ToString()).ToListAsync()).Any())
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }


                return new RetornoAction { Code = 0, Mensaje = "" };

            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }
    }
}