using System;
using System.Collections.Generic;

namespace LabBook.Forms.MainForm.Model
{
    public class ExpCommon
    {
        public long Id { get; set; } = -1;
        public long LabBookId { get; set; } = 1;
        public string ScrubISO11998 { get; set; }
        public long ScrubISO11998Class { get; set; } = 1;
        public string ScrubBrush { get; set; }
        public string DryingISO9117_1 { get; set; }
        public string DryingISO9117_3 { get; set; }
        public string YellowingISO7724 { get; set; }
        public string SchockISO6272 { get; set; }
        public string PersozISO2409 { get; set; }
        public string KoenigISO2409 { get; set; }
        public string ScratchISO6272_1 { get; set; }
        public string AdhesionISO2409 { get; set; }
        public string StainISO2812_4 { get; set; }
        public string WaterISO2812_2 { get; set; }
        public string SaltSprayISO9227 { get; set; }
        public string FlashRust { get; set; }
        public string UV { get; set; }
        public string Hardness { get; set; }
        public string FlowLimit { get; set; }
        public string RunOff { get; set; }
        public string Yield { get; set; }
        public string Other { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ExpCommon(long id, long labBookId, string scrubISO11998, long scrubISO11998Class, string scrubBrush, 
            string dryingISO9117_1, string dryingISO9117_3, string yellowingISO7724, string schockISO6272, string persozISO2409, 
            string koenigISO2409, string scratchISO6272_1, string adhesionISO2409, string stainISO2812_4, string waterISO2812_2, 
            string saltSprayISO9227, string flashRust, string uV, string hardness, string flowLimit, string runOff, string yield, 
            string other, DateTime created, DateTime updated)
        {
            Id = id;
            LabBookId = labBookId;
            ScrubISO11998 = scrubISO11998;
            ScrubISO11998Class = scrubISO11998Class;
            ScrubBrush = scrubBrush;
            DryingISO9117_1 = dryingISO9117_1;
            DryingISO9117_3 = dryingISO9117_3;
            YellowingISO7724 = yellowingISO7724;
            SchockISO6272 = schockISO6272;
            PersozISO2409 = persozISO2409;
            KoenigISO2409 = koenigISO2409;
            ScratchISO6272_1 = scratchISO6272_1;
            AdhesionISO2409 = adhesionISO2409;
            StainISO2812_4 = stainISO2812_4;
            WaterISO2812_2 = waterISO2812_2;
            SaltSprayISO9227 = saltSprayISO9227;
            FlashRust = flashRust;
            UV = uV;
            Hardness = hardness;
            FlowLimit = flowLimit;
            RunOff = runOff;
            Yield = yield;
            Other = other;
            Created = created;
            Updated = updated;
        }

        public ExpCommon(long labBookId)
        {
            LabBookId = labBookId;
        }

        public override bool Equals(object obj)
        {
            return obj is ExpCommon common &&
                   Id == common.Id &&
                   LabBookId == common.LabBookId &&
                   ScrubISO11998 == common.ScrubISO11998 &&
                   ScrubISO11998Class == common.ScrubISO11998Class &&
                   ScrubBrush == common.ScrubBrush &&
                   DryingISO9117_1 == common.DryingISO9117_1 &&
                   DryingISO9117_3 == common.DryingISO9117_3 &&
                   YellowingISO7724 == common.YellowingISO7724 &&
                   SchockISO6272 == common.SchockISO6272 &&
                   PersozISO2409 == common.PersozISO2409 &&
                   KoenigISO2409 == common.KoenigISO2409 &&
                   ScratchISO6272_1 == common.ScratchISO6272_1 &&
                   AdhesionISO2409 == common.AdhesionISO2409 &&
                   StainISO2812_4 == common.StainISO2812_4 &&
                   WaterISO2812_2 == common.WaterISO2812_2 &&
                   SaltSprayISO9227 == common.SaltSprayISO9227 &&
                   FlashRust == common.FlashRust &&
                   UV == common.UV &&
                   Hardness == common.Hardness &&
                   FlowLimit == common.FlowLimit &&
                   RunOff == common.RunOff &&
                   Yield == common.Yield &&
                   Other == common.Other &&
                   Created == common.Created &&
                   Updated == common.Updated;
        }

        public override int GetHashCode()
        {
            int hashCode = 956710367;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ScrubISO11998);
            hashCode = hashCode * -1521134295 + ScrubISO11998Class.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ScrubBrush);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DryingISO9117_1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DryingISO9117_3);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(YellowingISO7724);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SchockISO6272);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PersozISO2409);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KoenigISO2409);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ScratchISO6272_1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AdhesionISO2409);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StainISO2812_4);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WaterISO2812_2);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SaltSprayISO9227);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FlashRust);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UV);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Hardness);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FlowLimit);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RunOff);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Yield);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Other);
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            return hashCode;
        }
    }
}
