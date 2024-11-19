using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.DTOs;
using Sail_MockApi.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddInformation(Information information)
        {
            _informations.Add(information);
        }

        public List<Information> GetInformation(int limit, int offset, string? title = null, Guid? categoryId = null, string? categoryName = null)
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

            return filteredList.ToList();
        }

        public Information? GetInformationById(Guid informationId)
        {
            return _informations.FirstOrDefault(info => info.id == informationId);
        }

        public void UpdateInformation(Information updatedInformation)
        {
            var existingInfo = _informations.FirstOrDefault(info => info.id == updatedInformation.id);

            if (existingInfo != null)
            {
                existingInfo.Category = updatedInformation.Category;
                existingInfo.Title = updatedInformation.Title;
                existingInfo.Value = updatedInformation.Value;
            }
            else
            {
                throw new InvalidOperationException("Information not found");
            }
        }

        public bool DeleteInformation(Guid informationId)
        {
            var infoToDelete = _informations.FirstOrDefault(info => info.id == informationId);

            if (infoToDelete != null)
            {
                _informations.Remove(infoToDelete);
                return true;
            }

            return false;
        }

        public InfoCategory CreateCategory(string name)
        {
            var newCategory = new InfoCategory
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _categories.Add(newCategory);
            return newCategory;
        }

        public List<InfoCategory> GetCategories(int limit, int offset, string? name)
        {
            var query = _categories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return query.Skip(offset).Take(limit).ToList();
        }

        public InfoCategory? GetCategoryById(Guid id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public InfoCategory? UpdateCategory(Guid id, string newName)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.Name = newName;
                return category;
            }
            return null;
        }

        public bool DeleteCategory(Guid id)
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
                new InfoCategory { Id = Guid.NewGuid(), Name = "GeneralInformation" },
                new InfoCategory { Id = Guid.NewGuid(), Name = "Toilet" },
                new InfoCategory { Id = Guid.NewGuid(), Name = "Kitchen" },
                new InfoCategory { Id = Guid.NewGuid(), Name = "Laundry" },
                new InfoCategory { Id = Guid.NewGuid(), Name = "Garage" }
            };

            _informations = new List<Information>
            {
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "The Art of Procrastination", Value = "A detailed analysis of why doing things later is sometimes better." },
                new Information { id = Guid.NewGuid(), Category = _categories[1], Title = "Optimal Toilet Paper Placement", Value = "The eternal debate: over or under?" },
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "The Secret Life of Socks", Value = "Where do they go? A journey into the sock dimension." },
                new Information { id = Guid.NewGuid(), Category = _categories[2], Title = "Mystery of the Lost Spoon", Value = "A guide to finding spoons that have vanished mid-meal prep." },
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "Why Mondays Exist", Value = "Exploring the mystery of why we keep having Mondays." },
                new Information { id = Guid.NewGuid(), Category = _categories[1], Title = "Bathroom Reading Material Guide", Value = "Top 5 recommended magazines for your bathroom library." },
                new Information { id = Guid.NewGuid(), Category = _categories[3], Title = "How to Fold a Fitted Sheet", Value = "An exploration of this mysterious, almost mythical skill." },
                new Information { id = Guid.NewGuid(), Category = _categories[2], Title = "The Tale of the Wandering Fork", Value = "How utensils mysteriously move between drawers." },
                new Information { id = Guid.NewGuid(), Category = _categories[4], Title = "Finding the Right Tool", Value = "A scavenger hunt for that one tool you swear you had." },
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "The Origin of Dust Bunnies", Value = "What are they, and why are they multiplying?" },
                new Information { id = Guid.NewGuid(), Category = _categories[1], Title = "Toilet Meditation Techniques", Value = "Finding inner peace in the quietest room of the house." },
                new Information { id = Guid.NewGuid(), Category = _categories[2], Title = "Expired Foods: Friend or Foe?", Value = "Is that yogurt still good? A guide to questionable food dates." },
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "Where Lost Pens Go", Value = "A theory on why pens disappear only to reappear in the laundry." },
                new Information { id = Guid.NewGuid(), Category = _categories[4], Title = "The Mysterious Case of the Vanishing Screwdriver", Value = "Tools that seem to vanish into thin air." },
                new Information { id = Guid.NewGuid(), Category = _categories[1], Title = "Toilet Paper Economics", Value = "Understanding how many rolls are 'enough' in case of emergency." },
                new Information { id = Guid.NewGuid(), Category = _categories[2], Title = "The Ultimate Guide to Spices", Value = "Rediscovering spices lost at the back of the cabinet." },
                new Information { id = Guid.NewGuid(), Category = _categories[3], Title = "Sorting Socks Made Easy", Value = "Tips for keeping socks together in the wash." },
                new Information { id = Guid.NewGuid(), Category = _categories[0], Title = "Why Cats Love Boxes", Value = "An investigation into the box-loving behavior of cats." },
                new Information { id = Guid.NewGuid(), Category = _categories[4], Title = "Deciphering the Tool Drawer", Value = "A guide to the strange tools no one remembers buying." },
                new Information { id = Guid.NewGuid(), Category = _categories[1], Title = "The Science of Flushing", Value = "A deeper look into the mechanisms behind the flush." }
            };
        }
    }
}
