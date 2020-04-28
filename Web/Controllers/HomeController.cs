using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _Owner;
        private readonly IUnitOfWork<PortfolioItem> _Portfolio;

        public HomeController(IUnitOfWork<Owner> owner, IUnitOfWork<PortfolioItem> portfolio)
        {
            _Owner = owner;
            _Portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Owner = _Owner.Entity.GetAll().First(),
                PortfolioItems = _Portfolio.Entity.GetAll().ToList()
            };
            return View(model);
        }
    }
}