using FRANLES_DENT_3.Libreria;
using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //SucursalAreaAtencionImp = new SucursalAreaAtencion();
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
        //public SucursalAreaAtencion SucursalAreaAtencionImp { get; set; }
        //public List<SucursalAreaAtencionMant> DtSucursalAreaAtencionMants { get; set; }

        public HorarioMedicoMant HorarioMedicoMantInput { get; set; }

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

        //public class SucursalAreaAtencion
        //{
        //    public string SucursalId { get; set; }
        //    public string SucursalNom { get; set; }
        //    public string SucursalDirc { get; set; }
        //    public string Activo { get; set; }

        //    public List<SelectListItem> Area_Atencions { get; set; }
        //}

        //public class SucursalAreaAtencionMant
        //{
        //    public string SucursalId { get; set; }
        //    public string SucursalNom { get; set; }
        //    public string SucursalDirc { get; set; }
        //    public string Activo { get; set; }

        //    public List<SelectListItem> Area_Atencions { get; set; }
        //}

        public class HorarioMedicoMant
        {
            public HorarioMedicoMant()
            {
                HorarioMedicos = new List<HorarioMedicoTableMant>();
                DiaScheduler = TransfParam.ParamDiaSemanaScheduler();
            }

            public List<HorarioMedicoTableMant> HorarioMedicos { get; set; }
            public List<SelectListItem> DiaScheduler { get; set; }
            public List<SelectListItem> Area_Atencions { get; set; }

        }

        public class HorarioMedicoTableMant : HorarioMedico
        {
            public string NombreSucursal { get; set; }

            public string NombreTipoHorario { get; set; }
        }
    }
}