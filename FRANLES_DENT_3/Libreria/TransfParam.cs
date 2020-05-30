using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FRANLES_DENT_3.Libreria
{
    static public class TransfParam
    {
        static public List<SelectListItem> ParamSexo()
        {
            return new List<SelectListItem> { new SelectListItem { Text = "Masculino", Value = "M" }, new SelectListItem { Text = "Femenino", Value = "F" } };
        }

        static public List<SelectListItem> ParamParentesco()
        {
            return new List<SelectListItem> { new SelectListItem { Text = "Padres", Value = "1" },
                                              new SelectListItem { Text = "Pareja", Value = "2" },
                                              new SelectListItem { Text = "Hermano", Value = "3" },
                                              new SelectListItem { Text = "Abuelos", Value = "4" },
                                              new SelectListItem { Text = "Otros", Value = "5"}
            };
        }

        static public List<SelectListItem> ParamTipoDocumento()
        {
            return new List<SelectListItem> { new SelectListItem { Text = "DNI", Value = "1" },
                                              new SelectListItem { Text = "Documento Extranjeria", Value = "2" },
                                              new SelectListItem { Text = "Pasaporte", Value = "3" }
            };
        }

        public static bool ValParmSexo(string val)
        {
            return ParamSexo().Any(a => a.Value == val);
        }

        public static bool ValParmParentesc(string val)
        {
            return ParamParentesco().Any(a => a.Value == val);
        }

        public static bool ValParmTipoDoc(string val)
        {
            return ParamTipoDocumento().Any(a => a.Value == val);
        }
    }
}