using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository.Interface;
using Service.Implement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace VehicleTrackingTest.Service
{
    public class VehicleServiceTest
    {
        private readonly Mock<IRepositoryWrapper> _repositoryWrapper;
        private readonly Mock<ILoggerManager> _loggerManager;
        private readonly VehicleService _vehicleService;
        private readonly VehiclePositonRecordService _vehiclePositonRecordService;
        public VehicleServiceTest()
        {
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _loggerManager = new Mock<ILoggerManager>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Vehicle, VehicleRegisterDto>();
                opts.CreateMap<VehicleRegisterDto, Vehicle>();
            });

            var mapper = config.CreateMapper();
            _vehicleService = new VehicleService(_repositoryWrapper.Object, mapper, _loggerManager.Object);
            _vehiclePositonRecordService = new VehiclePositonRecordService(_repositoryWrapper.Object, mapper, _loggerManager.Object);
        }


        private List<PositionTransaction> InitVehicleList()
        {

            var vehicleList = new List<PositionTransaction>();
            vehicleList.Add(new PositionTransaction
            {
                DiviseId = 234566,
                Vehicle = new Vehicle
                {
                    DiviseId = 234566,
                    VehicleName = "XX"
                }
            });

            vehicleList.Add(new PositionTransaction
            {
                DiviseId = 234567,
                Vehicle = new Vehicle
                {
                    DiviseId = 234567,
                    VehicleName = "CC"
                }
            });

            return vehicleList;

        }

        [Fact]
        public async Task CreateVehicleCalledOnce_ShouldPass()
        {
            Vehicle vehicle = null;

            _repositoryWrapper.Setup(r => r.Vehicle.Create(It.IsAny<Vehicle>()))
                .Callback<Vehicle>(x => vehicle = x);

            var vehicleDto = new VehicleRegisterDto
            {
                DiviseId = 234566,
                LicensePlateNumber = "235-BKK-TH",
                VehicleName = "Toyota",
            };


            await _vehicleService.CreateAsync(vehicleDto);

            _repositoryWrapper.Verify(x => x.Vehicle.Create(It.IsAny<Vehicle>()), Times.Once);

            Assert.Equal(vehicle.DiviseId, vehicleDto.DiviseId);
            Assert.Equal(vehicle.LicensePlateNumber, vehicleDto.LicensePlateNumber);
            Assert.Equal(vehicle.VehicleName, vehicleDto.VehicleName);
        }


        [Fact]
        public async Task GetVehicleByCondition()
        {

            // Arrange
            var contextMock = new Mock<IDbContext>();
            contextMock.Setup(a => a.Set<PositionTransaction>()).Returns(Mock.Of<IDbSet<PositionTransaction>>);
            //contextMock.Setup(a => a.Set<Role>()).Returns(Mock.Of<IDbSet<Role>>);
            //contextMock.Setup(a => a.Set<Team>()).Returns(Mock.Of<IDbSet<Team>>);

            var unitOfWorkMock = new Mock<IRepositoryWrapper>();

            //var consoleMock = new Mock<IConsole>();
            //consoleMock.Setup(c => c.ReadInput()).Returns(new Queue<string>(new[] { "2", "5" }).Dequeue);

            var container = GetMockedContainer(contextMock.Object, unitOfWorkMock.Object, consoleMock.Object);

            // Act
            Program.SetContainer(container);
            Program.Main(null);

            // Assert
            unitOfWorkMock.Verify(a => a.StartTransaction(), Times.Exactly(1));
            unitOfWorkMock.Verify(a => a.CommitTransaction(), Times.Exactly(1));

        }

    }
}
