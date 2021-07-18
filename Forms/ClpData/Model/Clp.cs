using System.Collections.Generic;

namespace LabBook.Forms.ClpData.Model
{
    public class Clp
    {
        public int Id { get; set; }
        public string ClpHP { get; set; }
        public string ClpClass { get; set; }
        public string ClpDescription { get; set; }
        public int Ordering { get; set; }
        public bool ClpSelected { get; set; } = false;

        public Clp(int id, string clpHP, string clpClass, string clpDescription, int ordering)
        {
            Id = id;
            ClpHP = clpHP;
            ClpClass = clpClass;
            ClpDescription = clpDescription;
            Ordering = ordering;
        }

        public override bool Equals(object obj)
        {
            return obj is Clp clp &&
                   Id == clp.Id &&
                   ClpHP == clp.ClpHP;
        }

        public override int GetHashCode()
        {
            int hashCode = 1808620438;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClpHP);
            return hashCode;
        }
    }
}
