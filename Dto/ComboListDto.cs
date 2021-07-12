using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ComboListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ComboListDto(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public ComboListDto(string name)
        {
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is ComboListDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = -48284730;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
