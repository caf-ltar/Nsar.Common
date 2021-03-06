﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nsar.Common.Measure.Models
{
    public class PhysicalQuantity
    {
        private readonly decimal value;
        private readonly string unit;
        private readonly int precision;

        public decimal Value { get { return value; } }
        public string Unit { get { return unit; } }
        public int Precision { get { return precision; } }

        public PhysicalQuantity(
            decimal value,
            string unit,
            int precision = int.MaxValue)
        {
            this.value = value;
            this.unit = unit;
            this.precision = precision;
        }

        // See: http://www.loganfranken.com/blog/692/overriding-equals-in-c-part-1/
        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((PhysicalQuantity)obj);
        }

        public bool Equals(PhysicalQuantity other)
        {
            if (!int.Equals(Precision, other.Precision))
            {
                return false;
            }

            decimal diff = Math.Abs(
                this.Value - other.Value);
            if(diff > (decimal) precision)
            {
                return false;
            }

            if(!String.Equals(Unit, other.Unit))
            {
                return false;
            }

            return true; 
        }

        // See: http://www.loganfranken.com/blog/692/overriding-equals-in-c-part-2/
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Value) ? Value.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Unit) ? Unit.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Precision) ? Precision.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
