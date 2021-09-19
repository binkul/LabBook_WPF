using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Dto
{
    public class CompositionEnterDto
    {
        public long NumberD { get; set; }
        public string Title { get; set; }
        public decimal Density { get; set; }

        public CompositionEnterDto(long numberD, string title, decimal density)
        {
            NumberD = numberD;
            Title = title;
            Density = density;
        }

        public override bool Equals(object obj)
        {
            return obj is CompositionEnterDto dto &&
                   NumberD == dto.NumberD &&
                   Title == dto.Title &&
                   Density == dto.Density;
        }

        public override int GetHashCode()
        {
            int hashCode = -1986107688;
            hashCode = hashCode * -1521134295 + NumberD.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + Density.GetHashCode();
            return hashCode;
        }
    }
}
