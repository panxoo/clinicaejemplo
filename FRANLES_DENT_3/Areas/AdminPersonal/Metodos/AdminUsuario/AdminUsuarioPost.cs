using FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Metodos.AdminUsuario
{
    public class AdminUsuarioPost
    {
        public AdminUsuarioPost(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> PostCreaUsuario(CreaUsuarioInputPost _model, string accion)
        {
            RetornoAction retornoAction = new RetornoAction();
            AdminUsuarioVal validacion = new AdminUsuarioVal(_lstGnrl);

            retornoAction = await validacion.ValCreaUsuarioInicial(_model, accion);

            if (retornoAction.Code != 0)
            {
                return retornoAction;
            }

            string alias = "";

            if (accion == "Upd")
            {
                var dtusrTemp = await _lstGnrl._context.Usuarios.Where(f => f.ClinicaId.Equals(_lstGnrl._datosUsuario.ClinicaId) && f.UsuarioId.Equals(_model.UsuarioId))
                                                                .Select(s => new
                                                                {
                                                                    Accesact = s.Acceso,
                                                                    UserAccesExist = !string.IsNullOrEmpty(s.Nombre_Cuenta)
                                                                }).FirstOrDefaultAsync();

                _model.Accesact = dtusrTemp.Accesact;
                _model.UserNomExist = dtusrTemp.UserAccesExist;
            }

            if (_model.Acceso && (accion == "Add" || !_model.UserNomExist))
            {
                alias = await _lstGnrl._context.Clinicas.Where(p => p.ClinicaID.Equals(_lstGnrl._datosUsuario.ClinicaId)).Select(s => s.Dominio).FirstOrDefaultAsync();
                _model.Nombre_Cuenta = _model.Nombre_Cuenta.Replace(" ", "") + '@' + alias;
            }

            retornoAction = await validacion.ValCreaUsuarioComplementaria(_model, accion, alias);

            if (retornoAction.Code == 0)
            {
                if (accion == "Add")
                {
                    retornoAction = await new AdminUsuarioSave(_lstGnrl).SaveCreaUsuarioAdd(_model);
                }
                if (accion == "Upd")
                {
                    retornoAction = await new AdminUsuarioSave(_lstGnrl).SaveCreaUsuarioUpd(_model);
                }
            }

            return retornoAction;
        }

        public async Task<RetornoAction> PostDetalleUsuarioMedUpd(UsuarioViewPost.UsuarioMedicoPost _model)
        {

            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new AdminUsuarioVal(_lstGnrl).ValDetalleUsuarioMedUpd(_model);

            if (retornoAction.Code == 0)
            {              
                retornoAction = await new AdminUsuarioSave(_lstGnrl).SaveDetalleUsuarioMedUpd(_model);
            }

            return retornoAction;

             

        }

    }
}