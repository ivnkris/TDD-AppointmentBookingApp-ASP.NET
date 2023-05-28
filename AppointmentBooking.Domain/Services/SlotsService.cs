using AppointmentBooking.Domain.Database;
using AppointmentBooking.Domain.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppointmentBooking.Domain.Services
{
    public interface ISlotsService
    {
        Task<Slots> GetAvailableSlotsForEmployee(int serviceId, int employeeId);

        Task<Slots> GetCombinedAvailableSlots(int serviceId);
    }

    public class SlotsService : ISlotsService
    {
        private readonly ApplicationContext _context;
        private readonly ApplicationSettings _settings;
        private readonly DateTime _now;
        private readonly TimeSpan _roundingIntervalSpan;

        public SlotsService(ApplicationContext context, INowService nowService, IOptions<ApplicationSettings> settings)
        {
            _context = context;
            _settings = settings.Value;
            _roundingIntervalSpan = TimeSpan.FromMinutes(_settings.RoundUpInMin);
            _now = RoundUpToNearest(nowService.Now);
        }

        public async Task<Slots> GetAvailableSlotsForEmployee(int serviceId, int employeeId)
        {
            var service = await _context.Services!.SingleOrDefaultAsync(x => x.Id == serviceId);
            if (service is null)
            {
                throw new ArgumentException($"Service with id {serviceId} not found");
            }
            var isEmpFound = await _context.Employees!.AnyAsync(x => x.Id == employeeId);
            if (!isEmpFound)
            {
                throw new ArgumentException($"Employee with id {employeeId} not found");
            }
            return null;
        }

        public async Task<Slots> GetCombinedAvailableSlots(int serviceId)
        {
            throw new NotImplementedException();
        }

        private DateTime RoundUpToNearest(DateTime dt)
        {
            var ticksInSpan = _roundingIntervalSpan.Ticks;
            return new DateTime((dt.Ticks + ticksInSpan - 1) / ticksInSpan * ticksInSpan, dt.Kind);
        }
    }
}
