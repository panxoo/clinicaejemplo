using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg
{
    public class EmpresaConfgPost
    {
        public EmpresaConfgPost(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        private IListGeneral _lstGnrl;

        public async Task<RetornoAction> PostSucursalConfEdit(SucursalConfEditPost _model)
        {
            RetornoAction retornoAction = new RetornoAction();

            retornoAction = await new EmpresaConfgVal(_lstGnrl).ValSucursalConfEdit(_model);

            if (retornoAction.Code == 0)
            {
                return await new EmpresaConfgSave(_lstGnrl).SaveSucursalConfEdit(_model);
            }
            else
            {
                return retornoAction;
            }
        }

        public async Task<RetornoAction> PostSucursalConfAddArAt(SucursalConfAddArAtPost _model)
        {
            RetornoAction retornoAction;

            retornoAction = await new EmpresaConfgVal(_lstGnrl).ValSucursalConfAddArAt(_model);

            if (retornoAction.Code == 0)
            {
                retornoAction = await new EmpresaConfgSave(_lstGnrl).SaveSucursalConfAddArAt(_model);
                retornoAction.Parametro = await new EmpresaConfgGet(_lstGnrl).GetSucursalObtenerArea(_model.SucursalId);
                return retornoAction;
            }
            else
            {
                return retornoAction;
            }
        }

        public async Task<RetornoAction> PostRangoHorarioConfDetalle(RangoHorarioConfDetallePost _model, string _accion)
        {
            RetornoAction retornoAction;

            retornoAction = await new EmpresaConfgVal(_lstGnrl).ValRangoHorarioConfDetalle(_model, _accion);

            if (retornoAction.Code == 0)
            {
                if (_accion == "Upd")
                {
                    await new EmpresaConfgSave(_lstGnrl).SaveUpdRangoHorarioConfDetalle(_model);
                }
                else
                {
                    await new EmpresaConfgSave(_lstGnrl).SaveAddRangoHorarioConfDetalle(_model);

                }
            }


            return retornoAction;
           
        }    



        
    }
}