using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    internal class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int amount { get; set; }
        public int selfprice { get; set; }
        public virtual Type? Type { get; set; }
        public virtual List<Selling> Sellings { get; set; } = new();

    }
}
