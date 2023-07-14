using My_Final_Project.Models.Entities;
using System.Linq.Expressions;

namespace My_Final_Project.Interfaces.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAppointment (Appointment appointment);
        Task<Appointment> UpdateAppointment (Appointment appointment);
        void DeleteAppointment (Guid id);
        bool save(Appointment appointment);
        Task<Appointment> GetAppointment (Guid id);
        Task<Appointment> GetAppointment (Expression<Func<Appointment, bool>> expression);
        Task<IEnumerable<Appointment>> GetAllAppointment();

    }
}
