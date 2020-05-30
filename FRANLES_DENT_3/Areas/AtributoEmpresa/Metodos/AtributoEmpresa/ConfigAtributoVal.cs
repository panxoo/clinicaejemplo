using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.AtributoEmpresa
{
    public class ConfigAtributoVal
    {

        public ConfigAtributoVal(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> ValSaveEspecialidad(Especialidad _model)
        {
            try
            {
                if(await _lstGnrl._context.Especialidades.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    return new RetornoAction { Code = 1, Mensaje = "El nombre de especialidad ya existe." };
                }

                return new RetornoAction { Code = 0, Mensaje = "" };

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.Message);

                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }

        public async Task<RetornoAction> ValUpdatEspecialidad(Especialidad _model)
        {
            try
            {
                if (!await _lstGnrl._context.Especialidades.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.EspecialidadId.Equals(_model.EspecialidadId)))
                {
                    return new RetornoAction { Code = 2, Mensaje = "" };
                }

                if (await _lstGnrl._context.Especialidades.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && !a.EspecialidadId.Equals(_model.EspecialidadId) && a.Nombre.Equals(_model.Nombre.Trim())))
                {
                    return new RetornoAction { Code = 1, Mensaje = "El nombre de especialidad ya existe." };
                }

                return new RetornoAction { Code = 0, Mensaje = "" };

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.Message);

                return new RetornoAction { Code = 2, Mensaje = "Error de sistema, contactar con soporte" };
            }
        }


    }
}
