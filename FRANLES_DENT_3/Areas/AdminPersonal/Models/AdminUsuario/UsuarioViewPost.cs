using FRANLES_DENT_3.Models.MedicoDato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class UsuarioViewPost
    {
        public class UsuarioMedicoPost : DatosMedico
        {
            public List<string> EspecialIds { get; set; }
            public string ModAct { get; set; }
        }

        public class UsuarioSucursalAAPost
        {
            public string UsuarioId { get; set; }
            public string Sucursal { get; set; }            
            public List<string> AreaAtencions { get; set; }
            public string ModAct { get; set; }
        }
    }
}
