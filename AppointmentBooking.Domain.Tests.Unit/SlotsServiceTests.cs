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
    }
}
