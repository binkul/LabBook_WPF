using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;
using System.Data.SqlClient;

namespace LabBook.ADO.Service
{
    public class LabBookService
    {
        private readonly User _user;
        private readonly IRepository<LabBookDto> _labBookRepository;

        public LabBookService(User user)
        {
            _user = user;
            _labBookRepository = new LabBookRepository(_user);
        }

        public DataView GetAll()
        {
            DataTable table = _labBookRepository.GetAll();
            return new DataView(table);
        }
    }
}
