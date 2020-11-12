using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class UsuarioViewInput : DataModulo
    {
        public UsuarioViewInput()
        {
            ParentescoDet = "";
            DtMedicoInp = new DatosMedico();
            EspcialiSelLst = new List<SelectListItem>();
            ShedulerMedMantEditParam = new HorarioMedicoMantEditParam();
        }

        public Usuario DtUsuario { get; set; }
        public MedicoView DtMedico { get; set; }

        public string PerfilDet { get; set; }
        public string SexoDet { get; set; }
        public string TipoDocumento { get; set; }
        public string Departamento { get; set; }
        public string ParentescoDet { get; set; }
        public DatosMedico DtMedicoInp { get; set; }
        public List<SelectListItem> EspcialiSelLst { get; set; }


        public HorarioMedicoViewParam HorarioMedicoViewInput { get; set; }

        public HorarioMedicoMantEditParam ShedulerMedMantEditParam { get; set; }

        public class MedicoView
        {
            public MedicoView()
            {
                DatoMedico = new DatosMedico();
                EspcialidadMed = new List<SelectListItem>();
            }

            public DatosMedico DatoMedico { get; set; }
            public List<SelectListItem> EspcialidadMed { get; set; }
        }

        public class HorarioMedicoViewParam
        {
            public HorarioMedicoViewParam()
            {
                DiaScheduler = TransfParam.ParamDiaSemanaScheduler();
            }

            public List<SelectListItem> DiaScheduler { get; set; }
            public List<HorarioMedicoView> HorarioMedicoViews { get; set; }
            public bool MultiSucursal { get; set; }
            public List<SchedulerMarked> SchedulerMarkeds { get; set; }
        }

        public class HorarioMedicoView
        {
            public string HorarioId { get; set; }
            public int DiaSemanaId { get; set; }
            public DateTime FechaIni{ get; set; }
            public DateTime FechaFin { get; set; }
            public string Tipo_HorarioId { get; set; }
            public string SucursalName { get; set; }
            public string SucursalId { get; set; }
            public List<SelectListItem> AreaAtencions { get; set; }
        }

        public class HorarioMedicoMantEditParam
        {
            public HorarioMedicoMantEditParam()
            {
                DiaScheduler = TransfParam.ParamDiaSemanaScheduler();
            }

            public List<SelectListItem> DiaScheduler { get; set; }
            public List<SelectListItem> Sucursals { get; set; }
            public List<Tipo_Horario> Tipo_Horarios { get; set; }
            public List<AreAtenSucruSelect> Area_Atencions { get; set; }
            public bool MultiSucursal { get; set; }
            
            [Required(ErrorMessage ="Se requiere seleccionar el dia laboral")]
            public int MHE_DiaWeek { get; set; }
            public string MHE_Sucursal { get; set; }           
            [Required(ErrorMessage = "Se requiere seleccionar el tipo de horario")]
            public string MHE_Tipo_Horario { get; set; }

        }

        public class AreAtenSucruSelect
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public string Sucursals { get; set; }
        }
    }
}