using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IAppointmentRepository
    {
        Appointment GetAllByDoctor(Doctor doctor);

        Appointment GetAllByPatient(Patient patient);
    }
}