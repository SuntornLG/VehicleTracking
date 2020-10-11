using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Service.ExceptionHandler;

namespace Service.Implement
{
    public class VehiclePositonRecordService : IVehiclePositonRecord
    {
        private readonly IRepositoryWrapper _repositoryWraper;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public VehiclePositonRecordService(IRepositoryWrapper repositoryWraper,
            IMapper mapper, ILoggerManager loggerManager)
        {
            _repositoryWraper = repositoryWraper;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task<IEnumerable<PositionTransactionResponeDto>> GetAllPositionAsync(int deviseId, PositionRequest positionRequest)
        {
            var result = await _repositoryWraper.PositionTransaction
                .GetAllPosition(deviseId, positionRequest);

            return result;
        }


        /// <summary>
        /// Get current position by id
        /// </summary>
        /// <param name="deviseId"></param>
        /// <returns></returns>
        public async Task<List<PositionTransactionResponeDto>> GetCurrentPositionByIdAsync(int deviseId)
        {
            try
            {
                var result = await _repositoryWraper.PositionTransaction
                       .FindByCondition(x => x.DiviseId.Equals(deviseId)).Include(p => p.Vehicle)
                       .OrderByDescending(o => o.TransactionDate)
                       .Select(s => new PositionTransactionResponeDto
                       {
                           DiviseId = s.Vehicle.DiviseId,
                           LicensePlateNumber = s.Vehicle.LicensePlateNumber,
                           VehicleName = s.Vehicle.VehicleName,
                           Positions = new List<Position> { new Position { Latitude = s.Latitude, Longtitude = s.Longtitude, TransactionDate = s.TransactionDate } }
                       }).FirstOrDefaultAsync();

                if (result != null)
                    return new List<PositionTransactionResponeDto> { result };
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error in file name VehiclePositonRecordService.cs on function GetCurrentPositionById : {ex}");
            }

            return new List<PositionTransactionResponeDto> { };
        }


        /// <summary>
        /// Get position by reriod of time
        /// </summary>
        /// <param name="deviseId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<PositionTransactionResponeDto>> GetPositionByPeriodOfTimeAsync(int deviseId, DateTime start, DateTime end)
        {
            try
            {
                // Add milliseconds to avoid milliseconds gap.
                start = start.AddMilliseconds(000);
                end = end.AddMilliseconds(999);

                var result = await _repositoryWraper.PositionTransaction
                    .FindByCondition(x => x.DiviseId.Equals(deviseId) && x.TransactionDate >= start && x.TransactionDate <= end)
                    .Include(p => p.Vehicle).ToListAsync();

                if (result != null && result.Any())
                {
                    var response = new PositionTransactionResponeDto
                    {
                        DiviseId = result?.FirstOrDefault()?.Vehicle?.DiviseId ?? 0,
                        LicensePlateNumber = result?.FirstOrDefault()?.Vehicle?.LicensePlateNumber ?? "",
                        VehicleName = result?.FirstOrDefault()?.Vehicle?.VehicleName ?? "",
                        Positions = result.Select(s => new Position
                        {
                            Latitude = s.Latitude,
                            Longtitude = s.Longtitude,
                            TransactionDate = s.TransactionDate
                        }).OrderByDescending(o => o.TransactionDate).ToList()
                    };
                    return new List<PositionTransactionResponeDto> { response };
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error in file name VehiclePositonRecordService.cs on function GetPositionByPeriodOfTime : {ex}");
            }
            return new List<PositionTransactionResponeDto> { };
        }

        public async Task<CommonCreatedResponseDto> RecordPositionAsync(PositionRecordRequestDto position)
        {
            try
            {

                var entity = _mapper.Map<PositionTransaction>(position);
                var isValidVehicle = _repositoryWraper.Vehicle
                    .FindByCondition(x => x.DiviseId == entity.DiviseId && x.IsActive).Any();

                if (isValidVehicle)
                {
                    entity.TransactionDate = DateTime.Now;
                    _repositoryWraper.PositionTransaction.Create(entity);
                    await _repositoryWraper.SaveAsync();
                    return new CommonCreatedResponseDto { IsSuccess = true, Message = string.Empty };
                }

                throw new ServiceCustomException("Divise not active or no vehicle found.");
            }
            catch (ServiceCustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error in file name VehiclePositonRecordService.cs on function RecordPosition : {ex}");
                throw ex;
            }
        }
    }
}
