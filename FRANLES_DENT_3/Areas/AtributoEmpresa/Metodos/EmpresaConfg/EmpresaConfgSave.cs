using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Models.Empresa;
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
    }
}
