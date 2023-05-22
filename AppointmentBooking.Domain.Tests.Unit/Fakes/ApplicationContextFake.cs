using AppointmentBooking.Domain.Database;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Domain.Tests.Unit.Fakes
{
    public class ApplicationContextFake : ApplicationContext
    {
        public ApplicationContextFake() : base(new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: $"AppointmentBookingTest-{Guid.NewGuid()}").Options) { }
    }
}
