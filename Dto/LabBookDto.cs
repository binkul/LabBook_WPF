using System;

namespace LabBook.Dto
{
    public class LabBookDto
    {
        public long Id { get; set; } = 0;
        public string Title { get; set; }
        public decimal Density { get; set; } = 0;
        public string Observation { get; set; }
        public string Remarks { get; set; }
        public long UserId { get; set; } = 1;
        public long CycleId { get; set; } = 1;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;

        public LabBookDto() 
        { }

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

        public LabBookDto(string title, long userId, long cycleId)
        {
            Title = title;
            UserId = userId;
            CycleId = cycleId;
        }
    }
}
