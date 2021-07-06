using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Dto
{
    public class ComboListDto
    {
        public long id { get; set; }
        public string name { get; set; }

        public ComboListDto(long id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public ComboListDto(string name)
        {
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is ComboListDto dto &&
                   id == dto.id &&
                   name == dto.name;
        }

        public override int GetHashCode()
        {
            int hashCode = -48284730;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            return hashCode;
        }
    }
}
