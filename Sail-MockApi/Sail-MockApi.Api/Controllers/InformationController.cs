using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Models;
using Sail_MockApi.Api.Services;
using System;

namespace Sail_MockApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformationController : ControllerBase
    {
        private readonly InformationService _informationService;

        public InformationController(InformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpPost]
        public IActionResult AddInformation([FromBody] InformationRequestDTO informationRequestDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(informationRequestDTO.CategoryId) || !Guid.TryParse(informationRequestDTO.CategoryId, out Guid categoryId))
                {
                    return BadRequest("Invalid or missing CategoryId format.");
                }

                InformationResponseDTO informationResponseDTO = new InformationResponseDTO
                {
                    Id = Guid.NewGuid(),  // Generate a new GUID for Id
                    CategoryId = categoryId,
                    CategoryName = "Something important",
                    Title = informationRequestDTO.Title,
                    Value = informationRequestDTO.Value,
                };

                Information information = new Information()
                {
                    id = informationResponseDTO.Id,
                    Category = new InfoCategory()
                    {
                        Id = categoryId,
                        Name = "Something important"
                    },
                    Title = informationResponseDTO.Title,
                    Value = informationResponseDTO.Value,

                };

                _informationService.AddInformation(information);

                return Ok(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetInformation(
            [FromQuery] int limit = 50,
            [FromQuery] int offset = 0,
            [FromQuery] string? title = null,
            [FromQuery] string? categoryId = null,
            [FromQuery] string? categoryName = null)
        {
            try
            {
                Guid? parsedCategoryId = null;

                // Attempt to parse the categoryId if it is not null
                if (!string.IsNullOrEmpty(categoryId))
                {
                    if (Guid.TryParse(categoryId, out Guid categoryGuid))
                    {
                        parsedCategoryId = categoryGuid;
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid CategoryId format.");
                    }
                }

                // Fetch information from the service
                List<Information> list = _informationService.GetInformation(
                    limit,
                    offset,
                    title,
                    parsedCategoryId,  // Use parsedCategoryId, which can be null
                    categoryName);

                // Map to DTOs
                List<InformationResponseDTO> newList = new List<InformationResponseDTO>();

                foreach (Information info in list)
                {
                    InformationResponseDTO informationResponseDTO = new InformationResponseDTO
                    {
                        Id = info.id,
                        CategoryId = info.Category.Id,
                        CategoryName = info.Category.Name,
                        Title = info.Title,
                        Value = info.Value,
                    };
                    newList.Add(informationResponseDTO);
                }

                return new OkObjectResult(newList);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("{informationId}")]
        public IActionResult GetInformationById(string informationId)
        {
            try
            {
                if (!Guid.TryParse(informationId, out Guid infoGuid))
                {
                    return new BadRequestObjectResult("Invalid informationId format.");
                }

                // Fetch the information item by ID
                Information? info = _informationService.GetInformationById(infoGuid);

                if (info == null)
                {
                    return new NotFoundObjectResult(new { message = "Information not found" });
                }

                // Map the information to the response DTO
                var informationResponseDTO = new InformationResponseDTO
                {
                    Id = info.id,
                    CategoryId = info.Category.Id,
                    CategoryName = info.Category.Name,
                    Title = info.Title,
                    Value = info.Value
                };

                return new OkObjectResult(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }

        [HttpPatch("{informationId}")]
        public IActionResult UpdateInformation(string informationId, [FromBody] UpdateInformationDTO updateInformationDTO)
        {
            try
            {
                if (!Guid.TryParse(informationId, out Guid infoGuid))
                {
                    return new BadRequestObjectResult("Invalid informationId format.");
                }

                var info = _informationService.GetInformationById(infoGuid);

                if (info == null)
                {
                    return new NotFoundObjectResult(new { message = "Information not found" });
                }

                if (!string.IsNullOrEmpty(updateInformationDTO.Title))
                {
                    info.Title = updateInformationDTO.Title;
                }

                if (!string.IsNullOrEmpty(updateInformationDTO.Value))
                {
                    info.Value = updateInformationDTO.Value;
                }

                if (updateInformationDTO.CategoryId != null)
                {
                    if (!Guid.TryParse(updateInformationDTO.CategoryId, out Guid categoryGuid))
                    {
                        return new BadRequestObjectResult("Invalid CategoryId format.");
                    }

                    var category = _informationService.GetCategoryById(categoryGuid);
                    if (category == null)
                    {
                        return new BadRequestObjectResult(new { message = "Invalid CategoryId provided" });
                    }
                    info.Category = category;
                }

                _informationService.UpdateInformation(info);

                var informationResponseDTO = new InformationResponseDTO
                {
                    Id = info.id,
                    CategoryId = info.Category.Id,
                    CategoryName = info.Category.Name,
                    Title = info.Title,
                    Value = info.Value
                };

                return new OkObjectResult(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = "An error occurred while updating the information.", details = ex.Message });
            }
        }

        [HttpDelete("{informationId}")]
        public IActionResult DeleteInformation(string informationId)
        {
            try
            {
                if (!Guid.TryParse(informationId, out Guid infoGuid))
                {
                    return new BadRequestObjectResult("Invalid informationId format.");
                }

                var isDeleted = _informationService.DeleteInformation(infoGuid);

                if (!isDeleted)
                {
                    return new NotFoundObjectResult(new { message = "Information not found" });
                }

                return new OkObjectResult(new { message = "Information successfully deleted" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = "An error occurred while deleting the information.", details = ex.Message });
            }
        }
    }
}
