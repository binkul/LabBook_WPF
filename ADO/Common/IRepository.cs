using System.Data;

namespace LabBook.ADO.Common
{
    public interface IRepository<T>
    {
        DataTable GetAll();
        bool Delete();
        T Save(T data);
        T Update(T data);
    }
}
