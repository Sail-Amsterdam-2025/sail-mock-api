using Sail_MockApi.Api.DTOs;
using System.Text.RegularExpressions;

namespace Sail_MockApi.Api.Services
{
    public class GroupService
    {
        private static List<GroupResponseDto> Groups { get; set; } = new List<GroupResponseDto>();

        public GroupService()
        {
            AddDataToList();
            
        }

        public void AddDataToList()
        {
            if (Groups.Count == 0)
            {
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Development Team", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Marketing Team", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Sales Team", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Human Resources", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Research and Development", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
                Groups.Add(new GroupResponseDto(Guid.NewGuid().ToString(), "Customer Support", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
            }
            
        }

        public GroupResponseDto AddGroup(GroupRequestDto groupRequestDto)
        {
            string id = Guid.NewGuid().ToString();
            GroupResponseDto newGroup = new GroupResponseDto(groupRequestDto, id);
            Groups.Add(newGroup);
            return newGroup;
        }

        public List<GroupResponseDto> GetAllGroups(int limit = 10, int offset = 0, string name = null, string? roleId = null, string? teamLeaderId = null)
        {
            var query = Groups.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                query = query.Where(g => g.RoleId.Contains(roleId, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(teamLeaderId))
            {
                query = query.Where(g => g.TeamLeaderId.Contains(teamLeaderId, StringComparison.OrdinalIgnoreCase));
            }

            query = query.Skip(offset).Take(limit);

            return query.ToList();
        }

        public GroupResponseDto GetGroupById(string id)
        {
              return Groups.FirstOrDefault(g => g.Id == id);
        }

        public bool DeleteGroupById(string groupId)
        {
            var groupToDelete = Groups.FirstOrDefault(g => g.Id == groupId);

            if (groupToDelete != null)
            { 
                Groups.Remove(groupToDelete);
                return true; 
            }

            return false; 
        }

        public GroupResponseDto PatchGroupById(string groupId, GroupRequestDto updatedGroup)
        {
            var group = Groups.FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updatedGroup.Name))
            {
                group.Name = updatedGroup.Name;
            }

            if (!string.IsNullOrEmpty(updatedGroup.RoleId))
            {
                group.RoleId = updatedGroup.RoleId;
            }

            if (!string.IsNullOrEmpty(updatedGroup.TeamLeaderId))
            {
                group.TeamLeaderId = updatedGroup.TeamLeaderId;
            }

            return group;
        }
    }
}
