using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Forms.MainForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.ADO.Service
{
    public class ExperimentalCommonService
    {
        private readonly IRepository<ExpCommon> _repository = new ExperimentalCommonRepository();
        private bool _modified = false;

        public ExperimentalCommonService()
        {
        }

        public ExpCommon GetCurrent(long id)
        {
            if (!_repository.ExistById(id, ExperimentalCommonRepository.ExistByIdQuery))
                return _repository.Save(new ExpCommon(id));
            else
                return _repository.GetById(id, ExperimentalCommonRepository.GetByLabbokIdQuery);
        }

        public bool Modified
        {
            get
            {
                return _modified;
            }
            private set
            {
                _modified = value;
            }
        }

    }
}
