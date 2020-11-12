using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminPersonal;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminPersonal
{
    public class AdminPersonalGet
    {
        private IListGeneral _lstGnrl;
        public AdminPersonalGet(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        public async Task<RetornoAction> GetProfilePersonal(string id)
        {

            RetornoAction retorno =  new RetornoAction() ;

            if(!await _lstGnrl._context.Usuarios.IgnoreQueryFilters().AnyAsync(a => a.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && a.UsuarioId.Equals(id)))
            {
                retorno = new RetornoAction { Code = 2, Mensaje = "El usuario no esiste" };
            }
            else
            {
                ProfilePersonalInput _model = new ProfilePersonalInput();

                _model.DtUsuario = await _lstGnrl._context.Usuarios.Where(w => w.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && w.UsuarioId.Equals(id))
                                                                   .Select(s => new ProfilePersonalInput.usuarioPerfilDt
                                                                   {
                                                                       Activo = s.Activo,
                                                                       Nombre = s.Nombre,
                                                                       Apellidos = s.Apellido_Paterno + ' ' + s.Apellido_Materno,
                                                                       Correo = s.Correo,
                                                                       Movil = s.Movil,
                                                                       DistritoName = s.Distrito.Provincia.Departamento.Name + " " + s.Distrito.Provincia.Name + " " + s.Distrito.Name,
                                                                       DatosEmergenciaUsuario = _lstGnrl._context.DatosEmergenciaUsuarios.FirstOrDefault(f => f.UsuarioId.Equals(s.UsuarioId)),
                                                                       Avatar = s.Avatar
                                                                   }).FirstOrDefaultAsync();


            }

            return retorno;



        }
    }
}
