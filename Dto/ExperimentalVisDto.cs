using System;

namespace LabBook.Dto
{
    public class ExperimentalVisDto
    {
        private long _id = -1;
        private long _labBookId;
        private DateTime _created;
        private DateTime _updated;
        private decimal _pH = -1;
        private string _type;
        private decimal _brook1 = -1;
        private decimal _brook5 = -1;
        private decimal _brook10 = -1;
        private decimal _brook20 = -1;
        private decimal _brook30 = -1;
        private decimal _brook40 = -1;
        private decimal _brook50 = -1;
        private decimal _brook60 = -1;
        private decimal _brook70 = -1;
        private decimal _brook80 = -1;
        private decimal _brook90 = -1;
        private decimal _brook100 = -1;
        private string _brookComment;
        private string _brookDisc;
        private decimal _brookXvisc = -1;
        private string _brookXrpm;
        private string _brookXdisc;
        private decimal _krebs = -1;
        private string _krebsComment;
        private decimal _ICI = -1;
        private string _ICIdisc;
        private string _ICIcomment;
        private string _temperature;

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5, 
            decimal brook10, decimal brook20, decimal brook30, decimal brook40, decimal brook50, decimal brook60, decimal brook70, decimal brook80, decimal brook90, 
            decimal brook100, string brookComment, string brookDisc, decimal brookXvisc, string brookXrpm, string brookXdisc, decimal krebs, string krebsComment, 
            decimal iCI, string iCIdisc, string iCIcomment, string temperature)
        {
            _id = iD;
            _labBookId = labBookId;
            _created = created;
            _updated = updated;
            _pH = pH;
            _type = type;
            _brook1 = brook1;
            _brook5 = brook5;
            _brook10 = brook10;
            _brook20 = brook20;
            _brook30 = brook30;
            _brook40 = brook40;
            _brook50 = brook50;
            _brook60 = brook60;
            _brook70 = brook70;
            _brook80 = brook80;
            _brook90 = brook90;
            _brook100 = brook100;
            _brookComment = brookComment;
            _brookDisc = brookDisc;
            _brookXvisc = brookXvisc;
            _brookXrpm = brookXrpm;
            _brookXdisc = brookXdisc;
            _krebs = krebs;
            _krebsComment = krebsComment;
            _ICI = iCI;
            _ICIdisc = iCIdisc;
            _ICIcomment = iCIcomment;
            _temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5,
            decimal brook10, decimal brook20, decimal brook50, decimal brook100, string brookComment, string brookDisc, string temperature)
        {
            _id = iD;
            _labBookId = labBookId;
            _created = created;
            _updated = updated;
            _pH = pH;
            _type = type;
            _brook1 = brook1;
            _brook5 = brook5;
            _brook10 = brook10;
            _brook20 = brook20;
            _brook50 = brook50;
            _brook100 = brook100;
            _brookComment = brookComment;
            _brookDisc = brookDisc;
            _temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, decimal brook1, decimal brook5,
            decimal brook20, string brookComment, string brookDisc, string temperature)
        {
            _id = iD;
            _labBookId = labBookId;
            _created = created;
            _updated = updated;
            _pH = pH;
            _type = type;
            _brook1 = brook1;
            _brook5 = brook5;
            _brook20 = brook20;
            _brookComment = brookComment;
            _brookDisc = brookDisc;
            _temperature = temperature;
        }

        public ExperimentalVisDto(long iD, long labBookId, DateTime created, DateTime updated, decimal pH, string type, 
            decimal brookXvisc, string brookXrpm, string brookXdisc, string temperature)
        {
            _id = iD;
            _labBookId = labBookId;
            _created = created;
            _updated = updated;
            _pH = pH;
            _type = type;
            _brookXvisc = brookXvisc;
            _brookXrpm = brookXrpm;
            _brookXdisc = brookXdisc;
            _temperature = temperature;
        }

        public long GetId()
        {
            return _id;
        }

        public long GetLabBookId()
        {
            return _labBookId;
        }

        public DateTime GetCreated()
        {
            return _created;
        }

        public DateTime GetUpdated()
        {
            return _updated;
        }

        public decimal GetpH()
        {
            return _pH;
        }

        public string GetViscosityType()
        {
            return _type;
        }

        public decimal GetBrookfield1()
        {
            return _brook1;
        }

        public decimal GetBrookfield5()
        {
            return _brook5;
        }

        public decimal GetBrookfield10()
        {
            return _brook10;
        }

        public decimal GetBrookfield20()
        {
            return _brook20;
        }

        public decimal GetBrookfield30()
        {
            return _brook30;
        }

        public decimal GetBrookfield40()
        {
            return _brook40;
        }

        public decimal GetBrookfield50()
        {
            return _brook50;
        }

        public decimal GetBrookfield60()
        {
            return _brook60;
        }

        public decimal GetBrookfield70()
        {
            return _brook70;
        }

        public decimal GetBrookfield80()
        {
            return _brook80;
        }

        public decimal GetBrookfield90()
        {
            return _brook90;
        }

        public decimal GetBrookfield100()
        {
            return _brook100;
        }

        public string GetBrookComment()
        {
            return _brookComment;
        }

        public string GetBrookDisc()
        {
            return _brookDisc;
        }

        public decimal GetBrookX()
        {
            return _brookXvisc;
        }

        public string GetBrookXRpm()
        {
            return _brookXrpm;
        }

        public string GetBrookXDisc()
        {
            return _brookXdisc;
        }

        public decimal GetKrebs()
        {
            return _krebs;
        }

        public string GetKrebsComment()
        {
            return _krebsComment;
        }

        public decimal GetICI()
        {
            return _ICI;
        }

        public string GetICIcomment()
        {
            return _ICIcomment;
        }

        public string GetICIdisc()
        {
            return _ICIdisc;
        }

        public string GetTemperature()
        {
            return _temperature;
        }
    }
}
