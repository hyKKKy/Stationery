using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    internal class Selling
    {
        public int Id { get; set; }
        public DateOnly date {  get; set; }
        public int price { get; set; }
        public int amount { get; set; }
        public virtual Item? Item { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual Buyer? Buyer { get; set; }
    }
}
