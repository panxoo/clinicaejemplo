using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Models.Empresa;
using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg
{
    public class EmpresaConfgSave
    {
        public EmpresaConfgSave(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> SaveSucursalConfEdit(SucursalConfEditPost _model)
        {
            try
            {
                Sucursal dt = await _lstGnrl._context.Sucursals.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.SucursalId.Equals(_model.SucursalId));

                dt.Nombre = _model.Nombre.Trim();
                dt.DistritoId = _model.DistritoId;
                dt.Direccion = _model.Direccion.Trim();
                dt.Telefono = _model.Telefono.Trim();
                dt.Telefono2 = string.IsNullOrEmpty(_model.Telefono2) ? "" : _model.Telefono2.Trim();
                dt.CorreoSucursal = string.IsNullOrEmpty(_model.CorreoSucursal) ? "" : _model.CorreoSucursal.Trim();
                dt.CorreoSucursal2 = string.IsNullOrEmpty(_model.CorreoSucursal2) ? "" : _model.CorreoSucursal2.Trim();

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0 };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en almacenamiento, comunicar con administrador" };
            }
        }

        public async Task<RetornoAction> SaveSucursalConfAddArAt(SucursalConfAddArAtPost _model)
        {
            try
            {
                List<Sucursal_Area_Atencion> dt = await _lstGnrl._context.Sucursal_Area_Atencions.Where(w => w.SucursalId.Equals(_model.SucursalId) && w.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                                 .IgnoreQueryFilters().ToListAsync();

                foreach (Sucursal_Area_Atencion saa in dt.Where(w => !w.Activo && _model.AreaAtencions.Contains(w.Area_AtencionId)))
                {
                    saa.Activo = true;
                }

                List<Sucursal_Area_Atencion> SucursalArAtAdd = new List<Sucursal_Area_Atencion>();

                foreach (string aaid in _model.AreaAtencions.Except(dt.Select(s => s.Area_AtencionId)))
                {
                    SucursalArAtAdd.Add(new Sucursal_Area_Atencion
                    {
                        Sucursal_Area_AtencionId = Ulid.NewUlid().ToString(),
                        Area_AtencionId = aaid,
                        SucursalId = _model.SucursalId,
                        Activo = true
                    });
                }

                if (SucursalArAtAdd.Count > 0)
                    await _lstGnrl._context.AddRangeAsync(SucursalArAtAdd);

                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0 };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en almacenamiento, comunicar con administrador" };
            }
        }

        public async Task<RetornoAction> SaveAddRangoHorarioConfDetalle(RangoHorarioConfDetallePost _model)
        {
            try
            {
                Tipo_Horario _dato = new Tipo_Horario
                {
                    Tipo_HorarioId = Ulid.NewUlid().ToString(),
                    Nombre = _model.Nombre.Trim(),
                    Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim(),
                    Hora_Inicio = _model.Hora_Inicio,
                    Hora_Fin = _model.Hora_Fin,
                    ClinicaId = _lstGnrl._datosUsuario.ClinicaId
                };

                await _lstGnrl._context.AddAsync(_dato);
                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0 };
            }
            catch(Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en almacenamiento, comunicar con administrador" };
            }
        }

        public async Task<RetornoAction> SaveUpdRangoHorarioConfDetalle(RangoHorarioConfDetallePost _model)
        {
            try
            {
                Tipo_Horario _dato =await _lstGnrl._context.Tipo_Horarios.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.Tipo_HorarioId.Equals(_model.Tipo_HorarioId));

                bool cambioHora = _dato.Hora_Inicio != _model.Hora_Inicio || _dato.Hora_Fin != _model.Hora_Fin;

                _dato.Nombre = _model.Nombre.Trim();
                _dato.Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim();
                _dato.Hora_Inicio = _model.Hora_Inicio;
                _dato.Hora_Fin = _model.Hora_Fin;
                

                 
                await _lstGnrl._context.SaveChangesAsync();

                return new RetornoAction { Code = 0 };
            }
            catch (Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en almacenamiento, comunicar con administrador" };
            }
        }
    }
}