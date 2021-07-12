using System.Collections.Generic;

namespace LabBook.Dto
{
    public class GhsDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int GHS { get; set; } = 1;
        public string File { get; set; } = "none";

        public GhsDto(long id, string description, int gHS, string file)
        {
            Id = id;
            Description = description;
            GHS = gHS;
            File = file;
        }

        public GhsDto(string description)
        {
            Description = description;
        }

        public override bool Equals(object obj)
        {
            return obj is GhsDto dto &&
                   Id == dto.Id &&
                   Description == dto.Description &&
                   GHS == dto.GHS &&
                   File == dto.File;
        }

        public override int GetHashCode()
        {
            int hashCode = -1875879505;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + GHS.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(File);
            return hashCode;
        }
    }
}
