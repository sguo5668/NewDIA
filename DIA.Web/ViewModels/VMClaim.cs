using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIA.Entities;

namespace DIA.Web.ViewModels
{
    public class VMClaim
    {
        public IPagedList<Claim> ClaimPageData { get; set; }
        public IPagedList<ReferenceTableValue> ReferencePageData { get; set; }
    }


}
