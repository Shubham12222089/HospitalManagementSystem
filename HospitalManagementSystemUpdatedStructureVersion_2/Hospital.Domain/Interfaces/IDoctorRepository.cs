using System.Collections.Generic;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        IEnumerable<Doctor> GetAll();
        Doctor? GetById(int id);
    }
}
