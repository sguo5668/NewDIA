using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIA.Entities;
using DIA.Web.ViewModels;

namespace DIA.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Claim, VMClaim>();
           // CreateMap<ClaimHistory, Claim>();
        }
    }
}
