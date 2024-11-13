using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Models;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformationController
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
                InformationResponseDTO informationResponseDTO = new InformationResponseDTO()
                {
                    Id = new Random().Next(1, 999999),
                    CategoryId = informationRequestDTO.CategoryId,
                    CategoryName = "SOmething important",
                    Title = informationRequestDTO.Title,
                    Value = informationRequestDTO.Value,
                };


                return new OkObjectResult(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetInformation(
        [FromQuery] int limit = 50,
        [FromQuery] int offset = 0,
        [FromQuery] string? title = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? categoryName = null)
        {
            try
            {

                List<Information> list = _informationService.GetInformation(limit, offset, title, categoryId, categoryName);

                List<InformationResponseDTO> newList = new List<InformationResponseDTO>();

                foreach (Information info in list)
                {
                    InformationResponseDTO informationResponseDTO = new InformationResponseDTO()
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
        public IActionResult GetInformationById(int informationId)
        {
            try
            {

                // Fetch the information item by ID
                Information? info = _informationService.GetInformationById(informationId);

                // If information not found, return 404
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

                // Return the information as a 200 OK response
                return new OkObjectResult(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = ex.Message });
            }
        }

        [HttpPatch("{informationId}")]
        public IActionResult UpdateInformation(int informationId, [FromBody] UpdateInformationDTO updateInformationDTO)
        {
            try
            {
                // Fetch the existing information item by ID
                var info = _informationService.GetInformationById(informationId);

                if (info == null)
                {
                    return new NotFoundObjectResult(new { message = "Information not found" });
                }

                // Check if Title is provided and update it
                if (!string.IsNullOrEmpty(updateInformationDTO.Title))
                {
                    info.Title = updateInformationDTO.Title;
                }

                // Check if Value is provided and update it
                if (!string.IsNullOrEmpty(updateInformationDTO.Value))
                {
                    info.Value = updateInformationDTO.Value;
                }

                // If a CategoryId is provided, update the Category
                if (updateInformationDTO.CategoryId.HasValue)
                {
                    var category = _informationService.GetCategoryById(updateInformationDTO.CategoryId.Value);
                    if (category == null)
                    {
                        return new BadRequestObjectResult(new { message = "Invalid CategoryId provided" });
                    }
                    info.Category = category; // Update the category of the information item
                }

                // Persist the updated information
                _informationService.UpdateInformation(info);

                // Map the updated information to the response DTO
                var informationResponseDTO = new InformationResponseDTO
                {
                    Id = info.id,
                    CategoryId = info.Category.Id,
                    CategoryName = info.Category.Name,
                    Title = info.Title,
                    Value = info.Value
                };

                // Return the updated information as a 200 OK response
                return new OkObjectResult(informationResponseDTO);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = "An error occurred while updating the information.", details = ex.Message });
            }
        }

        [HttpDelete("{informationId}")]
        public IActionResult DeleteInformation(int informationId)
        {
            try
            {
                // Try to delete the information item from the service
                var isDeleted = _informationService.DeleteInformation(informationId);

                if (!isDeleted)
                {
                    // Return 404 if the information was not found
                    return new NotFoundObjectResult(new { message = "Information not found" });
                }

                // Return a 200 OK response to indicate successful deletion
                return new OkObjectResult(new { message = "Information successfully deleted" });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a 400 BadRequest
                return new BadRequestObjectResult(new { message = "An error occurred while deleting the information.", details = ex.Message });
            }
        }


    }
}