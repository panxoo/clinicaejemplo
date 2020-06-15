﻿using FRANLES_DENT_3.Models.MedicoDato;
using FRANLES_DENT_3.Models.Personal;
using FRANLES_DENT_3.Models.Sistema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AdminPersonal.Models.AdminUsuario
{
    public class UsuarioViewInput: DataModulo
    {
        public UsuarioViewInput()
        {
            ParentescoDet = "";
            DtMedicoInp = new DatosMedico();
            EspcialiSelLst = new List<SelectListItem>();
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
    }
}