using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Permisos;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
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


            model.PerfilesSelect =await _lstGnrl._context.Perfils.Where(p => p.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
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
                model.Accesact = true;
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

    }
}
