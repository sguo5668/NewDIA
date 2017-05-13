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
        public IPagedList<Claim> PageData { get; set; }
    }
}
