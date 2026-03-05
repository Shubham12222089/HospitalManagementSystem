using System.Collections.Generic;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;

namespace Hospital.Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public void AddDoctor(string name, string specialization, decimal consultationFee)
        {
            var doctor = new Doctor
            {
                Name = name,
                Specialization = specialization,
                ConsultationFee = consultationFee
            };
            _doctorRepository.Add(doctor);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor? GetDoctorById(int id)
        {
            return _doctorRepository.GetById(id);
        }
    }
}
