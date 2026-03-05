using System;

namespace Hospital.Domain.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        // Foreign Keys
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        // Navigation Properties
        public Doctor Doctor { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
    }
}
