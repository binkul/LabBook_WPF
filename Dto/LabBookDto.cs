using System;

namespace LabBook.Dto
{
    public class LabBookDto
    {
        long Id { get; set; }
        string Title { get; set; }
        decimal Density { get; set; }
        string Observation { get; set; }
        string Remarks { get; set; }
        long UserId { get; set; }
        long CycleId { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
        bool Deleted { get; set; }

        public LabBookDto(long id, string title, decimal density, string observation, string remarks, long userId, long cycleId, DateTime created, DateTime modified, bool deleted)
        {
            Id = id;
            Title = title;
            Density = density;
            Observation = observation;
            Remarks = remarks;
            UserId = userId;
            CycleId = cycleId;
            Created = created;
            Modified = modified;
            Deleted = deleted;
        }
    }
}
