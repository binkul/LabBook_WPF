using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ClpDto
    {
        public long Id { get; set; }
        public string Class { get; set; } = "-- Brak --";
        public string Clp { get; set; }
        public string Description { get; set; }
        public int Ordering { get; set; } = 0;
        public bool Is_H { get; set; } = false;
        public int GHS_id { get; set; } = 11;
        public int Signal_id { get; set; } = 1;
        public DateTime Created { get; set; } = DateTime.Now;

        public ClpDto(long id, string @class, string clp, string description, int ordering, bool is_H, int gHS_id, int signal_id, DateTime created)
        {
            Id = id;
            Class = @class;
            Clp = clp;
            Description = description;
            Ordering = ordering;
            Is_H = is_H;
            GHS_id = gHS_id;
            Signal_id = signal_id;
            Created = created;
        }

        public ClpDto(string clp, string description)
        {
            Clp = clp;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            return obj is ClpDto dto &&
                   Id == dto.Id &&
                   Class == dto.Class &&
                   Clp == dto.Clp &&
                   Description == dto.Description &&
                   Ordering == dto.Ordering &&
                   Is_H == dto.Is_H &&
                   GHS_id == dto.GHS_id &&
                   Signal_id == dto.Signal_id &&
                   Created == dto.Created;
        }

        public override int GetHashCode()
        {
            int hashCode = -2105736356;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Class);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Clp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Ordering.GetHashCode();
            hashCode = hashCode * -1521134295 + Is_H.GetHashCode();
            hashCode = hashCode * -1521134295 + GHS_id.GetHashCode();
            hashCode = hashCode * -1521134295 + Signal_id.GetHashCode();
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            return hashCode;
        }
    }
}
