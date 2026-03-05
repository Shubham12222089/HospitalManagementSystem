using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorPatientManagementSystem.DAL
{
    using DoctorPatientManagementSystem.Interfaces;
    using Microsoft.Data.SqlClient;

    public class DoctorManager : IDoctorManager
    {
        string connStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public void AddDoctorToDB()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine()??"";

            Console.Write("Specialization: ");
            string spec = Console.ReadLine()??"";

            Console.Write("Consultation Fee: ");
            decimal fee = decimal.Parse(Console.ReadLine()??"");

            using SqlConnection con = new SqlConnection(connStr);
            string query = "INSERT INTO Doctors VALUES(@n,@s,@f)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@s", spec);
            cmd.Parameters.AddWithValue("@f", fee);

            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("Doctor added successfully.");
        }
        public void DeleteDoctorFromDB()
        {
            Console.Write("Enter Doctor ID to Delete: ");
            int id = int.Parse(Console.ReadLine() ?? "");

            using SqlConnection con = new SqlConnection(connStr);
            string query = "DELETE FROM Doctors WHERE DoctorId=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ? "Doctor deleted." : "Doctor not found.");
        }
        public void ListDoctorsFromDB()
        {
            using SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Doctors", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine($"{dr["DoctorId"]} - {dr["Name"]} ({dr["Specialization"]})");
            }
        }

        // Console wrappers
        public void AddDoctor() => AddDoctorToDB();
        public void ListDoctors() => ListDoctorsFromDB();
        public void DeleteDoctor() => DeleteDoctorFromDB();
    }
}
