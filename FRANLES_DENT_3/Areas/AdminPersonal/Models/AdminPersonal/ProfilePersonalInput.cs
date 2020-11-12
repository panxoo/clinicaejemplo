using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminPersonal
{
    public class ProfilePersonalInput
    {
        public usuarioPerfilDt DtUsuario { get; set; }


        public class usuarioPerfilDt : Usuario
        {
            public string PerfilName { get; set; }
            public string DistritoName { get; set; }
            public string ParentName { get; set; }
            public DatosMedico DtMedico { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }

        }
    }
}
