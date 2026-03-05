using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Repositories;
using Hospital.Application.Services;

namespace Hospital.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure DbContext with SQL Server
            var connectionString = "Data Source=.\\SQLEXPRESS;Database=HospitalManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
            
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var context = new HospitalDbContext(optionsBuilder.Options);
            
            // Ensure database is created
            context.Database.EnsureCreated();

            // Manual Dependency Injection - Create repositories
            IDoctorRepository doctorRepository = new DoctorRepository(context);
            IPatientRepository patientRepository = new PatientRepository(context);
            IAppointmentRepository appointmentRepository = new AppointmentRepository(context);

            // Create services
            var doctorService = new DoctorService(doctorRepository);
            var patientService = new PatientService(patientRepository);
            var appointmentService = new AppointmentService(appointmentRepository, doctorRepository, patientRepository);

            // Run the application
            RunApplication(doctorService, patientService, appointmentService);
        }

        static void RunApplication(DoctorService doctorService, PatientService patientService, AppointmentService appointmentService)
        {
            bool exit = false;

            while (!exit)
            {
                System.Console.Clear();
                System.Console.WriteLine("========================================");
                System.Console.WriteLine("   HOSPITAL MANAGEMENT SYSTEM");
                System.Console.WriteLine("========================================");
                System.Console.WriteLine("1. Add Doctor");
                System.Console.WriteLine("2. Add Patient");
                System.Console.WriteLine("3. Book Appointment");
                System.Console.WriteLine("4. List Doctors");
                System.Console.WriteLine("5. List Patients");
                System.Console.WriteLine("6. List Appointments");
                System.Console.WriteLine("7. Exit");
                System.Console.WriteLine("========================================");
                System.Console.Write("Enter your choice: ");

                var choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDoctor(doctorService);
                        break;
                    case "2":
                        AddPatient(patientService);
                        break;
                    case "3":
                        BookAppointment(appointmentService, doctorService, patientService);
                        break;
                    case "4":
                        ListDoctors(doctorService);
                        break;
                    case "5":
                        ListPatients(patientService);
                        break;
                    case "6":
                        ListAppointments(appointmentService);
                        break;
                    case "7":
                        exit = true;
                        System.Console.WriteLine("Thank you for using Hospital Management System!");
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        PressAnyKey();
                        break;
                }
            }
        }

        static void AddDoctor(DoctorService doctorService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== ADD DOCTOR ===\n");

            System.Console.Write("Enter Doctor Name: ");
            var name = System.Console.ReadLine() ?? "";

            System.Console.Write("Enter Specialization: ");
            var specialization = System.Console.ReadLine() ?? "";

            System.Console.Write("Enter Consultation Fee: ");
            if (decimal.TryParse(System.Console.ReadLine(), out decimal fee))
            {
                doctorService.AddDoctor(name, specialization, fee);
                System.Console.WriteLine("\nDoctor added successfully!");
            }
            else
            {
                System.Console.WriteLine("\nInvalid fee amount.");
            }

            PressAnyKey();
        }

        static void AddPatient(PatientService patientService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== ADD PATIENT ===\n");

            System.Console.Write("Enter Patient Name: ");
            var name = System.Console.ReadLine() ?? "";

            System.Console.Write("Enter Age: ");
            if (!int.TryParse(System.Console.ReadLine(), out int age))
            {
                System.Console.WriteLine("\nInvalid age.");
                PressAnyKey();
                return;
            }

            System.Console.Write("Enter Medical Condition: ");
            var condition = System.Console.ReadLine() ?? "";

            patientService.AddPatient(name, age, condition);
            System.Console.WriteLine("\nPatient added successfully!");

            PressAnyKey();
        }

        static void BookAppointment(AppointmentService appointmentService, DoctorService doctorService, PatientService patientService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== BOOK APPOINTMENT ===\n");

            // Show available doctors
            System.Console.WriteLine("Available Doctors:");
            var doctors = doctorService.GetAllDoctors();
            foreach (var doc in doctors)
            {
                System.Console.WriteLine($"  ID: {doc.DoctorId} | {doc.Name} ({doc.Specialization})");
            }

            System.Console.Write("\nEnter Doctor ID: ");
            if (!int.TryParse(System.Console.ReadLine(), out int doctorId))
            {
                System.Console.WriteLine("Invalid Doctor ID.");
                PressAnyKey();
                return;
            }

            // Show available patients
            System.Console.WriteLine("\nAvailable Patients:");
            var patients = patientService.GetAllPatients();
            foreach (var pat in patients)
            {
                System.Console.WriteLine($"  ID: {pat.PatientId} | {pat.Name} (Age: {pat.Age})");
            }

            System.Console.Write("\nEnter Patient ID: ");
            if (!int.TryParse(System.Console.ReadLine(), out int patientId))
            {
                System.Console.WriteLine("Invalid Patient ID.");
                PressAnyKey();
                return;
            }

            System.Console.Write("Enter Appointment Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(System.Console.ReadLine(), out DateTime appointmentDate))
            {
                System.Console.WriteLine("Invalid date format.");
                PressAnyKey();
                return;
            }

            System.Console.Write("Enter Reason for Visit: ");
            var reason = System.Console.ReadLine() ?? "";

            var success = appointmentService.BookAppointment(doctorId, patientId, appointmentDate, reason);
            
            if (success)
            {
                System.Console.WriteLine("\nAppointment booked successfully!");
            }
            else
            {
                System.Console.WriteLine("\nFailed to book appointment. Please check Doctor ID and Patient ID.");
            }

            PressAnyKey();
        }

        static void ListDoctors(DoctorService doctorService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== LIST OF DOCTORS ===\n");

            var doctors = doctorService.GetAllDoctors();
            
            if (!doctors.Any())
            {
                System.Console.WriteLine("No doctors found.");
            }
            else
            {
                System.Console.WriteLine($"{"ID",-5} {"Name",-20} {"Specialization",-20} {"Fee",-10}");
                System.Console.WriteLine(new string('-', 55));
                
                foreach (var doctor in doctors)
                {
                    System.Console.WriteLine($"{doctor.DoctorId,-5} {doctor.Name,-20} {doctor.Specialization,-20} {doctor.ConsultationFee,-10:C}");
                }
            }

            PressAnyKey();
        }

        static void ListPatients(PatientService patientService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== LIST OF PATIENTS ===\n");

            var patients = patientService.GetAllPatients();
            
            if (!patients.Any())
            {
                System.Console.WriteLine("No patients found.");
            }
            else
            {
                System.Console.WriteLine($"{"ID",-5} {"Name",-20} {"Age",-5} {"Condition",-25}");
                System.Console.WriteLine(new string('-', 55));
                
                foreach (var patient in patients)
                {
                    System.Console.WriteLine($"{patient.PatientId,-5} {patient.Name,-20} {patient.Age,-5} {patient.Condition,-25}");
                }
            }

            PressAnyKey();
        }

        static void ListAppointments(AppointmentService appointmentService)
        {
            System.Console.Clear();
            System.Console.WriteLine("=== LIST OF APPOINTMENTS ===\n");

            var appointments = appointmentService.GetAllAppointments();
            
            if (!appointments.Any())
            {
                System.Console.WriteLine("No appointments found.");
            }
            else
            {
                System.Console.WriteLine($"{"ID",-5} {"Doctor",-15} {"Patient",-15} {"Date",-12} {"Reason",-20}");
                System.Console.WriteLine(new string('-', 70));
                
                foreach (var appointment in appointments)
                {
                    System.Console.WriteLine($"{appointment.AppointmentId,-5} {appointment.Doctor.Name,-15} {appointment.Patient.Name,-15} {appointment.AppointmentDate:yyyy-MM-dd} {appointment.Reason,-20}");
                }
            }

            PressAnyKey();
        }

        static void PressAnyKey()
        {
            System.Console.WriteLine("\nPress any key to continue...");
            System.Console.ReadKey();
        }
    }
}
