﻿using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminUsuario
{
    public class AdminUsuarioGet
    {
        public AdminUsuarioGet(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<UsuarioMantList> GetUsuarioMant()
        {
            UsuarioMantList _model = new UsuarioMantList();

            _model.ListDatos = await _lstGnrl._context.Usuarios.IgnoreQueryFilters()
                                                             .Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                             .Select(s => new UsuarioMantList.UsuarioMantDt
                                                             {
                                                                 Activo = s.Activo,
                                                                 ClinicaId = s.ClinicaId,
                                                                 Nombre = s.Nombre + " " + s.Apellido_Paterno + " " + s.Apellido_Materno,
                                                                 Sucursal = "A15",
                                                                 Perfil = s.Perfil.Nombre,
                                                                 UsuarioId = s.UsuarioId,
                                                                 UserName = s.Nombre_Cuenta
                                                             }).ToListAsync();

            _model.Metodo = VarGnrl.GetModuloKey("Mant_Usuari");
            _model.ModAct = VarGnrl.GetModuloActionKey("Mant_Usuari", "Lst");

            return _model;
        }

        public async Task<CreaUsuarioInput> GetCreaUsuario(string id, string _accion)
        {
            if (_accion == "Upd")
                if (!await _lstGnrl._context.Usuarios.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.UsuarioId.Equals(id)))
                    return null;

            CreaUsuarioInput model = new CreaUsuarioInput();

            model.Alias = await _lstGnrl._context.Clinicas.Where(p => p.ClinicaID.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                         .Select(s => s.Dominio)
                                                         .FirstOrDefaultAsync();

            model.PerfilesSelect = await _lstGnrl._context.Perfils.Where(p => p.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                            .Select(p => new CreaUsuarioInput.SelPerfil
                                                            {
                                                                Value = p.PerfilId,
                                                                Text = p.Nombre,
                                                                IsMedic = p.IsMedic,
                                                                IsAsistent = p.IsAsistente
                                                            }).ToListAsync();

            string ubigeo = "";

            if (_accion == "Upd")
            {
                model.Input = await _lstGnrl._context.Usuarios.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.UsuarioId.Equals(id));
                model.DatoEmergencia = await _lstGnrl._context.DatosEmergenciaUsuarios.FirstOrDefaultAsync(f => f.UsuarioId.Equals(id));
                model.Accesact = model.Input.Acceso;
                model.UserNomExist = !string.IsNullOrEmpty(model.Input.Nombre_Cuenta);
                ubigeo = model.Input.DistritoId;
                model.Input.Avatar = !string.IsNullOrEmpty(model.Input.Avatar) ? _lstGnrl._datosUsuario.CodClinica + "/417661746172/" + model.Input.Avatar : "DEFAULT/417661746172/default.png";
            }
            else
            {
                model.Input = new Usuario();
                model.Input.Acceso = true;
                model.Input.Avatar = "DEFAULT/417661746172/default.png";
            }

            if (_accion == "Add" || string.IsNullOrEmpty(ubigeo))
            {
                ubigeo = await _lstGnrl._context.Clinicas.Where(f => f.ClinicaID.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => s.DistritoId).FirstOrDefaultAsync();
            }

            model.LocalizacionGnrl.DistriId = ubigeo;
            model.LocalizacionGnrl.ProvinId = ubigeo.Substring(0, 4);
            model.LocalizacionGnrl.DepartId = ubigeo.Substring(0, 2);

            CargaLocation cargaLocation = new CargaLocation();

            model.LocalizacionGnrl.LstDistri = await cargaLocation.ObtenerLocalizacion(model.LocalizacionGnrl.ProvinId, _lstGnrl._context);

            model.LocalizacionGnrl.LstProvin = await cargaLocation.ObtenerLocalizacion(model.LocalizacionGnrl.DepartId, _lstGnrl._context);

            model.LocalizacionGnrl.LstDepart = await cargaLocation.ObtenerLocalizacion("", _lstGnrl._context);

            model.Metodo = VarGnrl.GetModuloKey("Mant_Usuari");
            model.ModAct = VarGnrl.GetModuloActionKey("Mant_Usuari", _accion);
            model.Action = _accion;

            return model;
        }

        public async Task<UsuarioViewInput> GetUsuarioView(string id, string accion)
        {
            UsuarioViewInput _model = new UsuarioViewInput();

            _model.DtUsuario = await _lstGnrl._context.Usuarios.Include(i => i.DatosEmergenciaUsuario).IgnoreQueryFilters().FirstOrDefaultAsync(f => f.UsuarioId.Equals(id) && f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId));

            if (_model.DtUsuario == null)
            {
                return null;
            }

            _model.PerfilDet = await _lstGnrl._context.Perfils.IgnoreQueryFilters().Where(f => f.PerfilId.Equals(_model.DtUsuario.PerfilId) && f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                   .Select(s => s.Nombre).FirstOrDefaultAsync();

            _model.SexoDet = TransfParam.ParamSexo().FirstOrDefault(f => f.Value.Equals(_model.DtUsuario.Sexo)).Text;
            _model.TipoDocumento = TransfParam.ParamTipoDocumento().FirstOrDefault(f => f.Value.Equals(_model.DtUsuario.TipoDocumento.ToString())).Text;

            _model.Departamento = await _lstGnrl._context.Distritos.Where(w => w.DistritoId.Equals(_model.DtUsuario.DistritoId))
                                                                   .Select(s => s.Name + " / " + s.Provincia.Name + " / " + s.Provincia.Departamento.Name)
                                                                   .FirstOrDefaultAsync();

            _model.SucursalSelLst = await _lstGnrl._context.Sucursals.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => new SelectListItem { Text = s.Nombre, Value = s.SucursalId }).ToListAsync();

            if (_model.DtUsuario.DatosEmergenciaUsuario != null)
                _model.ParentescoDet = TransfParam.ParamParentesco().FirstOrDefault(f => f.Value.Equals(_model.DtUsuario.DatosEmergenciaUsuario.Parentesco.ToString())).Text;

            if (_model.DtUsuario.IsMedic)
            {
                _model.DtMedico = await GetObtenerDatosMedico(_model.DtUsuario.UsuarioId);

                _model.EspcialiSelLst = await _lstGnrl._context.Especialidades.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                              .Select(s => new SelectListItem
                                                                              {
                                                                                  Value = s.EspecialidadId.ToString(),
                                                                                  Text = s.Nombre
                                                                              })
                                                                              .ToListAsync();

                _model.HorarioMedicoViewInput = await GetHorarioMedicoView(id);
            
            }

            _model.Metodo = VarGnrl.GetModuloKey("Mant_Usuari");
            _model.ModAct = VarGnrl.GetModuloActionKey("Mant_Usuari", "Vie");

            return _model;
        }

        public async Task<UsuarioViewInput.MedicoView> GetObtenerDatosMedico(string _usuarioId)
        {
            UsuarioViewInput.MedicoView _model = new UsuarioViewInput.MedicoView();

            _model.DatoMedico = await _lstGnrl._context.DatosMedicos.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.UsuarioId.Equals(_usuarioId));

            if (_model.DatoMedico == null)
            {
                _model.DatoMedico = new FRANLES_DENT_3.Models.MedicoDato.DatosMedico();
                _model.EspcialidadMed = new List<SelectListItem>();
            }
            else
            {
                if (await _lstGnrl._context.Especialidad_Medicos.AnyAsync(w => w.DatosMedicoId.Equals(_model.DatoMedico.DatosMedicoId) && w.Especialidad.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
                {
                    _model.EspcialidadMed = await _lstGnrl._context.Especialidad_Medicos.Where(w => w.DatosMedicoId.Equals(_model.DatoMedico.DatosMedicoId) && w.Especialidad.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                        .Select(s => new SelectListItem
                                                                                        {
                                                                                            Value = s.EspecialidadId,
                                                                                            Text = s.Especialidad.Nombre
                                                                                        }).ToListAsync();
                }
                else
                {
                    _model.EspcialidadMed = new List<SelectListItem>();
                }
            }
            

            return _model;
        }

        public async Task<UsuarioViewInput.HorarioMedicoViewParam> GetHorarioMedicoView(string id)
        {
            UsuarioViewInput.HorarioMedicoViewParam _model = new UsuarioViewInput.HorarioMedicoViewParam();

            _model.HorarioMedicoViews = await _lstGnrl._context.HorarioMedicos.Where(w => w.UsuarioId.Equals(id) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                              .Select(s => new UsuarioViewInput.HorarioMedicoView
                                                                              {
                                                                                  HorarioId = s.HorarioMedicoId,
                                                                                  HoraIni = s.Hora_Inicio ,
                                                                                  HoraFin = s.Hora_Fin,
                                                                                  SucursalName = s.Sucursal.Nombre,
                                                                                  DiaSemanaId = s.DiaWeekId
                                                                              }).ToListAsync();


            List<SelectListItem> AreAtencionAux = await _lstGnrl._context.HorarioMedicoAreaAtencions.Where(w => w.HorarioMedico.UsuarioId.Contains(id) && w.HorarioMedico.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                                    .Select(s => new SelectListItem
                                                                                                    {
                                                                                                        Value = s.HorarioMedicoId,
                                                                                                        Text = s.Area_Atencion.Nombre
                                                                                                    }).ToListAsync();

            _model.HorarioMedicoViews.ForEach(f =>
            {
                f.AreaAtencions = AreAtencionAux.Where(w => w.Value.Equals(f.HorarioId)).Select(s => s.Text).ToList();
            });

            return _model;

        }

        public async Task<UsuarioViewInput.HorarioMedicoMantEdit> GetHorarioMedicoEdit(string id)
        {
            UsuarioViewInput.HorarioMedicoMantEdit _model = new UsuarioViewInput.HorarioMedicoMantEdit();

            _model.Sucursals = await _lstGnrl._context.Sucursals.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => new SelectListItem { Text = s.Nombre, Value = s.SucursalId }).ToListAsync();
            _model.Area_Atencions = await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => new SelectListItem { Text = s.Nombre, Value = s.Area_AtencionId }).ToListAsync();
            _model.Tipo_Horarios = await _lstGnrl._context.Tipo_Horarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).ToListAsync();

            _model.HorarioMedicos = await _lstGnrl._context.HorarioMedicos.Where(w => w.UsuarioId.Equals(id) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                          .Select(s => new UsuarioViewInput.HorarioMedicoTableMantEdit
                                                                          {
                                                                              DiaWeekId = s.DiaWeekId,
                                                                              NombreTipoHorario = s.Tipo_Horario.Nombre,
                                                                              Tipo_HorarioId = s.Tipo_HorarioId,
                                                                              SucursalId = s.SucursalId,
                                                                              NombreSucursal = s.Sucursal.Nombre,
                                                                              Hora_Inicio = s.Hora_Inicio,
                                                                              Hora_Fin = s.Hora_Fin,
                                                                              HorarioMedicoId = s.HorarioMedicoId
                                                                          }).ToListAsync();

            _model.HorarioMedicos.ForEach(async f  =>
            {
            f.Area_AtencionsSel = await _lstGnrl._context.HorarioMedicoAreaAtencions.Where(w => w.HorarioMedicoId.Equals(f.HorarioMedicoId))
                                                                                    .Select(s => new SelectListItem
                                                                                    {
                                                                                        Text = s.Area_Atencion.Nombre,
                                                                                        Value = s.Area_AtencionId
                                                                                    }).ToListAsync();
            });

            return _model;
        }

        public async Task<UsuarioViewInput.HorarioMedicoMantEdit> GetDetalleHorarioMedico(string id)
        {
            UsuarioViewInput.HorarioMedicoMantEdit _model = new UsuarioViewInput.HorarioMedicoMantEdit();

            _model.Area_Atencions = await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                          .Select(s => new SelectListItem
                                                                          {
                                                                              Text = s.Nombre,
                                                                              Value = s.Area_AtencionId
                                                                          }).ToListAsync();

            _model.Tipo_Horarios = await _lstGnrl._context.Tipo_Horarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).ToListAsync();

            _model.Sucursals = await _lstGnrl._context.Sucursals.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                .Select(s => new SelectListItem
                                                                {
                                                                    Value = s.SucursalId,
                                                                    Text = s.Nombre
                                                                }).ToListAsync();

            _model.HorarioMedicos = await _lstGnrl._context.HorarioMedicos.Where(w => w.UsuarioId.Equals(id) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                          .Select( s => new UsuarioViewInput.HorarioMedicoTableMantEdit
                                                                          {
                                                                              DiaWeekId = s.DiaWeekId,
                                                                              SucursalId = s.SucursalId,
                                                                              NombreSucursal = s.Sucursal.Nombre,
                                                                              HorarioMedicoId = s.HorarioMedicoId,
                                                                              Hora_Fin = s.Hora_Fin,
                                                                              Hora_Inicio = s.Hora_Inicio,
                                                                              Tipo_HorarioId = s.Tipo_HorarioId,
                                                                              NombreTipoHorario = s.Tipo_Horario.Nombre
                                                                              ,Area_AtencionsSel =  _lstGnrl._context.HorarioMedicoAreaAtencions.Where(w => w.HorarioMedicoId.Equals(s.HorarioMedicoId))
                                                                                                                                                     .Select(s1 => new SelectListItem { Text = s1.Area_Atencion.Nombre, Value = s1.Area_AtencionId}).ToList()
                                                                          }).ToListAsync();


            return _model;

        }

        //public async Task<List<UsuarioViewInput.SucursalAreaAtencion>> GetObtenerSucursalAreaAtenAsignadosUsuario(string idUsr)
        //{
        //    List<UsuarioViewInput.SucursalAreaAtencion> _model = new List<UsuarioViewInput.SucursalAreaAtencion>();

        //    List<string> sucursalUsrs;

        //    if (await _lstGnrl._context.Area_Medicos.AnyAsync(a => a.UsuarioId.Equals(idUsr) && a.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
        //    {
        //        sucursalUsrs = await _lstGnrl._context.Area_Medicos.Where(w => w.UsuarioId.Equals(idUsr) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
        //                                                      .Select(s => s.Sucursal_Area_Atencion.SucursalId).ToListAsync();

        //        foreach (string sucursalUsr in sucursalUsrs)
        //        {
        //            UsuarioViewInput.SucursalAreaAtencion _dt;

        //            _dt = await _lstGnrl._context.Area_Medicos.Where(w => w.UsuarioId.Equals(idUsr) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.Sucursal_Area_Atencion.SucursalId.Equals(sucursalUsr))
        //                                               .Select(s => new UsuarioViewInput.SucursalAreaAtencion
        //                                               {
        //                                                   SucursalId = s.Sucursal_Area_Atencion.Sucursal.SucursalId,
        //                                                   SucursalNom = s.Sucursal_Area_Atencion.Sucursal.Nombre,
        //                                                   SucursalDirc = s.Sucursal_Area_Atencion.Sucursal.Direccion + ". " + s.Sucursal_Area_Atencion.Sucursal.Distrito.Name + ". " + s.Sucursal_Area_Atencion.Sucursal.Distrito.Provincia.Name
        //                                                                  + ". " + s.Sucursal_Area_Atencion.Sucursal.Distrito.Provincia.Departamento.Name
        //                                               }).FirstOrDefaultAsync();

        //            _dt.Area_Atencions = await _lstGnrl._context.Area_Medicos.Where(w => w.UsuarioId.Equals(idUsr) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.Sucursal_Area_Atencion.SucursalId.Equals(sucursalUsr))
        //                                                                   .Select(s => new SelectListItem
        //                                                                   {
        //                                                                       Value = s.Sucursal_Area_Atencion.Area_AtencionId,
        //                                                                       Text = s.Sucursal_Area_Atencion.Area_Atencion.Nombre
        //                                                                   }).ToListAsync();

        //            _model.Add(_dt);
        //        }
        //    }

        //    return _model;
        //}

        //public async Task<UsuarioViewInput.SucursalAreaAtencion> GetAreaAtencionSucursal(string sucursalid, string usrId)
        //{
        //    UsuarioViewInput.SucursalAreaAtencion _model = new UsuarioViewInput.SucursalAreaAtencion();

        //    if (await _lstGnrl._context.Sucursal_Area_Atencions.AnyAsync(a => a.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.SucursalId.Equals(sucursalid)))
        //    {
        //        _model = await _lstGnrl._context.Sucursal_Area_Atencions.Where(w => w.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.SucursalId.Equals(sucursalid))
        //                                                                .Select(s => new UsuarioViewInput.SucursalAreaAtencion
        //                                                                {
        //                                                                    SucursalId = s.SucursalId,
        //                                                                    SucursalNom = s.Sucursal.Nombre,
        //                                                                    SucursalDirc = s.Sucursal.Direccion + ". " + s.Sucursal.Distrito.Name + ". " + s.Sucursal.Distrito.Provincia.Name
        //                                                                      + ". " + s.Sucursal.Distrito.Provincia.Departamento.Name
        //                                                                }).FirstOrDefaultAsync();

        //        _model.Area_Atencions = await _lstGnrl._context.Sucursal_Area_Atencions.Where(w => w.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.SucursalId.Equals(sucursalid))
        //                                                                               .Select(s => new SelectListItem
        //                                                                               {
        //                                                                                   Value = s.Area_AtencionId,
        //                                                                                   Text = s.Area_Atencion.Nombre,
        //                                                                                   Selected = false
        //                                                                               }).ToListAsync();

        //        if (await _lstGnrl._context.Area_Medicos.AnyAsync(a => a.UsuarioId.Equals(usrId) && a.Sucursal_Area_Atencion.SucursalId.Equals(sucursalid) && a.Sucursal_Area_Atencion.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
        //        {
        //            List<string> areaAsignada = await _lstGnrl._context.Area_Medicos.Where(w => w.UsuarioId.Equals(usrId) && w.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.Sucursal_Area_Atencion.SucursalId.Equals(sucursalid))
        //                                                                         .Select(s => s.Sucursal_Area_Atencion.Area_AtencionId).ToListAsync();

        //            if (areaAsignada.Count > 0)
        //            {
        //                _model.Area_Atencions.ForEach(f =>
        //                {
        //                    f.Selected = areaAsignada.Contains(f.Value);
        //                });
        //            }
        //        }
        //    }

        //    return _model;
        //}
    }
}