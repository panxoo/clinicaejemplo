using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Empresa;
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

                if (_model.EspecialIds.Except(await _lstGnrl._context.Especialidades.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(a => a.EspecialidadId).ToListAsync()).Any())
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

        public async Task<RetornoAction> ValDetalleUsuarioHorarioMedico(UsuarioViewPost.UsuarioHorarioMedicoPost _model, string accion)
        {
            try
            {

                if (string.IsNullOrEmpty(_model.UsuarioId))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if(!TransfParam.ValParmDiaSemana(_model.MHE_DiaWeek.ToString()))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (!await _lstGnrl._context.Usuarios.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.UsuarioId.Equals(_model.UsuarioId) && a.IsMedic))
                {
                    return new RetornoAction { Code = 2, Mensaje = "Error de usuario se debe seleccionar un usuario con el perfil de medico" };
                }

                if (accion == "Mod")
                {
                    if (string.IsNullOrEmpty(_model.MedicoHorarioId))
                    {
                        return new RetornoAction { Code = 1, Mensaje = "Error en seleccionar el tipo de horario a modificar" };
                    }

                    if (!await _lstGnrl._context.HorarioMedicos.AnyAsync(a => a.HorarioMedicoId.Equals(_model.MedicoHorarioId) && a.UsuarioId.Equals(_model.UsuarioId) && a.Usuario.IsMedic && a.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
                        {
                        return new RetornoAction { Code = 2, Mensaje = "Error en seleccionar el tipo de horario a modificar" };
                    }
                }

                if(_lstGnrl._datosUsuario.MultiSucursal)
                {
                    if (string.IsNullOrEmpty(_model.MHE_Sucursal))
                    {
                        return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar sucursal para el horario" };
                    }

                    if (!await _lstGnrl._context.Sucursals.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.SucursalId.Equals(_model.MHE_Sucursal)))
                    {
                        return new RetornoAction { Code = 2, Mensaje = "" };
                    }

                    if (_model.AreaAtencion.Except(await _lstGnrl._context.Sucursal_Area_Atencions.Where(w => w.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.SucursalId.Equals(_model.MHE_Sucursal))
                                                                                                   .Select(a => a.Area_AtencionId).Distinct().ToListAsync()).Any())
                    {
                        return new RetornoAction { Code = 2, Mensaje = "" };
                    }

                }
                else
                {
                    if (_model.AreaAtencion.Except(await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(a => a.Area_AtencionId).ToListAsync()).Any())
                    {
                        return new RetornoAction { Code = 2, Mensaje = "" };
                    }
                }

                bool valhora = true;

                if(accion == "Mod")
                {
                    if (!await _lstGnrl._context.HorarioMedicos.AnyAsync(a => a.HorarioMedicoId.Equals(_model.MedicoHorarioId) && a.UsuarioId.Equals(_model.UsuarioId) && 
                                                                             (!a.Tipo_HorarioId.Equals(_model.MHE_Tipo_Horario) || !a.SucursalId.Equals(_model.MHE_Sucursal) || !a.DiaWeekId.Equals(_model.MHE_DiaWeek))))
                    {
                        valhora = false;
                    }
                }

                if(valhora)
                {
                    var hora = await _lstGnrl._context.Tipo_Horarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.Tipo_HorarioId.Equals(_model.MHE_Tipo_Horario))
                                                            .Select(s => new { horaini = s.Hora_Inicio, horafin = s.Hora_Fin }).FirstOrDefaultAsync();

                    DateTime dia = new DateTime(2020, 7, 20);
                    DateTime diaIni = dia.AddDays(_model.MHE_DiaWeek) + hora.horaini;

                    DateTime diaFin = dia.AddDays(hora.horafin < hora.horaini ? _model.MHE_DiaWeek : _model.MHE_DiaWeek) + hora.horafin;


                    if (accion == "Mod")
                    {

                        if( await _lstGnrl._context.ViewHorarioMedicoDateWeeks.AnyAsync(w => w.UsuarioId.Equals(_model.UsuarioId) && w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) &&
                                                                                                       ((diaIni >= w.FechaInicio && diaIni < w.Fecha_Fin) || (diaFin > w.Fecha_Inicio && diaFin <= w.FechaFin) || (w.FechaInicio >= diaIni && w.FechaInicio < diaFin)) && 
                                                                                                       !w.HorarioMedicoId.Equals(_model.MedicoHorarioId)))
                        {
                            return new RetornoAction { Code = 1, Mensaje = "El horario ingresado ya se encuentra seleccionado, favor de seleccionar un tipo de horario distinto o crear un nuevo tipo de horario" };

                        }
                    }
                    else
                    {
                        if (await _lstGnrl._context.ViewHorarioMedicoDateWeeks.AnyAsync(w => w.UsuarioId.Equals(_model.UsuarioId) && w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) &&
                                                                                                                               ((diaIni >= w.FechaInicio && diaIni < w.Fecha_Fin) || (diaFin > w.Fecha_Inicio && diaFin <= w.FechaFin) || (w.FechaInicio >= diaIni && w.FechaInicio < diaFin))))
                        {
                            return new RetornoAction { Code = 1, Mensaje = "El horario ingresado ya se encuentra seleccionado, favor de seleccionar un tipo de horario distinto o crear un nuevo tipo de horario" };
                        }
                    }
                }


                return new RetornoAction { Code =0, Mensaje = "" };

            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }
    }
}