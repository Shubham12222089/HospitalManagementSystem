using DoctorPatientManagementSystem.BLL;
using DoctorPatientManagementSystem.DAL;
using DoctorPatientManagementSystem.Interfaces;

using System;

class Program
{
    static void Main()
    {
        IDoctorManager doctorDAL = new DoctorManager();
        IPatientManager patientDAL = new PatientManager();

        DoctorsBLL doctorBLL = new DoctorsBLL(doctorDAL);
        PatientsBLL patientBLL = new PatientsBLL(patientDAL);

        while (true)
        {
            Console.WriteLine("\n===== Doctor Patient Management =====");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. List Doctors");
            Console.WriteLine("3. Add Patient");
            Console.WriteLine("4. Delete Doctor");
            Console.WriteLine("5. Edit Patient");
            Console.WriteLine("6. Delete Patient");
            Console.WriteLine("7. List Patients");
            Console.WriteLine("8. Find Patient");
            Console.WriteLine("9. Exit");

            Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine()??"");

            switch (choice)
            {
                case 1:
                    doctorBLL.AddDoctor();
                    break;

                case 2:
                    doctorBLL.ListDoctors();
                    break;

                case 3:
                    Console.Write("Doctor ID: ");
                    int did = int.Parse(Console.ReadLine()??"");
                    patientBLL.AddPatient(did);
                    break;

                case 4:
                    doctorBLL.DeleteDoctor();
                    break;

                case 5:
                    patientBLL.EditPatient();
                    break;

                case 6:
                    patientBLL.DeletePatient();
                    break;

                case 7:
                    patientBLL.ListPatients();
                    break;

                case 8:
                    Console.Write("Patient Name: ");
                    string name = Console.ReadLine()??"";
                    patientBLL.FindPatient(name);
                    break;

                case 9:
                    return;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}