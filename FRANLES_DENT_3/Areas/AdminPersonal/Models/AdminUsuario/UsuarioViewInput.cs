using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.Empresa.Atributos;
using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class UsuarioViewInput : DataModulo
    {
        public UsuarioViewInput()
        {
            ParentescoDet = "";
            DtMedicoInp = new DatosMedico();
            EspcialiSelLst = new List<SelectListItem>();
            HorarioMedicoMantEditInput = new HorarioMedicoMantEdit();
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

        public List<SelectListItem> SucursalSelLst { get; set; }
        public string SucursalSelId { get; set; }

        public HorarioMedicoViewParam HorarioMedicoViewInput { get; set; }

        public HorarioMedicoMantEdit HorarioMedicoMantEditInput { get; set; }

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
        }

        public class HorarioMedicoView
        {
            public string HorarioId { get; set; }
            public int DiaSemanaId { get; set; }
            public TimeSpan HoraIni { get; set; }
            public TimeSpan HoraFin { get; set; }
            public string SucursalName { get; set; }
            public List<string> AreaAtencions { get; set; }
        }

        public class HorarioMedicoMantEdit
        {
            public HorarioMedicoMantEdit()
            {
                HorarioMedicos = new List<HorarioMedicoTableMantEdit>();
                DiaScheduler = TransfParam.ParamDiaSemanaScheduler();
            }

            public List<HorarioMedicoTableMantEdit> HorarioMedicos { get; set; }
            public List<SelectListItem> DiaScheduler { get; set; }
            public List<SelectListItem> Sucursals { get; set; }
            public List<Tipo_Horario> Tipo_Horarios { get; set; }
            public List<SelectListItem> Area_Atencions { get; set; }
        }

        public class HorarioMedicoTableMantEdit : HorarioMedico
        {
            public string NombreSucursal { get; set; }
            public string NombreTipoHorario { get; set; }
            public List<SelectListItem> Area_AtencionsSel { get; set; }
        }
    }
}