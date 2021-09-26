using System;
using System.Collections.Generic;

namespace LabBook.Dto
{
    public class CurrencyDto
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "";
        public decimal Rate { get; set; } = -1;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public CurrencyDto()
        { }

        public CurrencyDto(int id, string name, decimal rate, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            Rate = rate;
            DateCreated = dateCreated;
        }

        public override bool Equals(object obj)
        {
            return obj is CurrencyDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   Rate == dto.Rate;
        }

        public override int GetHashCode()
        {
            int hashCode = 887233405;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Rate.GetHashCode();
            return hashCode;
        }
    }
}
