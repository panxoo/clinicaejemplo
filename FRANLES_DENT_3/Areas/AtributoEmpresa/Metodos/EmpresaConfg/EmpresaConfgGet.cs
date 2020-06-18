using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Sistema;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg
{
    public class EmpresaConfgGet
    {
        public EmpresaConfgGet(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<DataCollect<SucursalConfManList>> GetSucursalConfMant()
        {
            DataCollect<SucursalConfManList> _model = new DataCollect<SucursalConfManList>();

            _model.ListDatos = await _lstGnrl._context.Sucursals.IgnoreQueryFilters()
                                            .Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                            .Select(s => new SucursalConfManList
                                            {
                                                Nombre = s.Nombre,
                                                Principal = s.Principal,
                                                Direccion = s.Direccion,
                                                SucursalId = s.SucursalId,
                                                DistritoNom = s.Distrito.Name,
                                                ProvinciaNom = s.Distrito.Provincia.Name,
                                                DepartamentoNom = s.Distrito.Provincia.Departamento.Name
                                            }).OrderByDescending(o => o.Principal).ThenBy(t => t.Nombre).ToListAsync();

            _model.Metodo = VarGnrl.GetModuloKey("Conf_Empres");
            _model.ModAct = VarGnrl.GetModuloActionKey("Conf_Empres", "Lst");

            return _model;
        }

        public async Task<RetornoAction> GetSucursalConfEdit(string id, string action)
        {
            RetornoAction retornoAction = new RetornoAction();

            if (string.IsNullOrEmpty(id))
            {
                return new RetornoAction { Code = 2 };
            }

            SucursalConfEditInput _model = new SucursalConfEditInput();

            _model.Input = await _lstGnrl._context.Sucursals.FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.SucursalId.Equals(id));

            if (_model.Input == null)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en el registro, volver abrir pantalla para registro." };
            }

            _model.LocalizacionGnrl.DistriId = _model.Input.DistritoId;
            _model.LocalizacionGnrl.ProvinId = _model.Input.DistritoId.Substring(0, 4);
            _model.LocalizacionGnrl.DepartId = _model.Input.DistritoId.Substring(0, 2);

            CargaLocation cargaLocation = new CargaLocation();

            _model.LocalizacionGnrl.LstDistri = await cargaLocation.ObtenerLocalizacion(_model.LocalizacionGnrl.ProvinId, _lstGnrl._context);

            _model.LocalizacionGnrl.LstProvin = await cargaLocation.ObtenerLocalizacion(_model.LocalizacionGnrl.DepartId, _lstGnrl._context);

            _model.LocalizacionGnrl.LstDepart = await cargaLocation.ObtenerLocalizacion("", _lstGnrl._context);

            _model.Metodo = VarGnrl.GetModuloKey("Conf_Empres");
            _model.ModAct = VarGnrl.GetModuloActionKey("Conf_Empres", action);

            return new RetornoAction { Code = 0, Parametro = _model };
        }

        public async Task<RetornoAction> GetSucursalConf(string id, string action)
        {
            RetornoAction retornoAction = new RetornoAction();
            SucursalConfInput _model = new SucursalConfInput();

            if (string.IsNullOrEmpty(id))
            {
                return new RetornoAction { Code = 2 };
            }

            _model.Input = await _lstGnrl._context.Sucursals.IgnoreQueryFilters().FirstOrDefaultAsync(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.SucursalId.Equals(id));

            if (_model.Input == null)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error en el registro, volver abrir pantalla para registro." };
            }

            _model.Departamento = await _lstGnrl._context.Distritos.Where(w => w.DistritoId.Equals(_model.Input.DistritoId))
                                                                   .Select(s => s.Name + " / " + s.Provincia.Name + " / " + s.Provincia.Departamento.Name)
                                                                   .FirstOrDefaultAsync();

            if (await _lstGnrl._context.Sucursal_Area_Atencions.AnyAsync(w => w.Area_Atencion.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.SucursalId.Equals(id)))
            {
                _model.AreaSucursal = await _lstGnrl._context.Sucursal_Area_Atencions.Where(w => w.Area_Atencion.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.SucursalId.Equals(id))
                                                                                     .Select(s => new SucursalConfInput.AreaAtencionSucursal
                                                                                        {
                                                                                            AreaId = s.Area_AtencionId,
                                                                                            AreaNombre = s.Area_Atencion.Nombre,
                                                                                            CantMedicos = _lstGnrl._context.Area_Medicos.Include(i => i.Usuario).Count(c => c.Sucursal_Area_AtencionId.Equals(s.Sucursal_Area_AtencionId))
                                                                                        }).ToListAsync();
            }
         

            if (await _lstGnrl._context.Area_Atencions.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId)))
            {

                _model.AreaAtencionSucursalInputs = await _lstGnrl._context.Area_Atencions.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId))
                                                                   .Select(s => new SucursalConfInput.AreaAtencionSucursalInput
                                                                   {
                                                                       AreaAtencionId = s.Area_AtencionId,
                                                                       AANombre = s.Nombre,
                                                                       SucursalId = id,
                                                                       Activo = _lstGnrl._context.Sucursal_Area_Atencions.Any(a => a.Area_Atencion.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Area_AtencionId.Equals(s.Area_AtencionId) && a.SucursalId.Equals(id))
                                                                   }).ToListAsync();

            }

            _model.Metodo = VarGnrl.GetModuloKey("Conf_Empres");
            _model.ModAct = VarGnrl.GetModuloActionKey("Conf_Empres", action);

            return new RetornoAction { Code = 0, Parametro = _model };

             
        }
    }
}