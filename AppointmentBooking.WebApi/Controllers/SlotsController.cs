using AppointmentBooking.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlotsController : ControllerBase
    {
        private readonly ISlotsService _slotsService;

        public SlotsController(ISlotsService slotsService)
        {
            _slotsService = slotsService;
        }


        [HttpGet]
        public async Task<ActionResult<Contract.AvailableSlots>> GetSlots(int serviceId, int? employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
