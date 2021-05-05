using LabBook.ADO.Common;
using LabBook.Dto;
using System;
using System.Data;

namespace LabBook.ADO.Repository
{
    public class ExperimentalViscosity : IRepository<ExperimentalViscosityDto>
    {
        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllxViscosity()
        {
            return null;
        }

        public DataTable GetAllKrebs()
        {
            return null;
        }

        public DataTable GetAllICI()
        {
            return null;
        }

        public ExperimentalViscosityDto Save(ExperimentalViscosityDto data)
        {
            throw new NotImplementedException();
        }

        public ExperimentalViscosityDto Update(ExperimentalViscosityDto data)
        {
            throw new NotImplementedException();
        }
    }
}
