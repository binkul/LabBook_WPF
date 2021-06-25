namespace LabBook.Forms.MainForm.Model
{
    public class ExpAshBurnCalculation
    {
        private static double none = -1f;

        public double Solid_1 { get; set; } = none;
        public double Solid_2 { get; set; } = none;
        public double Solid_3 { get; set; } = none;
        public double SolidAvr { get; set; } = none;
        public double Ash450_1 { get; set; } = none;
        public double Ash450_2 { get; set; } = none;
        public double Ash450_3 { get; set; } = none;
        public double Ash450Avr { get; set; } = none;
        public double Ash900_1 { get; set; } = none;
        public double Ash900_2 { get; set; } = none;
        public double Ash900_3 { get; set; } = none;
        public double Ash900Avr { get; set; } = none;
        public double Organic_1 { get; set; } = none;
        public double Organic_2 { get; set; } = none;
        public double Organic_3 { get; set; } = none;
        public double OrganicAvr { get; set; } = none;
        public double Titanium_1 { get; set; } = none;
        public double Titanium_2 { get; set; } = none;
        public double Titanium_3 { get; set; } = none;
        public double TitaniumAvr { get; set; } = none;
        public double Chalk_1 { get; set; } = none;
        public double Chalk_2 { get; set; } = none;
        public double Chalk_3 { get; set; } = none;
        public double ChalkAvr { get; set; } = none;
        public double Others_1 { get; set; } = none;
        public double Others_2 { get; set; } = none;
        public double Others_3 { get; set; } = none;
        public double OthersAvr { get; set; } = none;

        public ExpAshBurnCalculation() { }

        public ExpAshBurnCalculation(double solid_1, double solid_2, double solid_3, double solidAvr, 
            double ash450_1, double ash450_2, double ash450_3, double ash450Avr,
            double ash900_1, double ash900_2, double ash900_3, double ash900Avr, 
            double organic_1, double organic_2, double organic_3, double organicAvr, 
            double titanium_1, double titanium_2, double titanium_3, double titaniumAvr, 
            double chalk_1, double chalk_2, double chalk_3, double chalkAvr, 
            double others_1, double others_2, double others_3, double othersAvr)
        {
            Solid_1 = solid_1;
            Solid_2 = solid_2;
            Solid_3 = solid_3;
            SolidAvr = solidAvr;
            Ash450_1 = ash450_1;
            Ash450_2 = ash450_2;
            Ash450_3 = ash450_3;
            Ash450Avr = ash450Avr;
            Ash900_1 = ash900_1;
            Ash900_2 = ash900_2;
            Ash900_3 = ash900_3;
            Ash900Avr = ash900Avr;
            Organic_1 = organic_1;
            Organic_2 = organic_2;
            Organic_3 = organic_3;
            OrganicAvr = organicAvr;
            Titanium_1 = titanium_1;
            Titanium_2 = titanium_2;
            Titanium_3 = titanium_3;
            TitaniumAvr = titaniumAvr;
            Chalk_1 = chalk_1;
            Chalk_2 = chalk_2;
            Chalk_3 = chalk_3;
            ChalkAvr = chalkAvr;
            Others_1 = others_1;
            Others_2 = others_2;
            Others_3 = others_3;
            OthersAvr = othersAvr;
        }

        public override bool Equals(object obj)
        {
            return obj is ExpAshBurnCalculation calculation &&
                   Solid_1 == calculation.Solid_1 &&
                   Solid_2 == calculation.Solid_2 &&
                   Solid_3 == calculation.Solid_3 &&
                   SolidAvr == calculation.SolidAvr &&
                   Ash450_1 == calculation.Ash450_1 &&
                   Ash450_2 == calculation.Ash450_2 &&
                   Ash450_3 == calculation.Ash450_3 &&
                   Ash450Avr == calculation.Ash450Avr &&
                   Ash900_1 == calculation.Ash900_1 &&
                   Ash900_2 == calculation.Ash900_2 &&
                   Ash900_3 == calculation.Ash900_3 &&
                   Ash900Avr == calculation.Ash900Avr &&
                   Organic_1 == calculation.Organic_1 &&
                   Organic_2 == calculation.Organic_2 &&
                   Organic_3 == calculation.Organic_3 &&
                   OrganicAvr == calculation.OrganicAvr &&
                   Titanium_1 == calculation.Titanium_1 &&
                   Titanium_2 == calculation.Titanium_2 &&
                   Titanium_3 == calculation.Titanium_3 &&
                   TitaniumAvr == calculation.TitaniumAvr &&
                   Chalk_1 == calculation.Chalk_1 &&
                   Chalk_2 == calculation.Chalk_2 &&
                   Chalk_3 == calculation.Chalk_3 &&
                   ChalkAvr == calculation.ChalkAvr &&
                   Others_1 == calculation.Others_1 &&
                   Others_2 == calculation.Others_2 &&
                   Others_3 == calculation.Others_3 &&
                   OthersAvr == calculation.OthersAvr;
        }

        public override int GetHashCode()
        {
            int hashCode = -207246434;
            hashCode = hashCode * -1521134295 + Solid_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Solid_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Solid_3.GetHashCode();
            hashCode = hashCode * -1521134295 + SolidAvr.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450_3.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450Avr.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash900_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash900_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash900_3.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash900Avr.GetHashCode();
            hashCode = hashCode * -1521134295 + Organic_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Organic_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Organic_3.GetHashCode();
            hashCode = hashCode * -1521134295 + OrganicAvr.GetHashCode();
            hashCode = hashCode * -1521134295 + Titanium_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Titanium_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Titanium_3.GetHashCode();
            hashCode = hashCode * -1521134295 + TitaniumAvr.GetHashCode();
            hashCode = hashCode * -1521134295 + Chalk_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Chalk_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Chalk_3.GetHashCode();
            hashCode = hashCode * -1521134295 + ChalkAvr.GetHashCode();
            hashCode = hashCode * -1521134295 + Others_1.GetHashCode();
            hashCode = hashCode * -1521134295 + Others_2.GetHashCode();
            hashCode = hashCode * -1521134295 + Others_3.GetHashCode();
            hashCode = hashCode * -1521134295 + OthersAvr.GetHashCode();
            return hashCode;
        }
    }
}
