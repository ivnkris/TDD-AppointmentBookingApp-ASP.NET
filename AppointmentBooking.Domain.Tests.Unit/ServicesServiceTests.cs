using AppointmentBooking.Domain.Services;
using AppointmentBooking.Domain.Tests.Unit.Fakes;

namespace AppointmentBooking.Domain.Tests.Unit
{
    public class ServicesServiceTests : IDisposable
    {
        private readonly ApplicationContextFakeBuilder _ctxBldr = new();
        private ServicesService? _sut;

        [Fact]
        public async Task GetActiveServices_NoServiceInTheSystem_NoServices()
        {
            // Arrange
            var ctx = _ctxBldr.Build();
            _sut = new ServicesService(ctx);

            // Act
            var actual = await _sut.GetActiveServices();

            // Assert
            Assert.True(!actual.Any());
        }

        public void Dispose()
        {
            _ctxBldr.Dispose();
        }
    }
}