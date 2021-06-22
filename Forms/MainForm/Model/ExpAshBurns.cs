using System;
using System.Collections.Generic;

namespace LabBook.Forms.MainForm.Model
{
    public class ExpAshBurns
    {
        public long LabBookId { get; set; } = 1;
        public double Solid { get; set; }
        public double Ash450 { get; set; }
        public double Ash900 { get; set; }
        public double Organic { get; set; }
        public double Titanium { get; set; }
        public double Chalk { get; set; }
        public double Others { get; set; }
        public int VocCatId { get; set; } = 1;
        public string VocAmount { get; set; }
        public double Crucible1 { get; set; }
        public double Crucible2 { get; set; }
        public double Crucible3 { get; set; }
        public double Paint1 { get; set; }
        public double Paint2 { get; set; }
        public double Paint3 { get; set; }
        public double Crucible105_1 { get; set; }
        public double Crucible105_2 { get; set; }
        public double Crucible105_3 { get; set; }
        public double Crucible405_1 { get; set; }
        public double Crucible405_2 { get; set; }
        public double Crucible405_3 { get; set; }
        public double Crucible900_1 { get; set; }
        public double Crucible900_2 { get; set; }
        public double Crucible900_3 { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ExpAshBurns(long labBookId, double solid, double ash450, double ash900, double organic, double titanium, 
            double chalk, double others, int vocCatId, string vocAmount, double crucible1, double crucible2, double crucible3,
            double paint1, double paint2, double paint3, double crucible105_1, double crucible105_2, double crucible105_3,
            double crucible405_1, double crucible405_2, double crucible405_3, double crucible900_1, double crucible900_2,
            double crucible900_3, DateTime created, DateTime updated) : this(labBookId)
        {
            Solid = solid;
            Ash450 = ash450;
            Ash900 = ash900;
            Organic = organic;
            Titanium = titanium;
            Chalk = chalk;
            Others = others;
            VocCatId = vocCatId;
            VocAmount = vocAmount;
            Crucible1 = crucible1;
            Crucible2 = crucible2;
            Crucible3 = crucible3;
            Paint1 = paint1;
            Paint2 = paint2;
            Paint3 = paint3;
            Crucible105_1 = crucible105_1;
            Crucible105_2 = crucible105_2;
            Crucible105_3 = crucible105_3;
            Crucible405_1 = crucible405_1;
            Crucible405_2 = crucible405_2;
            Crucible405_3 = crucible405_3;
            Crucible900_1 = crucible900_1;
            Crucible900_2 = crucible900_2;
            Crucible900_3 = crucible900_3;
            Created = created;
            Updated = updated;
        }

        public ExpAshBurns(long labBookId)
        {
            LabBookId = labBookId;
        }

        public override bool Equals(object obj)
        {
            return obj is ExpAshBurns burncs &&
                   LabBookId == burncs.LabBookId &&
                   Solid == burncs.Solid &&
                   Ash450 == burncs.Ash450 &&
                   Ash900 == burncs.Ash900 &&
                   Organic == burncs.Organic &&
                   Titanium == burncs.Titanium &&
                   Chalk == burncs.Chalk &&
                   Others == burncs.Others &&
                   VocCatId == burncs.VocCatId &&
                   VocAmount == burncs.VocAmount &&
                   Crucible1 == burncs.Crucible1 &&
                   Crucible2 == burncs.Crucible2 &&
                   Crucible3 == burncs.Crucible3 &&
                   Paint1 == burncs.Paint1 &&
                   Paint2 == burncs.Paint2 &&
                   Paint3 == burncs.Paint3 &&
                   Crucible105_1 == burncs.Crucible105_1 &&
                   Crucible105_2 == burncs.Crucible105_2 &&
                   Crucible105_3 == burncs.Crucible105_3 &&
                   Crucible405_1 == burncs.Crucible405_1 &&
                   Crucible405_2 == burncs.Crucible405_2 &&
                   Crucible405_3 == burncs.Crucible405_3 &&
                   Crucible900_1 == burncs.Crucible900_1 &&
                   Crucible900_2 == burncs.Crucible900_2 &&
                   Crucible900_3 == burncs.Crucible900_3 &&
                   Created == burncs.Created &&
                   Updated == burncs.Updated;
        }

        public override int GetHashCode()
        {
            int hashCode = -1002186340;
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Solid.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash900.GetHashCode();
            hashCode = hashCode * -1521134295 + Organic.GetHashCode();
            hashCode = hashCode * -1521134295 + Titanium.GetHashCode();
            hashCode = hashCode * -1521134295 + Chalk.GetHashCode();
            hashCode = hashCode * -1521134295 + Others.GetHashCode();
            hashCode = hashCode * -1521134295 + VocCatId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VocAmount);
            hashCode = hashCode * -1521134295 + Crucible1.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible2.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible3.GetHashCode();
            hashCode = hashCode * -1521134295 + Paint1.GetHashCode();
            hashCode = hashCode * -1521134295 + Paint2.GetHashCode();
            hashCode = hashCode * -1521134295 + Paint3.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible105_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible105_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible105_3.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible405_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible405_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible405_3.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible900_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible900_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Crucible900_3.GetHashCode();
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            return hashCode;
        }
    }
}
