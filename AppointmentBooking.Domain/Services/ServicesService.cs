using AppointmentBooking.Domain.Database;
using AppointmentBooking.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Domain.Services
{
    public interface IServicesService
    {
        Task<Service?> GetService(int id);

        Task<IEnumerable<Service>> GetActiveServices();
    }

    public class ServicesService : IServicesService
    {
        private readonly ApplicationContext _context;

        public ServicesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetActiveServices()
            => await _context.Services!.ToArrayAsync();

        public async Task<Service?> GetService(int id)
            => throw new NotImplementedException();
    }
}
