using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.RequestQueries;

namespace Sail_MockApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController
{
    private List<TaskResponseDto> _tasks;

    public TaskController()
    {
        _tasks = new List<TaskResponseDto>
        {
            //. Group 1
            new TaskResponseDto("e0ec6bc1-6f56-4771-b893-f042b7394714","Clean the floors", "You should clean the floors thoroughly so the people don't fall over objects", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            new TaskResponseDto("38b5fd51-f275-48e1-be2f-ab47859dc4b0","Clean the ceiling", "You should clean the ceilings thoroughly so the people don't get objects on their head", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            new TaskResponseDto("a0c5a3d4-40fc-4d47-819b-6cc9f4c84b86","Stand watch at the first aid area", "You need to stand watch at the first aid area and protect the staff", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            new TaskResponseDto("ee41ef11-735e-406b-9c38-50d55bf0bcfa","Be helpful", "You should be helpful to the visitors of the event :D", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            new TaskResponseDto("2c227c31-b334-40c9-83fe-f814f1f73cb7","Pass out drinks to volunteers of the event", "Drinks have been prepared for the visitors, please pass these out. Only 1 per volunteer.", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            
            //. Group 2
            new TaskResponseDto("e0ec6bc1-6f56-4771-b893-f012b7394714","Clean the floors", "You should clean the floors thoroughly so the people don't fall over objects", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            new TaskResponseDto("38b5fd51-f275-48e1-be2f-ab47559dc4b0","Clean the ceiling", "You should clean the ceilings thoroughly so the people don't get objects on their head", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            new TaskResponseDto("a0c5a3d4-40fc-4d47-819b-6cc4f4c84b86","Stand watch at the first aid area", "You need to stand watch at the first aid area and protect the staff", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            new TaskResponseDto("ee41ef11-735e-406b-9c38-50e55bf0bcfa","Be helpful", "You should be helpful to the visitors of the event :D", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            new TaskResponseDto("2c227c31-b334-40c9-83fe-a814f1f73cb7","Pass out drinks to volunteers of the event", "Drinks have been prepared for the visitors, please pass these out. Only 1 per volunteer.", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            
            //. Group 3
            new TaskResponseDto("e0ec6bc1-6f56-4771-b893-f041b7394714","Clean the floors", "You should clean the floors thoroughly so the people don't fall over objects", "a5054f14-d143-4610-9330-e3870b3687b0"),
            new TaskResponseDto("38b5fd51-f275-48e1-be2f-ab43859dc4b0","Clean the ceiling", "You should clean the ceilings thoroughly so the people don't get objects on their head", "a5054f14-d143-4610-9330-e3870b3687b0"),
            new TaskResponseDto("a0c5a3d4-40fc-4d47-819b-6cc9f4c84b56","Stand watch at the first aid area", "You need to stand watch at the first aid area and protect the staff", "a5054f14-d143-4610-9330-e3870b3687b0"),
            new TaskResponseDto("ee41ef11-735e-406b-9c38-50d25bf0bcfa","Be helpful", "You should be helpful to the visitors of the event :D", "a5054f14-d143-4610-9330-e3870b3687b0"),
            new TaskResponseDto("2c227c31-b334-40c9-83fe-f414f1f73cb7","Pass out drinks to volunteers of the event", "Drinks have been prepared for the visitors, please pass these out. Only 1 per volunteer.", "a5054f14-d143-4610-9330-e3870b3687b0"),
            
            //. Group 4
            new TaskResponseDto("e0ec6bc1-6f56-4771-b893-f042b7394514","Clean the floors", "You should clean the floors thoroughly so the people don't fall over objects", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
            new TaskResponseDto("38b5fd51-f275-48e1-be2f-ab47854dc4b0","Clean the ceiling", "You should clean the ceilings thoroughly so the people don't get objects on their head", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
            new TaskResponseDto("a0c5a3d4-40fc-4d47-819b-6cc3f4c84b86","Stand watch at the first aid area", "You need to stand watch at the first aid area and protect the staff", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
            new TaskResponseDto("ee41ef11-735e-406b-9c38-50d53bf0bcfa","Be helpful", "You should be helpful to the visitors of the event :D", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
            new TaskResponseDto("2c227c31-b334-40c9-83fe-f812f1f73cb7","Pass out drinks to volunteers of the event", "Drinks have been prepared for the visitors, please pass these out. Only 1 per volunteer.", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
        };
    }
    
    //. POST api/tasks
    [HttpPost]
    public IActionResult Post([FromBody] TaskRequestDto request)
    {
        TaskResponseDto responseDto = new TaskResponseDto(request);
        this._tasks.Add(responseDto);

        return new OkObjectResult(
            new DtoResponseObject<TaskResponseDto>("Task was added successfully.", responseDto));
    }
    //. GET api/tasks
    [HttpGet]
    public IActionResult Get([FromQuery] GetTasksQuery query)
    {
        IEnumerable<TaskResponseDto> response = _tasks;

        //. Filter by Name if provided (case-insensitive)
        if (!string.IsNullOrEmpty(query.Name))
        {
            response = response.Where(t => t.Name != null && t.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase));
        }

        //. Filter by GroupId if provided
        if (!string.IsNullOrEmpty(query.GroupId) && Guid.TryParse(query.GroupId, out Guid groupGuid))
        {
            response = response.Where(t => t.GroupId == groupGuid);
        }

        //. Filter by RoleId if provided
        if (!string.IsNullOrEmpty(query.RoleId) && Guid.TryParse(query.RoleId, out Guid roleGuid))
        {
            response = response.Where(t => t.RoleId == roleGuid);
        }

        //. Apply Offset if provided
        if (query.Offset.HasValue)
        {
            response = response.Skip(query.Offset.Value);
        }

        //. Apply Limit if provided
        if (query.Limit.HasValue)
        {
            response = response.Take(query.Limit.Value);
        }
        return new OkObjectResult(new DtoResponseObject<List<TaskResponseDto>>( response.Count() + " Tasks retrieved.", response.ToList()));
    }
    
    //. GET api/tasks/{taskId}
    [HttpGet("{taskId}")]
    public IActionResult Get(string taskId)
    {
        Guid.TryParse(taskId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Retrieve the message with the id
        TaskResponseDto? res = _tasks.FirstOrDefault(m => m.Id == guid);
        return res != null
            ? new OkObjectResult(res)
            : new BadRequestObjectResult("Task not found with the provided ID.");
    }
    
    //. PATCH api/tasks/{taskId}
    [HttpPatch("{taskId}")]
    public IActionResult Patch(string taskId, [FromBody] TaskRequestDto request)
    {
        Guid.TryParse(taskId, out Guid guid);
        
        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Retrieve the task
        TaskResponseDto? task = _tasks.FirstOrDefault(g => g.Id == guid);
        if (task == null)
        {
            return new BadRequestObjectResult("Task not found with the provided ID.");
        }
        
        //. Update the group with the new values
        task.Name = request.Name;
        task.Description = request.Description;
        
        Guid.TryParse(request.GroupId, out Guid groupGuid);
        Guid.TryParse(request.RoleId, out Guid roleGuid);
        
        if (groupGuid == Guid.Empty || roleGuid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided for the role or group.");
        }
        
        task.GroupId = Guid.Parse(request.GroupId);
        task.RoleId = Guid.Parse(request.RoleId);
        
        //. Get the index
        var index = _tasks.IndexOf(task);
        _tasks[index] = task;
        
        return new OkObjectResult(new DtoResponseObject<TaskResponseDto>("Task updated successfully.", task));
    }
    
    //. DELETE api/tasks/{taskId}
    [HttpDelete("{taskId}")]
    public IActionResult Delete(string taskId)
    {
        Guid.TryParse(taskId, out Guid guid);
        
        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Delete the message from the list of messages if it exists
        TaskResponseDto? res = _tasks.FirstOrDefault(m => m.Id == guid);

        if (res == null)
        {
            return new BadRequestObjectResult("Task not found with the provided ID.");
        }

        _tasks.Remove(res);
        return new OkObjectResult("Task successfully deleted.");
        
    }
}