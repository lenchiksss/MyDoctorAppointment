using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IGenericRepository<TSource> where TSource : Auditable
    {
        TSource Create(TSource source);

        TSource? GetById(int id);

        TSource Update(int id, TSource source);

        IEnumerable<TSource> GetAll();

        bool Delete(int id);

        void ShowInfo(TSource source);
    }
}
