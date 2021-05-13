using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class UserService
    {
        private readonly IRepository<UserDto> _userRepository = new UserRepository();
        private DataTable dataTable;

        public DataView GetAll()
        {
            dataTable = _userRepository.GetAll();
            DataView view = new DataView(dataTable);
            view.Sort = "id";
            return view;
        }
    }
}
