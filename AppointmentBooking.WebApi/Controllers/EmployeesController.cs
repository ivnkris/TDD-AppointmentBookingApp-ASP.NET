using AppointmentBooking.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<Contract.AvailableEmployees>> AvailableEmployees()
        {
            throw new NotImplementedException();
        }

    }
}
