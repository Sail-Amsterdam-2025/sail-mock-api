using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Models;

namespace Sail_MockApi.Api.Services
{
    public class InformationService
    {
        private List<InfoCategory> _categories;
        private List<Information> _informations;


        public InformationService()
        {
            GenerateMockData();
        }


        public List<Information> GetInformation(int limit, int offset, string? title = null, int? categoryId = null, string? categoryName = null)
        {
            // Start with the full list of information items
            IEnumerable<Information> filteredList = _informations;

            // Apply optional filters
            if (!string.IsNullOrEmpty(title))
            {
                filteredList = filteredList.Where(info => info.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryId.HasValue)
            {
                filteredList = filteredList.Where(info => info.Category.Id == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                filteredList = filteredList.Where(info => info.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            }

            // Apply pagination
            filteredList = filteredList.Skip(offset).Take(limit);

            // Convert the result to a List before returning
            return filteredList.ToList();
        }

        public Information? GetInformationById(int informationId)
        {
            // Look for the information with the given ID
            return _informations.FirstOrDefault(info => info.id == informationId);
        }

        public void UpdateInformation(Information updatedInformation)
        {
            // Find the existing information object by ID
            var existingInfo = _informations.FirstOrDefault(info => info.id == updatedInformation.id);

            if (existingInfo != null)
            {
                // Update the fields with the new values
                existingInfo.Category = updatedInformation.Category;  // Assuming the entire Category object might be updated
                existingInfo.Title = updatedInformation.Title;
                existingInfo.Value = updatedInformation.Value;

                // Save logic (e.g., to the database or in-memory list) should go here
            }
            else
            {
                throw new InvalidOperationException("Information not found");
            }
        }

        public bool DeleteInformation(int informationId)
        {
            // Find the information item by ID
            var infoToDelete = _informations.FirstOrDefault(info => info.id == informationId);

            if (infoToDelete != null)
            {
                // Remove the item from the list (or perform the delete operation on your database)
                _informations.Remove(infoToDelete);

                return true; // Return true if the item was successfully deleted
            }

            return false; // Return false if the item was not found
        }

        public InfoCategory CreateCategory(string name)
        {
            // Generate a new ID (In a real-world scenario, this would be handled by the database)
            var newId = _categories.Count == 0 ? 1 : _categories.Max(c => c.Id) + 1;

            var newCategory = new InfoCategory
            {
                Id = newId,
                Name = name
            };

            _categories.Add(newCategory);
            return newCategory;
        }

        public List<InfoCategory> GetCategories(int limit, int offset, string? name)
        {
            // Filter by name if provided
            var query = _categories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            // Apply pagination with offset and limit
            var result = query.Skip(offset).Take(limit).ToList();
            return result;
        }

        // Helper method to get a category by ID
        public InfoCategory? GetCategoryById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }
        public InfoCategory? UpdateCategory(int id, string newName)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.Name = newName;  // Update the category name
                return category;
            }
            return null;
        }

        public bool DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
                return true;
            }
            return false;
        }












        private void GenerateMockData()
        {
             _categories = new List<InfoCategory>
            {
                new InfoCategory { Id = 1, Name = "GeneralInformation" },
                new InfoCategory { Id = 2, Name = "Toilet" },
                new InfoCategory { Id = 3, Name = "Kitchen" },
                new InfoCategory { Id = 4, Name = "Laundry" },
                new InfoCategory { Id = 5, Name = "Garage" }
            };

            _informations = new List<Information>
            {
                new Information { id = 1, Category = _categories[0], Title = "The Art of Procrastination", Value = "A detailed analysis of why doing things later is sometimes better." },
                new Information { id = 2, Category = _categories[1], Title = "Optimal Toilet Paper Placement", Value = "The eternal debate: over or under?" },
                new Information { id = 3, Category = _categories[0], Title = "The Secret Life of Socks", Value = "Where do they go? A journey into the sock dimension." },
                new Information { id = 4, Category = _categories[2], Title = "Mystery of the Lost Spoon", Value = "A guide to finding spoons that have vanished mid-meal prep." },
                new Information { id = 5, Category = _categories[0], Title = "Why Mondays Exist", Value = "Exploring the mystery of why we keep having Mondays." },
                new Information { id = 6, Category = _categories[1], Title = "Bathroom Reading Material Guide", Value = "Top 5 recommended magazines for your bathroom library." },
                new Information { id = 7, Category = _categories[3], Title = "How to Fold a Fitted Sheet", Value = "An exploration of this mysterious, almost mythical skill." },
                new Information { id = 8, Category = _categories[2], Title = "The Tale of the Wandering Fork", Value = "How utensils mysteriously move between drawers." },
                new Information { id = 9, Category = _categories[4], Title = "Finding the Right Tool", Value = "A scavenger hunt for that one tool you swear you had." },
                new Information { id = 10, Category = _categories[0], Title = "The Origin of Dust Bunnies", Value = "What are they, and why are they multiplying?" },
                new Information { id = 11, Category = _categories[1], Title = "Toilet Meditation Techniques", Value = "Finding inner peace in the quietest room of the house." },
                new Information { id = 12, Category = _categories[2], Title = "Expired Foods: Friend or Foe?", Value = "Is that yogurt still good? A guide to questionable food dates." },
                new Information { id = 13, Category = _categories[0], Title = "Where Lost Pens Go", Value = "A theory on why pens disappear only to reappear in the laundry." },
                new Information { id = 14, Category = _categories[4], Title = "The Mysterious Case of the Vanishing Screwdriver", Value = "Tools that seem to vanish into thin air." },
                new Information { id = 15, Category = _categories[1], Title = "Toilet Paper Economics", Value = "Understanding how many rolls are 'enough' in case of emergency." },
                new Information { id = 16, Category = _categories[2], Title = "The Ultimate Guide to Spices", Value = "Rediscovering spices lost at the back of the cabinet." },
                new Information { id = 17, Category = _categories[3], Title = "Sorting Socks Made Easy", Value = "Tips for keeping socks together in the wash." },
                new Information { id = 18, Category = _categories[0], Title = "Why Cats Love Boxes", Value = "An investigation into the box-loving behavior of cats." },
                new Information { id = 19, Category = _categories[4], Title = "Deciphering the Tool Drawer", Value = "A guide to the strange tools no one remembers buying." },
                new Information { id = 20, Category = _categories[1], Title = "The Science of Flushing", Value = "A deeper look into the mechanisms behind the flush." }
            };
        }


    }
}
