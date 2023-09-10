using MyDoctorAppointment.Domain.Enums;

namespace MyDoctorAppointment.Domain.Entities
{
    public class Patient : UserBase
    {
        public IllnestTypes IllnestType { get; set; }

        public string? AdditionalInfo { get; set; }

        public string? Address { get; set; }
    }
}