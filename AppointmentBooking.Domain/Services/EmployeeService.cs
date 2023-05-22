using AppointmentBooking.Domain.DomainObjects;

namespace AppointmentBooking.Domain.Services
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employee>> GetEmployees();
    }

    public class EmployeesService : IEmployeesService
    {
        public async Task<IEnumerable<Employee>> GetEmployees()
            => throw new NotImplementedException();
    }
}
