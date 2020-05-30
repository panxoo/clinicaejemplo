using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Libreria
{
    public class UsersRoles : ListGeneral
    {
        public UsersRoles()
        {
            _selectList = new List<SelectListItem>();
        }

        public async Task<List<SelectListItem>> getRole(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, string ID)
        {
            IdentityUser users = await userManager.FindByIdAsync(ID);
            IList<string> roles = await userManager.GetRolesAsync(users);

            if (roles.Count.Equals(0))
            {
                _selectList.Add(new SelectListItem { Value = "0", Text = "No roles" });
            }
            else
            {
                foreach (string dt in roles)
                {
                    var roleUser = roleManager.Roles.Where(m => m.Name.Equals(dt));
                    foreach (var item in roleUser)
                    {
                        _selectList.Add(new SelectListItem { Value = item.Id, Text = item.Name });
                    }
                }
            }

            return _selectList;
        }

        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();
            roles.ForEach(item =>
            {
                _selectList.Add(new SelectListItem
                {
                    Value = item.Id,
                    Text = item.Name
                });
            });
            return _selectList;
        }

        public List<SelectListItem> getRolesCB(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();
            List<SelectListItem> _selectListCB = new List<SelectListItem>();
            roles.ForEach(item =>
            {
                _selectListCB.Add(new SelectListItem
                {
                    Value = item.Id,
                    Text = item.Name,
                    Selected = false
                });
            });
            return _selectListCB;
        }
    }
}