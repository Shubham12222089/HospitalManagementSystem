using System.Collections.Generic;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;

namespace Hospital.Application.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public void AddPatient(string name, int age, string condition)
        {
            var patient = new Patient
            {
                Name = name,
                Age = age,
                Condition = condition
            };
            _patientRepository.Add(patient);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient? GetPatientById(int id)
        {
            return _patientRepository.GetById(id);
        }
    }
}
