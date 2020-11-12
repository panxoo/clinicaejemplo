using FRANLES_DENT_3.Models.MedicoDato;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public class UsuarioHorarioMedicoPost
        {
            public string MedicoHorarioId { get; set; }
            public string UsuarioId { get; set; }

            [Required(ErrorMessage = "Se requiere seleccionar el dia laboral")]
            public short MHE_DiaWeek { get; set; }

            public string MHE_Sucursal { get; set; }
            
            [Required(ErrorMessage = "Se requiere seleccionar el horario de ingresar")]
            public string MHE_Tipo_Horario { get; set; }

            [Required(ErrorMessage = "Se requiere seleccionar las area de atención del horario")]
            public List<string> AreaAtencion { get; set; }
            public string ModAct { get; set; }
            public string Act { get; set; }
        }

    }
}
