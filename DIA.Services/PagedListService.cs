using PagedList.Core;
using System.Collections.Generic;
using System.Linq;
using DIA.Entities;

namespace DIA.Services
{
    public class PagedListService   
    {
       private  IList<Claim> Data;
        


        public PagedListService(IRepository<Claim> repo)
        {
            this.Data = repo.GetAll().ToList();
        
        }


        public   IPagedList<Claim> GetPageData(int pageNumber, int pageSize)
        {
           var pagedata = this.Data.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        

            return new StaticPagedList<Claim>(pagedata, pageNumber, pageSize, this.Data.Count);
            

        }
    }
}
