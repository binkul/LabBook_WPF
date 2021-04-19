using System.Data;

namespace LabBook.ADO.Common
{
    public interface IRepository<T>
    {
        DataTable GetAll();
    }
}
