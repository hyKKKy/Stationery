﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    internal class Buyer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Selling> Sellings { get; set; } = new();
    }
}
