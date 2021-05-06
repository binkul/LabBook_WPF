using System;

namespace LabBook.Dto
{
    public enum ViscosityType
    {
        brookfield,
        brookfield_x,
        krebs,
        ici
    }

    public class ExperimentalVisDto
    {
        long ID { get; set; }
        long LabBookId { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
        decimal pH { get; set; }
        ViscosityType Type { get; set; }
        decimal Brook1 { get; set; }
        decimal Brook5 { get; set; }
        decimal Brook10 { get; set; }
        decimal Brook20 { get; set; }
        decimal Brook30 { get; set; }
        decimal Brook40 { get; set; }
        decimal Brook50 { get; set; }
        decimal Brook60 { get; set; }
        decimal Brook70 { get; set; }
        decimal Brook80 { get; set; }
        decimal Brook90 { get; set; }
        decimal Brook100 { get; set; }
        string BrookComment { get; set; }
        string BrookDisc { get; set; }
        decimal BrookXvisc { get; set; }
        string BrookXrpm { get; set; }
        string BrookXdisc { get; set; }
        decimal Krebs { get; set; }
        string KrebsComment { get; set; }
        decimal ICI { get; set; }
        string ICIdisc { get; set; }
        string ICIcomment { get; set; }
        string Temperature { get; set; }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, ViscosityType type, decimal brook1, decimal brook5, 
            decimal brook10, decimal brook20, decimal brook30, decimal brook40, decimal brook50, decimal brook60, decimal brook70, decimal brook80, decimal brook90, 
            decimal brook100, string brookComment, string brookDisc, decimal brookXvisc, string brookXrpm, string brookXdisc, decimal krebs, string krebsComment, 
            decimal iCI, string iCIdisc, string iCIcomment, string temperature)
        {
            ID = iD;
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

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, ViscosityType type, decimal brook1, decimal brook5,
            decimal brook10, decimal brook20, decimal brook50, decimal brook100, string brookComment, string brookDisc, string temperature)
        {
            ID = iD;
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

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, ViscosityType type, decimal brook1, decimal brook5,
            decimal brook20, string brookComment, string brookDisc, string temperature)
        {
            ID = iD;
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

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, ViscosityType type, 
            decimal brookXvisc, string brookXrpm, string brookXdisc, string temperature)
        {
            ID = iD;
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

    }
}
