using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ExperimentalSpectroDto
    {
        public long Id { get; set; } = -1;
        public long LabBookId { get; set; } = 1;
        public decimal Lm { get; set; }
        public decimal Am { get; set; }
        public decimal Bm { get; set; }
        public decimal Ls { get; set; }
        public decimal As { get; set; }
        public decimal Bs { get; set; }
        public decimal WIm { get; set; }
        public decimal YIm { get; set; }
        public decimal WIs { get; set; }
        public decimal YIs { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ExperimentalSpectroDto(long id, long labBookId, decimal lm, decimal am, decimal bm, decimal ls, decimal @as, decimal bs, decimal wIm, decimal yIm, 
            decimal wIs, decimal yIs, decimal x, decimal y, decimal z, string comment, DateTime created, DateTime updated)
        {
            Id = id;
            LabBookId = labBookId;
            Lm = lm;
            Am = am;
            Bm = bm;
            Ls = ls;
            As = @as;
            Bs = bs;
            WIm = wIm;
            YIm = yIm;
            WIs = wIs;
            YIs = yIs;
            X = x;
            Y = y;
            Z = z;
            Comment = comment;
            Created = created;
            Updated = updated;
        }

        public ExperimentalSpectroDto(long labBookId)
        {
            LabBookId = labBookId;
        }

        public override bool Equals(object obj)
        {
            return obj is ExperimentalSpectroDto dto &&
                   Id == dto.Id &&
                   LabBookId == dto.LabBookId &&
                   Lm == dto.Lm &&
                   Am == dto.Am &&
                   Bm == dto.Bm &&
                   Ls == dto.Ls &&
                   As == dto.As &&
                   Bs == dto.Bs &&
                   WIm == dto.WIm &&
                   YIm == dto.YIm &&
                   WIs == dto.WIs &&
                   YIs == dto.YIs &&
                   X == dto.X &&
                   Y == dto.Y &&
                   Z == dto.Z &&
                   Comment == dto.Comment &&
                   Created == dto.Created &&
                   Updated == dto.Updated;
        }

        public override int GetHashCode()
        {
            int hashCode = -1049898043;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Lm.GetHashCode();
            hashCode = hashCode * -1521134295 + Am.GetHashCode();
            hashCode = hashCode * -1521134295 + Bm.GetHashCode();
            hashCode = hashCode * -1521134295 + Ls.GetHashCode();
            hashCode = hashCode * -1521134295 + As.GetHashCode();
            hashCode = hashCode * -1521134295 + Bs.GetHashCode();
            hashCode = hashCode * -1521134295 + WIm.GetHashCode();
            hashCode = hashCode * -1521134295 + YIm.GetHashCode();
            hashCode = hashCode * -1521134295 + WIs.GetHashCode();
            hashCode = hashCode * -1521134295 + YIs.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            return hashCode;
        }
    }
}
