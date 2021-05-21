using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ExperimentalOpacityDto
    {
        long Id { get; set; }
        long LabBookId { get; set; }
        decimal Contrast75 { get; set; }
        decimal Tw75 { get; set; }
        decimal Sp75 { get; set; }
        decimal Contrast100 { get; set; }
        decimal Tw100 { get; set; }
        decimal Sp100 { get; set; }
        decimal Contrast150 { get; set; }
        decimal Tw150 { get; set; }
        decimal Sp150 { get; set; }
        decimal Contrast240 { get; set; }
        decimal Tw240 { get; set; }
        decimal Sp240 { get; set; }
        decimal OtherA { get; set; }
        long OtherAtype { get; set; } = 1;
        decimal OtherB { get; set; }
        long OtherBtype { get; set; } = 1;
        long ContrastClass { get; set; } = 1;
        long ContrastYield { get; set; } = 1;
        string Comment { get; set; }
        DateTime Created { get; set; } = DateTime.Now;
        DateTime Updated { get; set; } = DateTime.Now;

        public ExperimentalOpacityDto(long id, long labBookId, decimal contrast75, decimal tw75, decimal sp75, decimal contrast100, decimal tw100, decimal sp100, 
            decimal contrast150, decimal tw150, decimal sp150, decimal contrast240, decimal tw240, decimal sp240, decimal otherA, long otherAtype, decimal otherB, 
            long otherBtype, long contrastClass, long contrastYield, string comment, DateTime created, DateTime updated)
        {
            Id = id;
            LabBookId = labBookId;
            Contrast75 = contrast75;
            Tw75 = tw75;
            Sp75 = sp75;
            Contrast100 = contrast100;
            Tw100 = tw100;
            Sp100 = sp100;
            Contrast150 = contrast150;
            Tw150 = tw150;
            Sp150 = sp150;
            Contrast240 = contrast240;
            Tw240 = tw240;
            Sp240 = sp240;
            OtherA = otherA;
            OtherAtype = otherAtype;
            OtherB = otherB;
            OtherBtype = otherBtype;
            ContrastClass = contrastClass;
            ContrastYield = contrastYield;
            Comment = comment;
            Created = created;
            Updated = updated;
        }

        public ExperimentalOpacityDto(long labBookId)
        {
            LabBookId = labBookId;
        }

        public override bool Equals(object obj)
        {
            return obj is ExperimentalOpacityDto dto &&
                   Id == dto.Id &&
                   LabBookId == dto.LabBookId &&
                   Contrast75 == dto.Contrast75 &&
                   Tw75 == dto.Tw75 &&
                   Sp75 == dto.Sp75 &&
                   Contrast100 == dto.Contrast100 &&
                   Tw100 == dto.Tw100 &&
                   Sp100 == dto.Sp100 &&
                   Contrast150 == dto.Contrast150 &&
                   Tw150 == dto.Tw150 &&
                   Sp150 == dto.Sp150 &&
                   Contrast240 == dto.Contrast240 &&
                   Tw240 == dto.Tw240 &&
                   Sp240 == dto.Sp240 &&
                   OtherA == dto.OtherA &&
                   OtherAtype == dto.OtherAtype &&
                   OtherB == dto.OtherB &&
                   OtherBtype == dto.OtherBtype &&
                   ContrastClass == dto.ContrastClass &&
                   ContrastYield == dto.ContrastYield &&
                   Comment == dto.Comment &&
                   Created == dto.Created &&
                   Updated == dto.Updated;
        }

        public override int GetHashCode()
        {
            int hashCode = -437011463;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Contrast75.GetHashCode();
            hashCode = hashCode * -1521134295 + Tw75.GetHashCode();
            hashCode = hashCode * -1521134295 + Sp75.GetHashCode();
            hashCode = hashCode * -1521134295 + Contrast100.GetHashCode();
            hashCode = hashCode * -1521134295 + Tw100.GetHashCode();
            hashCode = hashCode * -1521134295 + Sp100.GetHashCode();
            hashCode = hashCode * -1521134295 + Contrast150.GetHashCode();
            hashCode = hashCode * -1521134295 + Tw150.GetHashCode();
            hashCode = hashCode * -1521134295 + Sp150.GetHashCode();
            hashCode = hashCode * -1521134295 + Contrast240.GetHashCode();
            hashCode = hashCode * -1521134295 + Tw240.GetHashCode();
            hashCode = hashCode * -1521134295 + Sp240.GetHashCode();
            hashCode = hashCode * -1521134295 + OtherA.GetHashCode();
            hashCode = hashCode * -1521134295 + OtherAtype.GetHashCode();
            hashCode = hashCode * -1521134295 + OtherB.GetHashCode();
            hashCode = hashCode * -1521134295 + OtherBtype.GetHashCode();
            hashCode = hashCode * -1521134295 + ContrastClass.GetHashCode();
            hashCode = hashCode * -1521134295 + ContrastYield.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            return hashCode;
        }
    }
}
