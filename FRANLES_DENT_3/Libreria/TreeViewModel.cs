using FRANLES_DENT_3.Models.Sistema;
using System.Collections.Generic;
using System.Linq;

namespace FRANLES_DENT_3.Libreria
{
    public class TreeViewModel
    {
        public List<TreeViewDato> TreeViewRealiza(List<TreeViewTemp> rol_Temps, string actmtd)
        {
            List<TreeViewDato> records;

            if (actmtd == "Vie")
            {
                records = rol_Temps.Where(w => w.FatherId == null).OrderBy(l => l.Name)
                                  .Select(s => new TreeViewDato
                                  {
                                      id = s.Id,
                                      text = s.Name,
                                      cssIcon = s.Check ? "fas fa-check text-success" : "fas fa-times text-accent",
                                      children = s.Hijo ? AddRolesChildrenVisual(rol_Temps, s.Id) : null
                                  }).ToList();
            }
            else
            {
                records = rol_Temps.Where(w => w.FatherId == null).OrderBy(l => l.Name)
                                   .Select(s => new TreeViewDato
                                   {
                                       id = s.Id,
                                       text = s.Name,
                                       @checked = s.Check,
                                       children = s.Hijo ? AddRolesChildren(rol_Temps, s.Id) : null
                                   }).ToList();
            }

            return records;
        }

        private List<TreeViewDato> AddRolesChildren(List<TreeViewTemp> rol_Temps, string parentId)
        {
            return rol_Temps.Where(w => w.FatherId == parentId).OrderBy(l => l.Name)
                            .Select(s => new TreeViewDato
                            {
                                id = s.Id,
                                text = s.Name,
                                @checked = s.Check,
                                children = s.Hijo ? AddRolesChildren(rol_Temps, s.Id) : null
                            }).ToList();
        }

        private List<TreeViewDato> AddRolesChildrenVisual(List<TreeViewTemp> rol_Temps, string parentId)
        {
            return rol_Temps.Where(w => w.FatherId == parentId).OrderBy(l => l.Name)
                            .Select(s => new TreeViewDato
                            {
                                id = s.Id,
                                text = s.Name,
                                cssIcon = s.Check ? "fas fa-check text-success" : "fas fa-times text-accent",
                                children = s.Hijo ? AddRolesChildrenVisual(rol_Temps, s.Id) : null
                            }).ToList();
        }
    }
}