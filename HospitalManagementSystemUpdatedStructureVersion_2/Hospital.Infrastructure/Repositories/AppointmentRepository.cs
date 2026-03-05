using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToList();
        }

        public Appointment? GetById(int id)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.AppointmentId == id);
        }
    }
}
