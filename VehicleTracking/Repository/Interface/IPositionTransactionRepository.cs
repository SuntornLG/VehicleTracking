using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPositionTransactionRepository : IRepositoryBase<PositionTransaction>
    {
        Task<IEnumerable<PositionTransactionResponeDto>> GetAllPosition(int deviseId, PositionRequest positionParam );
    }
}
