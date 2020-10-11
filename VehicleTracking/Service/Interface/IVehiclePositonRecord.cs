using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IVehiclePositonRecord
    {
        Task<IEnumerable<PositionTransactionResponeDto>> GetAllPositionAsync(int deviseId, PositionRequest positionRequest);
        Task<CommonCreatedResponseDto> RecordPositionAsync(PositionRecordRequestDto position);

        Task<List<PositionTransactionResponeDto>> GetCurrentPositionByIdAsync(int deviseId);

        Task<List<PositionTransactionResponeDto>> GetPositionByPeriodOfTimeAsync(int deviseId, DateTime start, DateTime end);
    }
}
