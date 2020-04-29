using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PortfolioViewModel
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }

        public PortfolioViewModel()
        {

        }
        public PortfolioViewModel(PortfolioItem portfolioItem)
        {
            Id = portfolioItem.Id;
            ProjectName = portfolioItem.ProjectName;
            ImageUrl = portfolioItem.ImageUrl;
            Description = portfolioItem.Description;
        }

        public PortfolioItem Map()
        {
            return new PortfolioItem
            {
                Id = Id,
                ProjectName = ProjectName,
                ImageUrl = File != null ? Path.GetFileName(File.FileName) : ImageUrl,
                Description = Description
            };
        }
    }
}
