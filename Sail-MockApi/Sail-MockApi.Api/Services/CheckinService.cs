using Sail_MockApi.Api.DTOs.Users;

namespace Sail_MockApi.Api.Services
{
    public class CheckinService
    {
        private readonly List<ReturnUserDTO> _users;
        private readonly List<ReturnCheckinDTO> _checkins;

        public CheckinService()
        {
            _checkins = new List<ReturnCheckinDTO>()
            {
                new ReturnCheckinDTO("1", DateTime.Now),
                new ReturnCheckinDTO("2", DateTime.Now),
            };

            _users = new List<ReturnUserDTO>
            {
                new ReturnUserDTO { Guid = "1", Email = "Henk@gmail.com", FirstName = "Henk", LastName = "Jansen", VolunteerId = "1234", RoleId = 1, GroupId = 1 },
                new ReturnUserDTO { Guid = "2", Email = "Karel@gmail.com", FirstName = "Karel", LastName = "Jansen", VolunteerId = "1235", RoleId = 2, GroupId = 2 },
            };
        }

        public ReturnCheckinDTO Add(NewCheckinDTO newCheckin)
        {
            ReturnCheckinDTO checkin = new ReturnCheckinDTO(newCheckin.UserId, DateTime.Now);
            _checkins.Add(checkin);
            return checkin;
        }

        public IEnumerable<ReturnCheckinDTO> GetAllCheckins(
            int limit = 100,
            int offset = 0,
            int? groupId = null,
            int? roleId = null)
        {
            // Perform a left join between _checkins and _users
            var query = _checkins
                .GroupJoin(
                    _users,
                    checkin => checkin.userId,           // Key selector for check-ins
                    user => user.Guid,                   // Key selector for users
                    (checkin, users) => new { Checkin = checkin, User = users.SingleOrDefault() }) // Select single user or null
                .AsQueryable();

            // Filter by groupId if specified, based on the User's GroupId (if User exists)
            if (groupId.HasValue)
                query = query.Where(cu => cu.User == null || cu.User.GroupId == groupId.Value);

            // Filter by roleId if specified, based on the User's RoleId (if User exists)
            if (roleId.HasValue)
                query = query.Where(cu => cu.User == null || cu.User.RoleId == roleId.Value);

            // Apply pagination and select to ReturnCheckinDTO
            return query
                .Skip(offset)
                .Take(limit)
                .Select(cu => new ReturnCheckinDTO
                {
                    checkinTime = cu.Checkin.checkinTime,
                    userId = cu.Checkin.userId,
                })
                .ToList();
        }


        public ReturnCheckinDTO? GetById(string userId) => _checkins.FirstOrDefault(c => c.userId == userId);

        public void Delete(string userId)
        {
            var checkin = _checkins.FirstOrDefault(c => c.userId == userId);
            if (checkin != null)
                _checkins.Remove(checkin);
        }






    }
}
