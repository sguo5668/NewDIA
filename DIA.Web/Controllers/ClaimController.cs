using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIA.Entities;
using DIA.Services;
using DIA.Web.ViewModels;
using AutoMapper;

namespace DIA.Web.Controllers
{
    public class ClaimController : Controller
    {
     
        private PagedListService _pageService;
        private readonly IRepository<Claim> _repoClaim;

        public ClaimController(IRepository<Claim>  repoClaim)
        {
            _repoClaim = repoClaim;
            _pageService = new PagedListService(_repoClaim);

        }

        [HttpGet]
        public IActionResult Index(string currentFilter, string searchString, int? page)
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
            //var claim = _repoClaim.GetAll();

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 2;
  
            var x = new VMClaim();
            x.PageData = this._pageService.GetPageData(pageNumber, pageSize);

            //var claimHistory = _mapper.Map<IEnumerable<VMClaim>>(claim);
           return View(x);
        }
    }
}