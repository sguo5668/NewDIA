using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIA.Entities
{
    public class ReferenceTableValue : BaseEntity
    {

        //public int Id { get; set; }
        public string SystemValue { get; set; }
        public string TableName { get; set; }
    }
}
