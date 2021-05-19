using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class ExperimentalVisDto
    {
        private long Id { get; set; } = -1;
        private long LabBookId { get; set; } = 1;
        private DateTime Created { get; set; } = DateTime.Now;
        private DateTime Updated { get; set; } = DateTime.Now;
        private decimal pH { get; set; }
        private string Type { get; set; }
        private decimal Brook1 { get; set; }
        private decimal Brook5 { get; set; }
        private decimal Brook10 { get; set; }
        private decimal Brook20 { get; set; }
        private decimal Brook30 { get; set; }
        private decimal Brook40 { get; set; }
        private decimal Brook50 { get; set; }
        private decimal Brook60 { get; set; }
        private decimal Brook70 { get; set; }
        private decimal Brook80 { get; set; }
        private decimal Brook90 { get; set; }
        private decimal Brook100 { get; set; }
        private string BrookComment { get; set; }
        private string BrookDisc { get; set; }
        private decimal BrookXvisc { get; set; }
        private string BrookXrpm { get; set; }
        private string BrookXdisc { get; set; }
        private decimal Krebs { get; set; }
        private string KrebsComment { get; set; }
        private decimal ICI { get; set; }
        private string ICIdisc { get; set; }
        private string ICIcomment { get; set; }
        private string Temperature { get; set; }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5, 
            decimal brook10, decimal brook20, decimal brook30, decimal brook40, decimal brook50, decimal brook60, decimal brook70, decimal brook80, decimal brook90, 
            decimal brook100, string brookComment, string brookDisc, decimal brookXvisc, string brookXrpm, string brookXdisc, decimal krebs, string krebsComment, 
            decimal iCI, string iCIdisc, string iCIcomment, string temperature)
        {
            Id = iD;
            LabBookId = labBookId;
            Created = created;
            Updated = updated;
            this.pH = pH;
            Type = type;
            Brook1 = brook1;
            Brook5 = brook5;
            Brook10 = brook10;
            Brook20 = brook20;
            Brook30 = brook30;
            Brook40 = brook40;
            Brook50 = brook50;
            Brook60 = brook60;
            Brook70 = brook70;
            Brook80 = brook80;
            Brook90 = brook90;
            Brook100 = brook100;
            BrookComment = brookComment;
            BrookDisc = brookDisc;
            BrookXvisc = brookXvisc;
            BrookXrpm = brookXrpm;
            BrookXdisc = brookXdisc;
            Krebs = krebs;
            KrebsComment = krebsComment;
            ICI = iCI;
            ICIdisc = iCIdisc;
            ICIcomment = iCIcomment;
            Temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5,
            decimal brook10, decimal brook20, decimal brook50, decimal brook100, string brookComment, string brookDisc, string temperature)
        {
            Id = iD;
            LabBookId = labBookId;
            Created = created;
            Updated = updated;
            this.pH = pH;
            Type = type;
            Brook1 = brook1;
            Brook5 = brook5;
            Brook10 = brook10;
            Brook20 = brook20;
            Brook50 = brook50;
            Brook100 = brook100;
            BrookComment = brookComment;
            BrookDisc = brookDisc;
            Temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5,
            decimal brook20, string brookComment, string brookDisc, string temperature)
        {
            Id = iD;
            LabBookId = labBookId;
            Created = created;
            Updated = updated;
            this.pH = pH;
            Type = type;
            Brook1 = brook1;
            Brook5 = brook5;
            Brook20 = brook20;
            BrookComment = brookComment;
            BrookDisc = brookDisc;
            Temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, 
            decimal brookXvisc, string brookXrpm, string brookXdisc, string temperature)
        {
            Id = iD;
            LabBookId = labBookId;
            Created = created;
            Updated = updated;
            this.pH = pH;
            Type = type;
            BrookXvisc = brookXvisc;
            BrookXrpm = brookXrpm;
            BrookXdisc = brookXdisc;
            Temperature = temperature;
        }

        public override bool Equals(object obj)
        {
            return obj is ExperimentalVisDto dto &&
                   Id == dto.Id &&
                   LabBookId == dto.LabBookId &&
                   Created == dto.Created &&
                   Updated == dto.Updated &&
                   pH == dto.pH &&
                   Type == dto.Type &&
                   Brook1 == dto.Brook1 &&
                   Brook5 == dto.Brook5 &&
                   Brook10 == dto.Brook10 &&
                   Brook20 == dto.Brook20 &&
                   Brook30 == dto.Brook30 &&
                   Brook40 == dto.Brook40 &&
                   Brook50 == dto.Brook50 &&
                   Brook60 == dto.Brook60 &&
                   Brook70 == dto.Brook70 &&
                   Brook80 == dto.Brook80 &&
                   Brook90 == dto.Brook90 &&
                   Brook100 == dto.Brook100 &&
                   BrookComment == dto.BrookComment &&
                   BrookDisc == dto.BrookDisc &&
                   BrookXvisc == dto.BrookXvisc &&
                   BrookXrpm == dto.BrookXrpm &&
                   BrookXdisc == dto.BrookXdisc &&
                   Krebs == dto.Krebs &&
                   KrebsComment == dto.KrebsComment &&
                   ICI == dto.ICI &&
                   ICIdisc == dto.ICIdisc &&
                   ICIcomment == dto.ICIcomment &&
                   Temperature == dto.Temperature;
        }

        public override int GetHashCode()
        {
            int hashCode = -590270970;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Created.GetHashCode();
            hashCode = hashCode * -1521134295 + Updated.GetHashCode();
            hashCode = hashCode * -1521134295 + pH.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + Brook1.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook5.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook10.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook20.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook30.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook40.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook50.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook60.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook70.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook80.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook90.GetHashCode();
            hashCode = hashCode * -1521134295 + Brook100.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BrookComment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BrookDisc);
            hashCode = hashCode * -1521134295 + BrookXvisc.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BrookXrpm);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BrookXdisc);
            hashCode = hashCode * -1521134295 + Krebs.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KrebsComment);
            hashCode = hashCode * -1521134295 + ICI.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ICIdisc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ICIcomment);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Temperature);
            return hashCode;
        }
    }
}
