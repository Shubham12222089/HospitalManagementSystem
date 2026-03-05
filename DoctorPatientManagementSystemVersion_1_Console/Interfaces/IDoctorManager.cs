using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorPatientManagementSystem.Interfaces
{
    public interface IDoctorManager
    {
        void AddDoctor();
        void ListDoctors();
        void DeleteDoctor();
        // DB operations
        void AddDoctorToDB();
        void ListDoctorsFromDB();
        void DeleteDoctorFromDB();
    }
}
