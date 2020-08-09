using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg
{
    public class EmpresaConfgVal
    {
        public EmpresaConfgVal(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> ValSucursalConfEdit(SucursalConfEditPost _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.SucursalId))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (!await _lstGnrl._context.Sucursals.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.SucursalId.Equals(_model.SucursalId)))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (await _lstGnrl._context.Sucursals.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && !a.SucursalId.Equals(_model.SucursalId) && a.Nombre.Equals(_model.Nombre)))
                {
                    return new RetornoAction { Code = 1, Mensaje = "El nombre de la clinica ya esiste, se debe ingresar un nombre distinto" };
                }

                return new RetornoAction { Code = 0 };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }

        public async Task<RetornoAction> ValSucursalConfAddArAt(SucursalConfAddArAtPost _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.SucursalId))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (!await _lstGnrl._context.Sucursals.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.SucursalId.Equals(_model.SucursalId)))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (_model.AreaAtencions.Count == 0)
                {
                    return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar el area de atención." };
                }

                List<string> areaAten = await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => s.Area_AtencionId).ToListAsync();

                if (_model.AreaAtencions.Except(areaAten).Any())
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                return new RetornoAction { Code = 0 };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }

        public async Task<RetornoAction> ValRangoHorarioConfDetalle(RangoHorarioConfDetallePost _model, string _accion)
        {
            try
            {

                if (_accion == "Upd")
                {
                    if (string.IsNullOrEmpty(_model.Tipo_HorarioId))
                    {
                        return new RetornoAction { Code = 2, Mensaje = "" };
                    }

                    if (!await _lstGnrl._context.Tipo_Horarios.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Tipo_HorarioId.Equals(_model.Tipo_HorarioId)))
                    {
                        return new RetornoAction { Code = 2 };
                    }

                    if (await _lstGnrl._context.Tipo_Horarios.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && !a.Tipo_HorarioId.Equals(_model.Tipo_HorarioId) && a.Nombre.Equals(_model.Nombre)))
                    {
                        return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar otro Nombre, ya que el tipo de horario ya se encuentra registrado" };
                    }
                }
                else
                {
                    if (await _lstGnrl._context.Tipo_Horarios.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Nombre.Equals(_model.Nombre)))
                    {
                        return new RetornoAction { Code = 1, Mensaje = "Se debe seleccionar otro Nombre, ya que el tipo de horario ya se encuentra registrado" };
                    }
                }

                //DateTime valTime;

                //DateTime.TryParse(_model.Hora_Inicio, out valTime);

                //if(valTime.Date.Year != DateTime.Now.Year)
                //{
                //    return new RetornoAction { Code = 1, Mensaje = "Error en el formato de la fecha de inicio HH:MM" };
                //}


                //DateTime.TryParse(_model.Hora_Fin, out valTime);

                //if (valTime.Date.Year != DateTime.Now.Year)
                //{
                //    return new RetornoAction { Code = 1, Mensaje = "Error en el formato de la fecha de fin HH:MM" };
                //}

                return new RetornoAction { Code = 0 };

            }
            catch(Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }
    }
}