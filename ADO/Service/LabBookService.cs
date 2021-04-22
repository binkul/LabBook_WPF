﻿using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class LabBookService
    {
        private readonly User _user;
        private readonly IRepository<LabBookDto> _labBookRepository;
        private DataTable dataTable;

        public LabBookService(User user)
        {
            _user = user;
            _labBookRepository = new LabBookRepository(_user);
        }

        public DataView GetAll()
        {
            dataTable = _labBookRepository.GetAll();
            DataView view = new DataView(dataTable);
            view.Sort = "id";
            return view;
        }
    }
}
