using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ExperimentCycleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }

        public ExperimentCycleDto(long id, string name, long userId, DateTime date)
        {
            Id = id;
            Name = name;
            UserId = userId;
            Date = date;
        }

        public override bool Equals(object obj)
        {
            return obj is ExperimentCycleDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   UserId == dto.UserId;
        }

        public override int GetHashCode()
        {
            int hashCode = 1187969077;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            return hashCode;
        }
    }
}
