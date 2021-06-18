using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Forms.MainForm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.ADO.Service
{
    public class ExperimentalCommonService
    {
        private readonly IRepository<ExpCommon> _repository = new ExperimentalCommonRepository();

        public ExperimentalCommonService()
        {
        }

        public ExpCommon GetCurrent(long labBookId)
        {
            if (!_repository.ExistById(labBookId, ExperimentalCommonRepository.ExistByLabBookIdQuery))
                return new ExpCommon(labBookId);
            else
                return _repository.GetById(labBookId, ExperimentalCommonRepository.GetByLabbokIdQuery);
        }

        public ExpCommon RefreshData(long labBookId)
        {
            return _repository.GetById(labBookId, ExperimentalCommonRepository.GetByLabbokIdQuery);
        }

        public ExpCommon Save(long labBookId, ExpCommon expCommon)
        {
            if (expCommon.Id == CommonConstant.IdNewAdded)
            {
                expCommon.LabBookId = labBookId;
                return _repository.Save(expCommon);
            }
            else
            {
                _repository.Update(expCommon);
                return expCommon;
            }
        }

        public DataView GetScrubingClass()
        {
            ExperimentalCommonRepository repository = (ExperimentalCommonRepository)_repository;
            DataTable table = repository.GetScrubingClass();
            return new DataView(table) { Sort = "name" };
        }
    }
}
