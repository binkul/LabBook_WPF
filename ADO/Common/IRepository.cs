using LabBook.ADO.Exceptions;
using System.Data;

namespace LabBook.ADO.Common
{
    public interface IRepository<T>
    {
        DataTable GetAll(string query);
        bool Delete(long id, string query);
        T Save(T data);
        ExceptionCode Save(DataRow data, string query);
        void Update(T data);
        ExceptionCode Update(DataRow data, string query);
        bool ExistById(long id, string query);
        bool ExistByName(string name, string query);
        T GetById(long id, string query);
    }
}
