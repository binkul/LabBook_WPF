using LabBook.ADO.Exceptions;
using System.Data;

namespace LabBook.ADO.Common
{
    public interface IRepository<T>
    {
        DataTable GetAll(string query);
        bool Delete(long id, string query);
        T Save(T data, string query);
        ExceptionCode Save(DataRow data, string query);
        T Update(T data, string query);
        ExceptionCode Update(DataRow data, string query);
    }
}
