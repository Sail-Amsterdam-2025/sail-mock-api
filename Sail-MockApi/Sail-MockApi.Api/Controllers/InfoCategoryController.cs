using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Models;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoCategoryController : ControllerBase
    {
        private readonly InformationService _informationService;

        public InfoCategoryController(InformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] InfoCategoryRequestDTO requestDTO)
        {
            try
            {
                // Validate incoming request
                if (string.IsNullOrEmpty(requestDTO.Name))
                {
                    return BadRequest(new { message = "Category name cannot be empty" });
                }

                // Create the new category
                var newCategory = _informationService.CreateCategory(requestDTO.Name);

                // Map the created category to a response DTO
                var responseDTO = new InfoCategoryResponseDTO
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name
                };

                // Return the response with a 201 Created status
                return CreatedAtAction(nameof(GetCategoryById), new { categoryId = newCategory.Id }, responseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetCategories(
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0,
            [FromQuery] string? name = null)
        {
            try
            {
                // Get the categories from the service with the given parameters
                var categories = _informationService.GetCategories(limit, offset, name);

                // If no categories are found, return a 404
                if (categories == null || categories.Count == 0)
                {
                    return NotFound(new { message = "No categories found" });
                }

                // Map the result to InfoCategoryResponseDTO
                var responseDTO = categories.Select(c => new InfoCategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                // Return the list of categories with a 200 OK status
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {
                // Fetch the category from the service by ID
                var category = _informationService.GetCategoryById(categoryId);

                // If the category is not found, return 404 Not Found
                if (category == null)
                {
                    return NotFound(new { message = "Category not found" });
                }

                // Map the result to the InfoCategoryResponseDTO
                var responseDTO = new InfoCategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };

                // Return the category with a 200 OK response
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                // If any error occurs, return a 500 Internal Server Error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] InfoCategoryRequestDTO updateCategoryRequest)
        {
            try
            {
                // Update the category using the service
                var updatedCategory = _informationService.UpdateCategory(categoryId, updateCategoryRequest.Name);

                // If the category is not found, return 404 Not Found
                if (updatedCategory == null)
                {
                    return NotFound(new { message = "Category not found" });
                }

                // Map the result to the InfoCategoryResponseDTO
                var responseDTO = new InfoCategoryResponseDTO
                {
                    Id = updatedCategory.Id,
                    Name = updatedCategory.Name
                };

                // Return the updated category with a 200 OK response
                return Ok(responseDTO);
            }
            catch (Exception ex)
            {
                // If any error occurs, return a 500 Internal Server Error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                // Delete the category using the service
                var isDeleted = _informationService.DeleteCategory(categoryId);

                // If the category is not found, return 404 Not Found
                if (!isDeleted)
                {
                    return new NotFoundObjectResult(new { message = "Category not found" });
                }

                // Return a 204 No Content response if the deletion is successful
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // If any error occurs, return a 500 Internal Server Error
                return new NotFoundObjectResult(new { message = ex.Message });
            }
        }

    }
}
