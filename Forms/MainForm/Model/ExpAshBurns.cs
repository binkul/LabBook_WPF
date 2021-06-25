using System;
using System.Collections.Generic;

namespace LabBook.Forms.MainForm.Model
{
    public class ExpAshBurns
    {
        public static double none = -1f;

        public bool Row_1 { get; set; } = true;
        public bool Row_2 { get; set; } = true;
        public bool Row_3 { get; set; } = true;
        public long LabBookId { get; set; } = 1;
        public double Solid { get; set; } = none;
        public double Ash450 { get; set; } = none;
        public double Ash900 { get; set; } = none;
        public double Organic { get; set; } = none;
        public double Titanium { get; set; } = none;
        public double Chalk { get; set; } = none;
        public double Others { get; set; } = none;
        public int VocCatId { get; set; } = 1;
        public string VocAmount { get; set; }
        public double Crucible1 { get; set; } = none;
        public double Crucible2 { get; set; } = none;
        public double Crucible3 { get; set; } = none;
        public double Paint1 { get; set; } = none;
        public double Paint2 { get; set; } = none;
        public double Paint3 { get; set; } = none;
        public double Crucible105_1 { get; set; } = none;
        public double Crucible105_2 { get; set; } = none;
        public double Crucible105_3 { get; set; } = none;
        public double Crucible405_1 { get; set; } = none;
        public double Crucible405_2 { get; set; } = none;
        public double Crucible405_3 { get; set; } = none;
        public double Crucible900_1 { get; set; } = none;
        public double Crucible900_2 { get; set; } = none;
        public double Crucible900_3 { get; set; } = none;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ExpAshBurns(bool row_1, bool row_2, bool row_3, long labBookId, 
            double solid, double ash450, double ash900, double organic, 
            double titanium, double chalk, double others, int vocCatId, string vocAmount,
            double crucible1, double crucible2, double crucible3, double paint1, double paint2, double paint3, 
            double crucible105_1, double crucible105_2, double crucible105_3, double crucible405_1, 
            double crucible405_2, double crucible405_3, double crucible900_1, double crucible900_2, 
            double crucible900_3, DateTime created, DateTime updated)
        {
            Row_1 = row_1;
            Row_2 = row_2;
            Row_3 = row_3;
            LabBookId = labBookId;
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
            return obj is ExpAshBurns burns &&
                   LabBookId == burns.LabBookId &&
                   Solid == burns.Solid &&
                   Ash450 == burns.Ash450 &&
                   Ash900 == burns.Ash900 &&
                   Organic == burns.Organic &&
                   Titanium == burns.Titanium &&
                   Chalk == burns.Chalk &&
                   Others == burns.Others &&
                   VocCatId == burns.VocCatId &&
                   VocAmount == burns.VocAmount &&
                   Crucible1 == burns.Crucible1 &&
                   Crucible2 == burns.Crucible2 &&
                   Crucible3 == burns.Crucible3 &&
                   Paint1 == burns.Paint1 &&
                   Paint2 == burns.Paint2 &&
                   Paint3 == burns.Paint3 &&
                   Crucible105_1 == burns.Crucible105_1 &&
                   Crucible105_2 == burns.Crucible105_2 &&
                   Crucible105_3 == burns.Crucible105_3 &&
                   Crucible405_1 == burns.Crucible405_1 &&
                   Crucible405_2 == burns.Crucible405_2 &&
                   Crucible405_3 == burns.Crucible405_3 &&
                   Crucible900_1 == burns.Crucible900_1 &&
                   Crucible900_2 == burns.Crucible900_2 &&
                   Crucible900_3 == burns.Crucible900_3 &&
                   Created == burns.Created &&
                   Updated == burns.Updated;
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
