using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Dto
{
    public class ExperimentalGlossDto
    {
        public long Id { get; set; } = -1;
        public long LabBookId { get; set; } = 1;
        public decimal Gloss20 { get; set; }
        public decimal Gloss60 { get; set; }
        public decimal Gloss85 { get; set; }
        public long GlossClass { get; set; } = 1;
        public string Comment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ExperimentalGlossDto(long id, long labBookId, decimal gloss20, decimal gloss60, decimal gloss85, 
            long glossClass, string comment, DateTime created, DateTime updated)
        {
            Id = id;
            LabBookId = labBookId;
            Gloss20 = gloss20;
            Gloss60 = gloss60;
            Gloss85 = gloss85;
            GlossClass = glossClass;
            Comment = comment;
            Created = created;
            Updated = updated;
        }

        public ExperimentalGlossDto(long labBookId, decimal gloss20, decimal gloss60, decimal gloss85, string comment)
        {
            LabBookId = labBookId;
            Gloss20 = gloss20;
            Gloss60 = gloss60;
            Gloss85 = gloss85;
            Comment = comment;
        }

        public override bool Equals(object obj)
        {
            return obj is ExperimentalGlossDto dto &&
                   Id == dto.Id &&
                   LabBookId == dto.LabBookId &&
                   Gloss20 == dto.Gloss20 &&
                   Gloss60 == dto.Gloss60 &&
                   Gloss85 == dto.Gloss85 &&
                   GlossClass == dto.GlossClass &&
                   Comment == dto.Comment &&
                   Created == dto.Created &&
                   Updated == dto.Updated;
        }

        public override int GetHashCode()
        {
            int hashCode = -2124187796;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Gloss20.GetHashCode();
            hashCode = hashCode * -1521134295 + Gloss60.GetHashCode();
            hashCode = hashCode * -1521134295 + Gloss85.GetHashCode();
            hashCode = hashCode * -1521134295 + GlossClass.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            return hashCode;
        }
    }
}
