using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Events_Data;
using EventsWeb.Shared.Model;

namespace EventsWeb.Server.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,EventsCategory>().ReverseMap();
            CreateMap<Product, EventsProduct>().ReverseMap();
        }
    }
}
