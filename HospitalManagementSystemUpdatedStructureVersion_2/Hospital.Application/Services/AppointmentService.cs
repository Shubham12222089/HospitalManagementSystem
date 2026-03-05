using System;
using System.Collections.Generic;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;

namespace Hospital.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public bool BookAppointment(int doctorId, int patientId, DateTime appointmentDate, string reason)
        {
            // Verify doctor exists
            var doctor = _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                return false;
            }

            // Verify patient exists
            var patient = _patientRepository.GetById(patientId);
            if (patient == null)
            {
                return false;
            }

            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDate,
                Reason = reason
            };

            _appointmentRepository.Add(appointment);
            return true;
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }
    }
}
