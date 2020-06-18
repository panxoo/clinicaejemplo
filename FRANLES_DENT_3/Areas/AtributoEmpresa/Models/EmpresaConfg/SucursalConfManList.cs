using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.EmpresaConfg
{
    public class SucursalConfManList
    {
        public string Nombre { get; set; }
        public bool Principal { get; set; }
        public string Direccion { get; set; }
        public string SucursalId { get; set; }
        public string DistritoNom { get; set; }
        public string ProvinciaNom { get; set; }
        public string DepartamentoNom { get; set; }
    }
}
