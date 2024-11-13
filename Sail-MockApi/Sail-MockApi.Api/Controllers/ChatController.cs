using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.RequestQueries;

namespace Sail_MockApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController
{
    private List<string> _userIds = new();
    private List<ChatMessageResponseDto> _chatMessages = new List<ChatMessageResponseDto>();
    private List<ChatGroupResponseDto> _groupChats = new List<ChatGroupResponseDto>();

    public ChatController()
    {
        _userIds = new List<string>
        {
            "fa85ccc1-94e8-48f3-8dbe-ffa813a205e7",
            "d9e02b6f-3b3f-4ad6-91ef-58ff285e9b62",
            "16eafa46-d55f-46fc-baf3-0081c4d17480",
        };

        _groupChats = new List<ChatGroupResponseDto>
        {
            new ChatGroupResponseDto("The boys", "c1994145-b1bb-4700-93f5-137b1d1b52b3"),
            new ChatGroupResponseDto("The girls", "f04fce09-fbbd-4e8e-9731-bcb8fd459e03"),
            new ChatGroupResponseDto("The gang", "a5054f14-d143-4610-9330-e3870b3687b0"),
            new ChatGroupResponseDto("The communist party of china", "f3ff7479-f304-4413-8c01-4dd8bd27c4af"),
        };

        _chatMessages = new List<ChatMessageResponseDto>
        {
            new ChatMessageResponseDto(id: "854d5c89-15e5-440b-8ded-41917bbc65a8", userId: _userIds[0],
                chatGroupId: _groupChats[0].Id.ToString(), message: "Hello everyone"),
            new ChatMessageResponseDto(id: "afbd7eb1-78b0-43c1-a1cd-8399d1a438d1", userId: _userIds[0],
                chatGroupId: _groupChats[1].Id.ToString(), message: "Hiiii"),
            new ChatMessageResponseDto(id: "4b423f55-bc7e-4c16-a2e7-b8a6b064c9c4", userId: _userIds[1],
                chatGroupId: _groupChats[1].Id.ToString(), message: "How u doin?"),
            new ChatMessageResponseDto(id: "ea2dc654-b747-4972-b622-aec156e5ef3c", userId: _userIds[1],
                chatGroupId: _groupChats[3].Id.ToString(), message: "Be quiet"),
            new ChatMessageResponseDto(id: "cf2c025f-f108-4f06-8275-865f10a9d0b2", userId: _userIds[1],
                chatGroupId: _groupChats[1].Id.ToString(), message: "Gimme my money"),
            new ChatMessageResponseDto(id: "db020c83-a089-4614-bc52-bacbc62f0e51", userId: _userIds[2],
                chatGroupId: _groupChats[0].Id.ToString(), message: "Amsterdam is lit"),
            new ChatMessageResponseDto(id: "e78e0094-5798-497b-bd39-96a4577cb494", userId: _userIds[0],
                chatGroupId: _groupChats[2].Id.ToString(), message: "I like boats"),
            new ChatMessageResponseDto(id: "2613f5ba-d2c3-41f5-8306-788a4a2234f1", userId: _userIds[2],
                chatGroupId: _groupChats[3].Id.ToString(), message: "I like trains"),
            new ChatMessageResponseDto(id: "340ac704-5410-4dc2-93e4-f90fc283134f", userId: _userIds[2],
                chatGroupId: _groupChats[1].Id.ToString(), message: "Can I get there by car?"),
            new ChatMessageResponseDto(id: "f6d762bf-4324-477b-9e1b-d1671ca949ad", userId: _userIds[2],
                chatGroupId: _groupChats[2].Id.ToString(), message: "You blind?")
        };

        foreach (var groupChat in _groupChats)
        {
            groupChat.Messages = _chatMessages.Where(m => m.ChatGroupId == groupChat.Id).ToList();
        }
    }

    //. POST api/chat/messages
    [HttpPost("messages")]
    public IActionResult Post([FromBody] ChatMessageRequestDto request)
    {
        //. Check if all the required fields are present
        if (!request.ContainsContent())
        {
            return new BadRequestObjectResult("A message and/or image content are required.");
        }

        ChatMessageResponseDto responseDto = new ChatMessageResponseDto(request);
        this._chatMessages.Add(responseDto);

        return new OkObjectResult(
            new DtoResponseObject<ChatMessageResponseDto>("Chat message sent successfully.", responseDto));
    }

    //. GET api/chat/messages
    [HttpGet("messages")]
    public IActionResult Get([FromQuery] GetChatMessagesQuery query)
    {
        IEnumerable<ChatMessageResponseDto> response = _chatMessages;

        //. Filter by ChatGroupId if provided
        if (!string.IsNullOrEmpty(query.ChatGroupId))
        {
            response = response.Where(m => m.ChatGroupId.ToString() == query.ChatGroupId);
        }

        //. Filter by UserId if provided
        if (query.UserId.HasValue)
        {
            response = response.Where(m => m.UserId == new Guid(query.UserId.ToString()));
        }

        //. Filter by GetMessagesBefore if provided
        if (!string.IsNullOrEmpty(query.GetMessagesBefore) &&
            DateTime.TryParse(query.GetMessagesBefore, out DateTime beforeDate))
        {
            response = response.Where(m => m.SentAt < beforeDate);
        }

        //. Filter by GetMessagesAfter if provided
        if (!string.IsNullOrEmpty(query.GetMessagesAfter) &&
            DateTime.TryParse(query.GetMessagesAfter, out DateTime afterDate))
        {
            response = response.Where(m => m.SentAt > afterDate);
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
        return new OkObjectResult(new DtoResponseObject<List<ChatMessageResponseDto>>("Chat message sent successfully.", response.ToList()));
    }

    //. GET api/chat/messages/{messageId}
    [HttpGet("messages/{messageId}")]
    public IActionResult Get(string messageId)
    {
        Guid.TryParse(messageId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Retrieve the message with the id
        ChatMessageResponseDto? res = _chatMessages.FirstOrDefault(m => m.Id == guid);
        return res != null
            ? new OkObjectResult(res)
            : new BadRequestObjectResult("Message not found with the provided ID.");
    }

    //. DELETE api/chat/message/{messageId}
    [HttpDelete("messages/{messageId}")]
    public IActionResult Delete(string messageId)
    {
        Guid.TryParse(messageId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Delete the message from the list of messages if it exists
        ChatMessageResponseDto? res = _chatMessages.FirstOrDefault(m => m.Id == guid);

        if (res == null)
        {
            return new BadRequestObjectResult("Message not found with the provided ID.");
        }

        _chatMessages.Remove(res);
        return new OkObjectResult("Message successfully deleted.");
    }

    //. POST /api/chat/groups
    [HttpPost("groups")]
    public IActionResult Post([FromBody] ChatGroupRequestDto request)
    {
        ChatGroupResponseDto responseDto = new ChatGroupResponseDto(request);
        this._groupChats.Add(responseDto);
        int nrOfUsers = responseDto.Users?.Count() ?? 0;

        return new OkObjectResult(
            new DtoResponseObject<ChatGroupResponseDto>(
                "Chat group was added succesfully with " + nrOfUsers + " users.", responseDto));
    }

    //. GET /api/chat/groups
    [HttpGet("groups")]
    public IActionResult Get([FromQuery] GetChatGroupsQuery query)
    {
        IEnumerable<ChatGroupResponseDto> response = _groupChats;

        //. Filter by Name if provided (case-insensitive)
        if (!string.IsNullOrEmpty(query.Name))
        {
            response = response.Where(g =>
                g.Name != null && g.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase));
        }

        //. Filter by UserId if provided
        if (!string.IsNullOrEmpty(query.UserId))
        {
            Guid userGuid;
            if (Guid.TryParse(query.UserId, out userGuid))
            {
                response = response.Where(g => g.Users != null && g.Users.Any(u => u.UserId == userGuid));
            }
        }

        //. Handle IncludeMessages flag and Message filters
        if (query.IncludeMessages.HasValue && query.IncludeMessages.Value)
        {
            foreach (var group in response)
            {
                if (group.Messages != null)
                {
                    //. Filter messages by date range if applicable
                    IEnumerable<ChatMessageResponseDto> messages = group.Messages;

                    if (!string.IsNullOrEmpty(query.GetMessagesBefore) &&
                        DateTime.TryParse(query.GetMessagesBefore, out DateTime beforeDate))
                    {
                        messages = messages.Where(m => m.SentAt < beforeDate);
                    }

                    if (!string.IsNullOrEmpty(query.GetMessagesAfter) &&
                        DateTime.TryParse(query.GetMessagesAfter, out DateTime afterDate))
                    {
                        messages = messages.Where(m => m.SentAt > afterDate);
                    }

                    //. Apply MessageOffset and MessageLimit
                    if (query.MessageOffset.HasValue)
                    {
                        messages = messages.Skip(query.MessageOffset.Value);
                    }

                    if (query.MessageLimit.HasValue)
                    {
                        messages = messages.Take(query.MessageLimit.Value);
                    }

                    group.Messages = messages.ToList();
                }
            }
        }
        else
        {
            //. Manually set the message lists to null
            foreach (var group in response)
            {
                group.Messages = null;
            }
        }

        //. Apply Offset to the groups if provided
        if (query.Offset.HasValue)
        {
            response = response.Skip(query.Offset.Value);
        }

        //. Apply Limit to the groups if provided
        if (query.Limit.HasValue)
        {
            response = response.Take(query.Limit.Value);
        }

        return new OkObjectResult(new DtoResponseObject<List<ChatGroupResponseDto>>("Chat message sent successfully.", response.ToList()));
    }
    //. PATCH /api/chat/group/{groupId}
    [HttpPatch("groups/{groupId}")]
    public IActionResult Patch(string groupId, [FromBody] ChatGroupRequestDto request)
    {
        Guid.TryParse(groupId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Retrieve the groupchat
        ChatGroupResponseDto? groupChat = _groupChats.FirstOrDefault(g => g.Id == guid);
        if (groupChat == null)
        {
            return new BadRequestObjectResult("Group not found with the provided ID.");
        }
        
        //. Update the group with the new values
        groupChat.Name = request.Name;
        groupChat.Users = request.Users;
        
        //. Get the index
        var index = _groupChats.IndexOf(groupChat);
        _groupChats[index] = groupChat;
        
        //. For the purpose of testing this is simply set to null. In a real environment the messages would not be retrieved here so they would also be null
        groupChat.Messages = null;
        
        return new OkObjectResult(new DtoResponseObject<ChatGroupResponseDto>("Chat group updated successfully.", groupChat));
    }
    
    //. GET /api/chat/group/{groupId}
    [HttpGet("groups/{groupId}")]
    public IActionResult GetChatGroup(string groupId)
    {
        Guid.TryParse(groupId, out Guid guid);

        //. Retrieve the message with the id
        ChatGroupResponseDto? res = _groupChats.FirstOrDefault(m => m.Id == guid);

        res.Messages = null;
        
        return res != null
            ? new OkObjectResult(res)
            : new BadRequestObjectResult("Chatgroup not found with the provided ID.");
    }
    
    //. DELETE api/chat/groups/{groupId}
    [HttpDelete("groups/{groupId}")]
    public IActionResult DeleteChatGroup(string groupId)
    {
        Guid.TryParse(groupId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Delete the message from the list of messages if it exists
        ChatGroupResponseDto? res = _groupChats.FirstOrDefault(m => m.Id == guid);

        if (res == null)
        {
            return new BadRequestObjectResult("Chatgroup not found with the provided ID.");
        }

        _groupChats.Remove(res);
        return new OkObjectResult("Chatgroup successfully deleted.");
    }
    
    //. POST api/chat/groups/addusers/{groupId}
    [HttpPost("groups/addusers/{groupId}")]
    public IActionResult AddUsersToGroup(string groupId, [FromBody] ChatGroupAddUsersDto request)
    {
        Guid.TryParse(groupId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Delete the message from the list of messages if it exists
        ChatGroupResponseDto? groupChat = _groupChats.FirstOrDefault(m => m.Id == guid);

        if (groupChat == null)
        {
            return new BadRequestObjectResult("Chatgroup not found with the provided ID.");
        }
        
        //. Update the group by adding the users
        //. Get the index
        var index = _groupChats.IndexOf(groupChat);
        if (groupChat.Users == null)
        {
            groupChat.Users = request.Users;
        }
        else
        {
            groupChat.Users.AddRange(request.Users);
        }
        _groupChats[index] = groupChat;
        
        //. For the purpose of testing this is simply set to null. In a real environment the messages would not be retrieved here so they would also be null
        groupChat.Messages = null;
        
        return new OkObjectResult(new DtoResponseObject<ChatGroupResponseDto>( request.Users.Count + " Users have been added to the group chat.", groupChat));
    }
    //. POST api/chat/groups/removeusers/{groupId}
    [HttpPost("groups/removeusers/{groupId}")]
    public IActionResult RemoveUsersFromGroup(string groupId, [FromBody] RemoveGroupUsersRequestDto request)
    {
        Guid.TryParse(groupId, out Guid guid);

        if (guid == Guid.Empty)
        {
            return new BadRequestObjectResult("Invalid ID provided.");
        }
        
        //. Delete the message from the list of messages if it exists
        ChatGroupResponseDto? groupChat = _groupChats.FirstOrDefault(m => m.Id == guid);

        if (groupChat == null)
        {
            return new BadRequestObjectResult("Chatgroup not found with the provided ID.");
        }
        
        //. Update the group by removing the users
        if (groupChat.Users == null)
        {
            //. Just return that they were all deleted
            return new OkObjectResult(new DtoResponseObject<ChatGroupResponseDto>( request.UserIds.Count + " Users have been removed from the group chat.", groupChat));
        }
        else
        {
            List<GroupUserResponseDto> usersToRemove = groupChat.Users.Where(u => request.UserIds.Any(id => id == u.UserId.ToString())).ToList();
            foreach (var user in usersToRemove)
            {
                groupChat.Users.Remove(user);
            }
        }
        
        //. Get the index
        var index = _groupChats.IndexOf(groupChat);
        _groupChats[index] = groupChat;
        
        //. For the purpose of testing this is simply set to null. In a real environment the messages would not be retrieved here so they would also be null
        groupChat.Messages = null;
        
        return new OkObjectResult(new DtoResponseObject<ChatGroupResponseDto>(request.UserIds.Count + " Users have been removed from the group chat.", groupChat));
    }
}

