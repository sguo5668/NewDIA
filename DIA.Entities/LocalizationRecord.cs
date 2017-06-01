using System;
using System.Collections.Generic;
using System.Text;

namespace DIA.Entities
{
    public class LocalizationRecord
    {

        public int Id { get; set; }
        public string Key { get; set; }
        public string ResourceKey { get; set; }
        public string Text { get; set; }
        public string LocalizationCulture { get; set; }

        public DateTime UpdatedTimestamp { get; set; } = DateTime.UtcNow;
    }
}