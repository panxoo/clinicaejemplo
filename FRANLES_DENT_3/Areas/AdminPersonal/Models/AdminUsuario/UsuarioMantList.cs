using FRANLES_DENT_3.Models.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class UsuarioMantList : DataCollect<UsuarioMantList.UsuarioMantDt>
    {
        public class UsuarioMantDt
        {
            public string UsuarioId { get; set; }
            public bool Activo { get; set; }
            public string Perfil { get; set; }
            public string Sucursal { get; set; }
            public string UserName { get; set; }
            public string Nombre { get; set; }
            public string ClinicaId { get; set; }
        }
    }
}
