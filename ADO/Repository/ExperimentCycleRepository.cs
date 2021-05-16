using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentCycleRepository : RepositoryCommon<ExperimentCycleDto>
    {
        public static readonly string AllQuery = "Select cy.id, cy.name, cy.user_id, cy.date From LabBook.dbo.ExpCycle cy Order by name";

    }
}
