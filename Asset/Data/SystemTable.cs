﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Data
{
    public class SystemTable
    {

        [Key]
        public int SystemId { get; set; }
        public string SystemType { get; set; }
        public String Brand { get; set; }
        public string ModelNo { get; set; }
        public string SerialNo { get; set; }
        public int Price { get; set; }
        public bool Avialable { get; set; }
        public string Configaration { get; set; }

    }
}

