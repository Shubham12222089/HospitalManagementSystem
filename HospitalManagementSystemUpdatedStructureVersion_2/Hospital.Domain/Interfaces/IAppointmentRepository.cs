using System.Collections.Generic;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        IEnumerable<Appointment> GetAll();
        Appointment? GetById(int id);
    }
}
