using System.Collections.Generic;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        IEnumerable<Patient> GetAll();
        Patient? GetById(int id);
    }
}
