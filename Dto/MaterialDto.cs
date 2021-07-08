﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Dto
{
    public class MaterialDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsIntermediate { get; set; } = false;
        public bool IsDanger { get; set; } = false;
        public bool IsProduction { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public long IntermediateNrD { get; set; } = -1;
        public int ClpSignalWordId { get; set; } = 1;
        public long FunctionId { get; set; } = 1;
        public decimal Price { get; set; } = 0;
        public int CurrencyId { get; set; } = 1;
        public int UnitId { get; set; } = 1;
        public double Density { get; set; } = 0d;
        public double Solids { get; set; } = 0d;
        public double Ash450 { get; set; } = 0d;
        public double VOC { get; set; } = 0d;
        public string Remarks { get; set; }
        public long LoginId { get; set; } = 1;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public MaterialDto(long id, string name, bool isIntermediate, bool isDanger, bool isProduction, bool isActive, long intermediateNrD, 
            int clpSignalWordId, long functionId, decimal price, int currencyId, int unitId, double density, double solids, double ash450, double voc, 
            string remarks, long loginId, DateTime dateCreated, DateTime dateUpdated)
        {
            Id = id;
            Name = name;
            IsIntermediate = isIntermediate;
            IsDanger = isDanger;
            IsProduction = isProduction;
            IsActive = isActive;
            IntermediateNrD = intermediateNrD;
            ClpSignalWordId = clpSignalWordId;
            FunctionId = functionId;
            Price = price;
            CurrencyId = currencyId;
            UnitId = unitId;
            Density = density;
            Solids = solids;
            Ash450 = ash450;
            VOC = voc;
            Remarks = remarks;
            LoginId = loginId;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public MaterialDto(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is MaterialDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   IsIntermediate == dto.IsIntermediate &&
                   IsDanger == dto.IsDanger &&
                   IsProduction == dto.IsProduction &&
                   IsActive == dto.IsActive &&
                   IntermediateNrD == dto.IntermediateNrD &&
                   ClpSignalWordId == dto.ClpSignalWordId &&
                   FunctionId == dto.FunctionId &&
                   Price == dto.Price &&
                   CurrencyId == dto.CurrencyId &&
                   UnitId == dto.UnitId &&
                   Density == dto.Density &&
                   Solids == dto.Solids &&
                   Ash450 == dto.Ash450 &&
                   VOC == dto.VOC &&
                   Remarks == dto.Remarks &&
                   LoginId == dto.LoginId &&
                   DateCreated == dto.DateCreated &&
                   DateUpdated == dto.DateUpdated;
        }

        public override int GetHashCode()
        {
            int hashCode = 1966701578;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + IsIntermediate.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDanger.GetHashCode();
            hashCode = hashCode * -1521134295 + IsProduction.GetHashCode();
            hashCode = hashCode * -1521134295 + IsActive.GetHashCode();
            hashCode = hashCode * -1521134295 + IntermediateNrD.GetHashCode();
            hashCode = hashCode * -1521134295 + ClpSignalWordId.GetHashCode();
            hashCode = hashCode * -1521134295 + FunctionId.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + CurrencyId.GetHashCode();
            hashCode = hashCode * -1521134295 + UnitId.GetHashCode();
            hashCode = hashCode * -1521134295 + Density.GetHashCode();
            hashCode = hashCode * -1521134295 + Solids.GetHashCode();
            hashCode = hashCode * -1521134295 + Ash450.GetHashCode();
            hashCode = hashCode * -1521134295 + VOC.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Remarks);
            hashCode = hashCode * -1521134295 + LoginId.GetHashCode();
            hashCode = hashCode * -1521134295 + DateCreated.GetHashCode();
            hashCode = hashCode * -1521134295 + DateUpdated.GetHashCode();
            return hashCode;
        }
    }
}