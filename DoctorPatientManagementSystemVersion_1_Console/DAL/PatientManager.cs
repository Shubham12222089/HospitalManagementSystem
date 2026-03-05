using DoctorPatientManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace DoctorPatientManagementSystem.DAL
{
    using Microsoft.Data.SqlClient;

    public class PatientManager : IPatientManager
    {
        private readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public void AddPatient(int doctorId)
        {
            AddPatientToDB(doctorId);
        }

        public void EditPatient()
        {
            EditPatientInDB();
        }

        public void DeletePatient()
        {
            DeletePatientFromDB();
        }

        public void ListPatients()
        {
            ListPatientsFromDB();
        }

        public void FindPatient(string patientName)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM Patients WHERE Name LIKE @name";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", "%" + patientName + "%");

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\n--- Search Results ---");
            while (dr.Read())
            {
                Console.WriteLine(
                    $"ID:{dr["PatientId"]}, Name:{dr["Name"]}, Age:{dr["Age"]}, Condition:{dr["Condition"]}");
            }
        }

        public void AddPatientToDB(int doctorId)
        {
            Console.Write("Patient Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine()??"");

            Console.Write("Condition: ");
            string condition = Console.ReadLine() ?? "";

            using SqlConnection con = new SqlConnection(connectionString);
            string query = @"INSERT INTO Patients 
                        (Name, Age, Condition, AppointmentDate, DoctorId)
                        VALUES (@n,@a,@c,GETDATE(),@d)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@a", age);
            cmd.Parameters.AddWithValue("@c", condition);
            cmd.Parameters.AddWithValue("@d", doctorId);

            con.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine("Patient added successfully.");
        }

        public void EditPatientInDB()
        {
            Console.Write("Enter Patient ID to Edit: ");
            int id = int.Parse(Console.ReadLine()??"");

            Console.Write("New Condition: ");
            string condition = Console.ReadLine()??"";

            using SqlConnection con = new SqlConnection(connectionString);
            string query =
                "UPDATE Patients SET Condition=@c WHERE PatientId=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@c", condition);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ? "Patient updated." : "Patient not found.");
        }

        public void DeletePatientFromDB()
        {
            Console.Write("Enter Patient ID to Delete: ");
            int id = int.Parse(Console.ReadLine()??"");

            using SqlConnection con = new SqlConnection(connectionString);
            string query = "DELETE FROM Patients WHERE PatientId=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ? "Patient deleted." : "Patient not found.");
        }

        public void ListPatientsFromDB()
        {
            using SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM Patients";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\n--- Patients List ---");
            while (dr.Read())
            {
                Console.WriteLine(
                    $"ID:{dr["PatientId"]}, Name:{dr["Name"]}, Age:{dr["Age"]}, Condition:{dr["Condition"]}, DoctorId:{dr["DoctorId"]}");
            }
        }
    }
}
