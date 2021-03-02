// Basically a Mapper from/to dto

using AutoMapper;
using AirlineApi.Dtos;
using AirlineApi.Models;

namespace AirlineApi.Profiles
{
    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
            // Source -> Target
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
        }
    }
}