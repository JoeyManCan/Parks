﻿using AutoMapper;
using Parks.API.Models;
using Parks.API.Models.DTOs;

namespace Parks.API.ParksMapper
{
    public class ParksMapping:Profile
    {
        public ParksMapping()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
            CreateMap<NationalPark, NationalParkUpdateDTO>().ReverseMap();

            CreateMap<Trail, TrailDTO>().ReverseMap();
            CreateMap<Trail, TrailUpdateDTO>().ReverseMap();
            CreateMap<Trail, TrailCreateDTO>().ReverseMap();
        }
    }
}
