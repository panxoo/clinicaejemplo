using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        #region Crea Usuario

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

        #endregion Crea Usuario

        #region Usuario View

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
                _model.ShedulerMedMantEditParam = await GetHorarioMedicoParam();
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

            _model.MultiSucursal = _lstGnrl._datosUsuario.MultiSucursal;

            _model.HorarioMedicoViews = await _lstGnrl._context.ViewHorarioMedicoDateWeeks.Where(w => w.UsuarioId.Equals(id) && w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                        .Select(s => new UsuarioViewInput.HorarioMedicoView
                                                                                        {
                                                                                            HorarioId = s.HorarioMedicoId,
                                                                                            FechaIni = s.FechaInicio,
                                                                                            FechaFin = s.FechaFin,
                                                                                            SucursalName = s.SucursalName,
                                                                                            DiaSemanaId = s.DiaWeekId,
                                                                                            SucursalId = s.SucursalId,
                                                                                            Tipo_HorarioId = s.Tipo_HorarioId
                                                                                        }).ToListAsync();

            var AreAtencionAux = await _lstGnrl._context.HorarioMedicoAreaAtencions.Where(w => w.HorarioMedico.UsuarioId.Contains(id) && w.HorarioMedico.Usuario.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                                    .Select(s => new
                                                                                                    {
                                                                                                        Value = s.Area_AtencionId,
                                                                                                        Text = s.Area_Atencion.Nombre,
                                                                                                        HrMe = s.HorarioMedicoId
                                                                                                    }).ToListAsync();



            _model.HorarioMedicoViews.ForEach(f =>
            {
                f.AreaAtencions = AreAtencionAux.Where(w => w.HrMe.Equals(f.HorarioId)).Select(s => new SelectListItem { Text = s.Text, Value = s.Value }).ToList();
            });


            _model.SchedulerMarkeds = _model.HorarioMedicoViews.Select(s => new SchedulerMarked
                                            {
                                                start_date = s.FechaIni.ToString("yyyy-MM-ddTHH:mm:ss"),
                                                end_date = s.FechaFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                                                css = "sel-sched1",
                                                type = "dhx_time_block",
                                                html = "" 
            }).ToList();


           

            return _model;
        }

        public async Task<UsuarioViewInput.HorarioMedicoMantEditParam> GetHorarioMedicoParam()
        {
            UsuarioViewInput.HorarioMedicoMantEditParam _model = new UsuarioViewInput.HorarioMedicoMantEditParam();

            _model.MultiSucursal = _lstGnrl._datosUsuario.MultiSucursal;

            _model.Tipo_Horarios = await _lstGnrl._context.Tipo_Horarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)).ToListAsync();

            if (_lstGnrl._datosUsuario.MultiSucursal)
            {
                _model.Area_Atencions = (await _lstGnrl._context.Sucursal_Area_Atencions.Include(i => i.Area_Atencion).Include(i => i.Sucursal)
                                                                                      .Where(w => w.Area_Atencion.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.Sucursal.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                                      .Select(s => new { s.Area_AtencionId, Area_Atencion_Nom = s.Area_Atencion.Nombre, s.SucursalId }).ToListAsync())
                                                                                      .GroupBy(g => new { g.Area_Atencion_Nom, g.Area_AtencionId })
                                                                                      .Select(s => new UsuarioViewInput.AreAtenSucruSelect
                                                                                      {
                                                                                          Text = s.Key.Area_Atencion_Nom,
                                                                                          Value = s.Key.Area_AtencionId,
                                                                                          Sucursals = string.Join("|", s.Select(t => t.SucursalId))
                                                                                      }).ToList();

                _model.Sucursals = await _lstGnrl._context.Sucursals.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                    .Select(s => new SelectListItem { Text = s.Nombre, Value = s.SucursalId, Disabled = s.Principal })
                                                                    .OrderBy(o => o.Text).OrderByDescending(o => o.Disabled).ToListAsync();
            }
            else
            {
                _model.Area_Atencions = await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                     .Select(s => new UsuarioViewInput.AreAtenSucruSelect
                                                                     {
                                                                         Text = s.Nombre,
                                                                         Value = s.Area_AtencionId,
                                                                         Sucursals = ""
                                                                     }).ToListAsync();
            }

            return _model;
        }

        #endregion Usuario View
    }
}