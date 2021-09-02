using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Dto
{
    public enum RecipeOperation
    {
        Blank = 1,
        Start = 2,
        Inside = 3,
        End = 4
    }
    public class CompositionDto
    {
        public long LabBookId { get; set; }
        public int Ordering { get; set; }
        public string Component { get; set; }
        public bool IsIntermediate { get; set; } = false;
        public decimal Amount { get; set; }
        public RecipeOperation Operation { get; set; } = RecipeOperation.Blank;
        public string Comment { get; set; }

        public CompositionDto(long labBookId, int ordering, string component, bool isIntermediate, decimal amount, RecipeOperation operation, string comment)
        {
            LabBookId = labBookId;
            Ordering = ordering;
            Component = component;
            IsIntermediate = isIntermediate;
            Amount = amount;
            Operation = operation;
            Comment = comment;
        }

        public CompositionDto(long labBookId, int ordering, string component, decimal amount, string comment)
        {
            LabBookId = labBookId;
            Ordering = ordering;
            Component = component;
            Amount = amount;
            Comment = comment;
        }

        public override bool Equals(object obj)
        {
            return obj is CompositionDto dto &&
                   LabBookId == dto.LabBookId &&
                   Ordering == dto.Ordering &&
                   Component == dto.Component &&
                   IsIntermediate == dto.IsIntermediate &&
                   Amount == dto.Amount &&
                   Operation == dto.Operation &&
                   Comment == dto.Comment;
        }

        public override int GetHashCode()
        {
            int hashCode = 534806982;
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Ordering.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Component);
            hashCode = hashCode * -1521134295 + IsIntermediate.GetHashCode();
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + Operation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            return hashCode;
        }
    }
}
