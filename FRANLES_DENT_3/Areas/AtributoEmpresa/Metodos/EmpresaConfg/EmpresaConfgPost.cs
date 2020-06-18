using FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg;
using FRANLES_DENT_3.Servicios.Interfaces;
using FRANLES_DENT_3.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Metodos.EmpresaConfg
{
    public class EmpresaConfgPost
    {

        public EmpresaConfgPost(IListGeneral lstGnrl)
        {
            _lstGnrl = lstGnrl;
        }

        IListGeneral _lstGnrl;

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
    }
}
