using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorPatientManagementSystem.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = "";
        public string Specialization { get; set; } = "";
        public decimal ConsultationFee { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
