namespace AppointmentBooking.Domain.DomainObjects
{
    public class Service
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public short AppointmentTimeSpanInMin { get; set; }
        public decimal Price { get; set; }
    }
}
