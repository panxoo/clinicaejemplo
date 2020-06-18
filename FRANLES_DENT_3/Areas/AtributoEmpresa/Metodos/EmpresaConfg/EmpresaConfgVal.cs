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
                if(string.IsNullOrEmpty(_model.SucursalId ))
                {
                return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if(!await _lstGnrl._context.Sucursals.AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.SucursalId.Equals(_model.SucursalId)))
                    {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if(await _lstGnrl._context.Sucursals.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && !a.SucursalId.Equals(_model.SucursalId) && a.Nombre.Equals(_model.Nombre)))
                {
                    return new RetornoAction { Code = 1, Mensaje = "El nombre de la clinica ya esiste, se debe ingresar un nombre distinto" };
                }

                return new RetornoAction { Code = 0 };
            }
            catch(Exception ex)
            {
                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };

            }

        }

    }
}
