using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Text;
using DIA.Entities;

namespace DIA.Services
{
    public interface IPagedListService
    {
        IPagedList<Claim> GetPageData(int pageNumber, int pageSize);
    }
}
