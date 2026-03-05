using DoctorPatientManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorPatientManagementSystem.BLL
{
    public class PatientsBLL
    {
        private readonly IPatientManager _patientManager;

        public PatientsBLL(IPatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        public void AddPatient(int doctorId)
        {
            if (doctorId <= 0)
            {
                Console.WriteLine("Invalid Doctor ID");
                return;
            }
            _patientManager.AddPatient(doctorId);
        }

        public void EditPatient()
        {
            _patientManager.EditPatient();
        }

        public void DeletePatient()
        {
            _patientManager.DeletePatient();
        }

        public void ListPatients()
        {
            _patientManager.ListPatients();
        }

        public void FindPatient(string name)
        {
            _patientManager.FindPatient(name);
        }
    }
}
