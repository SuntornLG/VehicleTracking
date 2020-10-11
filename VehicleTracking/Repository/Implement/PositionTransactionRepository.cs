using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Helper;


namespace Repository.Implement
{
    public class PositionTransactionRepository : RepositoryBase<PositionTransaction>, IPositionTransactionRepository
    {
        public PositionTransactionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<PositionTransactionResponeDto>> GetAllPosition(int deviseId, PositionRequest positionParam)
        {

            var result = PagedList<PositionTransaction>
                .ToPagedList(
                FindByCondition(x => x.DiviseId != 0)
                .Include(i => i.Vehicle)
                .OrderBy(o => o.DiviseId)
                , positionParam.PageNumber, positionParam.PageSize);

            if (result != null && result.Any())
            {
                var group = result.GroupBy(g => g.DiviseId);
                var response = group.Select(s => new PositionTransactionResponeDto
                {
                    DiviseId = s.FirstOrDefault().Vehicle.DiviseId,
                    LicensePlateNumber = s.FirstOrDefault().Vehicle.LicensePlateNumber,
                    VehicleName = s.FirstOrDefault().Vehicle.VehicleName,
                    Positions = s.Select(c => new Position
                    {
                        DeviseId = c.DiviseId,
                        Latitude = c.Latitude,
                        Longtitude = c.Longtitude,
                        TransactionDate = c.TransactionDate,
                    }).ToList()
                }).ToList();

                return await Task.FromResult<IEnumerable<PositionTransactionResponeDto>>(response);
            }
            return new List<PositionTransactionResponeDto> { };
        }
    }
}
