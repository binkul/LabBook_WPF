using LabBook.Security;
using System;
using System.Collections.Generic;

namespace LabBook.Forms.Composition.Model
{
    public class CompositionData
    {
        public long Id { get; set; } = -1;
        public long LabBookId { get; set; } = 0;
        public int Version { get; set; } = 1;
        public double Mass { get; set; } = 1000d;
        public double Amount { get; set; } = 100d;
        public DateTime ChangeDate { get; set; } = DateTime.Today;
        public string Comment { get; set; }
        public long LoginId { get; set; } = UserSingleton.Id;
        public string LoginShortcut { get; set; } = UserSingleton.Identifier;
        public string Permision { get; set; } = UserSingleton.Permission;
        public string Title { get; set; } = "";
        public decimal Density { get; set; } = 0m;

        public CompositionData()
        { }

        public CompositionData(long labBookId, string title, decimal density)
        {
            LabBookId = labBookId;
            Title = title;
            Density = density;
        }

        public CompositionData(long id, long labBookId, int version, double mass, double amount, DateTime changeDate, string comment, 
            long loginId, string loginShortcut, string permision, string title, decimal density)
        {
            Id = id;
            LabBookId = labBookId;
            Version = version;
            Mass = mass;
            Amount = amount;
            ChangeDate = changeDate;
            Comment = comment;
            LoginId = loginId;
            LoginShortcut = loginShortcut;
            Permision = permision;
            Title = title;
            Density = density;
        }

        public override bool Equals(object obj)
        {
            return obj is CompositionData data &&
                   Id == data.Id &&
                   LabBookId == data.LabBookId &&
                   Version == data.Version &&
                   Mass == data.Mass &&
                   Amount == data.Amount &&
                   ChangeDate == data.ChangeDate &&
                   Comment == data.Comment &&
                   LoginId == data.LoginId &&
                   LoginShortcut == data.LoginShortcut &&
                   Permision == data.Permision &&
                   Title == data.Title &&
                   Density == data.Density;
        }

        public override int GetHashCode()
        {
            int hashCode = -1424578035;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + LabBookId.GetHashCode();
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            hashCode = hashCode * -1521134295 + Mass.GetHashCode();
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + ChangeDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + LoginId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LoginShortcut);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Permision);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + Density.GetHashCode();
            return hashCode;
        }
    }
}
