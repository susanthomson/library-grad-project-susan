using System.Collections.Generic;

namespace LibraryGradProject.Repos
{
    public interface IReservationRepository<T, U, V>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Borrow(U entity, V user);
        void Return(U entity, V user);
    }
}