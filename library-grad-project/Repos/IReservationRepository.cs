using System.Collections.Generic;

namespace LibraryGradProject.Repos
{
    public interface IReservationRepository<T, U>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Borrow(U entity);
        void Return(U entity);
    }
}