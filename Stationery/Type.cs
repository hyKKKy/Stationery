using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    internal class Type
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Item> Items { get; set; } = new();

    }
}
