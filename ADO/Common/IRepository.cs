using LabBook.ADO.Exceptions;
using System.Data;

namespace LabBook.ADO.Common
{
    public interface IRepository<T>
    {
        DataTable GetAll();
        bool Delete(long id);
        T Save(T data);
        ExceptionCode Save(DataRow data);
        T Update(T data);
        ExceptionCode Update(DataRow data);
    }
}
