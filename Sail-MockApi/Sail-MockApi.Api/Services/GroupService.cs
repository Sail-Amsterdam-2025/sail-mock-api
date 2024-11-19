using Sail_MockApi.Api.DTOs;
using System.Text.RegularExpressions;

namespace Sail_MockApi.Api.Services
{
    public class GroupService
    {
        private static List<GroupResponseDto> Groups { get; set; } = new List<GroupResponseDto>();
    private List<GroupLeaderResponseDto> GroupLeaders { get; set; } = new List<GroupLeaderResponseDto>();
        public GroupService()
        {
            AddDataToList();
            
        }

        public void AddDataToList()
        {
            if (Groups.Count == 0)
            {
                Guid[] groupIds = new Guid[6];
                for (int i = 0; i < groupIds.Length; i++)
                {
                    groupIds[i] = Guid.NewGuid();
                }
                
                
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[0], Guid.NewGuid(), "0612345678"));
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[1], Guid.NewGuid(), "0612345678"));
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[2], Guid.NewGuid(), "0612345678"));
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[3], Guid.NewGuid(), "0612345678"));
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[4], Guid.NewGuid(), "0612345678"));
                GroupLeaders.Add(new GroupLeaderResponseDto(groupIds[5], Guid.NewGuid(), "0612345678"));
                
                
                Groups.Add(new GroupResponseDto(groupIds[0].ToString(), "Development Team", Guid.NewGuid().ToString(), GroupLeaders[0]));
                Groups.Add(new GroupResponseDto(groupIds[1].ToString(), "Marketing Team", Guid.NewGuid().ToString(), GroupLeaders[1]));
                Groups.Add(new GroupResponseDto(groupIds[2].ToString(), "Sales Team", Guid.NewGuid().ToString(), GroupLeaders[2]));
                Groups.Add(new GroupResponseDto(groupIds[3].ToString(), "Human Resources", Guid.NewGuid().ToString(), GroupLeaders[3]));
                Groups.Add(new GroupResponseDto(groupIds[4].ToString(), "Research and Development", Guid.NewGuid().ToString(), GroupLeaders[4]));
                Groups.Add(new GroupResponseDto(groupIds[5].ToString(), "Customer Support", Guid.NewGuid().ToString(), GroupLeaders[5]));
            }
            
        }

        public GroupResponseDto AddGroup(GroupRequestDto groupRequestDto)
        {
            try
            {

                string id = Guid.NewGuid().ToString();
                GroupResponseDto newGroup = new GroupResponseDto(groupRequestDto, id);
                Groups.Add(newGroup);
                return newGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupResponseDto> GetAllGroups(int limit = 10, int offset = 0, string name = null, string? roleId = null, string? groupLeaderId = null)
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

            if (!string.IsNullOrEmpty(groupLeaderId))
            {
                query = query.Where(g => g.GroupLeader.Id == Guid.Parse(groupLeaderId));
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

            if (updatedGroup.GroupLeader != null)
            {
                group.GroupLeader = new GroupLeaderResponseDto(updatedGroup.GroupLeader);
            }

            return group;
        }
    }
}
