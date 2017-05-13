using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIA.Entities
{
    public class BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string IPAddress { get; set; }
    }
}
