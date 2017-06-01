using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using DIA.Entities;
using DIA.Services;
using DIA.Web.ViewModels;
using AutoMapper;


namespace DIA.Web.Controllers
{
    public class ReferenceController : Controller
    {

        private ReferencePagedListService _pageService;
        private readonly IRepository<ReferenceTableValue> _repoReference;

        public ReferenceController(IRepository<ReferenceTableValue> repoReference)
        {
            _repoReference = repoReference;
            _pageService = new ReferencePagedListService(_repoReference);

        }

        [Route("[controller]")]
        // GET: Reference
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
       

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 2;

            var x = new VMClaim();
            x.ReferencePageData = this._pageService.GetPageData(pageNumber, pageSize) ;

            //var claimHistory = _mapper.Map<IEnumerable<VMClaim>>(claim);
            return View(x);
        }


        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult ResourcesList(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 2;

            var x = new VMClaim();
            x.ReferencePageData = this._pageService.GetPageData(pageNumber, pageSize);

            //var claimHistory = _mapper.Map<IEnumerable<VMClaim>>(claim);
            return View(x);
        }


        // GET: Reference/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reference/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reference/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reference/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reference/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reference/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}