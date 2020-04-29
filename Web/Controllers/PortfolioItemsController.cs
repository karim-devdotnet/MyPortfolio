using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Core.Interfaces;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _Portfolio;
        private readonly IHostingEnvironment _HostingEnvironment;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolio, IHostingEnvironment hostingEnvironment)
        {
            _Portfolio = portfolio;
            _HostingEnvironment = hostingEnvironment;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_Portfolio.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _Portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File != null)
                {
                    var uploads = Path.Combine(_HostingEnvironment.WebRootPath, @"img\portfolio");
                    var fullPath = Path.Combine(uploads, Path.GetFileName(model.File.FileName));
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                var portfolioItem = model.Map();
                _Portfolio.Entity.Insert(portfolioItem);
                _Portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _Portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            return View(new PortfolioViewModel(portfolioItem));
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(model.File != null)
                    {
                        var uploads = Path.Combine(_HostingEnvironment.WebRootPath, @"img\portfolio");
                        var fullPath = Path.Combine(uploads, Path.GetFileName(model.File.FileName));
                        //delete old file
                        var oldImagePath = Path.Combine(uploads, model.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);

                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    var portfolioItem = model.Map();
                    _Portfolio.Entity.Update(portfolioItem);
                    _Portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _Portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //delete file
            var portfolioItem = _Portfolio.Entity.GetById(id);
            var uploads = Path.Combine(_HostingEnvironment.WebRootPath, @"img\portfolio");

            var oldImagePath = Path.Combine(uploads, portfolioItem.ImageUrl);
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);

            _Portfolio.Entity.Delete(id);
            _Portfolio.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _Portfolio.Entity.GetById(id) != null;
        }
    }
}
