using LabBook.ADO.Service;
using LabBook.Forms.Composition.Model;
using System.Collections.Generic;
using System.Data;

namespace LabBook.Dto
{
    public class CompositionSubRecipeDto
    {
        public int Level { get; }
        public long NrD { get; }
        public int Operation { get; }
        public double Amount { get; }
        public double Mass { get; }

        public CompositionSubRecipeDto(int level, long nrD, int operation, double amount, double mass)
        {
            Level = level;
            NrD = nrD;
            Operation = operation;
            Amount = amount;
            Mass = mass;
        }

        public override bool Equals(object obj)
        {
            return obj is CompositionSubRecipeDto dto &&
                   Level == dto.Level &&
                   NrD == dto.NrD &&
                   Operation == dto.Operation &&
                   Amount == dto.Amount &&
                   Mass == dto.Mass;
        }

        public override int GetHashCode()
        {
            int hashCode = -1800033708;
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + NrD.GetHashCode();
            hashCode = hashCode * -1521134295 + Operation.GetHashCode();
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + Mass.GetHashCode();
            return hashCode;
        }
    }
}
