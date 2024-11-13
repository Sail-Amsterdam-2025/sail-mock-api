﻿using Sail_MockApi.Api.DTOs.TimeBlocks;

namespace Sail_MockApi.Api.Services
{

    public class TimeblockService 
    {
        private readonly List<TimeblockResponseDTO> _timeblocks;

        public TimeblockService()
        {
            // Sample data 
            _timeblocks = new List<TimeblockResponseDTO>();
            CreateMockData();
        }

        public TimeblockResponseDTO AddTimeblock(TimeblockRequestDTO timeblockRequestDTO)
        {
            // Create a new timeblock from the request DTO
            var newTimeblock = new TimeblockResponseDTO
            {
                Id = _timeblocks.Count + 1, // Generate a new ID
                GroupId = timeblockRequestDTO.GroupId,
                StartTime = timeblockRequestDTO.StartTime,
                EndTime = timeblockRequestDTO.EndTime,
                LocationId = timeblockRequestDTO.LocationId
            };

            // Add to in-memory data (this would be a database call in a real app)
            _timeblocks.Add(newTimeblock);

            return newTimeblock;
        }

        public List<TimeblockResponseDTO> GetTimeblocks(int limit, int offset, int? groupId, DateTime? startTime, DateTime? endTime)
        {
            var query = _timeblocks.AsQueryable();

            // Apply filtering based on the query parameters
            if (groupId.HasValue)
            {
                query = query.Where(t => t.GroupId == groupId.Value);
            }

            if (startTime.HasValue)
            {
                query = query.Where(t => t.StartTime >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                query = query.Where(t => t.EndTime <= endTime.Value);
            }

            // Apply offset and limit
            var result = query.Skip(offset).Take(limit).ToList();

            return result;
        }

        public void UpdateTimeblock(TimeblockResponseDTO timeblock)
        {
            // Here, you would update the timeblock in your database or in-memory data store.
            var existingTimeblock = _timeblocks.FirstOrDefault(t => t.Id == timeblock.Id);

            if (existingTimeblock != null)
            {
                existingTimeblock.GroupId = timeblock.GroupId;
                existingTimeblock.StartTime = timeblock.StartTime;
                existingTimeblock.EndTime = timeblock.EndTime;
                existingTimeblock.LocationId = timeblock.LocationId;
            }
        }

        public TimeblockResponseDTO GetTimeblockById(int timeblockId)
        {
            // In this case, we're assuming _timeblocks is a list containing timeblock data
            return _timeblocks.FirstOrDefault(t => t.Id == timeblockId);
        }

        public bool DeleteTimeblock(int timeblockId)
        {
            // Find the timeblock to delete
            var timeblock = _timeblocks.FirstOrDefault(t => t.Id == timeblockId);

            if (timeblock == null)
            {
                return false;  // Return false if the timeblock is not found
            }

            // Remove the timeblock from the list (or your database)
            _timeblocks.Remove(timeblock);

            return true;  // Return true indicating successful deletion
        }




        private void CreateMockData()
        {
            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 1,
                GroupId = 1,
                StartTime = DateTime.Parse("2024-04-27T14:30:00Z"),
                EndTime = DateTime.Parse("2024-04-27T16:00:00Z"),
                LocationId = 1
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 1,
                GroupId = 1,
                StartTime = DateTime.Parse("2024-04-27T14:30:00Z"),
                EndTime = DateTime.Parse("2024-04-27T16:00:00Z"),
                LocationId = 1
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 2,
                GroupId = 2,
                StartTime = DateTime.Parse("2024-05-01T09:00:00Z"),
                EndTime = DateTime.Parse("2024-05-01T11:00:00Z"),
                LocationId = 2
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 3,
                GroupId = 3,
                StartTime = DateTime.Parse("2024-05-02T13:00:00Z"),
                EndTime = DateTime.Parse("2024-05-02T15:00:00Z"),
                LocationId = 3
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 4,
                GroupId = 1,
                StartTime = DateTime.Parse("2024-05-03T08:30:00Z"),
                EndTime = DateTime.Parse("2024-05-03T10:00:00Z"),
                LocationId = 1
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 5,
                GroupId = 2,
                StartTime = DateTime.Parse("2024-05-04T14:30:00Z"),
                EndTime = DateTime.Parse("2024-05-04T16:00:00Z"),
                LocationId = 2
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 6,
                GroupId = 3,
                StartTime = DateTime.Parse("2024-05-05T10:00:00Z"),
                EndTime = DateTime.Parse("2024-05-05T12:00:00Z"),
                LocationId = 3
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 7,
                GroupId = 4,
                StartTime = DateTime.Parse("2024-05-06T16:00:00Z"),
                EndTime = DateTime.Parse("2024-05-06T18:00:00Z"),
                LocationId = 4
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 8,
                GroupId = 5,
                StartTime = DateTime.Parse("2024-05-07T09:30:00Z"),
                EndTime = DateTime.Parse("2024-05-07T11:30:00Z"),
                LocationId = 5
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 9,
                GroupId = 1,
                StartTime = DateTime.Parse("2024-05-08T12:00:00Z"),
                EndTime = DateTime.Parse("2024-05-08T14:00:00Z"),
                LocationId = 1
            });

            _timeblocks.Add(new TimeblockResponseDTO
            {
                Id = 10,
                GroupId = 2,
                StartTime = DateTime.Parse("2024-05-09T13:30:00Z"),
                EndTime = DateTime.Parse("2024-05-09T15:00:00Z"),
                LocationId = 2
            });
        }
    }
}
