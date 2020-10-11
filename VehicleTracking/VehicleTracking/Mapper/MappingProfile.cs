using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace VehicleTracking.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleRegisterDto>();

            CreateMap<VehicleRegisterDto, Vehicle>();
            CreateMap<Vehicle, VehicleResponseDto>();

            CreateMap<User, UserRegisterRequestDto>();
            CreateMap<UserRegisterRequestDto, User>();

            CreateMap<UserResponseDto, User>();
            CreateMap<User, UserResponseDto>();

            CreateMap<PositionTransaction, PositionRecordRequestDto>();
            CreateMap<PositionRecordRequestDto, PositionTransaction>();

            CreateMap<PositionTransactionResponeDto, PositionTransaction>();
        }
    }
}
