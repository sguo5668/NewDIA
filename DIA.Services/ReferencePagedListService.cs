using PagedList.Core;
using System.Collections.Generic;
using System.Linq;
using DIA.Entities;

namespace DIA.Services
{
    public class ReferencePagedListService
    {
        private IList<ReferenceTableValue> Data;



        public ReferencePagedListService(IRepository<ReferenceTableValue> repo)
        {
            this.Data = repo.GetAll().ToList();

        }


        public IPagedList<ReferenceTableValue> GetPageData(int pageNumber, int pageSize)
        {
            var pagedata = this.Data.Skip((pageNumber - 1) * pageSize).Take(pageSize);


            return new StaticPagedList<ReferenceTableValue>(pagedata, pageNumber, pageSize, this.Data.Count);


        }
    }
}
