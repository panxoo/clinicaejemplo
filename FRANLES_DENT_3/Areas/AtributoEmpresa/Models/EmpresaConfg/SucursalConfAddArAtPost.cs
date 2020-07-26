using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg
{
    public class SucursalConfAddArAtPost
    {
               public string SucursalId { get; set; }

        [Required(ErrorMessage ="Se debe seleccionar el area atención")]
        public List<string> AreaAtencions { get; set; }

        public string ModAct { get; set; }

    }
}
