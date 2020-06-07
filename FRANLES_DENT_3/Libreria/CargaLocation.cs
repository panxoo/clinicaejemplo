using FRANLES_DENT_3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Libreria
{
    public class CargaLocation
    {
        public async Task<List<SelectListItem>> ObtenerLocalizacion(string Id, ApplicationDbContext context)
        {
            List<SelectListItem> dato = new List<SelectListItem>();

            switch (Id.Length)
            {
                case 0:
                    dato = await context.Departamentos.Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.DepartamentoId
                    }).OrderBy(o => o.Text)
                      .ToListAsync();
                    break;

                case 2:

                    dato = await context.Provincias.Where(w => w.DepartamentoId.Equals(Id))
                                                    .Select(s => new SelectListItem
                                                    {
                                                        Text = s.Name,
                                                        Value = s.ProvinciaId
                                                    }).OrderBy(o => o.Text)
                                                    .ToListAsync();
                    break;

                case 4:

                    dato = await context.Distritos.Where(w => w.ProvinciaId.Equals(Id))
                                                    .Select(s => new SelectListItem
                                                    {
                                                        Text = s.Name,
                                                        Value = s.DistritoId
                                                    }).OrderBy(o => o.Text)
                                                    .ToListAsync();
                    break;
            }

            return dato;
        }
    }
}