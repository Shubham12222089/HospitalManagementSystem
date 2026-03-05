using DoctorPatientManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorPatientManagementSystem.BLL
{
    public class DoctorsBLL
    {
        private readonly IDoctorManager _doctorManager;

        public DoctorsBLL(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        public void AddDoctor()
        {
            _doctorManager.AddDoctor();
        }

        public void ListDoctors()
        {
            _doctorManager.ListDoctors();
        }
        public void DeleteDoctor()
        {
            _doctorManager.DeleteDoctor();

        }
    }
}
