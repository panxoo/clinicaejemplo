using FRANLES_DENT_3.Models.MedicoDato.Atributo;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.AtributoEmpresa
{
    public class ConfigAtributoPost
    {

        public ConfigAtributoPost(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        IListGeneral _lstGnrl;

        public async Task<RetornoAction> PostSaveEspecialidad (Especialidad _model)
        {

            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new ConfigAtributoVal(_lstGnrl).ValSaveEspecialidad(_model);

            if(retornoAction.Code == 0)
            {
                Especialidad dato = new Especialidad
                {
                    EspecialidadId = Ulid.NewUlid().ToString(),
                    Nombre = _model.Nombre.Trim(),
                    Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim(),
                    Activo = true,
                    ClinicaId = _lstGnrl._datosUsuario.ClinicaId,
                    Fecha_Add = DateTime.Now,
                    Fecha_Upd = DateTime.Now,
                };

                await _lstGnrl._context.AddAsync(dato);
                await _lstGnrl._context.SaveChangesAsync();

            }

            return retornoAction;
        }

        public async Task<RetornoAction> PostUpdEspecialidad(Especialidad _model)
        {

            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new ConfigAtributoVal(_lstGnrl).ValUpdatEspecialidad(_model);

            if (retornoAction.Code == 0)
            {
                Especialidad dato = _lstGnrl._context.Especialidades.IgnoreQueryFilters().FirstOrDefault(f => f.EspecialidadId.Equals(_model.EspecialidadId) && f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId));

                dato.Nombre = _model.Nombre.Trim();
                dato.Descripcion = string.IsNullOrEmpty(_model.Descripcion) ? "" : _model.Descripcion.Trim();
                dato.Fecha_Upd = DateTime.Now;

                await _lstGnrl._context.SaveChangesAsync();
            }

            return retornoAction;
        }

    }
}
