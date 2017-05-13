using System;
using System.Collections.Generic;
using System.Text;

namespace DIA.Entities
{
    public class Claim :BaseEntity
    {

        //public int Id { get; set; }

        public string ClaimIDNumber { get; set; }

        public int ClaimTypeCode { get; set; }

        public int ClaimantID { get; set; }

        public DateTime EffectiveDate { get; set; }

    }
}
