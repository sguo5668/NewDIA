﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIA.Entities
{
    public class ReferenceTableValue : BaseEntity
    {

        //public int Id { get; set; }
        public string Key { get; set; }
        public string ResourceKey { get; set; }
        public string TableName { get; set; }
        public string Text { get; set; }
        public string LocalizationCulture { get; set; }
        public int ResourceType { get; set; }
    }
}
