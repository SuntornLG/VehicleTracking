using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using LoggerService;
using Repository.Interface;
using Service.ExceptionHandler;
using Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public VehicleService(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerManager loggerManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }
        public async Task<VehicleRegisterResponseDto> CreateAsync(VehicleRegisterDto vehicle)
        {
            try
            {
                var entity = _mapper.Map<Vehicle>(vehicle);

                // check if vehicle devise already exsisting.
                var isExistVehicle = _repositoryWrapper.Vehicle
                    .FindByCondition(x => x.DiviseId == entity.DiviseId && 
                    x.LicensePlateNumber.Equals(vehicle.LicensePlateNumber)).Any();

                if (!isExistVehicle)
                {
                    entity.RegisterDate = DateTime.Now;
                    entity.IsActive = true;
                    _repositoryWrapper.Vehicle.Create(entity);
                    await _repositoryWrapper.SaveAsync();
                    return new VehicleRegisterResponseDto
                    {
                        DiviseId = entity.DiviseId,
                        LicensePlateNumber = entity.LicensePlateNumber,
                        VehicleName = entity.VehicleName
                    };
                }
                throw new ServiceCustomException("Duplicate divise is not allow");
            }
            catch (ServiceCustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error in file name VehicleService.cs on function CreateVehicle : {ex}");
                throw ex;
            }
        }
    }
}
