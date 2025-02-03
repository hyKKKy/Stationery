using System.Diagnostics.Metrics;

namespace Stationery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Type WritingInstruments = new Type { Name = "Writing Instruments" };
                Type paperProducts = new Type { Name = "Paper Products" };
                Type officeTools = new Type { Name = "Office Tools" };
                db.Types.AddRange(WritingInstruments, paperProducts, officeTools);

                Item pen = new Item { Name = "Pen", Type = WritingInstruments, amount = 500, selfprice = 130 };
                Item notebook = new Item { Name = "Notebook", Type = paperProducts, amount = 600, selfprice = 200};
                Item stapler = new Item { Name = "Stapler", Type = officeTools, amount = 700, selfprice = 100};
                db.Items.AddRange(pen, notebook, stapler);

                Manager John = new Manager { Name = "John", Surname = "Smith" };
                Manager Brad = new Manager { Name = "Brad", Surname = "Pitt" };
                Manager Nick = new Manager { Name = "Nick", Surname = "Cobain" };
                db.Managers.AddRange(John, Brad, Nick);

                Buyer Kurt = new Buyer { Name = "Kurt" };
                Buyer Jason = new Buyer { Name = "Jason" };
                Buyer Alex = new Buyer { Name = "Alex" };
                db.Buyers.AddRange(Kurt, Jason, Alex);

                Selling s1 = new Selling { price = 200, amount = 30, date = new DateOnly(2025, 1, 13), Item = pen, Manager = John, Buyer = Kurt};
                Selling s2 = new Selling { price = 300, amount = 23, date = new DateOnly(2025, 1, 20), Item = notebook, Manager = Brad, Buyer = Jason };
                Selling s3 = new Selling { price = 150, amount = 56, date = new DateOnly(2025, 2, 2), Item = stapler, Manager = Nick, Buyer = Alex };
                db.Sellings.AddRange(s1, s2, s3);
                db.SaveChanges();

                //showInfo();
                //showTypes();
                //showMangers();
                showMinAmount();
            }
        }

        public static void showInfo()
        {
            using (ApplicationContext db = new())
            {
                var items = db.Items.ToList();
                Console.WriteLine("All items:");
                foreach (Item i in items)
                {
                    Console.WriteLine('\t'+i.Name);
                }
            }
        }
        public static void showTypes()
        {
            using (ApplicationContext db = new())
            {
                var types = db.Types.ToList();
                Console.WriteLine("All types:");
                foreach (Type t in types)
                {
                    Console.WriteLine('\t'+t.Name);
                }
            }
        }

        public static void showMangers()
        {
            using (ApplicationContext db = new())
            {
                var managers = db.Managers.ToList();
                Console.WriteLine("All Managers:");
                foreach (Manager m in managers)
                {
                    Console.WriteLine('\t' + m.Name + ' ' + m.Surname);
                }
            }
        }
        public static void showMaxAmount()
        {
            using (ApplicationContext db = new())
            {
                int? maxAmount = db.Items.Max(s => (int?)s.amount);

                var item = db.Items.FirstOrDefault(s => s.amount == maxAmount);
                Console.WriteLine($"Minimum amouts has {item.Name} item");
            }
        }

        public static void showMinAmount()
        {
            using (ApplicationContext db = new())
            {
                int? minAmount = db.Items.Min(s => (int?)s.amount);

                var item = db.Items.FirstOrDefault(s => s.amount == minAmount);
                Console.WriteLine($"Maximum amouts has {item.Name} item");
            }
        }
        public static void showMinSelfPrice() 
        {
            using (ApplicationContext db = new()) 
            {
                int? minSelfPrice = db.Items.Min(s => (int?)s.selfprice);

                var item = db.Items.FirstOrDefault(s => s.selfprice == minSelfPrice);
                Console.WriteLine($"Item with minimum selfprice: ");
            }
        }
        public static void showMaxSelfPrice()
        {
            using (ApplicationContext db = new())
            {
                int? maxSelfPrice = db.Items.Max(s => (int?)s.selfprice);

                var item = db.Items.FirstOrDefault(s => s.selfprice == maxSelfPrice);
                Console.WriteLine($"Item with minimum selfprice: ");
            }
        }

    }
}
