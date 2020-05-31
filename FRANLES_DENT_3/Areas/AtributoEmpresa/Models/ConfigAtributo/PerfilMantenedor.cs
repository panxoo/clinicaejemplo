using FRANLES_DENT_3.Models.Permisos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Areas.AtributoEmpresa.Models.ConfigAtributo
{
    public class PerfilMantenedor : Perfil
    {
        public int CantUser { get; set; }
    }
}
