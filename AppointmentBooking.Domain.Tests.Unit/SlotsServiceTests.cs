using AppointmentBooking.Domain.Services;
using AppointmentBooking.Domain.Tests.Unit.Fakes;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace AppointmentBooking.Domain.Tests.Unit
{
    public class SlotsServiceTests : IDisposable
    {
        private readonly ApplicationContextFakeBuilder _contextBuilder = new();
        private readonly INowService _nowService = Substitute.For<INowService>();
        private readonly ApplicationSettings _applicationSettings = new()
        { OpenAppointmentInDays = 7, RoundUpInMin = 5, RestInMin = 5 };
        private readonly IOptions<ApplicationSettings> _settings = Substitute.For<IOptions<ApplicationSettings>>();
        private SlotsService? _sut;

        public SlotsServiceTests()
        {
            _settings.Value.Returns(_applicationSettings);
        }

        public void Dispose()
        {
            _contextBuilder.Dispose();
        }

        [Fact]
        public async Task GetAvailableSlotsForEmployee_ServiceIdNotFound_ArgumentException()
        {
            // Arrange
            var ctx = _contextBuilder.Build();
            _sut = new SlotsService(ctx, _nowService, _settings);

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetAvailableSlotsForEmployee(-1, -1));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetAvailableSlotsForEmployee_EmployeeIdNotFound_ArgumentException()
        {
            // Arrange
            var ctx = _contextBuilder.WithSingleService(30).Build();
            _sut = new SlotsService(ctx, _nowService, _settings);

            // Act 
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetAvailableSlotsForEmployee(1, -1));

            // Assert
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public async Task GetAvailableSlotsForEmployee_NoShiftsForTomAndNoAppointmentsInSystem_NoSlots()
        {
            // Arrange
            var appointmentFrom = new DateTime(2022, 10, 3, 7, 0, 0);
            _nowService.Now.Returns(appointmentFrom);
            var ctx = _contextBuilder.WithSingleService(30).WithSingleEmployeeTom().Build();
            _sut = new SlotsService(ctx, _nowService, _settings);
            var tom = ctx.Employees!.Single();
            var mensCut30Min = ctx.Services!.Single();

            // Act
            var slots = await _sut.GetAvailableSlotsForEmployee(mensCut30Min.Id, tom.Id);

            // Assert
            var times = slots.DaysSlots.SelectMany(x => x.Times);
            Assert.Empty(times);
        }
    }
}
