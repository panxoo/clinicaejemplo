using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.Localization
{
    public class LocalizacionGnrl
    {
        public LocalizacionGnrl()
        {
            LstDepart = new List<SelectListItem>();
            LstDistri = new List<SelectListItem>();
            LstProvin = new List<SelectListItem>();
        }

        [Display(Name = "Departamento")]
        public string DepartId { get; set; }

        [Display(Name = "Departamento :")]
        public string DepartNom { get; set; }

        public List<SelectListItem> LstDepart { get; set; }

        [Display(Name = "Provincia")]
        public string ProvinId { get; set; }

        [Display(Name = "Provincia :")]
        public string ProvinNom { get; set; }

        public List<SelectListItem> LstProvin { get; set; }

        [Display(Name = "Distrito")]
        public string DistriId { get; set; }

        [Display(Name = "Distrito :")]
        public string DistriNom { get; set; }

        public List<SelectListItem> LstDistri { get; set; }
    }
}