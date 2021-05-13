using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class UserService
    {
        //private readonly User _user;
        private readonly IRepository<UserDto> _userRepository;
        private DataTable dataTable;

        public UserService() //User user)
        {
            //_user = user;
            _userRepository = new UserRepository(); // _user);
        }

        public DataView GetAll()
        {
            dataTable = _userRepository.GetAll();
            DataView view = new DataView(dataTable);
            view.Sort = "id";
            return view;
        }

    }
}
