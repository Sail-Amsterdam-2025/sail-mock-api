using Sail_MockApi.Api.DTOs;
using System.Text.RegularExpressions;

namespace Sail_MockApi.Api.Services
{
    public class RoleService
    {
        private static List<RoleResponseDto> Roles { get; set; } = new List<RoleResponseDto>();

        public RoleService() { 
        AddDataToList();
        
        }



        public void AddDataToList()
        {
            if (Roles.Count == 0)
            {
                 Roles.Add(new RoleResponseDto("role1", Guid.NewGuid().ToString()));
                Roles.Add(new RoleResponseDto("role2", Guid.NewGuid().ToString()));
                Roles.Add(new RoleResponseDto("role3", Guid.NewGuid().ToString()));
                Roles.Add(new RoleResponseDto("role4", Guid.NewGuid().ToString()));
                Roles.Add(new RoleResponseDto("role5", Guid.NewGuid().ToString()));
                Roles.Add(new RoleResponseDto("role6", Guid.NewGuid().ToString()));
            }
           

        }

        public List<RoleResponseDto> GetAllRoles(int limit = 10, int offset = 0, string name = null)
        {
            var query = Roles.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            query = query.Skip(offset).Take(limit);

            return query.ToList();
        }

        public RoleResponseDto GetRoleById(string id)
        {
            return Roles.FirstOrDefault(r => r.Id == id);
        }

        public RoleResponseDto AddRole(RoleRequestDto roleRequestDto)
        {
            string id = Guid.NewGuid().ToString();
            RoleResponseDto newRole = new RoleResponseDto(roleRequestDto, id);
            Roles.Add(newRole);
            return newRole;
        }

        public RoleResponseDto PatchRoleById(string roleId, RoleRequestDto updatedRole)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updatedRole.Name))
            {
                role.Name = updatedRole.Name;
            }

            return role;
        }

        public bool DeleteRoleById(string roleId)
        {
            var roleToDelete = Roles.FirstOrDefault(r => r.Id == roleId);

            if (roleToDelete != null)
            {
                Roles.Remove(roleToDelete);
                return true;
            }

            return false;
        }


    }
}
